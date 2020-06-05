import { SearchPatientResult } from './models/search-patient-result';
import { Patient } from './models/patient';

export interface ListPatientState {
    content: SearchPatientResult
}

export interface PatientState {
    content: Patient
}