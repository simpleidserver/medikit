import { PharmaPrescription } from "@app/stores/pharmaprescription/models/pharma-prescription";

export interface AddPharmaPrescriptionFormState {
    prescription: PharmaPrescription;
    stepperIndex: number;
    nextPatientFormBtnDisabled: boolean;
}