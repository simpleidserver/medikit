import { Translation } from "@app/infrastructure/Translation";
import { MedicinalPackage } from "./MedicinalPackage";
var MedicinalProduct = (function () {
    function MedicinalProduct() {
        this.Names = [];
        this.Packages = [];
    }
    MedicinalProduct.fromJson = function (json) {
        var result = new MedicinalProduct();
        result.Code = json["code"];
        json["names"].forEach(function (name) {
            result.Names.push(Translation.fromJson(name));
        });
        json["packages"].forEach(function (p) {
            result.Packages.push(MedicinalPackage.fromJson(p));
        });
        return result;
    };
    return MedicinalProduct;
}());
export { MedicinalProduct };
//# sourceMappingURL=MedicinalProduct.js.map