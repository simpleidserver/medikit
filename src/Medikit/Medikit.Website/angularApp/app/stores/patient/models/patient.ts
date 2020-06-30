export class Patient {
    logoUrl: string;
    firstname: string;
    lastname: string;
    gender: number;
    birthdate: Date;
    niss: string;
    eidCardNumber: string;
    eidCardValidity: Date;
    createDateTime: Date;
    updateDateTime: Date;

    public static fromJson(json: any): Patient {
        var result = new Patient();
        result.firstname = json["firstname"];
        result.birthdate = json["birthdate"];
        result.lastname = json["lastname"];
        result.gender = json["gender"];
        result.niss = json["niss"];
        result.eidCardNumber = json["eid_cardnumber"];
        result.eidCardValidity = json["eid_cardvalidity"];
        result.logoUrl = json["logo_url"];
        result.createDateTime = json["create_datetime"];
        result.updateDateTime = json["update_datetime"];
        return result;
    }
}