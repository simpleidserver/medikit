import { PharmaPosology, PharmaPosologyFreeText } from "./pharma-posology";
import { PharmaDuration } from "./pharma-duration";
import { Translation } from "@app/infrastructure/Translation";

export class PharmaDrugPrescription {
    constructor() {
        this.PackageNames = [];
    }

    TechnicalId: string;
    PackageCode: string;
    PackageNames: Translation[];
    Posology: PharmaPosology;
    Duration: PharmaDuration;
    InstructionForPatient: string;
    InstructionForReimbursement: string;
    Date: Date;
    EndExecutionDate: Date;

    public static fromJson(json: any) {
        var result = new PharmaDrugPrescription();
        result.InstructionForPatient = json["instruction_for_patient"];
        result.InstructionForReimbursement = json["instruction_for_reimbursement"];
        result.PackageCode = json["package"]["code"];
        result.PackageNames = json["package"]["translations"].map(function (translation: any) {
            return Translation.fromJson(translation);
        });
        if (json["posology"]) {
            var posology = json["posology"];
            if (posology["type"] === "freetext") {
                result.Posology = PharmaPosologyFreeText.fromJson(posology);
            }
        }

        return result;
    }
}