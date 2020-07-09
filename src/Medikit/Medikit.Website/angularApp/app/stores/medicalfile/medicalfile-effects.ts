import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, AddMedicalfile, AddPrescription, GetMedicalfile, RevokePrescription, SearchMedicalfiles, SearchPrescriptions, GetPrescription } from './medicalfile-actions';
import { MedicalfileService } from './services/medicalfile-service';

@Injectable()
export class MedicalfileEffects {
    constructor(
        private actions$: Actions,
        private medicalfileService: MedicalfileService,
    ) { }

    @Effect()
    searchMedicalfiles$ = this.actions$
        .pipe(
            ofType(ActionTypes.SEARCH_MEDICALFILES),
            mergeMap((evt: SearchMedicalfiles) => {
                return this.medicalfileService.search(evt.firstname, evt.lastname, evt.niss, evt.startIndex, evt.count, evt.active, evt.direction)
                    .pipe(
                        map(medicalfiles => { return { type: ActionTypes.MEDICALFILES_LOADED, medicalfiles: medicalfiles }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_SEARCH_MEDICALFILES }))
                    );
            }
            )
    );

    @Effect()
    addMedicalfile$ = this.actions$
        .pipe(
            ofType(ActionTypes.ADD_MEDICALFILE),
            mergeMap((evt: AddMedicalfile) => {
                return this.medicalfileService.add(evt.patientId)
                    .pipe(
                        map(medicalfile => { return { type: ActionTypes.MEDICALFILE_ADDED, medicalfile: medicalfile }; }),
                        catchError((e) => of({ type: ActionTypes.ERROR_ADD_MEDICALFILE, error: e.error }))
                    );
            }
            )
    );

    @Effect()
    getMedicalfile$ = this.actions$
        .pipe(
            ofType(ActionTypes.GET_MEDICALFILE),
            mergeMap((evt: GetMedicalfile) => {
                return this.medicalfileService.get(evt.medicalfileId)
                    .pipe(
                        map(medicalfile => { return { type: ActionTypes.MEDICALFILE_LOADED, medicalfile: medicalfile }; }),
                        catchError((e) => of({ type: ActionTypes.ERROR_LOAD_MEDICALFILE, error: e.error }))
                    );
            }
            )
    );

    @Effect()
    searchPrescriptions$ = this.actions$
        .pipe(
            ofType(ActionTypes.SEARCH_PRESCRIPTIONS),
            mergeMap((evt: SearchPrescriptions) => {
                if (evt.isOpened) {
                    return this.medicalfileService.getOpenedPrescriptions(evt.medicalfileid, evt.samlAssertion, evt.page)
                        .pipe(
                            map(prescriptions => { return { type: ActionTypes.PRESCRIPTIONS_LOADED, prescriptions: prescriptions }; }),
                            catchError((e) => of({ type: ActionTypes.ERROR_SEARCH_PRESCRIPTIONS, error: e.error }))
                        );
                }
                else {
                    return this.medicalfileService.getPrescriptions(evt.medicalfileid, evt.samlAssertion, evt.page)
                        .pipe(
                            map(prescriptions => { return { type: ActionTypes.PRESCRIPTIONS_LOADED, prescriptions: prescriptions }; }),
                            catchError((e) => of({ type: ActionTypes.ERROR_SEARCH_PRESCRIPTIONS, error: e.error }))
                        );
                }
            }
            )
    );

    @Effect()
    revokePrescription$ = this.actions$
        .pipe(
            ofType(ActionTypes.REVOKE_PRESCRIPTION),
            mergeMap((evt: RevokePrescription) => {
                return this.medicalfileService.revokePrescription(evt.medicalfileId, evt.rid, evt.reason, evt.samlAssertion)
                    .pipe(
                        map(() => { return { type: ActionTypes.PRESCRIPTION_REVOKED }; }),
                        catchError((e) => of({ type: ActionTypes.ERROR_REVOKE_PRESCRIPTION, error: e.error }))
                    );
            }
            )
    );

    @Effect()
    addPrescription$ = this.actions$
        .pipe(
            ofType(ActionTypes.ADD_PRESCRIPTION),
            mergeMap((evt: AddPrescription) => {
                return this.medicalfileService.addPrescription(evt.medicalfileId, evt.prescription, evt.samlAssertion)
                    .pipe(
                        map(() => { return { type: ActionTypes.PRESCRIPTION_ADDED }; }),
                        catchError((e) => of({ type: ActionTypes.ERROR_ADD_PRESCRIPTION, error: e.error }))
                    );
            }
            )
    );

    @Effect()
    getPrescription$ = this.actions$
        .pipe(
            ofType(ActionTypes.GET_PRESCRIPTION),
            mergeMap((evt: GetPrescription) => {
                return this.medicalfileService.getPrescription(evt.medicalfileId, evt.rid, evt.samlAssertion)
                    .pipe(
                        map((prescription) => { return { type: ActionTypes.PRESCRIPTION_LOADED, prescription: prescription }; }),
                        catchError((e) => of({ type: ActionTypes.ERROR_GET_PRESCRIPTION, error: e.error }))
                    );
            }
            )
        );
}