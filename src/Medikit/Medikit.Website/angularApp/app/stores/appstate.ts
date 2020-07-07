import { createSelector } from '@ngrx/store';
import * as fromPatientReducers from './patient/patient-reducer';
import * as fromPatientStates from './patient/patient-state';
import * as fromPharmaPrescriptionReducers from './pharmaprescription/prescription-reducer';
import * as fromPharmaPrescriptionStates from './pharmaprescription/prescription-state';
import * as fromMedicalfileReducers from './medicalfile/medicalfile-reducer';
import * as fromMedicalfileStates from './medicalfile/medicalfile-state';

export interface AppState {
    patients: fromPatientStates.ListPatientState,
    patientsByNiss: fromPatientStates.ListPatientState,
    patient: fromPatientStates.PatientState,
    pharmaPrescriptions: fromPharmaPrescriptionStates.PharmaPrescriptionListState,
    pharmaPrescription: fromPharmaPrescriptionStates.PharmaPrescriptionState,
    medicalfiles: fromMedicalfileStates.ListMedicalfileState
}

export const selectPatients = (state: AppState) => state.patients;
export const selectPatientsByNiss = (state: AppState) => state.patientsByNiss;
export const selectPatient = (state: AppState) => state.patient;
export const selectPharmaPrescriptions = (state: AppState) => state.pharmaPrescriptions;
export const selectPharmaPrescription = (state: AppState) => state.pharmaPrescription;
export const selectMedicalfiles = (state: AppState) => state.medicalfiles;

export const selectPatientsResult = createSelector(
    selectPatients,
    (state: fromPatientStates.ListPatientState) => {
        if (!state || state.content == null) {
            return null;
        }

        return state.content;
    }
);

export const selectPatientsByNissResult = createSelector(
    selectPatientsByNiss,
    (state: fromPatientStates.ListPatientState) => {
        if (!state || state.content == null) {
            return null;
        }

        return state.content;
    }
);

export const selectPatientResult = createSelector(
    selectPatient,
    (state: fromPatientStates.PatientState) => {
        if (!state || state.content == null) {
            return null;
        }

        return state.content;
    }
);

export const selectPharmaPrescriptionListResult = createSelector(
    selectPharmaPrescriptions,
    (state: fromPharmaPrescriptionStates.PharmaPrescriptionListState) => {
        if (!state || state.prescriptionIds == null) {
            return null;
        }

        return state.prescriptionIds;
    }
);

export const selectPharmaPrescriptionResult = createSelector(
    selectPharmaPrescription,
    (state: fromPharmaPrescriptionStates.PharmaPrescriptionState) => {
        if (!state || state.prescription == null) {
            return null;
        }

        return state.prescription;
    }
);

export const selectMedicalfilesResult = createSelector(
    selectMedicalfiles,
    (state: fromMedicalfileStates.ListMedicalfileState) => {
        if (!state || state.content == null) {
            return null;
        }

        return state.content;
    }
);

export const appReducer = {
    patients: fromPatientReducers.ListPatientsReducer,
    patientsByNiss: fromPatientReducers.ListPatientsByNissReducer,
    patient: fromPatientReducers.GetPatientReducer,
    pharmaPrescriptions: fromPharmaPrescriptionReducers.ListPharmaPrescriptionReducer,
    pharmaPrescription: fromPharmaPrescriptionReducers.ViewPharmaPrescriptionReducer,
    medicalfiles: fromMedicalfileReducers.ListMedicalfilesReducer
};