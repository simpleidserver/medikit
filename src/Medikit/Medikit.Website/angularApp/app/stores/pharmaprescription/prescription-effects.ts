import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, LoadPharmaPrescription, LoadPharmaPrescriptions } from './prescription-actions';
import { PharmaPrescriptionService } from './services/prescription-service';

@Injectable()
export class PharmaPrescriptionEffects {
    constructor(
        private actions$: Actions,
        private prescriptionService: PharmaPrescriptionService,
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

    @Effect()
    getPrescription$ = this.actions$
        .pipe(
            ofType(ActionTypes.LOAD_PHARMA_PRESCRIPTION),
            mergeMap((evt: LoadPharmaPrescription) => {
                return this.prescriptionService.getPrescription(evt.prescriptionId, evt.samlAssertion)
                    .pipe(
                        map(prescription => { return { type: ActionTypes.PHARMA_PRESCRIPTION_LOADED, prescription: prescription }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_LOAD_PHARMA_PRESCRIPTION }))
                    );
            }
            )
        );
}