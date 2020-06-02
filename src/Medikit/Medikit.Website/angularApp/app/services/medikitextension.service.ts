import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

const ehealthSessionName: string = "ehealthSession";
const extensionUrl = "http://localhost:5050";

@Injectable({
    providedIn: 'root'
})
export class MedikitExtensionService {
    constructor(private http: HttpClient) { }

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

    getEhealthCertificateAuth(): Observable<any> {
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'EHEALTH_AUTH', nonce: nonce });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<any>(targetUrl, request, { headers: headers }).pipe(map((res: any) => {
            sessionStorage.setItem(ehealthSessionName, JSON.stringify(res.content));
            return res;
        }));
    }

    getIdentityCertificates(): Observable<any> {
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'GET_IDENTITIY_CERTIFICATES', nonce: nonce });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<any>(targetUrl, request, { headers: headers });
    }

    chooseIdentityCertificate(certificate: string, password: string): Observable<any> {
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'CHOOSE_IDENTITY_CERTIFICATE', nonce: nonce, certificate: certificate, password: password });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<any>(targetUrl, request, { headers: headers });
    }

    getMedicalProfessions(): Observable<any> {
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'GET_MEDICAL_PROFESSIONS', nonce: nonce });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<any>(targetUrl, request, { headers: headers });
    }

    chooseMedicalProfession(profession: string): Observable<any> {
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'CHOOSE_MEDICAL_PROFESSION', nonce: nonce, profession: profession });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<any>(targetUrl, request, { headers: headers });
    }

    isExtensionInstalled(): boolean {
        return true;
    }

    disconnect() {
        sessionStorage.removeItem(ehealthSessionName);
    }
    
    buildGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
}