import { createSelector } from '@ngrx/store';
import * as fromMedicalfileReducers from './medicalfile/medicalfile-reducer';
import * as fromMedicalfileStates from './medicalfile/medicalfile-state';
import * as fromPatientReducers from './patient/patient-reducer';
import * as fromPatientStates from './patient/patient-state';

export interface AppState {
    patients: fromPatientStates.ListPatientState,
    patientsByNiss: fromPatientStates.ListPatientState,
    patient: fromPatientStates.PatientState,
    medicalfiles: fromMedicalfileStates.ListMedicalfileState,
    medicalfile: fromMedicalfileStates.MedicalfileState,
    prescriptions: fromMedicalfileStates.ListPrescriptionState,
    prescription: fromMedicalfileStates.PrescriptionState
}

export const selectPatients = (state: AppState) => state.patients;
export const selectPatientsByNiss = (state: AppState) => state.patientsByNiss;
export const selectPatient = (state: AppState) => state.patient;
export const selectPrescriptions = (state: AppState) => state.prescriptions;
export const selectPrescription = (state: AppState) => state.prescription;
export const selectMedicalfiles = (state: AppState) => state.medicalfiles;
export const selectMedicalfile = (state: AppState) => state.medicalfile;

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
    selectPrescriptions,
    (state: fromMedicalfileStates.ListPrescriptionState) => {
        if (!state || state.content == null) {
            return null;
        }

        return state.content;
    }
);

export const selectPharmaPrescriptionResult = createSelector(
    selectPrescription,
    (state: fromMedicalfileStates.PrescriptionState) => {
        if (!state || state.content == null) {
            return null;
        }

        return state.content;
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

export const selectMedicalfileResult = createSelector(
    selectMedicalfile,
    (state: fromMedicalfileStates.MedicalfileState) => {
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
    prescriptions: fromMedicalfileReducers.ListPrescriptionReducer,
    prescription: fromMedicalfileReducers.PrescriptionReducer,
    medicalfiles: fromMedicalfileReducers.ListMedicalfilesReducer,
    medicalfile: fromMedicalfileReducers.MedicalfileReducer
};