export class Medicalfile {
    id: string;
    firstname: string;
    lastname: string;
    niss: string;
    createDateTime: Date;
    updateDateTime: Date;

    public static fromJson(json: any): Medicalfile {
        var result = new Medicalfile();
        result.id = json['id'];
        result.firstname = json['firstname'];
        result.lastname = json['lastname'];
        result.niss = json['niss'];
        result.createDateTime = json['create_datetime'];
        result.updateDateTime = json['update_datetime'];
        return result;
    }
}