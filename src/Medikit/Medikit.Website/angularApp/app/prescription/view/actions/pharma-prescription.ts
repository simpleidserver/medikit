import { Action } from '@ngrx/store';
import { PharmaPrescription } from '@app/prescription/models/pharma-prescription';

export enum ActionTypes {
    LOAD_PHARMA_PRESCRIPTION = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTION",
    PHARMA_PRESCRIPTION_LOADED = "[PharmaPrescription] PHARMA_PRESCRIPTION_LOADED",
    ERROR_LOAD_PHARMA_PRESCRIPTION = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTION"
}

export class LoadPharmaPrescription implements Action {
    readonly type = ActionTypes.LOAD_PHARMA_PRESCRIPTION;
    constructor(public prescriptionId : string, public samlAssertion : string) { }
}

export class PharmaPrescriptionLoaded implements Action {
    readonly type = ActionTypes.PHARMA_PRESCRIPTION_LOADED;
    constructor(public prescription: PharmaPrescription) { }
}

export type ActionsUnion = LoadPharmaPrescription | PharmaPrescriptionLoaded;