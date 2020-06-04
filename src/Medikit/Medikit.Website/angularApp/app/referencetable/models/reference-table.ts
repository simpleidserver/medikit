import { ReferenceTableRecord } from "./reference-table-record";
import { Translation } from "@app/infrastructure/Translation";

export class ReferenceTable {
    constructor() {
        this.Content = [];
    }

    Name: string;
    Code: string;
    PublishedDate: Date;
    Status: string;
    Version: string;
    Content: ReferenceTableRecord[];

    public static fromJson(json: any): ReferenceTable {
        var result = new ReferenceTable();
        result.Name = json["name"];
        result.Code = json["code"];
        result.PublishedDate = json["published_date"];
        result.Status = json["status"];
        result.Version = json["version"];
        if (json.content) {
            for (var key in json.content) {
                var record = new ReferenceTableRecord();
                record.Code = key;
                json.content[key].translations.forEach(function (translation: any) {
                    var tr = new Translation();
                    tr.Language = translation.language;
                    tr.Value = translation.value;
                    record.Translations.push(tr)
                });

                result.Content.push(record);
            }
        }

        return result;
    }
}