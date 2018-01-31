import { Action } from '@ngrx/store';

export enum ActionTypes {
    LOAD_PHARMA_PRESCRIPTIONS = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTIONS",
    PHARMA_PRESCRIPTIONS_LOADED = "[PharmaPrescription] PHARMA_PRESCRIPTIONS_LOADED",
    ERROR_LOAD_PHARMA_PRESCRIPTIONS = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTIONS"
}

export class LoadPharmaPrescriptions implements Action {
    readonly type = ActionTypes.LOAD_PHARMA_PRESCRIPTIONS;
    constructor(public patientNiss : string, public page : number, public samlAssertion: string) { }
}

export class PharmaPrescriptionsLoaded implements Action {
    readonly type = ActionTypes.PHARMA_PRESCRIPTIONS_LOADED;
    constructor(public prescriptionIds: Array<string>) { }
}

export type ActionsUnion = LoadPharmaPrescriptions | PharmaPrescriptionsLoaded;