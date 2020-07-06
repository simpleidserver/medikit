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
import { throwError } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { Patient } from "../models/patient";
import { SearchPatientResult } from "../models/search-patient-result";
var PatientService = (function () {
    function PatientService(http) {
        this.http = http;
    }
    PatientService.prototype.getById = function (id) {
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/patients/" + id;
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(map(function (res) {
            return Patient.fromJson(res);
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    PatientService.prototype.getByNiss = function (niss) {
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/patients/niss/" + niss;
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(map(function (res) {
            return Patient.fromJson(res);
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    PatientService.prototype.add = function (patient) {
        var request = JSON.stringify(Patient.getJSON(patient));
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/patients";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(map(function (res) {
            return Patient.fromJson(res);
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    PatientService.prototype.search = function (firstName, lastName, niss, startIndex, count, active, direction) {
        if (active === void 0) { active = null; }
        if (direction === void 0) { direction = null; }
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/patients/.search";
        if (startIndex <= 0) {
            startIndex = 0;
        }
        if (count <= 0) {
            count = 5;
        }
        targetUrl += "?start_index=" + startIndex + "&count=" + count;
        if (firstName) {
            targetUrl += "&firstname=" + firstName;
        }
        if (lastName) {
            targetUrl += "&lastname=" + lastName;
        }
        if (niss) {
            targetUrl += "&niss=" + niss;
        }
        if (active) {
            targetUrl = targetUrl + "&order_by=" + active;
        }
        if (direction) {
            targetUrl = targetUrl + "&order=" + direction;
        }
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(map(function (res) {
            return SearchPatientResult.fromJson(res);
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    PatientService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], PatientService);
    return PatientService;
}());
export { PatientService };
//# sourceMappingURL=patient-service.js.map