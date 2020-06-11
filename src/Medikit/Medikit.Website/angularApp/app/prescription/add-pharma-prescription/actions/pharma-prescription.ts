import { Action } from '@ngrx/store';
import { PharmaDrugPrescription } from '@app/stores/pharmaprescription/models/pharma-drug-prescription';
import { Patient } from '@app/stores/patient/models/patient';

export enum ActionTypes {
    LOAD_PHARMA_PRESCRIPTION = "[AddPharmaPrescription] LOAD_ADD_PRESCRIPTION",
    NEXT_STEP = "[AddPharmaPrescription] NEXT_STEP",
    SELECT_PATIENT = "[AddPharmaPrescription] SELECT_PATIENT",
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

export class SelectPatient implements Action {
    readonly type = ActionTypes.SELECT_PATIENT;
    constructor(public patient : Patient) { }
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

export type ActionsUnion = LoadPrescription | NextStep | PreviousStep | AddDrugPrescription | DeleteDrugPrescription | SelectPatient;