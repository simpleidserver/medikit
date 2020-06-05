export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["SEARCH_PATIENTS"] = "[Patient] SEARCH_PATIENTS";
    ActionTypes["PATIENTS_LOADED"] = "[Patient] PATIENTS_LOADED";
    ActionTypes["ERROR_SEARCH_PATIENTS"] = "[Patient] ERROR_SEARCH_PATIENTS";
    ActionTypes["GET_PATIENT"] = "[Patient] GET_PATIENT";
    ActionTypes["PATIENT_LOADED"] = "[Patient] PATIENT_LOADED";
    ActionTypes["ERROR_GET_PATIENT"] = "[Patient] ERROR_GET_PATIENT";
})(ActionTypes || (ActionTypes = {}));
var SearchPatients = (function () {
    function SearchPatients(firstName, lastName, niss) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.niss = niss;
        this.type = ActionTypes.SEARCH_PATIENTS;
    }
    return SearchPatients;
}());
export { SearchPatients };
var GetPatient = (function () {
    function GetPatient(niss) {
        this.niss = niss;
        this.type = ActionTypes.GET_PATIENT;
    }
    return GetPatient;
}());
export { GetPatient };
var PatientsLoaded = (function () {
    function PatientsLoaded(patients) {
        this.patients = patients;
        this.type = ActionTypes.PATIENTS_LOADED;
    }
    return PatientsLoaded;
}());
export { PatientsLoaded };
var PatientLoaded = (function () {
    function PatientLoaded(patient) {
        this.patient = patient;
        this.type = ActionTypes.PATIENT_LOADED;
    }
    return PatientLoaded;
}());
export { PatientLoaded };
//# sourceMappingURL=patient-actions.js.map