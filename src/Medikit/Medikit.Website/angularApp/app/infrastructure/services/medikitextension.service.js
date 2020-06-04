var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from "@angular/core";
var ehealthSessionName = "ehealthSession";
var extensionId = 'jaolfgijgnmkkgldbbignheccpmjghma';
var EhealthSessionService = (function () {
    function EhealthSessionService() {
    }
    EhealthSessionService.prototype.getEhealthSession = function () {
        var session = sessionStorage.getItem(ehealthSessionName);
        return session;
    };
    EhealthSessionService.prototype.getEHEALTHCertificateAuth = function () {
        var promise = new Promise(function (resolve, reject) {
            var win = window;
            if (!win.MedikitExtension) {
                return;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getEHEALTHCertificateAuth().then(function (e) {
                sessionStorage.setItem(ehealthSessionName, e);
                resolve();
            }).catch(function () {
                reject();
            });
        });
        return promise;
    };
    EhealthSessionService.prototype.isExtensionInstalled = function () {
        try {
            var win = window;
            console.log(win.chrome.runtime.connect(extensionId));
            win.chrome.runtime.connect(extensionId);
            return true;
        }
        catch (ex) {
            console.log(ex);
            return false;
        }
    };
    EhealthSessionService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [])
    ], EhealthSessionService);
    return EhealthSessionService;
}());
export { EhealthSessionService };
//# sourceMappingURL=ehealthsession.service.js.map