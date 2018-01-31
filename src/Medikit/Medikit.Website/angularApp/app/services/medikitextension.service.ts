import { Injectable } from "@angular/core";

const ehealthSessionName: string = "ehealthSession";
const extensionId: string = 'jaolfgijgnmkkgldbbignheccpmjghma';

@Injectable({
    providedIn: 'root'
})
export class MedikitExtensionService {
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

    getEhealthCertificateAuth() : Promise<any> {
        let promise = new Promise((resolve : any, reject : any) => {
            const win: any = window;
            if (!win.MedikitExtension) {
                reject();
                return;
            }

            var medikitExtension = new win.MedikitExtension();
            medikitExtension.getEHEALTHCertificateAuth().then(function (e : any) {
                sessionStorage.setItem(ehealthSessionName, JSON.stringify(e.content));
                resolve();
            }).catch(function () {
                reject();
            });
        });

        return promise;
    }

    getIdentityCertificates(): Promise<any> {
        const win: any = window;
        if (!win.MedikitExtension) {
            return Promise.reject();
        }

        var medikitExtension = new win.MedikitExtension();
        return medikitExtension.getIdentityCertificates();
    }

    chooseIdentityCertificate(certificate: string, password: string) : Promise<any> {
        const win: any = window;
        if (!win.MedikitExtension) {
            return Promise.reject();
        }

        var medikitExtension = new win.MedikitExtension();
        return medikitExtension.chooseIdentityCertificate(certificate, password);
    }

    getMedicalProfessions(): Promise<any> {
        const win: any = window;
        if (!win.MedikitExtension) {
            return Promise.reject();
        }

        var medikitExtension = new win.MedikitExtension();
        return medikitExtension.getMedicalProfessions();
    }

    chooseMedicalProfession(profession: string): Promise<any> {
        const win: any = window;
        if (!win.MedikitExtension) {
            return Promise.reject();
        }

        var medikitExtension = new win.MedikitExtension();
        return medikitExtension.chooseMedicalProfession(profession);
    }

    isExtensionInstalled(): boolean {
        try {
            const win: any = window;
            win.chrome.runtime.connect(extensionId);
            return true;
        }
        catch (ex) {
            return false;
        }
    }

    disconnect() {
        sessionStorage.removeItem(ehealthSessionName);
    }
}