export class Patient {
    firstname: string;
    lastname: string;
    birthdate: Date;
    niss: string;
    createDateTime: Date;
    updateDateTime: Date;
    logoUrl: string;

    public static fromJson(json: any): Patient {
        var result = new Patient();
        result.firstname = json["firstname"];
        result.birthdate = json["birthdate"];
        result.lastname = json["lastname"];
        result.niss = json["niss"];
        result.logoUrl = json["logo_url"];
        result.createDateTime = json["create_datetime"];
        result.updateDateTime = json["update_datetime"];
        return result;
    }
}