import { Translation } from "@app/infrastructure/Translation";
var MedicinalPackage = (function () {
    function MedicinalPackage() {
        this.Names = [];
        this.Leafleturl = [];
        this.Crmurl = [];
        this.Spcurl = [];
    }
    MedicinalPackage.fromJson = function (json) {
        var result = new MedicinalPackage();
        result.Code = json['code'];
        result.Price = json['price'];
        result.Reimbursable = json['reimbursable'];
        if (json["names"]) {
            json["names"].forEach(function (pn) {
                result.Names.push(Translation.fromJson(pn));
            });
        }
        if (json["leafleturl"]) {
            json["leafleturl"].forEach(function (pn) {
                result.Leafleturl.push(Translation.fromJson(pn));
            });
        }
        if (json["crmurl"]) {
            json["crmurl"].forEach(function (pn) {
                result.Crmurl.push(Translation.fromJson(pn));
            });
        }
        if (json["spcurl"]) {
            json["spcurl"].forEach(function (pn) {
                result.Spcurl.push(Translation.fromJson(pn));
            });
        }
        return result;
    };
    return MedicinalPackage;
}());
export { MedicinalPackage };
//# sourceMappingURL=MedicinalPackage.js.map