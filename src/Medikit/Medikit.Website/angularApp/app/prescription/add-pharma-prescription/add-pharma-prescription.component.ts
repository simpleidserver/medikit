import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatListOption, MatPaginator, MatSelectionList, MatSnackBar, MatStepper } from '@angular/material';
import { MedicinalPackage } from '@app/medicinalproduct/models/MedicinalPackage';
import { MedicinalProduct } from '@app/medicinalproduct/models/MedicinalProduct';
import { SearchMedicinalProduct } from '@app/medicinalproduct/models/SearchMedicinalProduct';
import { MedicinalProductService } from '@app/medicinalproduct/services/medicinalproduct-service';
import { ReferenceTableRecord } from '@app/referencetable/models/reference-table-record';
import { ReferenceTableService } from '@app/referencetable/services/reference-table-service';
import { Translation } from "@app/infrastructure/Translation";
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ActionTypes, AddDrugPrescription, CheckNiss, DeleteDrugPrescription, LoadPrescription, NextStep, PreviousStep } from './actions/pharma-prescription';
import { PharmaDrugPrescription } from '@app/prescription/models/pharma-drug-prescription';
import { PharmaDuration } from '@app/prescription/models/pharma-duration';
import { PharmaPosologyFreeText, PharmaPosologyStructured, PharmaPosologyTakes } from '@app/prescription/models/pharma-posology';
import { AddPharmaPrescriptionFormState } from './states/pharma-prescription-state';

@Component({
    selector: 'add-pharma-prescription-component',
    templateUrl: './add-pharma-prescription.component.html',
    styleUrls: ['./add-pharma-prescription.component.scss']
})
export class AddPharmaPrescriptionComponent implements OnInit {
    nextPatientFormBtnDisabled: boolean = true;
    length: number;
    isLoadingDrugs: boolean;
    @ViewChild(MatSelectionList) selectionList: MatSelectionList;
    @ViewChild('stepper') stepper: MatStepper;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    patientSearchFormGroup: FormGroup;
    patientFormGroup: FormGroup;
    drugSearchFormGroup: FormGroup;
    drugFormGroup: FormGroup;
    prescribePharmadDrugs: PharmaDrugPrescription[] = []; 
    timeUnits: ReferenceTableRecord[] = [];
    administrationUnits: ReferenceTableRecord[] = [];
    medicinalProducts: MedicinalProduct[] = [];
    selectedMedicinalPackages: MedicinalPackage[] = [];

    constructor(private formBuilder: FormBuilder,
        private medicinalProductService: MedicinalProductService,
        private store: Store<AddPharmaPrescriptionFormState>,
        private translateService: TranslateService,
        private actions$: ScannedActionsSubject,
        private snackBar: MatSnackBar,
        private referenceTableService: ReferenceTableService) { }

    getAdministrationUnitTranslations(code: string): Translation[] {
        var result = this.administrationUnits.filter((a) => a.Code == code);
        if (result.length != 1) {
            return [];
        }

        return result[0].Translations;
    }

    getTimeUnitTranslations(code: string): Translation[] {
        var result = this.timeUnits.filter((a) => a.Code == code);
        if (result.length != 1) {
            return [];
        }

        return result[0].Translations;
    }

    ngOnInit(): void {
        this.selectionList.selectedOptions = new SelectionModel<MatListOption>(false);
        this.patientFormGroup = this.formBuilder.group({
            niss: new FormControl('', [
                Validators.required,
                Validators.pattern('^[0-9]{2}[0-9]{2}[0-9]{2}[0-9]{3}[0-9]{2}$')
            ]),
            firstname: new FormControl({
                value: '',
                disabled: true
            }),
            lastname: new FormControl({
                value: '',
                disabled: true
            }),
            birthdate: new FormControl({
                value: '',
                disabled: true
            })
        });
        this.drugSearchFormGroup = this.formBuilder.group({
            drugName: new FormControl('')
        });
        this.drugFormGroup = this.formBuilder.group({
            isPosologyFreeText: new FormControl(''),
            posologyText: new FormControl(''),
            structuredPosology: this.formBuilder.group({
                low: new FormControl(''),
                high: new FormControl(''),
                takes: this.formBuilder.group({
                    low: new FormControl(''),
                    high: new FormControl('')
                }),
                unit: new FormControl('')
            }),
            duration: this.formBuilder.group({
                value: new FormControl(''),
                unit: new FormControl('')
            }),
            instructionforpatient: new FormControl(''),
            instructionforreimbursement: new FormControl('')
        });
        this.actions$.pipe(
            filter((action: any) => action.type == ActionTypes.NISS_UNKNOWN))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('niss-unknown'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.store.pipe(select('pharmaPrescriptionForm')).subscribe((st: AddPharmaPrescriptionFormState) => {
            if (!st) {
                return;
            }

            this.stepper.selectedIndex = st.stepperIndex;
            this.nextPatientFormBtnDisabled = st.nextPatientFormBtnDisabled;
            if (st.prescription.Patient != null) {
                this.patientFormGroup.get('niss').setValue(st.prescription.Patient.niss);
                this.patientFormGroup.get('firstname').setValue(st.prescription.Patient.firstname);
                this.patientFormGroup.get('lastname').setValue(st.prescription.Patient.lastname);
                this.patientFormGroup.get('birthdate').setValue(st.prescription.Patient.birthdate);
            }

            this.prescribePharmadDrugs = st.prescription.DrugsPrescribed;
            if (this.nextPatientFormBtnDisabled) {
                this.patientFormGroup.controls['niss'].setErrors({ unknownNiss: true });
            } else {
                this.patientFormGroup.controls['niss'].setErrors(null);
            }
        });
        this.init();
    }

    checkNiss(evt: any) {
        evt.preventDefault();
        evt.stopPropagation();
        var niss = this.patientFormGroup.get('niss').value;
        this.store.dispatch(new CheckNiss(niss));
    }

    nextStep() {
        var request = new NextStep();
        this.store.dispatch(request);
    }

    previousStep() {
        var request = new PreviousStep();
        this.store.dispatch(request);
    }

    ngAfterViewInit() {
        this.paginator.page.subscribe(() => this.refreshSearchDrug());
    }

    refreshSearchDrug() {
        let startIndex: number = 0;
        let count: number = 5;
        if (this.paginator.pageIndex && this.paginator.pageSize) {
            startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        }

        if (this.paginator.pageSize) {
            count = this.paginator.pageSize;
        }

        const drug = this.drugSearchFormGroup.get('drugName').value;
        this.setIsLoadingProduct(true);
        this.medicinalProductService.search(drug, startIndex, count, true, "P").subscribe((res: SearchMedicinalProduct) => {
            this.length = res.Count;
            this.medicinalProducts = res.Content;
            this.setIsLoadingProduct(false);
        }, () => {
            this.setIsLoadingProduct(false);
        });
    }

    confirmAddDrug() {
        if (this.selectedMedicinalPackages.length != 1) {
            return;
        }

        var selectedPackage = this.selectedMedicinalPackages[0];
        var drugPrescription = new PharmaDrugPrescription();
        drugPrescription.TechnicalId = this.newGuid();
        drugPrescription.PackageCode = selectedPackage.DeliveryMethods[0].Code;
        drugPrescription.PackageNames = selectedPackage.PrescriptionNames;
        drugPrescription.InstructionForPatient = this.drugFormGroup.get('instructionforpatient').value;
        drugPrescription.InstructionForReimbursement = this.drugFormGroup.get('instructionforreimbursement').value;
        var duration = this.drugFormGroup.get('duration').value;
        var isPosologyFreeText = this.drugFormGroup.get('isPosologyFreeText').value;
        if (isPosologyFreeText) {
            var posologyFreeText = new PharmaPosologyFreeText();
            posologyFreeText.Content = this.drugFormGroup.get('posologyText').value;
            drugPrescription.Posology = posologyFreeText;
        } else {
            var posologyStructured = new PharmaPosologyStructured();
            var structuredPosology = this.drugFormGroup.get('structuredPosology').value;
            posologyStructured.LowPharmaUnitsPerTakes = structuredPosology.low;
            posologyStructured.HighPharmaUnitsPerTakes = structuredPosology.high;
            posologyStructured.Unit = structuredPosology.unit.Code;
            posologyStructured.Takes = new PharmaPosologyTakes();
            posologyStructured.Takes.LowNumberTakes = structuredPosology.takes.low;
            posologyStructured.Takes.HighNumberTakes = structuredPosology.takes.high;
            drugPrescription.Posology = posologyStructured;
        }
        if (duration.unit && duration.value) {

            drugPrescription.Duration = new PharmaDuration();
            drugPrescription.Duration.Unit = duration.unit;
            drugPrescription.Duration.Value = duration.value;
        }

        var addDrugPrescription = new AddDrugPrescription(drugPrescription);
        this.store.dispatch(addDrugPrescription);
    }

    deleteDrugPrescription(technicalId: string) {
        var deleteDrugPrescription = new DeleteDrugPrescription(technicalId);
        this.store.dispatch(deleteDrugPrescription);
    }

    private setIsLoadingProduct(isLoadingProduct: boolean) {
        if (isLoadingProduct) {
            this.isLoadingDrugs = true;
            this.drugSearchFormGroup.get('drugName').disable();
        } else {
            this.isLoadingDrugs = false;
            this.drugSearchFormGroup.get('drugName').enable();
        }
    }

    getTranslation(translations: Translation[]): string {
        var defaultLang = this.translateService.currentLang;
        var filteredTranslations = translations.filter(function (tr: Translation) {
            return tr.Language == defaultLang;
        });
        if (filteredTranslations.length == 0) {
            return "unknown";
        }

        return filteredTranslations[0].Value;
    }

    private newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    private init() {
        forkJoin(
            this.referenceTableService.get('CD-ADMINISTRATIONUNIT'),
            this.referenceTableService.get('CD-TIMEUNIT')).subscribe((res: any) => {
                this.administrationUnits = res[0].Content;
                this.timeUnits = res[1].Content;
                this.store.dispatch(new LoadPrescription());
            });
    }
}