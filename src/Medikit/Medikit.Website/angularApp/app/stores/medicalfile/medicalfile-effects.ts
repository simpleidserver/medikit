import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, SearchMedicalfiles, AddMedicalfile } from './medicalfile-actions';
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
}