export class Destination {
    id: string;
    quality: string;
    type: string;

    public static fromJson(json: any): Destination {
        var result = new Destination();
        result.id = json["id"];
        result.quality = json["quality"];
        result.type = json["type"];
        return result;
    }
}