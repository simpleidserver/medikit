import { Injectable } from '@angular/core';
import { PrescriptionService } from '@app/prescription/services/prescription-service';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, LoadPharmaPrescription } from '../actions/pharma-prescription';

@Injectable()
export class ViewPharmaPrescriptionEffects {
    constructor(
        private actions$: Actions,
        private prescriptionService: PrescriptionService,
    ) { }

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