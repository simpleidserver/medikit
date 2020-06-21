import { Component, OnDestroy, OnInit, ViewChild, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import * as fromAppState from '@app/stores/appstate';
import { Patient } from '@app/stores/patient/models/patient';
import { SearchPatientResult } from '@app/stores/patient/models/search-patient-result';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import * as fromPrescriptionActions from '@app/stores/pharmaprescription/prescription-actions';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import { MatPaginator, MatDialogRef, MAT_DIALOG_DATA, MatDialog, MatSnackBar } from '@angular/material';
import { filter } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

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
    selector: 'list-prescription-component',
    templateUrl: './list-prescription.component.html',
    styleUrls: ['./list-prescription.component.scss']
})
export class ListPrescriptionComponent implements OnInit, OnDestroy {
    prescriptionsLength: number;
    displayedColumns: string[] = [ "niss", "firstname", "lastname", "actions"];
    filteredPatientsByNiss: Patient[] = [];
    filteredPatients: Patient[] = [];
    nissFormGroup: FormGroup = new FormGroup({
        niss : new FormControl()
    });
    searchInsuredFormGroup: FormGroup = new FormGroup(
    {
        firstname: new FormControl(),
        lastname: new FormControl()
    });
    sessionExists: boolean = false;
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;
    prescriptionIds: Array<string> = [];
    @ViewChild('prescriptionPaginator') prescriptionPaginator: MatPaginator;

    constructor(private store: Store<fromAppState.AppState>,
        private medikitExtensionService: MedikitExtensionService,
        private dialog: MatDialog,
        private actions$: ScannedActionsSubject,
        private snackBar: MatSnackBar,
        private translateService : TranslateService) { }

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
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionListResult)).subscribe((st: Array<string>) => {
            if (!st) {
                return;
            }

            this.prescriptionIds = st;
        });
        this.store.pipe(select(fromAppState.selectPatientsByNissResult)).subscribe((st: SearchPatientResult) => {
            if (!st) {
                return;
            }

            this.filteredPatientsByNiss = st.content;
        })
        this.store.pipe(select(fromAppState.selectPatientsResult)).subscribe((st: SearchPatientResult) => {
            if (!st) {
                return;
            }

            this.filteredPatients = st.content;
            this.prescriptionsLength = st.totalLength;
        });
        this.nissFormGroup.controls["niss"].valueChanges.subscribe((_) => {
            self.store.dispatch(new fromPatientActions.SearchPatientsByNiss(_));
        });
        this.actions$.pipe(
            filter((action: any) => action.type == fromPrescriptionActions.ActionTypes.REVOKE_PHARMA_PRESCRIPTION_SUCCESS))
            .subscribe(() => {
                this.snackBar.open(self.translateService.instant('prescription-revoked'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromPrescriptionActions.ActionTypes.REVOKE_PHARMA_PRESCRIPTION_ERROR))
            .subscribe(() => {
                this.snackBar.open(self.translateService.instant('prescription-not-revoked'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.refreshInsured();
    }

    ngAfterViewInit() {
        this.prescriptionPaginator.page.subscribe(() => this.refreshInsured());
    }

    onSumitNissForm(form: any) {
        this.display(form.niss);
    }

    onSubmitSearchInsuredForm() {
        this.refreshInsured();
    }

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    }

    deletePrescription(rid: string) {
        if (!this.sessionExists) {
            return;
        }

        var session: any = this.medikitExtensionService.getEhealthSession();
        const self = this;
        const dialogRef = this.dialog.open(RemovePrescriptionDialog, {
            width: '300px',
            data: { rid: rid }
        });        
        dialogRef.afterClosed().subscribe(_ => {
            if (!_) {
                return;
            }

            var revokePrescription = new fromPrescriptionActions.RevokePharmaPrescription(_.rid, _.reason, session['assertion_token']);
            self.store.dispatch(revokePrescription);
        });
    }

    display(niss: any) {
        if (!this.sessionExists) {
            return;
        }

        var session: any = this.medikitExtensionService.getEhealthSession();
        var loadPharmaPrescriptions = new fromPrescriptionActions.LoadPharmaPrescriptions(niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }

    refreshInsured() {
        let startIndex = 0;
        let count = 5;
        var firstname = this.searchInsuredFormGroup.controls['firstname'].value;
        var lastname = this.searchInsuredFormGroup.controls['lastname'].value;
        if (this.prescriptionPaginator.pageIndex && this.prescriptionPaginator.pageSize) {
            startIndex = this.prescriptionPaginator.pageIndex * this.prescriptionPaginator.pageSize;
        }

        if (this.prescriptionPaginator.pageSize) {
            count = this.prescriptionPaginator.pageSize;
        }

        this.store.dispatch(new fromPatientActions.SearchPatients(firstname, lastname, startIndex, count));
    }
}