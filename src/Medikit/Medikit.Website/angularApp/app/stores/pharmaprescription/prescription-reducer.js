var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
import * as fromActions from './prescription-actions';
export var initialListState = {
    prescriptionIds: []
};
export var initialState = {
    prescription: null
};
export function ListPharmaPrescriptionReducer(state, action) {
    if (state === void 0) { state = initialListState; }
    switch (action.type) {
        case fromActions.ActionTypes.PHARMA_PRESCRIPTIONS_LOADED:
            state.prescriptionIds = action.prescriptionIds;
            return __assign({}, state);
        case fromActions.ActionTypes.REVOKE_PHARMA_PRESCRIPTION_SUCCESS:
            state.prescriptionIds = state.prescriptionIds.filter(function (_) { return _ != action.rid; });
            return __assign({}, state);
        default:
            return state;
    }
}
export function ViewPharmaPrescriptionReducer(state, action) {
    if (state === void 0) { state = initialState; }
    switch (action.type) {
        case fromActions.ActionTypes.PHARMA_PRESCRIPTION_LOADED:
            state.prescription = action.prescription;
            return __assign({}, state);
        default:
            return state;
    }
}
//# sourceMappingURL=prescription-reducer.js.map