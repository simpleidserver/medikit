var MedicinalDeliveryMethod = (function () {
    function MedicinalDeliveryMethod() {
    }
    MedicinalDeliveryMethod.fromJson = function (json) {
        var result = new MedicinalDeliveryMethod();
        result.Code = json["code"];
        result.CodeType = json["code_type"];
        result.DeliveryEnvironment = json["delivery_environment"];
        return result;
    };
    return MedicinalDeliveryMethod;
}());
export { MedicinalDeliveryMethod };
//# sourceMappingURL=MedicinalDeliveryMethod.js.map