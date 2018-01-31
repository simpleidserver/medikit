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
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatPaginator, MatSelectionList, MatSnackBar, MatStepper } from '@angular/material';
import { MedicinalProductService } from '@app/medicinalproduct/services/medicinalproduct-service';
import { ReferenceTableService } from '@app/referencetable/services/reference-table-service';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ActionTypes, AddDrugPrescription, CheckNiss, DeleteDrugPrescription, LoadPrescription, NextStep, PreviousStep } from './actions/pharma-prescription';
import { PharmaDrugPrescription } from '@app/prescription/models/pharma-drug-prescription';
import { PharmaDuration } from '@app/prescription/models/pharma-duration';
import { PharmaPosologyFreeText, PharmaPosologyStructured, PharmaPosologyTakes } from '@app/prescription/models/pharma-posology';
var AddPharmaPrescriptionComponent = (function () {
    function AddPharmaPrescriptionComponent(formBuilder, medicinalProductService, store, translateService, actions$, snackBar, referenceTableService) {
        this.formBuilder = formBuilder;
        this.medicinalProductService = medicinalProductService;
        this.store = store;
        this.translateService = translateService;
        this.actions$ = actions$;
        this.snackBar = snackBar;
        this.referenceTableService = referenceTableService;
        this.nextPatientFormBtnDisabled = true;
        this.prescribePharmadDrugs = [];
        this.timeUnits = [];
        this.administrationUnits = [];
        this.medicinalProducts = [];
        this.selectedMedicinalPackages = [];
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
        this.actions$.pipe(filter(function (action) { return action.type == ActionTypes.NISS_UNKNOWN; }))
            .subscribe(function () {
            _this.snackBar.open(_this.translateService.instant('niss-unknown'), _this.translateService.instant('undo'), {
                duration: 2000
            });
        });
        this.store.pipe(select('pharmaPrescriptionForm')).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.stepper.selectedIndex = st.stepperIndex;
            _this.nextPatientFormBtnDisabled = st.nextPatientFormBtnDisabled;
            if (st.prescription.Patient != null) {
                _this.patientFormGroup.get('niss').setValue(st.prescription.Patient.niss);
                _this.patientFormGroup.get('firstname').setValue(st.prescription.Patient.firstname);
                _this.patientFormGroup.get('lastname').setValue(st.prescription.Patient.lastname);
                _this.patientFormGroup.get('birthdate').setValue(st.prescription.Patient.birthdate);
            }
            _this.prescribePharmadDrugs = st.prescription.DrugsPrescribed;
            if (_this.nextPatientFormBtnDisabled) {
                _this.patientFormGroup.controls['niss'].setErrors({ unknownNiss: true });
            }
            else {
                _this.patientFormGroup.controls['niss'].setErrors(null);
            }
        });
        this.init();
    };
    AddPharmaPrescriptionComponent.prototype.checkNiss = function (evt) {
        evt.preventDefault();
        evt.stopPropagation();
        var niss = this.patientFormGroup.get('niss').value;
        this.store.dispatch(new CheckNiss(niss));
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
        this.medicinalProductService.search(drug, startIndex, count, true, "P").subscribe(function (res) {
            _this.length = res.Count;
            _this.medicinalProducts = res.Content;
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
    AddPharmaPrescriptionComponent.prototype.newGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
    AddPharmaPrescriptionComponent.prototype.init = function () {
        var _this = this;
        forkJoin(this.referenceTableService.get('CD-ADMINISTRATIONUNIT'), this.referenceTableService.get('CD-TIMEUNIT')).subscribe(function (res) {
            _this.administrationUnits = res[0].Content;
            _this.timeUnits = res[1].Content;
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
            TranslateService,
            ScannedActionsSubject,
            MatSnackBar,
            ReferenceTableService])
    ], AddPharmaPrescriptionComponent);
    return AddPharmaPrescriptionComponent;
}());
export { AddPharmaPrescriptionComponent };
//# sourceMappingURL=add-pharma-prescription.component.js.map