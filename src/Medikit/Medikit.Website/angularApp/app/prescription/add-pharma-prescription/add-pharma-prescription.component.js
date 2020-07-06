var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatPaginator, MatSelectionList, MatSnackBar, MatStepper } from '@angular/material';
import { Router } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { ReferenceTableService } from '@app/referencetable/services/reference-table-service';
import * as fromAppState from '@app/stores/appstate';
import { MedicinalProductService } from '@app/stores/medicinalproduct/services/medicinalproduct-service';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { PharmaDrugPrescription } from '@app/stores/pharmaprescription/models/pharma-drug-prescription';
import { PharmaDuration } from '@app/stores/pharmaprescription/models/pharma-duration';
import { PharmaPosologyFreeText, PharmaPosologyStructured, PharmaPosologyTakes } from '@app/stores/pharmaprescription/models/pharma-posology';
import * as fromPrescriptionActions from '@app/stores/pharmaprescription/prescription-actions';
import { AddPharmaPrescription } from '@app/stores/pharmaprescription/prescription-actions';
import { PharmaPrescriptionService } from '@app/stores/pharmaprescription/services/prescription-service';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';
import { AddDrugPrescription, DeleteDrugPrescription, LoadPrescription, NextStep, PreviousStep, SelectPatient } from './actions/pharma-prescription';
var PrescriptionType = (function () {
    function PrescriptionType(code, name) {
        this.code = code;
        this.name = name;
    }
    return PrescriptionType;
}());
var AddPharmaPrescriptionComponent = (function () {
    function AddPharmaPrescriptionComponent(formBuilder, medicinalProductService, store, appStore, translateService, referenceTableService, prescriptionService, datePipe, medikitExtensionService, actions$, snackBar, router) {
        this.formBuilder = formBuilder;
        this.medicinalProductService = medicinalProductService;
        this.store = store;
        this.appStore = appStore;
        this.translateService = translateService;
        this.referenceTableService = referenceTableService;
        this.prescriptionService = prescriptionService;
        this.datePipe = datePipe;
        this.medikitExtensionService = medikitExtensionService;
        this.actions$ = actions$;
        this.snackBar = snackBar;
        this.router = router;
        this.filteredPatientsByNiss = [];
        this.prescribePharmadDrugs = [];
        this.timeUnits = [];
        this.administrationUnits = [];
        this.medicinalPackages = [];
        this.selectedMedicinalPackages = [];
        this.prescriptionTypes = [];
    }
    AddPharmaPrescriptionComponent.prototype.getAdministrationUnitTranslations = function (code) {
        var result = this.administrationUnits.filter(function (a) { return a.Code == code; });
        if (result.length != 1) {
            return [];
        }
        return result[0].Translations;
    };
    AddPharmaPrescriptionComponent.prototype.getTimeUnitTranslations = function (code) {
        var result = this.timeUnits.filter(function (a) { return a.Code == code; });
        if (result.length != 1) {
            return [];
        }
        return result[0].Translations;
    };
    AddPharmaPrescriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        var self = this;
        if (this.medikitExtensionService.getEhealthSession() !== null) {
            this.sessionExists = true;
        }
        this.subscribeSessionCreated = this.medikitExtensionService.sessionCreated.subscribe(function () {
            _this.sessionExists = true;
        });
        this.subscribeSessionDropped = this.medikitExtensionService.sessionDropped.subscribe(function () {
            _this.sessionExists = false;
        });
        this.selectionList.selectedOptions = new SelectionModel(false);
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
        this.actions$.pipe(filter(function (action) { return action.type == fromPrescriptionActions.ActionTypes.ADD_PHARMA_PRESCRIPTION_ERROR; }))
            .subscribe(function () {
            _this.snackBar.open(_this.translateService.instant('error-add-prescription'), _this.translateService.instant('undo'), {
                duration: 2000
            });
            _this.isAddingPrescription = false;
        });
        this.actions$.pipe(filter(function (action) { return action.type == fromPrescriptionActions.ActionTypes.ADD_PHARMA_PRESCRIPTION_SUCCESS; }))
            .subscribe(function () {
            sessionStorage.removeItem('pharma-prescription');
            _this.snackBar.open(_this.translateService.instant('prescription-added'), _this.translateService.instant('undo'), {
                duration: 2000
            });
            _this.router.navigate(['/prescription']);
            _this.isAddingPrescription = false;
        });
        this.store.pipe(select('pharmaPrescriptionForm')).subscribe(function (_) {
            if (!_ || !_.prescription) {
                return;
            }
            _this.pharmaPrescription = _.prescription;
            _this.prescribePharmadDrugs = _.prescription.DrugsPrescribed;
            if (_.prescription.Patient != null) {
                self.patientFormGroup.controls['niss'].setErrors(null);
                _this.patientFormGroup.get('niss').setValue(_.prescription.Patient.niss);
                _this.patientFormGroup.get('firstname').setValue(_.prescription.Patient.firstname);
                _this.patientFormGroup.get('lastname').setValue(_.prescription.Patient.lastname);
                _this.patientFormGroup.get('birthdate').setValue(_this.datePipe.transform(_.prescription.Patient.birthdate, "MM/dd/yyyy"));
            }
            else {
                self.patientFormGroup.controls['niss'].setErrors({ unknownNiss: true });
            }
            _this.stepper.selectedIndex = _.stepperIndex;
            if (_this.stepper.steps) {
                _this.stepper.steps.forEach(function (__, i) {
                    if (i <= _.stepperIndex) {
                        __.interacted = true;
                    }
                });
            }
        });
        this.appStore.pipe(select(fromAppState.selectPatientsByNissResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.filteredPatientsByNiss = st.content;
        });
        this.patientFormGroup.controls['niss'].valueChanges.subscribe(function (_) {
            self.appStore.dispatch(new fromPatientActions.SearchPatientsByNiss(_));
        });
        this.init();
    };
    AddPharmaPrescriptionComponent.prototype.nextStep = function () {
        var request = new NextStep();
        this.store.dispatch(request);
    };
    AddPharmaPrescriptionComponent.prototype.previousStep = function () {
        var request = new PreviousStep();
        this.store.dispatch(request);
    };
    AddPharmaPrescriptionComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.paginator.page.subscribe(function () { return _this.refreshSearchDrug(); });
    };
    AddPharmaPrescriptionComponent.prototype.refreshSearchDrug = function () {
        var _this = this;
        var startIndex = 0;
        var count = 5;
        if (this.paginator.pageIndex && this.paginator.pageSize) {
            startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        }
        if (this.paginator.pageSize) {
            count = this.paginator.pageSize;
        }
        var drug = this.drugSearchFormGroup.get('drugName').value;
        this.setIsLoadingProduct(true);
        this.medicinalPackages = [];
        this.medicinalProductService.search(drug, startIndex, count, true, '1').subscribe(function (res) {
            _this.length = res.Count;
            _this.medicinalPackages = res.Content;
            _this.setIsLoadingProduct(false);
        }, function () {
            _this.setIsLoadingProduct(false);
        });
    };
    AddPharmaPrescriptionComponent.prototype.confirmAddDrug = function () {
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
        }
        else {
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
    };
    AddPharmaPrescriptionComponent.prototype.deleteDrugPrescription = function (technicalId) {
        var deleteDrugPrescription = new DeleteDrugPrescription(technicalId);
        this.store.dispatch(deleteDrugPrescription);
    };
    AddPharmaPrescriptionComponent.prototype.getTranslation = function (translations) {
        var defaultLang = this.translateService.currentLang;
        var filteredTranslations = translations.filter(function (tr) {
            return tr.Language == defaultLang;
        });
        if (filteredTranslations.length == 0) {
            return "unknown";
        }
        return filteredTranslations[0].Value;
    };
    AddPharmaPrescriptionComponent.prototype.selectPatient = function (evt) {
        var patient = this.filteredPatientsByNiss.filter(function (_) { return _.niss == evt.option.value; })[0];
        this.store.dispatch(new SelectPatient(patient));
    };
    AddPharmaPrescriptionComponent.prototype.addPrescription = function () {
        this.pharmaPrescription.CreateDateTime = this.createPrescriptionFormGroup.controls['startDate'].value;
        this.pharmaPrescription.EndExecutionDate = this.createPrescriptionFormGroup.controls['expirationDate'].value;
        this.pharmaPrescription.Type = this.createPrescriptionFormGroup.controls['prescriptionType'].value;
        var session = this.medikitExtensionService.getEhealthSession();
        this.isAddingPrescription = true;
        this.appStore.dispatch(new AddPharmaPrescription(this.pharmaPrescription, session['assertion_token']));
    };
    AddPharmaPrescriptionComponent.prototype.ngOnDestroy = function () {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    };
    AddPharmaPrescriptionComponent.prototype.setIsLoadingProduct = function (isLoadingProduct) {
        if (isLoadingProduct) {
            this.isLoadingDrugs = true;
            this.drugSearchFormGroup.get('drugName').disable();
        }
        else {
            this.isLoadingDrugs = false;
            this.drugSearchFormGroup.get('drugName').enable();
        }
    };
    AddPharmaPrescriptionComponent.prototype.newGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
    AddPharmaPrescriptionComponent.prototype.init = function () {
        var _this = this;
        forkJoin(this.referenceTableService.get('CD-ADMINISTRATIONUNIT'), this.referenceTableService.get('CD-TIMEUNIT'), this.prescriptionService.getMetadata()).subscribe(function (res) {
            _this.administrationUnits = res[0].Content;
            _this.timeUnits = res[1].Content;
            var prescriptionTypes = [];
            res[2]['prescriptionTypes'].children.forEach(function (prescriptionType) {
                var type = parseInt(Object.keys(prescriptionType)[0]);
                prescriptionTypes.push(new PrescriptionType(type, prescriptionType[type].translations[0]['en']));
            });
            _this.prescriptionTypes = prescriptionTypes;
            _this.store.dispatch(new LoadPrescription());
        });
    };
    __decorate([
        ViewChild(MatSelectionList),
        __metadata("design:type", MatSelectionList)
    ], AddPharmaPrescriptionComponent.prototype, "selectionList", void 0);
    __decorate([
        ViewChild('stepper'),
        __metadata("design:type", MatStepper)
    ], AddPharmaPrescriptionComponent.prototype, "stepper", void 0);
    __decorate([
        ViewChild(MatPaginator),
        __metadata("design:type", MatPaginator)
    ], AddPharmaPrescriptionComponent.prototype, "paginator", void 0);
    AddPharmaPrescriptionComponent = __decorate([
        Component({
            selector: 'add-pharma-prescription-component',
            templateUrl: './add-pharma-prescription.component.html',
            styleUrls: ['./add-pharma-prescription.component.scss']
        }),
        __metadata("design:paramtypes", [FormBuilder,
            MedicinalProductService,
            Store,
            Store,
            TranslateService,
            ReferenceTableService,
            PharmaPrescriptionService,
            DatePipe,
            MedikitExtensionService,
            ScannedActionsSubject,
            MatSnackBar,
            Router])
    ], AddPharmaPrescriptionComponent);
    return AddPharmaPrescriptionComponent;
}());
export { AddPharmaPrescriptionComponent };
//# sourceMappingURL=add-pharma-prescription.component.js.map