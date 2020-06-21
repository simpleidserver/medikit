import { Action } from '@ngrx/store';
import { PharmaPrescription } from './models/pharma-prescription';

export enum ActionTypes {
    LOAD_PHARMA_PRESCRIPTIONS = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTIONS",
    PHARMA_PRESCRIPTIONS_LOADED = "[PharmaPrescription] PHARMA_PRESCRIPTIONS_LOADED",
    ERROR_LOAD_PHARMA_PRESCRIPTIONS = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTIONS",
    LOAD_PHARMA_PRESCRIPTION = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTION",
    PHARMA_PRESCRIPTION_LOADED = "[PharmaPrescription] PHARMA_PRESCRIPTION_LOADED",
    ERROR_LOAD_PHARMA_PRESCRIPTION = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTION",
    ADD_PHARMA_PRESCRIPTION = "[PharmaPrescription] ADD_PHARMA_PRESCRIPTION",
    ADD_PHARMA_PRESCRIPTION_SUCCESS = "[PharmaPrescription] ADD_PHARMA_PRESCRIPTION_SUCCESS",
    ADD_PHARMA_PRESCRIPTION_ERROR = "[PharmaPrescription] ADD_PHARMA_PRESCRIPTION_ERROR",
    REVOKE_PHARMA_PRESCRIPTION = "[PharmaPrescription] REVOKE_PHARMA_PRESCRIPTION",
    REVOKE_PHARMA_PRESCRIPTION_SUCCESS = "[PharmaPrescription] REVOKE_PHARMA_PRESCRIPTION_SUCCESS",
    REVOKE_PHARMA_PRESCRIPTION_ERROR = "[PharmaPrescription] REVOKE_PHARMA_PRESCRIPTION_ERROR"
}

export class LoadPharmaPrescriptions implements Action {
    readonly type = ActionTypes.LOAD_PHARMA_PRESCRIPTIONS;
    constructor(public patientNiss : string, public page : number, public samlAssertion: string) { }
}

export class PharmaPrescriptionsLoaded implements Action {
    readonly type = ActionTypes.PHARMA_PRESCRIPTIONS_LOADED;
    constructor(public prescriptionIds: Array<string>) { }
}

export class LoadPharmaPrescription implements Action {
    readonly type = ActionTypes.LOAD_PHARMA_PRESCRIPTION;
    constructor(public prescriptionId: string, public samlAssertion: string) { }
}

export class PharmaPrescriptionLoaded implements Action {
    readonly type = ActionTypes.PHARMA_PRESCRIPTION_LOADED;
    constructor(public prescription: PharmaPrescription) { }
}

export class AddPharmaPrescription implements Action {
    readonly type = ActionTypes.ADD_PHARMA_PRESCRIPTION;
    constructor(public prescription: PharmaPrescription, public samlAssertion: string) { }
}

export class RevokePharmaPrescription implements Action {
    readonly type = ActionTypes.REVOKE_PHARMA_PRESCRIPTION;
    constructor(public rid: string, public reason: string, public samlAssertion: string) { }
}

export class PharmaPrescriptionRevoked implements Action {
    readonly type = ActionTypes.REVOKE_PHARMA_PRESCRIPTION_SUCCESS;
    constructor(public rid: string) { }
}

export type ActionsUnion = LoadPharmaPrescriptions | PharmaPrescriptionsLoaded | LoadPharmaPrescription | PharmaPrescriptionLoaded | AddPharmaPrescription | RevokePharmaPrescription | PharmaPrescriptionRevoked;