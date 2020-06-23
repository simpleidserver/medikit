import { Injectable, EventEmitter } from "@angular/core";
import { Observable, of } from "rxjs";

const ehealthSessionName: string = "ehealthSession";

@Injectable({
    providedIn: 'root'
})
export class MedikitExtensionService {
    sessionCreated: EventEmitter<any> = new EventEmitter();
    sessionDropped: EventEmitter<any> = new EventEmitter();

    constructor() { }

    getEhealthSession() {
        var json: any = sessionStorage.getItem(ehealthSessionName);
        if (!json) {
            return null;
        }

        var session : any = JSON.parse(json);
        const now: Date = new Date();
        if (now < new Date(session.not_before) || new Date(session.not_onorafter) < now) {
            this.disconnect();
            return null;
        }

        return session;
    }

    createEhealthSessionWithCertificate(): Observable<boolean> {
        const self = this;
        let promise = new Observable<boolean>((observer : any) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(false);
                return;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getEHEALTHCertificateAuth().then(function (e: any) {
                self.sessionCreated.emit(e.content);
                sessionStorage.setItem(ehealthSessionName, JSON.stringify(e.content));
                observer.next(true);
            }).catch(function () {
                observer.error(false);
            });
        });

        return promise;
    }

    createEhealthSessionWithEID(pin: string): Observable<boolean> {
        const self = this;
        let promise = new Observable<boolean>((observer: any) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(false);
                return;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getEIDAuth(pin).then(function (e: any) {
                self.sessionCreated.emit(e.content);
                sessionStorage.setItem(ehealthSessionName, JSON.stringify(e.content));
                observer.next(true);
            }).catch(function () {
                observer.error(false);
            });
        });

        return promise;
    }

    getIdentityCertificates(): Observable<any> {
        var result = new Observable<any>((observer) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getIdentityCertificates().then(function (e: any) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    }

    chooseIdentityCertificate(certificate: string, password: string): Observable<any> {
        var result = new Observable<any>((observer) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.chooseIdentityCertificate(certificate, password).then(function (e: any) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    }

    getMedicalProfessions(): Observable<any> {
        var result = new Observable<any>((observer) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getMedicalProfessions().then(function (e: any) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    }

    chooseMedicalProfession(profession: string): Observable<any> {
        var result = new Observable<any>((observer) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.chooseMedicalProfession(profession).then(function (e: any) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    }

    getIdentityCertificate(idCertificate: string, password: string): Observable<any> {
        var result = new Observable<any>((observer) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                observer.error(true);
                return null;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getIdentityCertificate(idCertificate, password).then(function (e: any) {
                observer.next(e);
            }).catch(function () {
                observer.error(false);
            });
        });
        return result;
    }

    isExtensionInstalled(): Observable<boolean> {
        const win: any = window;
        if (win.MedikitExtension) {
            return of(true);
        }

        return of(false);
    }

    disconnect() {
        this.sessionDropped.emit();
        sessionStorage.removeItem(ehealthSessionName);
    }
}