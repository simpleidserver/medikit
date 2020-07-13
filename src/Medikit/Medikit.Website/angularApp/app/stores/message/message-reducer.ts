import * as fromActions from './message-actions';
import { ListMessageState } from './message-state';

export const initialListState: ListMessageState = {
    content : null
};

export function ListMessageReducer(state = initialListState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.MESSAGES_LOADED:
            state.content = action.messages;
            return { ...state };
        default:
            return state;
    }
}