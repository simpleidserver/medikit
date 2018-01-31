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
import { ReferenceTable } from "../models/reference-table";
var ReferenceTableService = (function () {
    function ReferenceTableService(http) {
        this.http = http;
    }
    ReferenceTableService.prototype.get = function (code) {
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/referencetables/" + code;
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(map(function (res) {
            return ReferenceTable.fromJson(res);
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    ReferenceTableService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], ReferenceTableService);
    return ReferenceTableService;
}());
export { ReferenceTableService };
//# sourceMappingURL=reference-table-service.js.map