var HealthcareWorker = (function () {
    function HealthcareWorker() {
    }
    HealthcareWorker.fromJson = function (json) {
        var result = new HealthcareWorker();
        result.Firstname = json["firstname"];
        result.Lastname = json["lastname"];
        result.RizivNumber = json["inami"];
        return result;
    };
    return HealthcareWorker;
}());
export { HealthcareWorker };
//# sourceMappingURL=healthcare-worker.js.map