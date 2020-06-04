import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { select, Store } from '@ngrx/store';
import { LoadPharmaPrescriptions } from './actions/pharma-prescription';
import { PharmaPrescriptionsState } from './states/pharma-prescription-state';

@Component({
    selector: 'list-prescription-component',
    templateUrl: './list-prescription.component.html',
    styleUrls: ['./list-prescription.component.scss']
})
export class ListPrescriptionComponent implements OnInit, OnDestroy {
    sessionExists: boolean = false;
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;
    prescriptionIds: Array<string> = [];
    searchForm : FormGroup = new FormGroup({
        niss : new FormControl()
    });

    constructor(private store: Store<PharmaPrescriptionsState>,private medikitExtensionService: MedikitExtensionService) { }

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
        this.store.pipe(select('pharmaPrescriptionLst')).subscribe((st: PharmaPrescriptionsState) => {
            this.prescriptionIds = st.prescriptionIds;
        });
    }

    onSubmit(form: any) {
        if (!this.sessionExists) {
            return;
        }

        var session: any = this.medikitExtensionService.getEhealthSession();
        var loadPharmaPrescriptions = new LoadPharmaPrescriptions(form.niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    }

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    }
}