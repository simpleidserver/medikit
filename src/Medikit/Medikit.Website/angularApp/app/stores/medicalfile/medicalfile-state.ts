import { Medicalfile } from './models/medicalfile';
import { PharmaPrescription } from './models/pharma-prescription';
import { SearchMedicalfileResult } from './models/search-medicalfile';
import { SearchPrescriptionResult } from './models/search-prescription-result';

export interface ListMedicalfileState {
    content: SearchMedicalfileResult
}

export interface MedicalfileState {
    content: Medicalfile
}

export interface ListPrescriptionState {
    content: SearchPrescriptionResult
}

export interface PrescriptionState {
    content: PharmaPrescription;
}