import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import * as fromAppState from '@app/stores/appstate';
import { Patient } from '@app/stores/patient/models/patient';
import { SearchPatientResult } from '@app/stores/patient/models/search-patient-result';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import * as fromPrescriptionActions from '@app/stores/pharmaprescription/prescription-actions';
import { select, Store } from '@ngrx/store';

@Component({
    selector: 'list-prescription-component',
    templateUrl: './list-prescription.component.html',
    styleUrls: ['./list-prescription.component.scss']
})
export class ListPrescriptionComponent implements OnInit, OnDestroy {
    filteredPatientsByNiss: Patient[] = [];
    nissFormGroup: FormGroup = new FormGroup({
        niss : new FormControl()
    });
    sessionExists: boolean = false;
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;
    prescriptionIds: Array<string> = [];

    constructor(private store: Store<fromAppState.AppState>,
        private medikitExtensionService: MedikitExtensionService) { }

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
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionListResult)).subscribe((st: Array<string>) => {
            if (!st) {
                return;
            }

            this.prescriptionIds = st;
        });
        this.store.pipe(select(fromAppState.selectPatientsResult)).subscribe((st: SearchPatientResult) => {
            if (!st) {
                return;
            }

            this.filteredPatientsByNiss = st.content;
        });
        const self = this;
        this.nissFormGroup.controls["niss"].valueChanges.subscribe((_) => {
            self.store.dispatch(new fromPatientActions.SearchPatients(null, null, _));
        });
    }

    onSumitNissForm(form: any) {
        this.onSubmit(form.niss);
    }

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    }

    private onSubmit(niss: any) {
        if (!this.sessionExists) {
            return;
        }

        var session: any = this.medikitExtensionService.getEhealthSession();
        var loadPharmaPrescriptions = new fromPrescriptionActions.LoadPharmaPrescriptions(niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }
}