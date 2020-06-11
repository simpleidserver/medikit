export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["LOAD_PHARMA_PRESCRIPTIONS"] = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTIONS";
    ActionTypes["PHARMA_PRESCRIPTIONS_LOADED"] = "[PharmaPrescription] PHARMA_PRESCRIPTIONS_LOADED";
    ActionTypes["ERROR_LOAD_PHARMA_PRESCRIPTIONS"] = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTIONS";
    ActionTypes["LOAD_PHARMA_PRESCRIPTION"] = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTION";
    ActionTypes["PHARMA_PRESCRIPTION_LOADED"] = "[PharmaPrescription] PHARMA_PRESCRIPTION_LOADED";
    ActionTypes["ERROR_LOAD_PHARMA_PRESCRIPTION"] = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTION";
})(ActionTypes || (ActionTypes = {}));
var LoadPharmaPrescriptions = (function () {
    function LoadPharmaPrescriptions(patientNiss, page, samlAssertion) {
        this.patientNiss = patientNiss;
        this.page = page;
        this.samlAssertion = samlAssertion;
        this.type = ActionTypes.LOAD_PHARMA_PRESCRIPTIONS;
    }
    return LoadPharmaPrescriptions;
}());
export { LoadPharmaPrescriptions };
var PharmaPrescriptionsLoaded = (function () {
    function PharmaPrescriptionsLoaded(prescriptionIds) {
        this.prescriptionIds = prescriptionIds;
        this.type = ActionTypes.PHARMA_PRESCRIPTIONS_LOADED;
    }
    return PharmaPrescriptionsLoaded;
}());
export { PharmaPrescriptionsLoaded };
var LoadPharmaPrescription = (function () {
    function LoadPharmaPrescription(prescriptionId, samlAssertion) {
        this.prescriptionId = prescriptionId;
        this.samlAssertion = samlAssertion;
        this.type = ActionTypes.LOAD_PHARMA_PRESCRIPTION;
    }
    return LoadPharmaPrescription;
}());
export { LoadPharmaPrescription };
var PharmaPrescriptionLoaded = (function () {
    function PharmaPrescriptionLoaded(prescription) {
        this.prescription = prescription;
        this.type = ActionTypes.PHARMA_PRESCRIPTION_LOADED;
    }
    return PharmaPrescriptionLoaded;
}());
export { PharmaPrescriptionLoaded };
//# sourceMappingURL=prescription-actions.js.map