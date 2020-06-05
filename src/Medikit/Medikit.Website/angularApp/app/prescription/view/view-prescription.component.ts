import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { PharmaPrescription } from '@app/stores/pharmaprescription/models/pharma-prescription';
import { LoadPharmaPrescription } from '@app/stores/pharmaprescription/prescription-actions';
import * as fromAppState from '@app/stores/appstate';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'view-prescription-component',
    templateUrl: './view-prescription.component.html'
})
export class ViewPrescriptionComponent implements OnInit {
    prescription: PharmaPrescription;

    constructor(private translateService: TranslateService, private snackBar: MatSnackBar, private store: Store<fromAppState.AppState>, private route: ActivatedRoute, private medikitExtensionService: MedikitExtensionService) { }

    ngOnInit(): void {
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionResult)).subscribe((st: PharmaPrescription) => {
            if (!st) {
                return;
            }

            this.prescription = st;
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
        var loadPharmaPrescriptions = new LoadPharmaPrescription(id, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }
}