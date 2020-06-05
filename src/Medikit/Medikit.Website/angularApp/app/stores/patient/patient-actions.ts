import { Action } from '@ngrx/store';
import { SearchPatientResult } from './models/search-patient-result';
import { Patient } from './models/patient';

export enum ActionTypes {
    SEARCH_PATIENTS = "[Patient] SEARCH_PATIENTS",
    PATIENTS_LOADED = "[Patient] PATIENTS_LOADED",
    ERROR_SEARCH_PATIENTS = "[Patient] ERROR_SEARCH_PATIENTS",
    GET_PATIENT = "[Patient] GET_PATIENT",
    PATIENT_LOADED = "[Patient] PATIENT_LOADED",
    ERROR_GET_PATIENT = "[Patient] ERROR_GET_PATIENT"
}

export class SearchPatients implements Action {
    readonly type = ActionTypes.SEARCH_PATIENTS;
    constructor(public firstName : string, public lastName : string, public niss: string) { }
}

export class GetPatient implements Action {
    readonly type = ActionTypes.GET_PATIENT;
    constructor(public niss: string) { }
}

export class PatientsLoaded implements Action {
    readonly type = ActionTypes.PATIENTS_LOADED;
    constructor(public patients: SearchPatientResult) { }
}

export class PatientLoaded implements Action {
    readonly type = ActionTypes.PATIENT_LOADED;
    constructor(public patient: Patient) { }
}

export type ActionsUnion = SearchPatients | PatientsLoaded | GetPatient | PatientLoaded;