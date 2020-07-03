import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";

const url: string = "https://photon.komoot.de/api/?q={0}&lang=fr&limit=10";

export class Address {
    houseNumber: string;
    street: string;
    postalcode: string;
    municipality: string;
    country: string;
    coordinates: number[];

    public static fromJson(json: any): Address {
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
    }
}


@Injectable({
    providedIn: 'root'
})
export class AddressService {

    constructor(private http: HttpClient) { }

    search(query: string): Observable<Array<Address>> {
        var targetUrl = url.replace("{0}", query);
        let headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                var result: Address[] = [];
                if (res) {
                    res["features"].forEach(function (r: any) {
                        result.push(Address.fromJson(r));
                    });
                }

                return result;
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}