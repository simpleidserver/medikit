export class Translation {
    Language: string;
    Value: string;

    public static fromJson(json: any): Translation {
        var translation = new Translation();
        translation.Language = json["language"];
        translation.Value = json["value"];
        return translation;
    }
}