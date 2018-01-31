export class HealthcareWorker {
    Firstname: string;
    Lastname: string;
    RizivNumber: string;

    public static fromJson(json: any): HealthcareWorker {
        var result = new HealthcareWorker();
        result.Firstname = json["firstname"];
        result.Lastname = json["lastname"];
        result.RizivNumber = json["inami"];
        return result;
    }
}