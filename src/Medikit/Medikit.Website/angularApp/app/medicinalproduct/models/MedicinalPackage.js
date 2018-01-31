import { Translation } from "@app/Translation";
import { MedicinalDeliveryMethod } from "./MedicinalDeliveryMethod";
var MedicinalPackage = (function () {
    function MedicinalPackage() {
        this.DeliveryMethods = [];
        this.PrescriptionNames = [];
    }
    MedicinalPackage.fromJson = function (json) {
        var result = new MedicinalPackage();
        if (json["delivery_methods"]) {
            json["delivery_methods"].forEach(function (dm) {
                result.DeliveryMethods.push(MedicinalDeliveryMethod.fromJson(dm));
            });
        }
        if (json["prescription_names"]) {
            json["prescription_names"].forEach(function (pn) {
                result.PrescriptionNames.push(Translation.fromJson(pn));
            });
        }
        return result;
    };
    return MedicinalPackage;
}());
export { MedicinalPackage };
//# sourceMappingURL=MedicinalPackage.js.map