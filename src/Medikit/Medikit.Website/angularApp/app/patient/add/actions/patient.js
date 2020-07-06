export var ActionTypes;
(function (ActionTypes) {
    ActionTypes["LOAD_PATIENT"] = "[AddPatient] LOAD_PATIENT";
    ActionTypes["UPDATE_INFORMATION"] = "[AddPatient] UPDATE_INFORMATION";
    ActionTypes["UPDATE_ADDRESS"] = "[AddPatient] UPDATE_ADDRESS";
    ActionTypes["ADD_CONTACT_INFORMATON"] = "[AddPatient] ADD_CONTACT_INFORMATION";
    ActionTypes["DELETE_CONTACT_INFORMATION"] = "[AddPatient] DELETE_CONTACT_INFORMATION";
    ActionTypes["NEXT_STEP"] = "[AddPatient] NEXT_STEP";
    ActionTypes["PREVIOUS_STEP"] = "[AddPatient] PREVIOUS_STEP";
    ActionTypes["ADD_ADDRESS"] = "[AddPatient] ADD_ADDRESS";
    ActionTypes["ADD_CONTACT_INFO"] = "[AddPatient] ADD_CONTACT_INFO";
    ActionTypes["DELETE_CONTACT_INFO"] = "[AddPatient] DELETE_CONTACT_INFO";
})(ActionTypes || (ActionTypes = {}));
var LoadPatient = (function () {
    function LoadPatient() {
        this.type = ActionTypes.LOAD_PATIENT;
    }
    return LoadPatient;
}());
export { LoadPatient };
var UpdateInformation = (function () {
    function UpdateInformation(firstname, lastname, gender, niss, birthdate, eidCardNumber, eidCardValidity, base64Image) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.gender = gender;
        this.niss = niss;
        this.birthdate = birthdate;
        this.eidCardNumber = eidCardNumber;
        this.eidCardValidity = eidCardValidity;
        this.base64Image = base64Image;
        this.type = ActionTypes.UPDATE_INFORMATION;
    }
    return UpdateInformation;
}());
export { UpdateInformation };
var UpdateAddress = (function () {
    function UpdateAddress(address) {
        this.address = address;
        this.type = ActionTypes.UPDATE_ADDRESS;
    }
    return UpdateAddress;
}());
export { UpdateAddress };
var AddContactInformation = (function () {
    function AddContactInformation(contactInfo) {
        this.contactInfo = contactInfo;
        this.type = ActionTypes.ADD_CONTACT_INFO;
    }
    return AddContactInformation;
}());
export { AddContactInformation };
var DeleteContactInformation = (function () {
    function DeleteContactInformation(contactInfos) {
        this.contactInfos = contactInfos;
        this.type = ActionTypes.DELETE_CONTACT_INFO;
    }
    return DeleteContactInformation;
}());
export { DeleteContactInformation };
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
//# sourceMappingURL=patient.js.map