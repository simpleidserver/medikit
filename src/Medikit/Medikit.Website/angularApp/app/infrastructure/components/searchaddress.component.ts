import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Address, AddressService } from '../services/address.service';

@Component({
    selector: 'searchaddress',
    templateUrl: './searchaddress.component.html',
    styleUrls: ['./searchaddress.component.scss']
})
export class SearchAddressComponent implements OnInit {
    addressText: FormControl = new FormControl('');
    addresses: Address[] = [];
    isLoadingAddresses: boolean = false;
    @Output() select: EventEmitter<any> = new EventEmitter();

    constructor(private addressService: AddressService) { }

    ngOnInit(): void {
        const self = this;
        var sub: any;
        this.addressText.valueChanges.subscribe((_) => {
            self.isLoadingAddresses = true;
            if (sub != null) {
                sub.unsubscribe();
            }

            sub = self.addressService.search(_).subscribe(function (addresses: Address[]) {
                self.addresses = addresses;
                sub = null;
                self.isLoadingAddresses = false;
            });
        });
    }

    selectAddress(evt: any) {
        var val = evt.option.value;
        this.select.emit(val);
    }

    displayFn(address: Address) {
        if (!address) {
            return '';
        }

        return address.houseNumber + " " + address.street + " " + address.postalcode + " " + address.municipality;
    }
}