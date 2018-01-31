import { Patient } from '@app/patient/models/patient';
import { Action } from '@ngrx/store';
import { PharmaDrugPrescription } from '@app/prescription/models/pharma-drug-prescription';

export enum ActionTypes {
    LOAD_PHARMA_PRESCRIPTION = "[AddPharmaPrescription] LOAD_ADD_PRESCRIPTION",
    CHECK_NISS = "[AddPharmaPrescription] CHECK_NISS",
    NISS_CHECKED = "[AddPharmaPrescription] NISS_CHECKED",
    NISS_UNKNOWN = "[AddPharmaPrescription] NISS_UNNOWN", 
    NEXT_STEP = "[AddPharmaPrescription] NEXT_STEP",
    PREVIOUS_STEP = "[AddPharmaPrescription] PREVIOUS_STEP",
    ADD_DRUG_PRESCRIPTION = "[AddPharmaPrescription] ADD_DRUG_PRESCRIPTION",
    DELETE_DRUG_PRESCRIPTION = "[DeletePharmaPrescription] DELETE_DRUG_PRESCRIPTION"
}

export class LoadPrescription implements Action {
    readonly type = ActionTypes.LOAD_PHARMA_PRESCRIPTION;
    constructor() { }
}

export class CheckNiss implements Action {
    readonly type = ActionTypes.CHECK_NISS;
    constructor(public niss: string) { }
}

export class NissChecked implements Action {
    readonly type = ActionTypes.NISS_CHECKED;
    constructor(public patient: Patient) { }
}

export class NissUnknown implements Action {
    readonly type = ActionTypes.NISS_UNKNOWN;
    constructor(public niss : string) { }
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

export type ActionsUnion = LoadPrescription | CheckNiss | NissChecked | NissUnknown | NextStep | PreviousStep | AddDrugPrescription | DeleteDrugPrescription;