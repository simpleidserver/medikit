import { PharmaDrugPrescription } from '@app/stores/medicalfile/models/pharma-drug-prescription';
import { Action } from '@ngrx/store';

export enum ActionTypes {
    LOAD_PHARMA_PRESCRIPTION = "[AddPharmaPrescription] LOAD_ADD_PRESCRIPTION",
    NEXT_STEP = "[AddPharmaPrescription] NEXT_STEP",
    PREVIOUS_STEP = "[AddPharmaPrescription] PREVIOUS_STEP",
    ADD_DRUG_PRESCRIPTION = "[AddPharmaPrescription] ADD_DRUG_PRESCRIPTION",
    DELETE_DRUG_PRESCRIPTION = "[DeletePharmaPrescription] DELETE_DRUG_PRESCRIPTION"
}

export class LoadPrescription implements Action {
    readonly type = ActionTypes.LOAD_PHARMA_PRESCRIPTION;
    constructor() { }
}

export class NextStep implements Action {
    readonly type = ActionTypes.NEXT_STEP;
    constructor() { }
}

export class PreviousStep implements Action {
    readonly type = ActionTypes.PREVIOUS_STEP;
    constructor() { }
}

export class AddDrugPrescription implements Action {
    readonly type = ActionTypes.ADD_DRUG_PRESCRIPTION;
    constructor(public drugPrescription: PharmaDrugPrescription) { }
}

export class DeleteDrugPrescription implements Action {
    readonly type = ActionTypes.DELETE_DRUG_PRESCRIPTION
    constructor(public technicalId: string) { }
}

export type ActionsUnion = LoadPrescription | NextStep | PreviousStep | AddDrugPrescription | DeleteDrugPrescription;