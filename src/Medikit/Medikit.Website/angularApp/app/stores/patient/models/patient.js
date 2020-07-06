import { Address } from "@app/infrastructure/services/address.service";
import { ContactInformation } from './contact-information';
var Patient = (function () {
    function Patient() {
        this.contactInformations = [];
    }
    Patient.fromJson = function (json) {
        var result = new Patient();
        result.id = json["id"];
        result.firstname = json["firstname"];
        result.birthdate = json["birthdate"];
        result.lastname = json["lastname"];
        result.gender = json["gender"];
        result.niss = json["niss"];
        result.eidCardNumber = json["eid_cardnumber"];
        result.eidCardValidity = json["eid_cardvalidity"];
        result.logoUrl = json["logo_url"];
        result.createDateTime = json["create_datetime"];
        result.updateDateTime = json["update_datetime"];
        if (json['address']) {
            var address = json['address'];
            var adr = new Address();
            adr.country = address['country'];
            adr.postalcode = address['postal_code'];
            adr.street = address['street'];
            adr.houseNumber = address['street_number'];
            adr.coordinates = address['coordinates'];
            result.address = adr;
        }
        if (json['contact_infos']) {
            var contactInfos = json['contact_infos'];
            result.contactInformations = contactInfos.map(function (_) { return ContactInformation.fromJson(_); });
        }
        return result;
    };
    Patient.getJSON = function (patient) {
        var result = {};
        result['firstname'] = patient.firstname;
        result['lastname'] = patient.lastname;
        result['niss'] = patient.niss;
        result['gender'] = patient.gender;
        result['birthdate'] = patient.birthdate;
        result['base64_image'] = patient.base64Image;
        result['eid_cardnumber'] = patient.eidCardNumber;
        result['eid_cardvalidity'] = patient.eidCardValidity;
        if (patient.address) {
            var adr = {};
            adr['street'] = patient.address.street;
            adr['street_number'] = patient.address.houseNumber;
            adr['country'] = patient.address.country;
            adr['postal_code'] = patient.address.postalcode;
            adr['coordinates'] = patient.address.coordinates;
            result['address'] = adr;
        }
        if (patient.contactInformations) {
            var contactInfos = patient.contactInformations.map(function (_) {
                var ci = {};
                ci['type'] = _.type;
                ci['value'] = _.value;
                return ci;
            });
            result['contact_infos'] = contactInfos;
        }
        return result;
    };
    return Patient;
}());
export { Patient };
//# sourceMappingURL=patient.js.map