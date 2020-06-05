import { createSelector } from '@ngrx/store';
import * as fromPatientReducers from './patient/patient-reducer';
import * as fromPharmaPrescriptionReducers from './pharmaprescription/prescription-reducer';
export var selectPatients = function (state) { return state.patients; };
export var selectPatient = function (state) { return state.patient; };
export var selectPharmaPrescriptions = function (state) { return state.pharmaPrescriptions; };
export var selectPharmaPrescription = function (state) { return state.pharmaPrescription; };
export var selectPatientsResult = createSelector(selectPatients, function (state) {
    if (!state || state.content == null) {
        return null;
    }
    return state.content;
});
export var selectPatientResult = createSelector(selectPatient, function (state) {
    if (!state || state.content == null) {
        return null;
    }
    return state.content;
});
export var selectPharmaPrescriptionListResult = createSelector(selectPharmaPrescriptions, function (state) {
    if (!state || state.prescriptionIds == null) {
        return null;
    }
    return state.prescriptionIds;
});
export var appReducer = {
    patients: fromPatientReducers.ListPatientsReducer,
    patient: fromPatientReducers.GetPatientReducer,
    pharmaPrescriptions: fromPharmaPrescriptionReducers.ListPharmaPrescriptionReducer
};
//# sourceMappingURL=appstate.js.map