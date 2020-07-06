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
import { Patient } from '../../../stores/patient/models/patient';
import * as fromActions from '../actions/patient';
var sessionStorageKey = 'patient';
var persist = function (state) {
    sessionStorage.setItem(sessionStorageKey, JSON.stringify(state));
};
var getInitialState = function () {
    var json = sessionStorage.getItem(sessionStorageKey);
    if (!json) {
        return {
            patient: new Patient(),
            stepperIndex: 0
        };
    }
    return JSON.parse(json);
};
export var initialState = getInitialState();
export function PatientFormReducer(state, action) {
    if (state === void 0) { state = initialState; }
    switch (action.type) {
        case fromActions.ActionTypes.LOAD_PATIENT:
            var loaded = getInitialState();
            state.stepperIndex = loaded.stepperIndex;
            state.patient = loaded.patient;
            return __assign({}, state);
        case fromActions.ActionTypes.UPDATE_INFORMATION:
            state.patient.birthdate = action.birthdate;
            state.patient.eidCardNumber = action.eidCardNumber;
            state.patient.eidCardValidity = action.eidCardValidity;
            state.patient.firstname = action.firstname;
            state.patient.lastname = action.lastname;
            state.patient.base64Image = action.base64Image;
            state.patient.niss = action.niss;
            state.patient.gender = action.gender;
            state.stepperIndex += 1;
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.UPDATE_ADDRESS:
            state.patient.address = action.address;
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.ADD_CONTACT_INFO:
            state.patient.contactInformations.push(action.contactInfo);
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.DELETE_CONTACT_INFO:
            state.patient.contactInformations = state.patient.contactInformations.filter(function (_) {
                if (action.contactInfos.filter(function (__) {
                    return _.id == __;
                }).length > 0) {
                    return false;
                }
                return true;
            });
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.NEXT_STEP:
            state.stepperIndex += 1;
            persist(state);
            return __assign({}, state);
        case fromActions.ActionTypes.PREVIOUS_STEP:
            state.stepperIndex -= 1;
            persist(state);
            return __assign({}, state);
        default:
            return state;
    }
}
//# sourceMappingURL=patient-reducer.js.map