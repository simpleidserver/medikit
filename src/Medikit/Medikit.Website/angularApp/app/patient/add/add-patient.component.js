var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStepper, MatSnackBar } from '@angular/material';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import TileLayer from 'ol/layer/Tile';
import Map from 'ol/Map';
import XYZ from 'ol/source/XYZ';
import View from 'ol/View';
import { ContactInformation } from '../../stores/patient/models/contact-information';
import { AddContactInformation, DeleteContactInformation, LoadPatient, NextStep, PreviousStep, UpdateAddress, UpdateInformation } from './actions/patient';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { TranslateService } from '@ngx-translate/core';
import { filter } from 'rxjs/operators';
import { Router } from '@angular/router';
var Placemark = require('ol-ext/overlay/Placemark');
export var RADIUS = 6378137;
export var HALF_SIZE = Math.PI * RADIUS;
var AddPatientComponent = (function () {
    function AddPatientComponent(store, actions$, translateService, snackBar, appStore, router) {
        this.store = store;
        this.actions$ = actions$;
        this.translateService = translateService;
        this.snackBar = snackBar;
        this.appStore = appStore;
        this.router = router;
        this.isAddingPatient = false;
        this.contactInfos = [];
        this.selectedContactInfos = [];
        this.patientInfoFormGroup = new FormGroup({
            firstname: new FormControl('', [
                Validators.required
            ]),
            lastname: new FormControl('', [
                Validators.required
            ]),
            gender: new FormControl('', [
                Validators.required
            ]),
            niss: new FormControl('', [
                Validators.required,
                Validators.pattern('^[0-9]{2}[0-9]{2}[0-9]{2}[0-9]{3}[0-9]{2}$')
            ]),
            birthdate: new FormControl('', [
                Validators.required
            ]),
            eidCardNumber: new FormControl(),
            eidCardValidity: new FormControl()
        });
        this.addressFormGroup = new FormGroup({
            houseNumber: new FormControl({ value: '', disabled: true }),
            road: new FormControl({ value: '', disabled: true }),
            postalCode: new FormControl({ value: '', disabled: true }),
            city: new FormControl({ value: '', disabled: true }),
            country: new FormControl({ value: '', disabled: true }),
            countryCode: new FormControl({ value: '', disabled: true })
        });
        this.addContactInfoFormGroup = new FormGroup({
            contactType: new FormControl('', [
                Validators.required
            ]),
            contactValue: new FormControl('', [
                Validators.required
            ])
        });
    }
    AddPatientComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.store.pipe(select('patientForm')).subscribe(function (_) {
            if (!_ || !_.patient || !_this.map) {
                return;
            }
            if (_.patient != null) {
                _this.base64Image = _.patient.base64Image;
                _this.patientInfoFormGroup.controls['firstname'].setValue(_.patient.firstname);
                _this.patientInfoFormGroup.controls['lastname'].setValue(_.patient.lastname);
                _this.patientInfoFormGroup.controls['gender'].setValue(_.patient.gender);
                _this.patientInfoFormGroup.controls['niss'].setValue(_.patient.niss);
                _this.patientInfoFormGroup.controls['birthdate'].setValue(_.patient.birthdate);
                _this.patientInfoFormGroup.controls['eidCardNumber'].setValue(_.patient.eidCardNumber);
                _this.patientInfoFormGroup.controls['eidCardValidity'].setValue(_.patient.eidCardValidity);
                if (_.patient.address != null) {
                    var address = _.patient.address;
                    _this.addressFormGroup.controls['houseNumber'].setValue(address.houseNumber);
                    _this.addressFormGroup.controls['road'].setValue(address.street);
                    _this.addressFormGroup.controls['postalCode'].setValue(address.postalcode);
                    _this.addressFormGroup.controls['city'].setValue(address.municipality);
                    _this.addressFormGroup.controls['country'].setValue(address.country);
                    var newCoordinates = _this.fromEPSG4326(address.coordinates);
                    _this.map.getView().animate({
                        center: newCoordinates,
                        zoom: Math.max(_this.map.getView().getZoom(), 16)
                    });
                    _this.placemark.show(newCoordinates);
                }
                _this.contactInfos = _.patient.contactInformations;
                _this.patient = _.patient;
            }
            _this.stepper.selectedIndex = _.stepperIndex;
        });
        this.actions$.pipe(filter(function (action) { return action.type == fromPatientActions.ActionTypes.ADD_PATIENT_ERROR; }))
            .subscribe(function () {
            _this.snackBar.open(_this.translateService.instant('error-add-patient'), _this.translateService.instant('undo'), {
                duration: 2000
            });
            _this.isAddingPatient = false;
        });
        this.actions$.pipe(filter(function (action) { return action.type == fromPatientActions.ActionTypes.ADD_PATIENT_SUCCESS; }))
            .subscribe(function () {
            sessionStorage.removeItem('patient');
            _this.snackBar.open(_this.translateService.instant('patient-added'), _this.translateService.instant('undo'), {
                duration: 2000
            });
            _this.router.navigate(['/patient']);
            _this.isAddingPatient = false;
        });
        var self = this;
        setTimeout(function () {
            self.placemark = new Placemark.default({
                contentColor: '#000'
            });
            self.map = new Map({
                target: 'address_map',
                layers: [
                    new TileLayer({
                        source: new XYZ({
                            url: 'https://{a-c}.tile.openstreetmap.org/{z}/{x}/{y}.png'
                        })
                    })
                ],
                view: new View({
                    center: [0, 0],
                    zoom: 2
                })
            });
            self.map.addOverlay(self.placemark);
            var act = new LoadPatient();
            self.store.dispatch(act);
        }, 100);
    };
    AddPatientComponent.prototype.ngOnDestroy = function () {
    };
    AddPatientComponent.prototype.previousStep = function () {
        var request = new PreviousStep();
        this.store.dispatch(request);
    };
    AddPatientComponent.prototype.nextStep = function () {
        var request = new NextStep();
        this.store.dispatch(request);
    };
    AddPatientComponent.prototype.confirmGeneralInfo = function () {
        if (this.patientInfoFormGroup.invalid) {
            return;
        }
        var firstname = this.patientInfoFormGroup.controls['firstname'].value;
        var lastname = this.patientInfoFormGroup.controls['lastname'].value;
        var gender = this.patientInfoFormGroup.controls['gender'].value;
        var niss = this.patientInfoFormGroup.controls['niss'].value;
        var birthdate = this.patientInfoFormGroup.controls['birthdate'].value;
        var eidCardNumber = this.patientInfoFormGroup.controls['eidCardNumber'].value;
        var eidCardValidity = this.patientInfoFormGroup.controls['eidCardValidity'].value;
        var action = new UpdateInformation(firstname, lastname, gender, niss, birthdate, eidCardNumber, eidCardValidity, this.base64Image);
        this.store.dispatch(action);
    };
    AddPatientComponent.prototype.selectAddress = function (addr) {
        var action = new UpdateAddress(addr);
        this.store.dispatch(action);
    };
    AddPatientComponent.prototype.addContactInfo = function (contactInfo) {
        if (this.addContactInfoFormGroup.invalid) {
            return;
        }
        this.addContactInfoFormGroup.reset();
        var ci = new ContactInformation();
        ci.id = this.newGuid();
        ci.type = contactInfo.contactType;
        ci.value = contactInfo.contactValue;
        var action = new AddContactInformation(ci);
        this.store.dispatch(action);
    };
    AddPatientComponent.prototype.addPatient = function () {
        this.isAddingPatient = true;
        this.appStore.dispatch(new fromPatientActions.AddPatient(this.patient));
    };
    AddPatientComponent.prototype.deleteContactInfos = function () {
        var action = new DeleteContactInformation(this.selectedContactInfos.map(function (_) { return _.id; }));
        this.store.dispatch(action);
    };
    AddPatientComponent.prototype.onLogoSelected = function (evt) {
        var _this = this;
        if (!evt.target || !evt.target.files || evt.target.files.length === 0) {
            return;
        }
        var file = evt.target.files[0];
        var reader = new FileReader();
        reader.addEventListener('load', function (e) {
            _this.base64Image = e.target.result;
        });
        reader.readAsDataURL(file);
    };
    AddPatientComponent.prototype.fromEPSG4326 = function (input) {
        var length = input.length;
        var dimension = 2;
        var output;
        if (output === undefined) {
            if (dimension > 2) {
                output = input.slice();
            }
            else {
                output = new Array(length);
            }
        }
        var halfSize = HALF_SIZE;
        for (var i = 0; i < length; i += dimension) {
            output[i] = halfSize * input[i] / 180;
            var y = RADIUS *
                Math.log(Math.tan(Math.PI * (+input[i + 1] + 90) / 360));
            if (y > halfSize) {
                y = halfSize;
            }
            else if (y < -halfSize) {
                y = -halfSize;
            }
            output[i + 1] = y;
        }
        return output;
    };
    AddPatientComponent.prototype.newGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
    __decorate([
        ViewChild('stepper'),
        __metadata("design:type", MatStepper)
    ], AddPatientComponent.prototype, "stepper", void 0);
    AddPatientComponent = __decorate([
        Component({
            selector: 'add-patient-component',
            templateUrl: './add-patient.component.html',
            styleUrls: ['./add-patient.component.scss']
        }),
        __metadata("design:paramtypes", [Store,
            ScannedActionsSubject,
            TranslateService,
            MatSnackBar,
            Store,
            Router])
    ], AddPatientComponent);
    return AddPatientComponent;
}());
export { AddPatientComponent };
//# sourceMappingURL=add-patient.component.js.map