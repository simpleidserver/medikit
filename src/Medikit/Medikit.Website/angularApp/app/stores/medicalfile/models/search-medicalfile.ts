import { Medicalfile } from "./medicalfile";

export class SearchMedicalfileResult {
    constructor() {
        this.content = [];
    }

    count: number;
    startIndex: number;
    totalLength: number;
    content: Array<Medicalfile>;

    public static fromJson(json: any): SearchMedicalfileResult {
        var result = new SearchMedicalfileResult();
        result.count = json["count"];
        result.startIndex = json["start_index"];
        result.totalLength = json["total_length"];
        if (json["content"]) {
            json["content"].forEach((r: any) => {
                result.content.push(Medicalfile.fromJson(r));
            });
        }

        return result;
    }
}