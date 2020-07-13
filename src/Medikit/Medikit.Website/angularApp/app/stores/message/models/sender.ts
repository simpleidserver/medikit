export class Sender {
    id: string;
    quality: string;
    type: string;
    name: string;
    firstname: string;

    public static fromJson(json: any): Sender {
        var result = new Sender();
        result.id = json["id"];
        result.quality = json["quality"];
        result.type = json["type"];
        result.name = json["name"];
        result.firstname = json["firstname"];
        return result;
    }
}