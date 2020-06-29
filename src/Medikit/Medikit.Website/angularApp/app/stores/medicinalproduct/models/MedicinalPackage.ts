import { Translation } from "@app/infrastructure/Translation";

export class MedicinalPackage {
    constructor() {
        this.Names = [];
        this.Leafleturl = [];
        this.Crmurl = [];
        this.Spcurl = [];
    }

    Code: string;
    Names: Translation[];
    Price: string;
    Reimbursable: boolean;
    Leafleturl: Translation[];
    Crmurl: Translation[];
    Spcurl: Translation[];


    public static fromJson(json: any): MedicinalPackage {
        var result = new MedicinalPackage();
        result.Code = json['code'];
        result.Price = json['price'];
        result.Reimbursable = json['reimbursable'];
        if (json["names"]) {
            json["names"].forEach(function (pn: any) {
                result.Names.push(Translation.fromJson(pn));
            });
        }

        if (json["leafleturl"]) {
            json["leafleturl"].forEach(function (pn: any) {
                result.Leafleturl.push(Translation.fromJson(pn));
            });
        }

        if (json["crmurl"]) {
            json["crmurl"].forEach(function (pn: any) {
                result.Crmurl.push(Translation.fromJson(pn));
            });
        }

        if (json["spcurl"]) {
            json["spcurl"].forEach(function (pn: any) {
                result.Spcurl.push(Translation.fromJson(pn));
            });
        }

        return result;
    }
}