var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AddressService } from '../services/address.service';
var SearchAddressComponent = (function () {
    function SearchAddressComponent(addressService) {
        this.addressService = addressService;
        this.addressText = new FormControl('');
        this.addresses = [];
        this.isLoadingAddresses = false;
        this.select = new EventEmitter();
    }
    SearchAddressComponent.prototype.ngOnInit = function () {
        var self = this;
        var sub;
        this.addressText.valueChanges.subscribe(function (_) {
            self.isLoadingAddresses = true;
            if (sub != null) {
                sub.unsubscribe();
            }
            sub = self.addressService.search(_).subscribe(function (addresses) {
                self.addresses = addresses;
                sub = null;
                self.isLoadingAddresses = false;
            });
        });
    };
    SearchAddressComponent.prototype.selectAddress = function (evt) {
        var val = evt.option.value;
        this.select.emit(val);
    };
    SearchAddressComponent.prototype.displayFn = function (address) {
        if (!address) {
            return '';
        }
        return address.houseNumber + " " + address.street + " " + address.postalcode + " " + address.municipality;
    };
    __decorate([
        Output(),
        __metadata("design:type", EventEmitter)
    ], SearchAddressComponent.prototype, "select", void 0);
    SearchAddressComponent = __decorate([
        Component({
            selector: 'searchaddress',
            templateUrl: './searchaddress.component.html',
            styleUrls: ['./searchaddress.component.scss']
        }),
        __metadata("design:paramtypes", [AddressService])
    ], SearchAddressComponent);
    return SearchAddressComponent;
}());
export { SearchAddressComponent };
//# sourceMappingURL=searchaddress.component.js.map