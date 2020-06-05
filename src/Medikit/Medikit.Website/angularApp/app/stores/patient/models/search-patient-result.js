import { Patient } from "./patient";
var SearchPatientResult = (function () {
    function SearchPatientResult() {
        this.content = [];
    }
    SearchPatientResult.fromJson = function (json) {
        var result = new SearchPatientResult();
        result.count = json["count"];
        result.startIndex = json["start_index"];
        result.totalLength = json["total_length"];
        if (json["content"]) {
            json["content"].forEach(function (r) {
                result.content.push(Patient.fromJson(r));
            });
        }
        return result;
    };
    return SearchPatientResult;
}());
export { SearchPatientResult };
//# sourceMappingURL=search-patient-result.js.map