import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, SearchPatients, GetPatient, SearchPatientsByNiss } from './patient-actions';
import { PatientService } from './services/patient-service';

@Injectable()
export class PatientEffects {
    constructor(
        private actions$: Actions,
        private patientService: PatientService,
    ) { }

    @Effect()
    searchPatients$ = this.actions$
        .pipe(
            ofType(ActionTypes.SEARCH_PATIENTS),
            mergeMap((evt: SearchPatients) => {
                return this.patientService.search(evt.firstname, evt.lastname, null, evt.startIndex, evt.count)
                    .pipe(
                        map(patients => { return { type: ActionTypes.PATIENTS_LOADED, patients: patients }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_SEARCH_PATIENTS }))
                    );
            }
            )
    );

    @Effect()
    searchPatientsByNiss$ = this.actions$
        .pipe(
            ofType(ActionTypes.SEARCH_PATIENTS_BY_NISS),
            mergeMap((evt: SearchPatientsByNiss) => {
                return this.patientService.search(null, null, evt.niss, 0, 0)
                    .pipe(
                        map(patients => { return { type: ActionTypes.PATIENTS_LOADED_BY_NISS, patients: patients }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_SEARCH_PATIENTS_BY_NISS }))
                    );
            }
            )
        );

    @Effect()
    getPatient$ = this.actions$
        .pipe(
            ofType(ActionTypes.GET_PATIENT),
            mergeMap((evt: GetPatient) => {
                return this.patientService.get(evt.niss)
                    .pipe(
                        map(patient => { return { type: ActionTypes.PATIENT_LOADED, patient: patient }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_GET_PATIENT }))
                    );
            }
            )
        );
}