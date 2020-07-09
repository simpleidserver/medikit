import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import * as fromAppState from '@app/stores/appstate';
import { PharmaPrescription } from '@app/stores/medicalfile/models/pharma-prescription';
import { GetPrescription } from '@app/stores/medicalfile/medicalfile-actions';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import * as fromMedicalfileActions from '@app/stores/medicalfile/medicalfile-actions';
import { filter } from 'rxjs/operators';

@Component({
    selector: 'view-prescription-component',
    templateUrl: './view-prescription.component.html'
})
export class ViewPrescriptionComponent implements OnInit {
    medicalfileId: string = null;
    isLoading: boolean = false;
    nbPrescriptions: number = 1;
    updateNbPrescriptionsForm: FormGroup = new FormGroup({
        nbPrescriptions: new FormControl(1)
    });
    prescription: PharmaPrescription;

    constructor(private translateService: TranslateService,
        private snackBar: MatSnackBar,
        private store: Store<fromAppState.AppState>,
        private route: ActivatedRoute,
        private medikitExtensionService: MedikitExtensionService,
        private actions$: ScannedActionsSubject) { }

    ngOnInit(): void {
        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.ERROR_GET_PRESCRIPTION))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('error-get-prescription'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionResult)).subscribe((st: PharmaPrescription) => {
            if (!st) {
                return;
            }

            if (this.isLoading) {
                this.prescription = st;
                this.isLoading = false;
            }
        });
        this.medicalfileId = this.route.snapshot.params['id'];
        this.refresh();
    }

    refresh(): void {
        const undo = this.translateService.instant('undo');
        if (!this.medikitExtensionService.isExtensionInstalled()) {
            this.snackBar.open(this.translateService.instant('extension-not-installed'), undo, {
                duration: 2000,
            });
            return;
        }

        var session: any = this.medikitExtensionService.getEhealthSession();
        if (session == null) {
            this.snackBar.open(this.translateService.instant('no-active-session'), undo, {
                duration: 2000,
            });
            return;
        }

        var rid = this.route.snapshot.params['rid'];
        this.isLoading = true;
        var loadPharmaPrescriptions = new GetPrescription(this.medicalfileId, rid, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }

    updateNbPrescriptions(evt: any, form: any) {
        evt.preventDefault();
        this.nbPrescriptions = form.nbPrescriptions;
    }
}