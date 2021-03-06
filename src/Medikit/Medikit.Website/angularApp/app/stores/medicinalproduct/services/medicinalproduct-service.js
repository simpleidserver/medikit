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
import { SearchMedicinalProduct } from "../models/SearchMedicinalProduct";
var MedicinalProductService = (function () {
    function MedicinalProductService(http) {
        this.http = http;
    }
    MedicinalProductService.prototype.search = function (searchText, startIndex, count, isCommercialised, deliveryEnvironment) {
        var headers = new HttpHeaders();
        var targetUrl = process.env.API_URL + "/medicinalproducts/packages/.search?search_text=" + searchText + "&start_index=" + startIndex + "&count=" + count;
        targetUrl += "&is_commercialised=" + isCommercialised;
        if (deliveryEnvironment) {
            targetUrl += "&delivery_environment=" + deliveryEnvironment;
        }
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(map(function (res) {
            return SearchMedicinalProduct.fromJson(res);
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    MedicinalProductService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], MedicinalProductService);
    return MedicinalProductService;
}());
export { MedicinalProductService };
//# sourceMappingURL=medicinalproduct-service.js.map