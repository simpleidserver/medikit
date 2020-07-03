var Patient = (function () {
    function Patient() {
    }
    Patient.fromJson = function (json) {
        var result = new Patient();
        result.firstname = json["firstname"];
        result.birthdate = json["birthdate"];
        result.lastname = json["lastname"];
        result.niss = json["niss"];
        return result;
    };
    return Patient;
}());
export { Patient };
//# sourceMappingURL=patient.js.map