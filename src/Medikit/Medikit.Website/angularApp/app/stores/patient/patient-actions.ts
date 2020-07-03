import { Action } from '@ngrx/store';
import { SearchPatientResult } from './models/search-patient-result';
import { Patient } from './models/patient';

export enum ActionTypes {
    SEARCH_PATIENTS_BY_NISS = "[Patient] SEARCH_PATIENTS_BY_NISS",
    PATIENTS_LOADED_BY_NISS = "[Patient] PATIENTS_LOADED_BY_NISS",
    ERROR_SEARCH_PATIENTS_BY_NISS = "[Patient] ERROR_SEARCH_PATIENTS_BY_NISS",
    SEARCH_PATIENTS = "[Patient] SEARCH_PATIENTS",
    PATIENTS_LOADED = "[Patient] PATIENTS_LOADED",
    ERROR_SEARCH_PATIENTS = "[Patient] ERROR_SEARCH_PATIENTS",
    GET_PATIENT_BY_ID = "[Patient] GET_PATIENT_BY_ID",
    GET_PATIENT_BY_NISS = "[Patient] GET_PATIENT_BY_NISS",
    PATIENT_LOADED = "[Patient] PATIENT_LOADED",
    ERROR_GET_PATIENT = "[Patient] ERROR_GET_PATIENT",
    ADD_PATIENT = "[Patient] ADD_PATIENT",
    ADD_PATIENT_SUCCESS = "[Patient] ADD_PATIENT_SUCCESS",
    ADD_PATIENT_ERROR = "[Patient] ADD_PATIENT_ERROR"

}

export class SearchPatientsByNiss implements Action {
    readonly type = ActionTypes.SEARCH_PATIENTS_BY_NISS;
    constructor(public niss: string) { }
}

export class SearchPatients implements Action {
    readonly type = ActionTypes.SEARCH_PATIENTS;
    constructor(public niss: string, public firstname: string, public lastname: string, public startIndex : number, public count: number, public active : string = null, public direction: string = null) { }
}

export class GetPatientById implements Action {
    readonly type = ActionTypes.GET_PATIENT_BY_ID;
    constructor(public id: string) { }
}

export class GetPatientByNiss implements Action {
    readonly type = ActionTypes.GET_PATIENT_BY_NISS;
    constructor(public niss: string) { }
}

export class AddPatient implements Action {
    readonly type = ActionTypes.ADD_PATIENT;
    constructor(public patient: Patient) { }
}

export class PatientsLoaded implements Action {
    readonly type = ActionTypes.PATIENTS_LOADED;
    constructor(public patients: SearchPatientResult) { }
}

export class PatientLoaded implements Action {
    readonly type = ActionTypes.PATIENT_LOADED;
    constructor(public patient: Patient) { }
}

export class PatientsByNissLoaded implements Action {
    readonly type = ActionTypes.PATIENTS_LOADED_BY_NISS;
    constructor(public patients: SearchPatientResult) { }
}

export type ActionsUnion = SearchPatients | PatientsLoaded | GetPatientByNiss | PatientLoaded | SearchPatientsByNiss | PatientsByNissLoaded | AddPatient | GetPatientById;