import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { PharmaPrescription } from '@app/stores/pharmaprescription/models/pharma-prescription';
import { LoadPharmaPrescription } from '@app/stores/pharmaprescription/prescription-actions';
import * as fromAppState from '@app/stores/appstate';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
    selector: 'view-prescription-component',
    templateUrl: './view-prescription.component.html'
})
export class ViewPrescriptionComponent implements OnInit {
    isLoading: boolean = false;
    nbPrescriptions: number = 1;
    updateNbPrescriptionsForm: FormGroup = new FormGroup({
        nbPrescriptions: new FormControl(1)
    });
    prescription: PharmaPrescription;

    constructor(private translateService: TranslateService, private snackBar: MatSnackBar, private store: Store<fromAppState.AppState>, private route: ActivatedRoute, private medikitExtensionService: MedikitExtensionService) { }

    ngOnInit(): void {
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionResult)).subscribe((st: PharmaPrescription) => {
            if (!st) {
                return;
            }

            if (this.isLoading) {
                this.prescription = st;
                this.isLoading = false;
            }
        });
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

        var id = this.route.snapshot.params['id'];
        this.isLoading = true;
        var loadPharmaPrescriptions = new LoadPharmaPrescription(id, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }

    updateNbPrescriptions(evt: any, form: any) {
        evt.preventDefault();
        this.nbPrescriptions = form.nbPrescriptions;
    }
}