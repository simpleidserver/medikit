export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["LOAD_PHARMA_PRESCRIPTION"] = "[AddPharmaPrescription] LOAD_ADD_PRESCRIPTION";
    ActionTypes["CHECK_NISS"] = "[AddPharmaPrescription] CHECK_NISS";
    ActionTypes["NISS_CHECKED"] = "[AddPharmaPrescription] NISS_CHECKED";
    ActionTypes["NISS_UNKNOWN"] = "[AddPharmaPrescription] NISS_UNNOWN";
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
var CheckNiss = (function () {
    function CheckNiss(niss) {
        this.niss = niss;
        this.type = ActionTypes.CHECK_NISS;
    }
    return CheckNiss;
}());
export { CheckNiss };
var NissChecked = (function () {
    function NissChecked(patient) {
        this.patient = patient;
        this.type = ActionTypes.NISS_CHECKED;
    }
    return NissChecked;
}());
export { NissChecked };
var NissUnknown = (function () {
    function NissUnknown(niss) {
        this.niss = niss;
        this.type = ActionTypes.NISS_UNKNOWN;
    }
    return NissUnknown;
}());
export { NissUnknown };
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