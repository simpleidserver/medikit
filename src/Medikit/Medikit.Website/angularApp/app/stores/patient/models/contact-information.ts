export class ContactInformation {
    id: string;
    type: string;
    value: string;

    public static fromJson(json : any): ContactInformation {
        var result = new ContactInformation();
        result.type = json["type"];
        result.value = json["value"];
        return result;
    }
}