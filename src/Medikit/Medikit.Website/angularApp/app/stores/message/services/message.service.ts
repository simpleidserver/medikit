import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { Message } from '../models/message';

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    constructor(private http: HttpClient) { }

    searchInboxMessages(startIndex: number, endIndex: number, samlAssertion: string): Observable<Array<Message>> {
        const request = JSON.stringify({ assertion_token: samlAssertion, start_index: startIndex, end_index: endIndex });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/messages/inbox/search";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                var result : Message[]= [];
                res.forEach((m: any) => {
                    result.push(Message.fromJson(m));
                });

                return result;
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    searchSentboxMessages(startIndex: number, endIndex: number, samlAssertion: string): Observable<Array<Message>> {
        const request = JSON.stringify({ assertion_token: samlAssertion, start_index: startIndex, end_index: endIndex });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/messages/sentbox/search";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                var result: Message[] = [];
                res.forEach((m: any) => {
                    result.push(Message.fromJson(m));
                });

                return result;
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    deleteSentboxMessages(messageIds: Array<string>, samlAssertion: string) {
        const request = JSON.stringify({ assertion_token: samlAssertion, message_ids: messageIds });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/messages/sentbox/delete";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                return res;
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    deleteInboxMessages(messageIds: Array<string>, samlAssertion: string) {
        const request = JSON.stringify({ assertion_token: samlAssertion, message_ids: messageIds });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/messages/inbox/delete";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                return res;
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}