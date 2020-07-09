import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatListOption, MatPaginator, MatSelectionList, MatSnackBar, MatStepper } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { Translation } from "@app/infrastructure/Translation";
import { ReferenceTableRecord } from '@app/referencetable/models/reference-table-record';
import { ReferenceTableService } from '@app/referencetable/services/reference-table-service';
import * as fromAppState from '@app/stores/appstate';
import * as fromMedicalfileActions from '@app/stores/medicalfile/medicalfile-actions';
import { Medicalfile } from '@app/stores/medicalfile/models/medicalfile';
import { PharmaDrugPrescription } from '@app/stores/medicalfile/models/pharma-drug-prescription';
import { PharmaDuration } from '@app/stores/medicalfile/models/pharma-duration';
import { PharmaPosologyFreeText, PharmaPosologyStructured, PharmaPosologyTakes } from '@app/stores/medicalfile/models/pharma-posology';
import { PharmaPrescription } from '@app/stores/medicalfile/models/pharma-prescription';
import { MedicalfileService } from '@app/stores/medicalfile/services/medicalfile-service';
import { MedicinalPackage } from '@app/stores/medicinalproduct/models/MedicinalPackage';
import { SearchMedicinalProduct } from '@app/stores/medicinalproduct/models/SearchMedicinalProduct';
import { MedicinalProductService } from '@app/stores/medicinalproduct/services/medicinalproduct-service';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';
import { AddDrugPrescription, DeleteDrugPrescription, LoadPrescription, NextStep, PreviousStep } from './actions/pharma-prescription';
import { AddPharmaPrescriptionFormState } from './states/pharma-prescription-state';

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
    medicalfileId: string = null;
    medicalfile: Medicalfile;
    isLoadingDrugs: boolean;
    isAddingPrescription: boolean;
    sessionExists: boolean;
    pharmaPrescription: PharmaPrescription;
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
    medicinalPackages: MedicinalPackage[] = [];
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
        private medicalfileService: MedicalfileService,
        private medikitExtensionService: MedikitExtensionService,
        private actions$: ScannedActionsSubject,
        private snackBar: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute) { }

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
            niss: new FormControl({
                value: '',
                disabled: true
            }),
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
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.ERROR_ADD_PRESCRIPTION))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('error-add-prescription'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.isAddingPrescription = false;
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.PRESCRIPTION_ADDED))
            .subscribe(() => {
                sessionStorage.removeItem('pharma-prescription');
                this.snackBar.open(this.translateService.instant('prescription-added'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.router.navigate([ '/medicalfile/' + this.medicalfileId ]);
                this.isAddingPrescription = false;
            });
        this.store.pipe(select('pharmaPrescriptionForm')).subscribe((_: AddPharmaPrescriptionFormState) => {
            if (!_ || !_.prescription) {
                return;
            }

            this.pharmaPrescription = _.prescription;
            this.prescribePharmadDrugs = _.prescription.DrugsPrescribed;
            this.stepper.selectedIndex = _.stepperIndex;
            if (this.stepper.steps) {
                this.stepper.steps.forEach((__, i: number) => {
                    if (i <= _.stepperIndex) {
                        __.interacted = true;
                    }
                });
            }
        });
        this.appStore.pipe(select(fromAppState.selectMedicalfileResult)).subscribe((_: Medicalfile) => {
            if (!_) {
                return;
            }

            this.patientFormGroup.get('niss').setValue(_.niss);
            this.patientFormGroup.get('firstname').setValue(_.firstname);
            this.patientFormGroup.get('lastname').setValue(_.lastname);
        });
        this.medicalfileId = this.route.snapshot.params['id'];
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
        this.medicinalPackages = [];
        this.medicinalProductService.search(drug, startIndex, count, true, '1').subscribe((res: SearchMedicinalProduct) => {
            this.length = res.Count;
            this.medicinalPackages = res.Content;
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
        drugPrescription.PackageCode = selectedPackage.Code;
        drugPrescription.PackageNames = selectedPackage.Names;
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

    addPrescription() {
        this.pharmaPrescription.CreateDateTime = this.createPrescriptionFormGroup.controls['startDate'].value;
        this.pharmaPrescription.EndExecutionDate = this.createPrescriptionFormGroup.controls['expirationDate'].value;
        this.pharmaPrescription.Type = this.createPrescriptionFormGroup.controls['prescriptionType'].value;
        var session: any = this.medikitExtensionService.getEhealthSession();
        this.isAddingPrescription = true;
        this.appStore.dispatch(new fromMedicalfileActions.AddPrescription(this.medicalfileId, this.pharmaPrescription, session['assertion_token']));
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
            this.medicalfileService.getPrescriptionMetadata(this.medicalfileId)).subscribe((res: any) => {
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
        this.appStore.dispatch(new fromMedicalfileActions.GetMedicalfile(this.medicalfileId));
    }
}