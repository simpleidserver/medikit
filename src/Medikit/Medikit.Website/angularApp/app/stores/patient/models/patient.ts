export class Patient {
    firstname: string;
    lastname: string;
    birthdate: Date;
    niss: string;

    public static fromJson(json: any): Patient {
        var result = new Patient();
        result.firstname = json["firstname"];
        result.birthdate = json["birthdate"];
        result.lastname = json["lastname"];
        result.niss = json["niss"];
        return result;
    }
}