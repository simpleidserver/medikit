import { PharmaPosologyFreeText } from "./pharma-posology";
import { Translation } from "@app/infrastructure/Translation";
var PharmaDrugPrescription = (function () {
    function PharmaDrugPrescription() {
        this.PackageNames = [];
    }
    PharmaDrugPrescription.fromJson = function (json) {
        var result = new PharmaDrugPrescription();
        result.InstructionForPatient = json["instruction_for_patient"];
        result.InstructionForReimbursement = json["instruction_for_reimbursement"];
        result.PackageCode = json["package"]["code"];
        result.PackageNames = json["package"]["translations"].map(function (translation) {
            return Translation.fromJson(translation);
        });
        if (json["posology"]) {
            var posology = json["posology"];
            if (posology["type"] === "freetext") {
                result.Posology = PharmaPosologyFreeText.fromJson(posology);
            }
        }
        return result;
    };
    return PharmaDrugPrescription;
}());
export { PharmaDrugPrescription };
//# sourceMappingURL=pharma-drug-prescription.js.map