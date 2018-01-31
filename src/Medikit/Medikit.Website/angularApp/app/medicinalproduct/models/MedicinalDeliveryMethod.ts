export class MedicinalDeliveryMethod {
    Code: string;
    CodeType: string;
    DeliveryEnvironment: string;

    public static fromJson(json: any): MedicinalDeliveryMethod {
        var result = new MedicinalDeliveryMethod();
        result.Code = json["code"];
        result.CodeType = json["code_type"];
        result.DeliveryEnvironment = json["delivery_environment"];
        return result;
    }
}