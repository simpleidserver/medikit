import { SearchPrescriptionRecord } from "./search-prescription-record";

export class SearchPrescriptionResult {
    constructor() {
        this.prescriptions = [];
    }

    prescriptions: SearchPrescriptionRecord[];
    hasMoreResults: string;

    public static fromJson(json: any): SearchPrescriptionResult {
        var result = new SearchPrescriptionResult();
        result.hasMoreResults = json['has_more_results'];
        if (json['prescriptions']) {
            json['prescriptions'].forEach((pr: any) => {
                result.prescriptions.push(SearchPrescriptionRecord.fromJson(pr));
            });
        }

        return result;
    }
}