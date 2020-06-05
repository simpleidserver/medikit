export class PharmaPosology {
    constructor(public Type: string) { }
}

export class PharmaPosologyFreeText extends PharmaPosology {
    constructor() {
        super("freetext");
    }

    Content: string;

    public static fromJson(json: any): PharmaPosologyFreeText {
        var result = new PharmaPosologyFreeText();
        result.Content = json["content"];
        return result;
    }
}

export class PharmaPosologyStructured extends PharmaPosology {
    constructor() {
        super("structured");
    }

    LowPharmaUnitsPerTakes: number;
    HighPharmaUnitsPerTakes: number;
    Unit: string;
    Takes: PharmaPosologyTakes;
}

export class PharmaPosologyTakes {
    LowNumberTakes: number;
    HighNumberTakes: number;
}