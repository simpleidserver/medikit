var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
import { Component, ViewChild, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import * as fromAppState from '@app/stores/appstate';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import * as fromPrescriptionActions from '@app/stores/pharmaprescription/prescription-actions';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import { MatPaginator, MatDialogRef, MAT_DIALOG_DATA, MatDialog, MatSnackBar } from '@angular/material';
import { filter } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';
var RemovePrescriptionDialog = (function () {
    function RemovePrescriptionDialog(dialogRef, data) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.removeFormGroup = new FormGroup({
            reason: new FormControl('', [
                Validators.required
            ])
        });
    }
    RemovePrescriptionDialog.prototype.remove = function () {
        if (this.removeFormGroup.invalid) {
            return;
        }
        this.dialogRef.close({ rid: this.data.rid, reason: this.removeFormGroup.controls['reason'].value });
    };
    RemovePrescriptionDialog.prototype.close = function () {
        this.dialogRef.close();
    };
    RemovePrescriptionDialog = __decorate([
        Component({
            selector: 'remove-prescription',
            templateUrl: './remove-prescription.html'
        }),
        __param(1, Inject(MAT_DIALOG_DATA)),
        __metadata("design:paramtypes", [MatDialogRef, Object])
    ], RemovePrescriptionDialog);
    return RemovePrescriptionDialog;
}());
export { RemovePrescriptionDialog };
var ListPrescriptionComponent = (function () {
    function ListPrescriptionComponent(store, medikitExtensionService, dialog, actions$, snackBar, translateService) {
        this.store = store;
        this.medikitExtensionService = medikitExtensionService;
        this.dialog = dialog;
        this.actions$ = actions$;
        this.snackBar = snackBar;
        this.translateService = translateService;
        this.displayedColumns = ["niss", "firstname", "lastname", "actions"];
        this.filteredPatientsByNiss = [];
        this.filteredPatients = [];
        this.nissFormGroup = new FormGroup({
            niss: new FormControl()
        });
        this.searchInsuredFormGroup = new FormGroup({
            firstname: new FormControl(),
            lastname: new FormControl()
        });
        this.sessionExists = false;
        this.prescriptionIds = [];
    }
    ListPrescriptionComponent.prototype.ngOnInit = function () {
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
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionListResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.prescriptionIds = st;
        });
        this.store.pipe(select(fromAppState.selectPatientsByNissResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.filteredPatientsByNiss = st.content;
        });
        this.store.pipe(select(fromAppState.selectPatientsResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.filteredPatients = st.content;
            _this.prescriptionsLength = st.totalLength;
        });
        this.nissFormGroup.controls["niss"].valueChanges.subscribe(function (_) {
            self.store.dispatch(new fromPatientActions.SearchPatientsByNiss(_));
        });
        this.actions$.pipe(filter(function (action) { return action.type == fromPrescriptionActions.ActionTypes.REVOKE_PHARMA_PRESCRIPTION_SUCCESS; }))
            .subscribe(function () {
            _this.snackBar.open(self.translateService.instant('prescription-revoked'), _this.translateService.instant('undo'), {
                duration: 2000
            });
        });
        this.actions$.pipe(filter(function (action) { return action.type == fromPrescriptionActions.ActionTypes.REVOKE_PHARMA_PRESCRIPTION_ERROR; }))
            .subscribe(function () {
            _this.snackBar.open(self.translateService.instant('prescription-not-revoked'), _this.translateService.instant('undo'), {
                duration: 2000
            });
        });
        this.refreshInsured();
    };
    ListPrescriptionComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.prescriptionPaginator.page.subscribe(function () { return _this.refreshInsured(); });
    };
    ListPrescriptionComponent.prototype.onSumitNissForm = function (form) {
        this.display(form.niss);
    };
    ListPrescriptionComponent.prototype.onSubmitSearchInsuredForm = function () {
        this.refreshInsured();
    };
    ListPrescriptionComponent.prototype.ngOnDestroy = function () {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    };
    ListPrescriptionComponent.prototype.deletePrescription = function (rid) {
        if (!this.sessionExists) {
            return;
        }
        var session = this.medikitExtensionService.getEhealthSession();
        var self = this;
        var dialogRef = this.dialog.open(RemovePrescriptionDialog, {
            width: '300px',
            data: { rid: rid }
        });
        dialogRef.afterClosed().subscribe(function (_) {
            if (!_) {
                return;
            }
            var revokePrescription = new fromPrescriptionActions.RevokePharmaPrescription(_.rid, _.reason, session['assertion_token']);
            self.store.dispatch(revokePrescription);
        });
    };
    ListPrescriptionComponent.prototype.display = function (niss) {
        if (!this.sessionExists) {
            return;
        }
        var session = this.medikitExtensionService.getEhealthSession();
        var loadPharmaPrescriptions = new fromPrescriptionActions.LoadPharmaPrescriptions(niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    };
    ListPrescriptionComponent.prototype.refreshInsured = function () {
        var startIndex = 0;
        var count = 5;
        var firstname = this.searchInsuredFormGroup.controls['firstname'].value;
        var lastname = this.searchInsuredFormGroup.controls['lastname'].value;
        if (this.prescriptionPaginator.pageIndex && this.prescriptionPaginator.pageSize) {
            startIndex = this.prescriptionPaginator.pageIndex * this.prescriptionPaginator.pageSize;
        }
        if (this.prescriptionPaginator.pageSize) {
            count = this.prescriptionPaginator.pageSize;
        }
        this.store.dispatch(new fromPatientActions.SearchPatients(null, firstname, lastname, startIndex, count));
    };
    __decorate([
        ViewChild('prescriptionPaginator'),
        __metadata("design:type", MatPaginator)
    ], ListPrescriptionComponent.prototype, "prescriptionPaginator", void 0);
    ListPrescriptionComponent = __decorate([
        Component({
            selector: 'list-prescription-component',
            templateUrl: './list-prescription.component.html',
            styleUrls: ['./list-prescription.component.scss']
        }),
        __metadata("design:paramtypes", [Store,
            MedikitExtensionService,
            MatDialog,
            ScannedActionsSubject,
            MatSnackBar,
            TranslateService])
    ], ListPrescriptionComponent);
    return ListPrescriptionComponent;
}());
export { ListPrescriptionComponent };
//# sourceMappingURL=list-prescription.component.js.map