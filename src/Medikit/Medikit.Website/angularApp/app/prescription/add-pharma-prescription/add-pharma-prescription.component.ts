import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatListOption, MatPaginator, MatSelectionList, MatStepper, MatSnackBar } from '@angular/material';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { Translation } from "@app/infrastructure/Translation";
import { MedicinalPackage } from '@app/medicinalproduct/models/MedicinalPackage';
import { MedicinalProduct } from '@app/medicinalproduct/models/MedicinalProduct';
import { SearchMedicinalProduct } from '@app/medicinalproduct/models/SearchMedicinalProduct';
import { MedicinalProductService } from '@app/medicinalproduct/services/medicinalproduct-service';
import { ReferenceTableRecord } from '@app/referencetable/models/reference-table-record';
import { ReferenceTableService } from '@app/referencetable/services/reference-table-service';
import * as fromAppState from '@app/stores/appstate';
import { Patient } from '@app/stores/patient/models/patient';
import { SearchPatientResult } from '@app/stores/patient/models/search-patient-result';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { PharmaDrugPrescription } from '@app/stores/pharmaprescription/models/pharma-drug-prescription';
import { PharmaDuration } from '@app/stores/pharmaprescription/models/pharma-duration';
import { PharmaPosologyFreeText, PharmaPosologyStructured, PharmaPosologyTakes } from '@app/stores/pharmaprescription/models/pharma-posology';
import { PharmaPrescription } from '@app/stores/pharmaprescription/models/pharma-prescription';
import { PharmaPrescriptionService } from '@app/stores/pharmaprescription/services/prescription-service';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { AddPharmaPrescription } from '@app/stores/pharmaprescription/prescription-actions';
import * as fromPrescriptionActions from '@app/stores/pharmaprescription/prescription-actions';
import { AddDrugPrescription, DeleteDrugPrescription, LoadPrescription, NextStep, PreviousStep, SelectPatient } from './actions/pharma-prescription';
import { AddPharmaPrescriptionFormState } from './states/pharma-prescription-state';
import { filter } from 'rxjs/operators';
import { Router } from '@angular/router';

class PrescriptionType {
    constructor(public code: number, public name: string) { }
}

@Component({
    selector: 'add-pharma-prescription-component',
    templateUrl: './add-pharma-prescription.component.html',
    styleUrls: ['./add-pharma-prescription.component.scss']
})
export class AddPharmaPrescriptionComponent implements OnInit, OnDestroy {
    length: number;
    isLoadingDrugs: boolean;
    isAddingPrescription: boolean;
    sessionExists: boolean;
    pharmaPrescription: PharmaPrescription;
    filteredPatientsByNiss: Patient[] = [];
    @ViewChild(MatSelectionList) selectionList: MatSelectionList;
    @ViewChild('stepper') stepper: MatStepper;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    patientFormGroup: FormGroup;
    drugSearchFormGroup: FormGroup;
    drugFormGroup: FormGroup;
    createPrescriptionFormGroup: FormGroup;
    prescribePharmadDrugs: PharmaDrugPrescription[] = []; 
    timeUnits: ReferenceTableRecord[] = [];
    administrationUnits: ReferenceTableRecord[] = [];
    medicinalProducts: MedicinalProduct[] = [];
    selectedMedicinalPackages: MedicinalPackage[] = [];
    prescriptionTypes: PrescriptionType[] = [];
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;

    constructor(private formBuilder: FormBuilder,
        private medicinalProductService: MedicinalProductService,
        private store: Store<AddPharmaPrescriptionFormState>,
        private appStore: Store<fromAppState.AppState>,
        private translateService: TranslateService,
        private referenceTableService: ReferenceTableService,
        private prescriptionService: PharmaPrescriptionService,
        private datePipe: DatePipe,
        private medikitExtensionService: MedikitExtensionService,
        private actions$: ScannedActionsSubject,
        private snackBar: MatSnackBar,
        private router: Router) { }

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
        const self = this;
        if (this.medikitExtensionService.getEhealthSession() !== null) {
            this.sessionExists = true;
        }

        this.subscribeSessionCreated = this.medikitExtensionService.sessionCreated.subscribe(() => {
            this.sessionExists = true;
        });
        this.subscribeSessionDropped = this.medikitExtensionService.sessionDropped.subscribe(() => {
            this.sessionExists = false;
        });
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
            beginMoment: new FormControl(''),
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
        this.createPrescriptionFormGroup = this.formBuilder.group({
            prescriptionType: new FormControl(''),
            startDate: new FormControl(''),
            expirationDate: new FormControl(''),
        });
        this.actions$.pipe(
            filter((action: any) => action.type == fromPrescriptionActions.ActionTypes.ADD_PHARMA_PRESCRIPTION_ERROR))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('error-add-prescription'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.isAddingPrescription = false;
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromPrescriptionActions.ActionTypes.ADD_PHARMA_PRESCRIPTION_SUCCESS))
            .subscribe(() => {
                sessionStorage.removeItem('pharma-prescription');
                this.snackBar.open(this.translateService.instant('prescription-added'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.router.navigate(['/prescription']);
                this.isAddingPrescription = false;
            });
        this.store.pipe(select('pharmaPrescriptionForm')).subscribe((_: AddPharmaPrescriptionFormState) => {
            if (!_ || !_.prescription) {
                return;
            }

            this.pharmaPrescription = _.prescription;
            this.prescribePharmadDrugs = _.prescription.DrugsPrescribed;
            if (_.prescription.Patient != null) {
                self.patientFormGroup.controls['niss'].setErrors(null);
                this.patientFormGroup.get('niss').setValue(_.prescription.Patient.niss);
                this.patientFormGroup.get('firstname').setValue(_.prescription.Patient.firstname);
                this.patientFormGroup.get('lastname').setValue(_.prescription.Patient.lastname);
                this.patientFormGroup.get('birthdate').setValue(this.datePipe.transform(_.prescription.Patient.birthdate, "MM/dd/yyyy"));
            } else {
                self.patientFormGroup.controls['niss'].setErrors({ unknownNiss: true });
            }

            this.stepper.selectedIndex = _.stepperIndex;
            if (this.stepper.steps) {
                this.stepper.steps.forEach((__, i: number) => {
                    if (i <= _.stepperIndex) {
                        __.interacted = true;
                    }
                });
            }
        });
        this.appStore.pipe(select(fromAppState.selectPatientsByNissResult)).subscribe((st: SearchPatientResult) => {
            if (!st) {
                return;
            }

            this.filteredPatientsByNiss = st.content;
        });
        this.patientFormGroup.controls['niss'].valueChanges.subscribe((_) => {
            self.appStore.dispatch(new fromPatientActions.SearchPatientsByNiss(_));
        });
        this.init();
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
        this.medicinalProductService.search(drug, startIndex, count, true, '1').subscribe((res: SearchMedicinalProduct) => {
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
        drugPrescription.BeginMoment = this.drugFormGroup.get('beginMoment').value;
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

    selectPatient(evt: any) {
        var patient = this.filteredPatientsByNiss.filter(_ => _.niss == evt.option.value)[0];
        this.store.dispatch(new SelectPatient(patient));
    }

    addPrescription() {
        this.pharmaPrescription.CreateDateTime = this.createPrescriptionFormGroup.controls['startDate'].value;
        this.pharmaPrescription.EndExecutionDate = this.createPrescriptionFormGroup.controls['expirationDate'].value;
        this.pharmaPrescription.Type = this.createPrescriptionFormGroup.controls['prescriptionType'].value;
        var session: any = this.medikitExtensionService.getEhealthSession();
        this.isAddingPrescription = true;
        this.appStore.dispatch(new AddPharmaPrescription(this.pharmaPrescription, session['assertion_token']));
    }

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
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
            this.referenceTableService.get('CD-TIMEUNIT'),
            this.prescriptionService.getMetadata()).subscribe((res: any) => {
                this.administrationUnits = res[0].Content;
                this.timeUnits = res[1].Content;
                var prescriptionTypes: PrescriptionType[] = [];
                res[2]['prescriptionTypes'].children.forEach((prescriptionType: any) => {
                    var type: number = parseInt(Object.keys(prescriptionType)[0]);
                    prescriptionTypes.push(new PrescriptionType(type, prescriptionType[type].translations[0]['en']));
                });

                this.prescriptionTypes = prescriptionTypes;
                this.store.dispatch(new LoadPrescription());
            });
    }
}