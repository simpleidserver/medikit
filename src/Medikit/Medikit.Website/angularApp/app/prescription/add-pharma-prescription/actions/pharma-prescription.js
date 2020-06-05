export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["LOAD_PHARMA_PRESCRIPTION"] = "[AddPharmaPrescription] LOAD_ADD_PRESCRIPTION";
    ActionTypes["NEXT_STEP"] = "[AddPharmaPrescription] NEXT_STEP";
    ActionTypes["PREVIOUS_STEP"] = "[AddPharmaPrescription] PREVIOUS_STEP";
    ActionTypes["ADD_DRUG_PRESCRIPTION"] = "[AddPharmaPrescription] ADD_DRUG_PRESCRIPTION";
    ActionTypes["DELETE_DRUG_PRESCRIPTION"] = "[DeletePharmaPrescription] DELETE_DRUG_PRESCRIPTION";
})(ActionTypes || (ActionTypes = {}));
var LoadPrescription = (function () {
    function LoadPrescription() {
        this.type = ActionTypes.LOAD_PHARMA_PRESCRIPTION;
    }
    return LoadPrescription;
}());
export { LoadPrescription };
var NextStep = (function () {
    function NextStep() {
        this.type = ActionTypes.NEXT_STEP;
    }
    return NextStep;
}());
export { NextStep };
var PreviousStep = (function () {
    function PreviousStep() {
        this.type = ActionTypes.PREVIOUS_STEP;
    }
    return PreviousStep;
}());
export { PreviousStep };
var AddDrugPrescription = (function () {
    function AddDrugPrescription(drugPrescription) {
        this.drugPrescription = drugPrescription;
        this.type = ActionTypes.ADD_DRUG_PRESCRIPTION;
    }
    return AddDrugPrescription;
}());
export { AddDrugPrescription };
var DeleteDrugPrescription = (function () {
    function DeleteDrugPrescription(technicalId) {
        this.technicalId = technicalId;
        this.type = ActionTypes.DELETE_DRUG_PRESCRIPTION;
    }
    return DeleteDrugPrescription;
}());
export { DeleteDrugPrescription };
//# sourceMappingURL=pharma-prescription.js.map