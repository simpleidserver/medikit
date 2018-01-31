import { Injectable } from '@angular/core';
import { PrescriptionService } from '@app/prescription/services/prescription-service';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, LoadPharmaPrescriptions } from '../actions/pharma-prescription';

@Injectable()
export class ListPharmaPrescriptionEffects {
    constructor(
        private actions$: Actions,
        private prescriptionService: PrescriptionService,
    ) { }

    @Effect()
    getOpenedPrescriptions$ = this.actions$
        .pipe(
            ofType(ActionTypes.LOAD_PHARMA_PRESCRIPTIONS),
            mergeMap((evt: LoadPharmaPrescriptions) => {
                return this.prescriptionService.getOpenedPrescriptions(evt.patientNiss, evt.page, evt.samlAssertion)
                    .pipe(
                        map(prescriptionIds => { return { type: ActionTypes.PHARMA_PRESCRIPTIONS_LOADED, prescriptionIds: prescriptionIds }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_LOAD_PHARMA_PRESCRIPTIONS }))
                    );
            }
            )
    );
}