import { Action } from '@ngrx/store';
import { Address } from '@app/infrastructure/services/address.service';
import { ContactInformation } from '@app/stores/patient/models/contact-information';

export enum ActionTypes {
    LOAD_PATIENT = "[AddPatient] LOAD_PATIENT",
    UPDATE_INFORMATION = "[AddPatient] UPDATE_INFORMATION",
    UPDATE_ADDRESS = "[AddPatient] UPDATE_ADDRESS",
    ADD_CONTACT_INFORMATON = "[AddPatient] ADD_CONTACT_INFORMATION",
    DELETE_CONTACT_INFORMATION = "[AddPatient] DELETE_CONTACT_INFORMATION",
    NEXT_STEP = "[AddPatient] NEXT_STEP",
    PREVIOUS_STEP = "[AddPatient] PREVIOUS_STEP",
    ADD_ADDRESS = "[AddPatient] ADD_ADDRESS",
    ADD_CONTACT_INFO = "[AddPatient] ADD_CONTACT_INFO",
    DELETE_CONTACT_INFO = "[AddPatient] DELETE_CONTACT_INFO"
}

export class LoadPatient implements Action {
    readonly type = ActionTypes.LOAD_PATIENT;
    constructor() { }
}

export class UpdateInformation implements Action {
    readonly type = ActionTypes.UPDATE_INFORMATION;
    constructor(public firstname: string, public lastname: string, public gender: number, public niss: string, public birthdate: Date, public eidCardNumber: string, public eidCardValidity: Date, public base64Image: string) { }
}

export class UpdateAddress implements Action {
    readonly type = ActionTypes.UPDATE_ADDRESS;
    constructor(public address: Address) { }
}

export class AddContactInformation implements Action {
    readonly type = ActionTypes.ADD_CONTACT_INFO;
    constructor(public contactInfo: ContactInformation) { }
}

export class DeleteContactInformation implements Action {
    readonly type = ActionTypes.DELETE_CONTACT_INFO;
    constructor(public contactInfos : string[]) { }
}

export class NextStep implements Action {
    readonly type = ActionTypes.NEXT_STEP;
    constructor() { }
}

export class PreviousStep implements Action {
    readonly type = ActionTypes.PREVIOUS_STEP;
    constructor() { }
}

export type ActionsUnion = LoadPatient | NextStep | PreviousStep | UpdateInformation | UpdateAddress | AddContactInformation | DeleteContactInformation;