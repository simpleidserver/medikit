import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStepper, MatSnackBar } from '@angular/material';
import { Address } from '@app/infrastructure/services/address.service';
import { select, Store, ScannedActionsSubject } from '@ngrx/store';
import TileLayer from 'ol/layer/Tile';
import Map from 'ol/Map';
import XYZ from 'ol/source/XYZ';
import View from 'ol/View';
import { ContactInformation } from '../../stores/patient/models/contact-information';
import { AddContactInformation, DeleteContactInformation, LoadPatient, NextStep, PreviousStep, UpdateAddress, UpdateInformation } from './actions/patient';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { AddPatientFormState } from './states/patient-state';
import { TranslateService } from '@ngx-translate/core';
import * as fromAppState from '@app/stores/appstate';
import { filter } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Patient } from '@app/stores/patient/models/patient';
var Placemark = require('ol-ext/overlay/Placemark');


export var RADIUS = 6378137;
export var HALF_SIZE = Math.PI * RADIUS;

@Component({
    selector: 'add-patient-component',
    templateUrl: './add-patient.component.html',
    styleUrls: ['./add-patient.component.scss']
})
export class AddPatientComponent implements OnInit, OnDestroy {
    isAddingPatient: boolean = false;
    map: Map;
    placemark: any;
    interval: any;
    patient: Patient;
    @ViewChild('stepper') stepper: MatStepper;
    contactInfos: ContactInformation[] = [];
    selectedContactInfos: ContactInformation[] = [];
    base64Image: string;

    constructor(private store: Store<AddPatientFormState>,
        private actions$: ScannedActionsSubject,
        private translateService: TranslateService,
        private snackBar: MatSnackBar,
        private appStore: Store<fromAppState.AppState>,
        private router: Router) { }

    patientInfoFormGroup: FormGroup = new FormGroup({
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
        eidCardValidity : new FormControl()
    });

    addressFormGroup: FormGroup = new FormGroup({
        houseNumber: new FormControl({ value: '', disabled: true }),
        road: new FormControl({ value: '', disabled: true }),
        postalCode: new FormControl({ value: '', disabled: true }),
        city: new FormControl({ value: '', disabled: true }),
        country: new FormControl({ value: '', disabled: true }),
        countryCode: new FormControl({ value: '', disabled: true })
    });

    addContactInfoFormGroup: FormGroup = new FormGroup({
        contactType: new FormControl('', [
            Validators.required    
        ]),
        contactValue: new FormControl('', [
            Validators.required    
        ])
    });

    ngOnInit(): void {
        this.store.pipe(select('patientForm')).subscribe((_: AddPatientFormState) => {
            if (!_ || !_.patient || !this.map) {
                return;
            }

            if (_.patient != null) {
                this.base64Image = _.patient.base64Image;
                this.patientInfoFormGroup.controls['firstname'].setValue(_.patient.firstname);
                this.patientInfoFormGroup.controls['lastname'].setValue(_.patient.lastname);
                this.patientInfoFormGroup.controls['gender'].setValue(_.patient.gender);
                this.patientInfoFormGroup.controls['niss'].setValue(_.patient.niss);
                this.patientInfoFormGroup.controls['birthdate'].setValue(_.patient.birthdate);
                this.patientInfoFormGroup.controls['eidCardNumber'].setValue(_.patient.eidCardNumber);
                this.patientInfoFormGroup.controls['eidCardValidity'].setValue(_.patient.eidCardValidity);
                if (_.patient.address != null) {
                    var address = _.patient.address;
                    this.addressFormGroup.controls['houseNumber'].setValue(address.houseNumber);
                    this.addressFormGroup.controls['road'].setValue(address.street);
                    this.addressFormGroup.controls['postalCode'].setValue(address.postalcode);
                    this.addressFormGroup.controls['city'].setValue(address.municipality);
                    this.addressFormGroup.controls['country'].setValue(address.country);
                    var newCoordinates = this.fromEPSG4326(address.coordinates);
                    this.map.getView().animate({
                        center: newCoordinates,
                        zoom: Math.max(this.map.getView().getZoom(), 16)
                    });
                    this.placemark.show(newCoordinates);
                }

                this.contactInfos = _.patient.contactInformations;
                this.patient = _.patient;
            }

            this.stepper.selectedIndex = _.stepperIndex;
        });
        this.actions$.pipe(
            filter((action: any) => action.type == fromPatientActions.ActionTypes.ADD_PATIENT_ERROR))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('error-add-patient'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.isAddingPatient = false;
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromPatientActions.ActionTypes.ADD_PATIENT_SUCCESS))
            .subscribe(() => {
                sessionStorage.removeItem('patient');
                this.snackBar.open(this.translateService.instant('patient-added'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.router.navigate(['/patient']);
                this.isAddingPatient = false;
            });
        const self = this;
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
    }

    ngOnDestroy(): void {

    }

    previousStep() {
        var request = new PreviousStep();
        this.store.dispatch(request);

    }

    nextStep() {
        var request = new NextStep();
        this.store.dispatch(request);
    }

    confirmGeneralInfo() {
        if (this.patientInfoFormGroup.invalid) {
            return;
        }

        const firstname = this.patientInfoFormGroup.controls['firstname'].value;
        const lastname = this.patientInfoFormGroup.controls['lastname'].value;
        const gender = this.patientInfoFormGroup.controls['gender'].value;
        const niss = this.patientInfoFormGroup.controls['niss'].value;
        const birthdate = this.patientInfoFormGroup.controls['birthdate'].value;
        const eidCardNumber = this.patientInfoFormGroup.controls['eidCardNumber'].value;
        const eidCardValidity = this.patientInfoFormGroup.controls['eidCardValidity'].value;
        var action = new UpdateInformation(firstname, lastname, gender, niss, birthdate, eidCardNumber, eidCardValidity, this.base64Image);
        this.store.dispatch(action);
    }

    selectAddress(addr: Address) {
        var action = new UpdateAddress(addr);
        this.store.dispatch(action);
    }

    addContactInfo(contactInfo: any) {
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
    }

    addPatient() {
        this.isAddingPatient = true;
        this.appStore.dispatch(new fromPatientActions.AddPatient(this.patient));
    }

    deleteContactInfos() {
        var action = new DeleteContactInformation(this.selectedContactInfos.map(_ => _.id));
        this.store.dispatch(action);
    }

    onLogoSelected(evt: any) {
        if (!evt.target || !evt.target.files || evt.target.files.length === 0) {
            return;
        }

        var file = evt.target.files[0] as File;
        const reader = new FileReader();
        reader.addEventListener('load', (e: any) => {
            this.base64Image = e.target.result;
        });
        reader.readAsDataURL(file);
    }

    fromEPSG4326(input: number[]) {
        var length = input.length;
        var dimension = 2;
        var output : number[];
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

    }

    private newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
}