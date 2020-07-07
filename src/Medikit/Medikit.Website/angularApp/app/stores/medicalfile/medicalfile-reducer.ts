import * as fromActions from './medicalfile-actions';
import { ListMedicalfileState } from './medicalfile-state';

export const initialListState: ListMedicalfileState = {
    content : null
};

export function ListMedicalfilesReducer(state = initialListState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.MEDICALFILES_LOADED:
            state.content = action.medicalfiles;
            return { ...state };
        default:
            return state;
    }
}