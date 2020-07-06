var ContactInformation = (function () {
    function ContactInformation() {
    }
    ContactInformation.fromJson = function (json) {
        var result = new ContactInformation();
        result.type = json["type"];
        result.value = json["value"];
        return result;
    };
    return ContactInformation;
}());
export { ContactInformation };
//# sourceMappingURL=contact-information.js.map