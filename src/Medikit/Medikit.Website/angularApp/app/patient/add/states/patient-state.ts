import { Patient } from "@app/stores/patient/models/patient";

export interface AddPatientFormState {
    patient: Patient;
    stepperIndex: number;
}