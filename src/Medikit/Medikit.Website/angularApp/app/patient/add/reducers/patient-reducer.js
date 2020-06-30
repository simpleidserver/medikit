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
import { PharmaPrescription } from "@app/prescription/models/pharma-prescription";
import * as fromActions from '../actions/pharma-prescription';
var sessionStorageKey = 'pharma-prescription';
var persist = function (state) {
    sessionStorage.setItem(sessionStorageKey, JSON.stringify(state));
};
var getInitialState = function () {
    var json = sessionStorage.getItem(sessionStorageKey);
    if (!json) {
        return {
            prescription: new PharmaPrescription(),
            stepperIndex: 0,
            nextPatientFormBtnDisabled: true
        };
    }
    return JSON.parse(json);
};
export var initialState = getInitialState();
export function PharmaPrescriptionFormReducer(state, action) {
    if (state === void 0) { state = initialState; }
    switch (action.type) {
        case fromActions.ActionTypes.LOAD_PHARMA_PRESCRIPTION:
            var loaded = getInitialState();
            state.stepperIndex = loaded.stepperIndex;
            state.prescription = loaded.prescription;
            state.nextPatientFormBtnDisabled = loaded.nextPatientFormBtnDisabled;
            return __assign({}, state);
        case fromActions.ActionTypes.NEXT_STEP:
            state.stepperIndex += 1;
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.PREVIOUS_STEP:
            state.stepperIndex -= 1;
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.ADD_DRUG_PRESCRIPTION:
            state.prescription.DrugsPrescribed.push(action.drugPrescription);
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.DELETE_DRUG_PRESCRIPTION:
            state.prescription.DrugsPrescribed = state.prescription.DrugsPrescribed.filter(function (dp) {
                return dp.TechnicalId != action.technicalId;
            });
            persist(state);
            return __assign({}, state);
        default:
            return state;
    }
}
//# sourceMappingURL=pharma-prescription-reducer.js.map