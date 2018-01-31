import { MedicinalProduct } from "./MedicinalProduct";

export class SearchMedicinalProduct {
    constructor() {
        this.Content = [];
    }

    Content: MedicinalProduct[];
    StartIndex: number;
    Count: number;

    public static fromJson(json: any): SearchMedicinalProduct {
        var result = new SearchMedicinalProduct();
        result.Count = json["count"];
        result.StartIndex = json["start_index"];
        json["content"].forEach(function (r: any) {
            result.Content.push(MedicinalProduct.fromJson(r));
        });

        return result;
    }
}