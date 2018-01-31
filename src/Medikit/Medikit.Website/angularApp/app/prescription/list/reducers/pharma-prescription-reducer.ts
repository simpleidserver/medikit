import * as fromActions from '../actions/pharma-prescription';
import { PharmaPrescriptionsState } from "../states/pharma-prescription-state";

export const initialState: PharmaPrescriptionsState = {
    prescriptionIds: []
};

export function ListPharmaPrescriptionReducer(state = initialState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.PHARMA_PRESCRIPTIONS_LOADED:
            state.prescriptionIds = action.prescriptionIds;
            return { ...state };
        default:
            return state;
    }
}