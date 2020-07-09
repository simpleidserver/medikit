import { PharmaPrescription } from "@app/stores/medicalfile/models/pharma-prescription";

export interface AddPharmaPrescriptionFormState {
    prescription: PharmaPrescription;
    stepperIndex: number;
    nextPatientFormBtnDisabled: boolean;
}