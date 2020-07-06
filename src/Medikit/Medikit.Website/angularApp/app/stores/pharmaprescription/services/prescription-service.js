var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";
import { PharmaPrescription } from "../models/pharma-prescription";
var PharmaPrescriptionService = (function () {
    function PharmaPrescriptionService(http) {
        this.http = http;
    }
    PharmaPrescriptionService.prototype.getMetadata = function () {
        var targetUrl = process.env.API_URL + "/prescriptions/metadata";
        return this.http.get(targetUrl);
    };
    PharmaPrescriptionService.prototype.getOpenedPrescriptions = function (patientNiss, page, samlAssertion) {
        var headers = new HttpHeaders();
        var request = JSON.stringify({ assertion_token: samlAssertion, page_number: page, patient_niss: patientNiss });
        var targetUrl = process.env.API_URL + "/prescriptions/opened";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    PharmaPrescriptionService.prototype.getPrescription = function (prescriptionId, samlAssertion) {
        var request = JSON.stringify({ assertion_token: samlAssertion });
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/prescriptions/" + prescriptionId;
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(map(function (res) {
            return PharmaPrescription.fromJson(res);
        }));
    };
    PharmaPrescriptionService.prototype.revokePrescription = function (rid, reason, samlAssertion) {
        var request = JSON.stringify({ assertion_token: samlAssertion, reason: reason });
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/prescriptions/" + rid + "/revoke";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    PharmaPrescriptionService.prototype.addPrescription = function (prescription, samlAssertion) {
        var json = {
            assertion_token: samlAssertion,
            niss: prescription.Patient.niss,
            create_datetime: prescription.CreateDateTime,
            expiration_datetime: prescription.EndExecutionDate,
            prescription_type: prescription.Type,
            medications: []
        };
        if (prescription.DrugsPrescribed) {
            prescription.DrugsPrescribed.forEach(function (_) {
                var record = {
                    package_code: _.PackageCode
                };
                if (_.InstructionForPatient) {
                    record['instruction_for_patient'] = _.InstructionForPatient;
                }
                if (_.InstructionForReimbursement) {
                    record['instruction_for_reimbursement'] = _.InstructionForReimbursement;
                }
                if (_.BeginMoment) {
                    record['begin_moment'] = _.BeginMoment;
                }
                if (_.Posology.Type === 'freetext') {
                    var freeText = _.Posology;
                    record['posology'] = {
                        type: 'freetext',
                        value: freeText.Content
                    };
                }
                json.medications.push(record);
            });
        }
        var request = JSON.stringify(json);
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/prescriptions";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    PharmaPrescriptionService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], PharmaPrescriptionService);
    return PharmaPrescriptionService;
}());
export { PharmaPrescriptionService };
//# sourceMappingURL=prescription-service.js.map