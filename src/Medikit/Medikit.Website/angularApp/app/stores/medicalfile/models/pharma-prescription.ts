import { Patient } from "@app/stores/patient/models/patient";
import { HealthcareWorker } from "./healthcare-worker";
import { PharmaDrugPrescription } from "./pharma-drug-prescription";

export class PharmaPrescription {
    constructor() {
        this.DrugsPrescribed = [];
    }

    Id: string;
    CreateDateTime: Date;
    EndExecutionDate: Date;
    Type: string;
    Prescriber: HealthcareWorker;
    DrugsPrescribed: PharmaDrugPrescription[];
    Patient: Patient;

    public static fromJson(json: any): PharmaPrescription {
        var result = new PharmaPrescription();
        result.Id = json["id"];
        result.CreateDateTime = json["create_datetime"];
        result.EndExecutionDate = json["end_execution_datetime"];
        result.Type = json["prescription_type"];
        if (json["patient"]) {
            result.Patient = Patient.fromJson(json["patient"]);
        }

        if (json["prescriber"]) {
            result.Prescriber = HealthcareWorker.fromJson(json["prescriber"]);
        }

        if (json["medications"]) {
            result.DrugsPrescribed = json["medications"].map(function (med: any) {
                return PharmaDrugPrescription.fromJson(med);
            });
        };

        return result;
    }
}