import * as fromActions from './prescription-actions';
import { PharmaPrescriptionListState, PharmaPrescriptionState } from './prescription-state';

export const initialListState: PharmaPrescriptionListState = {
    prescriptionIds: []
};

export const initialState: PharmaPrescriptionState = {
    prescription: null
};

export function ListPharmaPrescriptionReducer(state = initialListState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PHARMA_PRESCRIPTIONS_LOADED:
            state.prescriptionIds = action.prescriptionIds;
            return { ...state };
        default:
            return state;
    }
}

export function ViewPharmaPrescriptionReducer(state = initialState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PHARMA_PRESCRIPTION_LOADED:
            state.prescription = action.prescription;
            return { ...state };
        default:
            return state;
    }
}