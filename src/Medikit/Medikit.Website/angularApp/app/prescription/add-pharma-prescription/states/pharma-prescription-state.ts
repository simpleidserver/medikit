import { PharmaPrescription } from "@app/prescription/models/pharma-prescription";

export interface AddPharmaPrescriptionFormState {
    prescription: PharmaPrescription;
    stepperIndex: number;
    nextPatientFormBtnDisabled: boolean;
}