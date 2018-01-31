import { Injectable } from '@angular/core';
import { PatientService } from '@app/patient/services/patient-service';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, CheckNiss } from '../actions/pharma-prescription';

@Injectable()
export class PharmaPrescriptionFormEffects {
    constructor(
        private actions$: Actions,
        private patientService: PatientService,
    ) { }

    @Effect()
    checkNiss$ = this.actions$
        .pipe(
            ofType(ActionTypes.CHECK_NISS),
            mergeMap((evt: CheckNiss) => {
                return this.patientService.get(evt.niss)
                    .pipe(
                        map(patient => { return { type: ActionTypes.NISS_CHECKED, patient: patient }; }),
                        catchError(() => of({ type: ActionTypes.NISS_UNKNOWN, niss : evt.niss }))
                    );
            }
            )
    );
}