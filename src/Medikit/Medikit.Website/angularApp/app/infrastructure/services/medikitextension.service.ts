import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, EventEmitter } from "@angular/core";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";

const ehealthSessionName: string = "ehealthSession";
const extensionUrl = "http://localhost:5050";

@Injectable({
    providedIn: 'root'
})
export class MedikitExtensionService {
    sessionCreated: EventEmitter<any> = new EventEmitter();
    sessionDropped: EventEmitter<any> = new EventEmitter();


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

    createEhealthSessionWithCertificate(): Observable<boolean> {
        var self = this;
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'EHEALTH_AUTH', nonce: nonce });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<any>(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                if (res.type !== 'SAML_ASSERTION' || res.nonce !== nonce) {
                    return false;
                }

                self.sessionCreated.emit(res.content);
                sessionStorage.setItem(ehealthSessionName, JSON.stringify(res.content));
                return res;
            }),
            catchError(() => of(false))
        );
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

    isExtensionInstalled(): Observable<boolean> {
        let headers = new HttpHeaders();
        var nonce = this.buildGuid();
        const request = JSON.stringify({ type: 'PING', nonce: nonce });
        let targetUrl = extensionUrl + "/operations";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<boolean>(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                if (res.nonce !== nonce) {
                    return false;
                }

                return true;
            }),
            catchError(() => of(false))
        );
    }

    disconnect() {
        this.sessionDropped.emit();
        sessionStorage.removeItem(ehealthSessionName);
    }
    
    buildGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
}