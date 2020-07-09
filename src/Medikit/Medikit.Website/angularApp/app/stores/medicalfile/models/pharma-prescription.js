import { Patient } from "@app/stores/patient/models/patient";
import { HealthcareWorker } from "./healthcare-worker";
import { PharmaDrugPrescription } from "./pharma-drug-prescription";
var PharmaPrescription = (function () {
    function PharmaPrescription() {
        this.DrugsPrescribed = [];
    }
    PharmaPrescription.fromJson = function (json) {
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
            result.DrugsPrescribed = json["medications"].map(function (med) {
                return PharmaDrugPrescription.fromJson(med);
            });
        }
        ;
        return result;
    };
    return PharmaPrescription;
}());
export { PharmaPrescription };
//# sourceMappingURL=pharma-prescription.js.map