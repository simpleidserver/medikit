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
import { Injectable, EventEmitter } from "@angular/core";
import { of } from "rxjs";
import { map, catchError } from "rxjs/operators";
var ehealthSessionName = "ehealthSession";
var extensionUrl = "http://localhost:5050";
var MedikitExtensionService = (function () {
    function MedikitExtensionService(http) {
        this.http = http;
        this.sessionCreated = new EventEmitter();
        this.sessionDropped = new EventEmitter();
    }
    MedikitExtensionService.prototype.getEhealthSession = function () {
        var json = sessionStorage.getItem(ehealthSessionName);
        if (!json) {
            return null;
        }
        var session = JSON.parse(json);
        var now = new Date();
        if (now < new Date(session.not_before) || new Date(session.not_onorafter) < now) {
            this.disconnect();
            return null;
        }
        return session;
    };
    MedikitExtensionService.prototype.createEhealthSessionWithCertificate = function () {
        var self = this;
        var headers = new HttpHeaders();
        var nonce = this.buildGuid();
        var request = JSON.stringify({ type: 'EHEALTH_AUTH', nonce: nonce });
        var targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(map(function (res) {
            if (res.type !== 'SAML_ASSERTION' || res.nonce !== nonce) {
                return false;
            }
            self.sessionCreated.emit(res.content);
            sessionStorage.setItem(ehealthSessionName, JSON.stringify(res.content));
            return res;
        }), catchError(function () { return of(false); }));
    };
    MedikitExtensionService.prototype.getIdentityCertificates = function () {
        var headers = new HttpHeaders();
        var nonce = this.buildGuid();
        var request = JSON.stringify({ type: 'GET_IDENTITIY_CERTIFICATES', nonce: nonce });
        var targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    MedikitExtensionService.prototype.chooseIdentityCertificate = function (certificate, password) {
        var headers = new HttpHeaders();
        var nonce = this.buildGuid();
        var request = JSON.stringify({ type: 'CHOOSE_IDENTITY_CERTIFICATE', nonce: nonce, certificate: certificate, password: password });
        var targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    MedikitExtensionService.prototype.getMedicalProfessions = function () {
        var headers = new HttpHeaders();
        var nonce = this.buildGuid();
        var request = JSON.stringify({ type: 'GET_MEDICAL_PROFESSIONS', nonce: nonce });
        var targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    MedikitExtensionService.prototype.chooseMedicalProfession = function (profession) {
        var headers = new HttpHeaders();
        var nonce = this.buildGuid();
        var request = JSON.stringify({ type: 'CHOOSE_MEDICAL_PROFESSION', nonce: nonce, profession: profession });
        var targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    };
    MedikitExtensionService.prototype.isExtensionInstalled = function () {
        var headers = new HttpHeaders();
        var nonce = this.buildGuid();
        var request = JSON.stringify({ type: 'PING', nonce: nonce });
        var targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(map(function (res) {
            if (res.nonce !== nonce) {
                return false;
            }
            return true;
        }), catchError(function () { return of(false); }));
    };
    MedikitExtensionService.prototype.disconnect = function () {
        this.sessionDropped.emit();
        sessionStorage.removeItem(ehealthSessionName);
    };
    MedikitExtensionService.prototype.buildGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
    MedikitExtensionService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], MedikitExtensionService);
    return MedikitExtensionService;
}());
export { MedikitExtensionService };
//# sourceMappingURL=medikitextension.service.js.map