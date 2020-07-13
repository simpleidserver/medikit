import { Destination } from "./destination";
import { Sender } from "./sender";

export class Message {
    contentType: string;
    expirationDate: Date;
    hasAnnex: boolean;
    id: string;
    isImportant: boolean;
    mimeType: string;
    publicationDate: Date;
    size: number;
    title: string;
    sender: Sender;
    destination: Destination;

    public static fromJson(json: any): Message {
        var result = new Message();
        result.contentType = json["content_type"];
        result.expirationDate = json["expiration_date"];
        result.hasAnnex = json["has_annex"];
        result.id = json["id"];
        result.isImportant = json["is_important"];
        result.mimeType = json["mime_type"];
        result.publicationDate = json["publication_date"];
        result.size = json["size"];
        result.title = json["title"];
        result.sender = Sender.fromJson(json["sender"]);
        result.destination = Destination.fromJson(json["destination"]);
        return result;
    }
}