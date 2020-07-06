var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, EventEmitter } from "@angular/core";
import { Observable, of } from "rxjs";
var ehealthSessionName = "ehealthSession";
var MedikitExtensionService = (function () {
    function MedikitExtensionService() {
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
        var promise = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(false);
                return;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getEHEALTHCertificateAuth().then(function (e) {
                self.sessionCreated.emit(e.content);
                sessionStorage.setItem(ehealthSessionName, JSON.stringify(e.content));
                observer.next(true);
            }).catch(function () {
                observer.error(false);
            });
        });
        return promise;
    };
    MedikitExtensionService.prototype.createEhealthSessionWithEID = function (pin) {
        var self = this;
        var promise = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(false);
                return;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getEIDAuth(pin).then(function (e) {
                self.sessionCreated.emit(e.content);
                sessionStorage.setItem(ehealthSessionName, JSON.stringify(e.content));
                observer.next(true);
            }).catch(function () {
                observer.error(false);
            });
        });
        return promise;
    };
    MedikitExtensionService.prototype.getIdentityCertificates = function () {
        var result = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getIdentityCertificates().then(function (e) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    };
    MedikitExtensionService.prototype.chooseIdentityCertificate = function (certificate, password) {
        var result = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.chooseIdentityCertificate(certificate, password).then(function (e) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    };
    MedikitExtensionService.prototype.getMedicalProfessions = function () {
        var result = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getMedicalProfessions().then(function (e) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    };
    MedikitExtensionService.prototype.chooseMedicalProfession = function (profession) {
        var result = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.chooseMedicalProfession(profession).then(function (e) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    };
    MedikitExtensionService.prototype.getIdentityCertificate = function (idCertificate, password) {
        var result = new Observable(function (observer) {
            var win = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }
            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getIdentityCertificate(idCertificate, password).then(function (e) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    };
    MedikitExtensionService.prototype.isExtensionInstalled = function () {
        var win = window;
        if (win.MedikitExtension) {
            return of(true);
        }
        return of(false);
    };
    MedikitExtensionService.prototype.disconnect = function () {
        this.sessionDropped.emit();
        sessionStorage.removeItem(ehealthSessionName);
    };
    MedikitExtensionService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [])
    ], MedikitExtensionService);
    return MedikitExtensionService;
}());
export { MedikitExtensionService };
//# sourceMappingURL=medikitextension.service.js.map