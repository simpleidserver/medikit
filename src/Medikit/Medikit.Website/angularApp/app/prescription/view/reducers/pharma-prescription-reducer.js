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
import * as fromActions from '../actions/pharma-prescription';
export var initialState = {
    prescription: null
};
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
//# sourceMappingURL=pharma-prescription-reducer.js.map