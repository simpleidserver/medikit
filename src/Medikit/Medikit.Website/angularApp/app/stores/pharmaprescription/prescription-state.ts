import { PharmaPrescription } from "./models/pharma-prescription";

export interface PharmaPrescriptionListState {
    prescriptionIds: Array<string>;
}

export interface PharmaPrescriptionState {
    prescription: PharmaPrescription;
}