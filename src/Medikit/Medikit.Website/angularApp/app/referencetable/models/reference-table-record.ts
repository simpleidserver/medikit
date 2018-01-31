import { Translation } from "@app/Translation";

export class ReferenceTableRecord {
    constructor() {
        this.Translations = [];
    }

    Code: string;
    Translations: Translation[];
}