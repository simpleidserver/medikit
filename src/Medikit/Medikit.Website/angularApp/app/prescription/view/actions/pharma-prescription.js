export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["LOAD_PHARMA_PRESCRIPTION"] = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTION";
    ActionTypes["PHARMA_PRESCRIPTION_LOADED"] = "[PharmaPrescription] PHARMA_PRESCRIPTION_LOADED";
    ActionTypes["ERROR_LOAD_PHARMA_PRESCRIPTION"] = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTION";
})(ActionTypes || (ActionTypes = {}));
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
//# sourceMappingURL=pharma-prescription.js.map