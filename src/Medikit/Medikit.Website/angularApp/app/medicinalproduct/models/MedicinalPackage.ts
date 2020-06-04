import { Translation } from "@app/infrastructure/Translation";
import { MedicinalDeliveryMethod } from "./MedicinalDeliveryMethod";

export class MedicinalPackage {
    constructor() {
        this.DeliveryMethods = [];
        this.PrescriptionNames = [];
    }

    DeliveryMethods: MedicinalDeliveryMethod[];
    PrescriptionNames: Translation[];

    public static fromJson(json: any): MedicinalPackage {
        var result = new MedicinalPackage();
        if (json["delivery_methods"]) {
            json["delivery_methods"].forEach(function (dm: any) {
                result.DeliveryMethods.push(MedicinalDeliveryMethod.fromJson(dm));
            });
        }

        if (json["prescription_names"]) {
            json["prescription_names"].forEach(function (pn: any) {
                result.PrescriptionNames.push(Translation.fromJson(pn));
            });
        }

        return result;
    }
}