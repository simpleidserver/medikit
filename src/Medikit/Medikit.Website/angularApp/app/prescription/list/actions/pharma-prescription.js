export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["LOAD_PHARMA_PRESCRIPTIONS"] = "[PharmaPrescription] LOAD_PHARMA_PRESCRIPTIONS";
    ActionTypes["PHARMA_PRESCRIPTIONS_LOADED"] = "[PharmaPrescription] PHARMA_PRESCRIPTIONS_LOADED";
    ActionTypes["ERROR_LOAD_PHARMA_PRESCRIPTIONS"] = "[PharmaPrescription] ERROR_LOAD_PHARMA_PRESCRIPTIONS";
})(ActionTypes || (ActionTypes = {}));
var LoadPharmaPrescriptions = (function () {
    function LoadPharmaPrescriptions() {
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
//# sourceMappingURL=pharma-prescription.js.map