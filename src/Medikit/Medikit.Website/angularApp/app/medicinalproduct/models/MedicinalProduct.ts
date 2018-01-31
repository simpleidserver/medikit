import { Translation } from "@app/Translation";
import { MedicinalPackage } from "./MedicinalPackage";

export class MedicinalProduct {
    constructor() {
        this.Names = [];
        this.Packages = [];
    }

    Code: string;
    OfficialName: string;
    Names: Translation[];
    Packages: MedicinalPackage[];

    public static fromJson(json: any): MedicinalProduct {
        var result = new MedicinalProduct();
        result.Code = json["code"];
        json["names"].forEach(function (name: any) {
            result.Names.push(Translation.fromJson(name));
        });
        json["packages"].forEach(function (p: any) {
            result.Packages.push(MedicinalPackage.fromJson(p));
        });

        return result;
    }
}