import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import * as fromAppState from '@app/stores/appstate';
import * as fromMedicalfileActions from '@app/stores/medicalfile/medicalfile-actions';
import { Medicalfile } from '@app/stores/medicalfile/models/medicalfile';
import { SearchPrescriptionResult } from '@app/stores/medicalfile/models/search-prescription-result';
import { Patient } from '@app/stores/patient/models/patient';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { filter } from 'rxjs/operators';

@Component({
    selector: 'remove-prescription',
    templateUrl: './remove-prescription.html'
})
export class RemovePrescriptionDialog {
    removeFormGroup: FormGroup = new FormGroup({
        reason: new FormControl('', [
            Validators.required
        ])
    });

    constructor(public dialogRef: MatDialogRef<RemovePrescriptionDialog>, @Inject(MAT_DIALOG_DATA) public data: any) { }

    remove() {
        if (this.removeFormGroup.invalid) {
            return;
        }

        this.dialogRef.close({ rid: this.data.rid, reason: this.removeFormGroup.controls['reason'].value });
    }

    close() {
        this.dialogRef.close();
    }
}

@Component({
    selector: 'view-medicalfile-component',
    templateUrl: './view-medicalfile.component.html',
    styleUrls: ['./view-medicalfile.component.scss']
})
export class ViewMedicalfileComponent implements OnInit, OnDestroy {
    isSessionActive: boolean = false;
    isLoadingPrescriptions: boolean = false;
    onlyOpened: boolean = false;
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;
    isLoading: boolean = true;
    searchPrescriptionResult: SearchPrescriptionResult = new SearchPrescriptionResult();
    page: number = 0;
    medicalFile: Medicalfile = new Medicalfile();
    patient: Patient;
    birthdateUrl: string = process.env.REDIRECT_URL + "/assets/images/birthday.png";

    constructor(private store: Store<fromAppState.AppState>,
        private route: ActivatedRoute,
        private actions$: ScannedActionsSubject,
        private translateService: TranslateService,
        private medikitExtensionService: MedikitExtensionService,
        private snackBar: MatSnackBar,
        private dialog: MatDialog) { }

    ngOnInit(): void {
        if (this.medikitExtensionService.getEhealthSession() !== null) {
            this.isSessionActive = true;
        }

        this.subscribeSessionCreated = this.medikitExtensionService.sessionCreated.subscribe(() => {
            this.isSessionActive = true;
            this.refreshPrescriptions();
        });
        this.subscribeSessionDropped = this.medikitExtensionService.sessionDropped.subscribe(() => {
            this.isSessionActive = false;
        });

        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.PRESCRIPTION_REVOKED))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('prescription-revoked'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.refreshPrescriptions();
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.ERROR_SEARCH_PRESCRIPTIONS))
            .subscribe(() => {
                this.isLoadingPrescriptions = false;
                this.snackBar.open(this.translateService.instant('error-getting-prescriptions'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.ERROR_REVOKE_PRESCRIPTION))
            .subscribe(() => {
                this.isLoadingPrescriptions = false;
                this.snackBar.open(this.translateService.instant('error-revoking-prescription'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.store.pipe(select(fromAppState.selectPatientResult)).subscribe((p: Patient) => {
            if (!p) {
                return;
            }

            this.patient = p;
            this.isLoading = false;
        });
        this.store.pipe(select(fromAppState.selectMedicalfileResult)).subscribe((mf: Medicalfile) => {
            if (!mf) {
                return;
            }

            this.medicalFile = mf;
            this.store.dispatch(new fromPatientActions.GetPatientById(mf.patientId));
        });
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionListResult)).subscribe((sr: SearchPrescriptionResult) => {
            if (!sr) {
                return;
            }

            this.searchPrescriptionResult = sr;
            this.isLoadingPrescriptions = false;
        });
        this.refresh();
    }

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    }

    toggleIsOpened(isChecked: boolean) {
        this.onlyOpened = isChecked;
        this.refreshPrescriptions();
    }

    deletePrescription(rid: string) {
        var session: any = this.medikitExtensionService.getEhealthSession();
        if (session === null) {
            return;
        }

        const self = this;
        const id: string = this.route.snapshot.params['id'];
        const dialogRef = this.dialog.open(RemovePrescriptionDialog, {
            width: '300px',
            data: { rid: rid }
        });
        dialogRef.afterClosed().subscribe(_ => {
            if (!_) {
                return;
            }

            self.isLoadingPrescriptions = true;
            var revokePrescription = new fromMedicalfileActions.RevokePrescription(id, _.rid, _.reason, session['assertion_token']);
            self.store.dispatch(revokePrescription);
        });
    } 

    refresh(): void {
        this.isLoading = true;
        const id: string = this.route.snapshot.params['id'];
        this.store.dispatch(new fromMedicalfileActions.GetMedicalfile(id));
        this.refreshPrescriptions();
    }

    refreshPrescriptions() {
        if (!this.isSessionActive) {
            return;
        }

        var session: any = this.medikitExtensionService.getEhealthSession();
        this.isLoadingPrescriptions = true;
        const id : string = this.route.snapshot.params['id'];
        this.store.dispatch(new fromMedicalfileActions.SearchPrescriptions(id, this.page, this.onlyOpened, session['assertion_token']))
    }
}