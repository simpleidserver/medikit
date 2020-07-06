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
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
var url = "https://photon.komoot.de/api/?q={0}&lang=fr&limit=10";
var Address = (function () {
    function Address() {
    }
    Address.fromJson = function (json) {
        var result = new Address();
        if (json['properties']) {
            var properties = json["properties"];
            result.houseNumber = properties.housenumber;
            result.street = properties.street;
            result.postalcode = properties.postcode;
            result.municipality = properties.state;
            result.country = properties.country;
        }
        if (json['geometry']) {
            var geometry = json['geometry'];
            result.coordinates = geometry.coordinates;
        }
        return result;
    };
    return Address;
}());
export { Address };
var AddressService = (function () {
    function AddressService(http) {
        this.http = http;
    }
    AddressService.prototype.search = function (query) {
        var targetUrl = url.replace("{0}", query);
        var headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(map(function (res) {
            var result = [];
            if (res) {
                res["features"].forEach(function (r) {
                    result.push(Address.fromJson(r));
                });
            }
            return result;
        }), catchError(function (err) {
            return throwError(err);
        }));
    };
    AddressService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], AddressService);
    return AddressService;
}());
export { AddressService };
//# sourceMappingURL=address.service.js.map