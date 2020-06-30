import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material';
import { select, Store } from '@ngrx/store';
import Map from 'ol/Map';
import View from 'ol/View'
import TileLayer from 'ol/layer/Tile';
var SearchPhoton = require('ol-ext/control/SearchPhoton');
var Placemark = require('ol-ext/overlay/Placemark');
import { defaults as defaultInteractions, DragRotateAndZoom } from 'ol/interaction';
import XYZ from 'ol/source/XYZ';
import { NextStep, PreviousStep, UpdateInformation } from './actions/patient';
import { AddPatientFormState } from './states/patient-state';

@Component({
    selector: 'add-patient-component',
    templateUrl: './add-patient.component.html',
    styleUrls: ['./add-patient.component.scss']
})
export class AddPatientComponent implements OnInit, OnDestroy {
    map: Map;
    interval: any;
    @ViewChild('stepper') stepper: MatStepper;
    logoUrl: string;

    constructor(private store: Store<AddPatientFormState>) { }

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
            Validators.required
        ]),
        birthdate: new FormControl(),
        eidCardNumber: new FormControl(),
        eidCardValidity : new FormControl()
    });

    ngOnInit(): void {
        this.store.pipe(select('patientForm')).subscribe((_: AddPatientFormState) => {
            if (!_ || !_.patient) {
                return;
            }

            if (_.patient != null) {
                this.logoUrl = _.patient.logoUrl;
                this.patientInfoFormGroup.controls['firstname'].setValue(_.patient.firstname);
                this.patientInfoFormGroup.controls['lastname'].setValue(_.patient.lastname);
                this.patientInfoFormGroup.controls['gender'].setValue(_.patient.gender);
                this.patientInfoFormGroup.controls['niss'].setValue(_.patient.niss);
                this.patientInfoFormGroup.controls['birthdate'].setValue(_.patient.birthdate);
                this.patientInfoFormGroup.controls['eidCardNumber'].setValue(_.patient.eidCardNumber);
                this.patientInfoFormGroup.controls['eidCardValidity'].setValue(_.patient.eidCardValidity);
            }

            this.stepper.selectedIndex = _.stepperIndex;
        });
        const self = this;
        setTimeout(function () {
            var placemark = new Placemark.default({
                contentColor: '#000',
                autoPan: true
            });
            self.map = new Map({
                interactions: defaultInteractions().extend([
                    new DragRotateAndZoom()
                ]),
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
            var search = new SearchPhoton.default({
                lang: "fr",
                reverse: true,
                position: true
            });
            self.map.addControl(search);
            self.map.addOverlay(placemark);
            search.on('select', function (e : any) {
                self.map.getView().animate({
                    center: e.coordinate,
                    zoom: Math.max(self.map.getView().getZoom(), 16)
                });
                placemark.show(e.coordinate);
            });
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
        console.log(gender);
        var action = new UpdateInformation(firstname, lastname, gender, niss, birthdate, eidCardNumber, eidCardValidity, this.logoUrl);
        this.store.dispatch(action);
    }

    addPatient() {

    }

    onLogoSelected(evt: any) {
        if (!evt.target || !evt.target.files || evt.target.files.length === 0) {
            return;
        }

        var file = evt.target.files[0] as File;
        const reader = new FileReader();
        reader.addEventListener('load', (e: any) => {
            console.log(e.target.result);
            this.logoUrl = e.target.result;
        });
        reader.readAsDataURL(file);
    }
}