import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { PharmaPrescription } from '../models/pharma-prescription';
import { LoadPharmaPrescription } from './actions/pharma-prescription';
import { PharmaPrescriptionState } from './states/pharma-prescription-state';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { TranslateService } from '@ngx-translate/core';
import { MatSnackBar } from '@angular/material';

@Component({
    selector: 'view-prescription-component',
    templateUrl: './view-prescription.component.html'
})
export class ViewPrescriptionComponent implements OnInit {
    prescription: PharmaPrescription;

    constructor(private translateService: TranslateService, private snackBar: MatSnackBar, private store: Store<PharmaPrescriptionState>, private route: ActivatedRoute, private medikitExtensionService: MedikitExtensionService) { }

    ngOnInit(): void {
        this.store.pipe(select('pharmaPrescriptionView')).subscribe((st: PharmaPrescriptionState) => {
            if (!st.prescription) {
                return;
            }

            this.prescription = st.prescription;
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