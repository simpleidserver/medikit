import * as fromActions from './patient-actions';
import { ListPatientState, PatientState } from './patient-state';

export const initialListState: ListPatientState = {
    content : null
};

export const initialListByNissState: ListPatientState = {
    content : null
};

export const initialGetState: PatientState = {
    content : null
};

export function ListPatientsReducer(state = initialListState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PATIENTS_LOADED:
            state.content = action.patients;
            return { ...state };
        default:
            return state;
    }
}

export function ListPatientsByNissReducer(state = initialListByNissState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PATIENTS_LOADED_BY_NISS:
            state.content = action.patients;
            return { ...state };
        default:
            return state;
    }
}

export function GetPatientReducer(state = initialGetState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PATIENT_LOADED:
            state.content = action.patient;
            return { ...state };
        default:
            return state;
    }
}