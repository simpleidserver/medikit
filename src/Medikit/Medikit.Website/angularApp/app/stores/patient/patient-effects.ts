import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, SearchPatients, GetPatientById, GetPatientByNiss, SearchPatientsByNiss, AddPatient } from './patient-actions';
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
                return this.patientService.search(evt.firstname, evt.lastname, evt.niss, evt.startIndex, evt.count, evt.active, evt.direction)
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
    getPatientById$ = this.actions$
        .pipe(
            ofType(ActionTypes.GET_PATIENT_BY_ID),
            mergeMap((evt: GetPatientById) => {
                return this.patientService.getById(evt.id)
                    .pipe(
                        map(patient => { return { type: ActionTypes.PATIENT_LOADED, patient: patient }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_GET_PATIENT }))
                    );
            }
            )
        );

    @Effect()
    getPatientByNiss$ = this.actions$
        .pipe(
            ofType(ActionTypes.GET_PATIENT_BY_NISS),
            mergeMap((evt: GetPatientByNiss) => {
                return this.patientService.getByNiss(evt.niss)
                    .pipe(
                        map(patient => { return { type: ActionTypes.PATIENT_LOADED, patient: patient }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_GET_PATIENT }))
                    );
            }
            )
    );

    @Effect()
    addPatient$ = this.actions$
        .pipe(
            ofType(ActionTypes.ADD_PATIENT),
            mergeMap((evt: AddPatient) => {
                return this.patientService.add(evt.patient)
                    .pipe(
                        map(() => { return { type: ActionTypes.ADD_PATIENT_SUCCESS }; }),
                        catchError(() => of({ type: ActionTypes.ADD_PATIENT_ERROR }))
                    );
            }
            )
        );
}