export class SearchPrescriptionRecord {
    rid: string;
    status: string;

    public static fromJson(json: any): SearchPrescriptionRecord {
        var result = new SearchPrescriptionRecord();
        result.rid = json['rid'];
        result.status = json['status'];
        return result;
    }
}