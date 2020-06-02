import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { MedikitExtensionService } from '../../services/medikitextension.service';
import { LoadPharmaPrescriptions } from './actions/pharma-prescription';
import { PharmaPrescriptionsState } from './states/pharma-prescription-state';

@Component({
    selector: 'list-prescription-component',
    templateUrl: './list-prescription.component.html',
    styleUrls: ['./list-prescription.component.scss']
})
export class ListPrescriptionComponent implements OnInit {
    prescriptionIds: Array<string> = [];
    isEhealthSessionActive: boolean;
    isExtensionInstalled: boolean;
    searchForm : FormGroup = new FormGroup({
        niss : new FormControl()
    });

    constructor(private store: Store<PharmaPrescriptionsState>, private translateService : TranslateService, private medikitExtensionService: MedikitExtensionService, private snackBar : MatSnackBar) { }

    ngOnInit(): void {
        this.isExtensionInstalled = this.medikitExtensionService.isExtensionInstalled();
        const session: any = this.medikitExtensionService.getEhealthSession();
        if (session) {
            this.isEhealthSessionActive = true;
        } else {
            this.isEhealthSessionActive = false;
        }

        this.store.pipe(select('pharmaPrescriptionLst')).subscribe((st: PharmaPrescriptionsState) => {
            this.prescriptionIds = st.prescriptionIds;
        });
    }

    authenticateEHEALTH(): void {
        if (!this.medikitExtensionService.isExtensionInstalled()) {
            return;
        }

        const self: any = this;
        this.medikitExtensionService.getEhealthCertificateAuth().subscribe(function () {
            self.isEhealthSessionActive = true;
        });
    }

    disconnectEHEALTH(): void {
        this.medikitExtensionService.disconnect();
        this.isEhealthSessionActive = false;
    }

    onSubmit(form: any) {
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

        var loadPharmaPrescriptions = new LoadPharmaPrescriptions(form.niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }
}