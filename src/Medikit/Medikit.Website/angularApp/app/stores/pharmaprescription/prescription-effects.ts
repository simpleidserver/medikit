import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, LoadPharmaPrescription, LoadPharmaPrescriptions, AddPharmaPrescription, RevokePharmaPrescription } from './prescription-actions';
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

    @Effect()
    addPrescription$ = this.actions$
        .pipe(
            ofType(ActionTypes.ADD_PHARMA_PRESCRIPTION),
            mergeMap((evt: AddPharmaPrescription) => {
                return this.prescriptionService.addPrescription(evt.prescription, evt.samlAssertion)
                    .pipe(
                        map(prescriptionId => { return { type: ActionTypes.ADD_PHARMA_PRESCRIPTION_SUCCESS, prescriptionId: prescriptionId }; }),
                        catchError(() => of({ type: ActionTypes.ADD_PHARMA_PRESCRIPTION_ERROR }))
                    );
            }
            )
        );

    @Effect()
    revokePrescription$ = this.actions$
        .pipe(
            ofType(ActionTypes.REVOKE_PHARMA_PRESCRIPTION),
            mergeMap((evt: RevokePharmaPrescription) => {
                return this.prescriptionService.revokePrescription(evt.rid, evt.reason, evt.samlAssertion)
                    .pipe(
                        map(() => { return { type: ActionTypes.REVOKE_PHARMA_PRESCRIPTION_SUCCESS, rid: evt.rid }; }),
                        catchError(() => of({ type: ActionTypes.REVOKE_PHARMA_PRESCRIPTION_ERROR }))
                    );
            }
            )
        );
}