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