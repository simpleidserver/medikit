import { Action } from '@ngrx/store';
import { Medicalfile } from './models/medicalfile';
import { PharmaPrescription } from './models/pharma-prescription';
import { SearchMedicalfileResult } from './models/search-medicalfile';
import { SearchPrescriptionResult } from './models/search-prescription-result';

export enum ActionTypes {
    SEARCH_MEDICALFILES = "[Medicalfile] SEARCH_MEDICALFILES",
    MEDICALFILES_LOADED = "[Medicalfile] MEDICALFILES_LOADED",
    ERROR_SEARCH_MEDICALFILES = "[Medicalfile] ERROR_SEARCH_MEDICALFILES",
    ADD_MEDICALFILE = "[Medicalfile] ADD_MEDICALFILE",
    MEDICALFILE_ADDED = "[Medicalfile] MEDICALFILE_ADDED",
    ERROR_ADD_MEDICALFILE = "[Medicalfile] ERROR_ADD_MEDICALFILE",
    GET_MEDICALFILE = "[Medicalfile] GET_MEDICALFILE",
    MEDICALFILE_LOADED = "[Medicalfile] MEDICALFILE_LOADED",
    ERROR_LOAD_MEDICALFILE = "[Medicalfile] ERROR_LOAD_MEDICALFILE",
    SEARCH_PRESCRIPTIONS = "[Medicalfile] SEARCH_PRESCRIPTIONS",
    PRESCRIPTIONS_LOADED = "[Medicalfile] PRESCRIPTIONS_LOADED",
    ERROR_SEARCH_PRESCRIPTIONS = "[Medicalfile] ERROR_SEARCH_PRESCRIPTIONS",
    REVOKE_PRESCRIPTION = "[Medicalfile] REVOKE_PRESCRIPTION",
    PRESCRIPTION_REVOKED = "[Medicalfile] PRESCRIPTION_REVOKED",
    ERROR_REVOKE_PRESCRIPTION = "[Medicalfile] ERROR_REVOKE_PRESCRIPTION",
    ADD_PRESCRIPTION = "[Medicalfile] ADD_PRESCRIPTION",
    PRESCRIPTION_ADDED = "[Medicalfile] PRESCRIPTION_ADDED",
    ERROR_ADD_PRESCRIPTION = "[Medicalfile] ERROR_ADD_PRESCRIPTION",
    GET_PRESCRIPTION = "[Medicalfile] GET_PRESCRIPTION",
    PRESCRIPTION_LOADED = "[Medicalfile] PRESCRIPTION_LOADED",
    ERROR_GET_PRESCRIPTION = "[Medicalfile] ERROR_GET_PRESCRIPTION"
}

export class SearchMedicalfiles implements Action {
    readonly type = ActionTypes.SEARCH_MEDICALFILES;
    constructor(public niss: string, public firstname: string, public lastname: string, public startIndex: number, public count: number, public active: string = null, public direction: string = null) { }
}

export class MedicalfilesLoaded implements Action {
    readonly type = ActionTypes.MEDICALFILES_LOADED;
    constructor(public medicalfiles: SearchMedicalfileResult) { }
}

export class AddMedicalfile implements Action {
    readonly type = ActionTypes.ADD_MEDICALFILE;
    constructor(public patientId: string) { }
}

export class MedicalfileAdded implements Action {
    readonly type = ActionTypes.MEDICALFILE_ADDED;
    constructor(public medicalfile: Medicalfile) { }
}

export class GetMedicalfile implements Action {
    readonly type = ActionTypes.GET_MEDICALFILE;
    constructor(public medicalfileId: string) { }
}

export class MedicalfileLoaded implements Action {
    readonly type = ActionTypes.MEDICALFILE_LOADED;
    constructor(public medicalfile: Medicalfile) { }
}

export class SearchPrescriptions implements Action {
    readonly type = ActionTypes.SEARCH_PRESCRIPTIONS;
    constructor(public medicalfileid : string, public page: number, public isOpened: boolean, public samlAssertion : string) { }
}

export class PrescriptionsLoaded implements Action {
    readonly type = ActionTypes.PRESCRIPTIONS_LOADED;
    constructor(public prescriptions: SearchPrescriptionResult) { }
}

export class RevokePrescription implements Action {
    readonly type = ActionTypes.REVOKE_PRESCRIPTION;
    constructor(public medicalfileId: string, public rid: string, public reason: string, public samlAssertion : string) { }
}

export class AddPrescription implements Action {
    readonly type = ActionTypes.ADD_PRESCRIPTION;
    constructor(public medicalfileId : string, public prescription: PharmaPrescription, public samlAssertion: string) { }
}

export class GetPrescription implements Action {
    readonly type = ActionTypes.GET_PRESCRIPTION;
    constructor(public medicalfileId: string, public rid: string, public samlAssertion: string) { }
}

export class PrescriptionLoaded implements Action {
    readonly type = ActionTypes.PRESCRIPTION_LOADED;
    constructor(public prescription: PharmaPrescription) { }

}

export type ActionsUnion =
    SearchMedicalfiles |
    MedicalfilesLoaded |
    AddMedicalfile |
    MedicalfileAdded |
    GetMedicalfile |
    MedicalfileLoaded |
    SearchPrescriptions |
    PrescriptionsLoaded |
    RevokePrescription |
    AddPrescription |
    GetPrescription |
    PrescriptionLoaded;