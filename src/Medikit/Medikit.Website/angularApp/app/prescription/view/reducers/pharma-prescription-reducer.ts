import * as fromActions from '../actions/pharma-prescription';
import { PharmaPrescriptionState } from '../states/pharma-prescription-state';

export const initialState: PharmaPrescriptionState = {
    prescription : null
};

export function ViewPharmaPrescriptionReducer(state = initialState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PHARMA_PRESCRIPTION_LOADED:
            state.prescription = action.prescription;
            return { ...state };
        default:
            return state;
    }
}