import { Translation } from "@app/infrastructure/Translation";

export class ReferenceTableRecord {
    constructor() {
        this.Translations = [];
    }

    Code: string;
    Translations: Translation[];
}