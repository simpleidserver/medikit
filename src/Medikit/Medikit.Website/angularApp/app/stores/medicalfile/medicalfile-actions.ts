import { Action } from '@ngrx/store';
import { Medicalfile } from './models/medicalfile';
import { SearchMedicalfileResult } from './models/search-medicalfile';

export enum ActionTypes {
    SEARCH_MEDICALFILES = "[Medicalfile] SEARCH_MEDICALFILES",
    MEDICALFILES_LOADED = "[Medicalfile] MEDICALFILES_LOADED",
    ERROR_SEARCH_MEDICALFILES = "[Medicalfile] ERROR_SEARCH_MEDICALFILES",
    ADD_MEDICALFILE = "[Medicalfile] ADD_MEDICALFILE",
    MEDICALFILE_ADDED = "[Medicalfile] MEDICALFILE_ADDED",
    ERROR_ADD_MEDICALFILE = "[Medicalfile] ERROR_ADD_MEDICALFILE"
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

export type ActionsUnion = SearchMedicalfiles | MedicalfilesLoaded | AddMedicalfile | MedicalfileAdded;