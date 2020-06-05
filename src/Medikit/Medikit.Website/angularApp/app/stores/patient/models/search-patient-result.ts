import { Patient } from "./patient";

export class SearchPatientResult {
    constructor() {
        this.content = [];
    }

    count: number;
    startIndex: number;
    totalLength: number;
    content: Array<Patient>;

    public static fromJson(json: any): SearchPatientResult {
        var result = new SearchPatientResult();
        result.count = json["count"];
        result.startIndex = json["start_index"];
        result.totalLength = json["total_length"];
        if (json["content"]) {
            json["content"].forEach((r: any) => {
                result.content.push(Patient.fromJson(r));
            });
        }

        return result;
    }
}