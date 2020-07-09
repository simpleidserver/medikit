import * as fromActions from './medicalfile-actions';
import { ListMedicalfileState, ListPrescriptionState, MedicalfileState, PrescriptionState } from './medicalfile-state';

export const initialMedicalfileListState: ListMedicalfileState = {
    content : null
};

export const initalMedicalfileState: MedicalfileState = {
    content : null
};

export const initialPrescriptionListState: ListPrescriptionState = {
    content : null
};

export const initialPrescriptionState: PrescriptionState = {
    content: null
};

export function ListMedicalfilesReducer(state = initialMedicalfileListState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.MEDICALFILES_LOADED:
            state.content = action.medicalfiles;
            return { ...state };
        default:
            return state;
    }
}

export function MedicalfileReducer(state = initalMedicalfileState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.MEDICALFILE_LOADED:
            state.content = action.medicalfile;
            return { ...state };
        default:
            return state;
    }
}

export function ListPrescriptionReducer(state = initialPrescriptionListState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PRESCRIPTIONS_LOADED:
            state.content = action.prescriptions;
            return { ...state };
        default:
            return state;
    }
}

export function PrescriptionReducer(state = initialPrescriptionState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PRESCRIPTION_LOADED:
            state.content = action.prescription;
            return { ...state };
        default:
            return state;
    }
}