﻿import { Patient } from '../../../stores/patient/models/patient';
import * as fromActions from '../actions/patient';
import { AddPatientFormState } from "../states/patient-state";

const sessionStorageKey: string = 'patient';

var persist = (state: any) => {
    sessionStorage.setItem(sessionStorageKey, JSON.stringify(state));
};

var getInitialState = (): AddPatientFormState => {
    var json : any = sessionStorage.getItem(sessionStorageKey);
    if (!json) {
        return {
            patient: new Patient(),
            stepperIndex: 0
        } as AddPatientFormState;
    }

    return JSON.parse(json) as AddPatientFormState;

};

export const initialState: AddPatientFormState = getInitialState();

export function PatientFormReducer(state = initialState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.LOAD_PATIENT:
            var loaded = getInitialState();
            state.stepperIndex = loaded.stepperIndex;
            state.patient = loaded.patient;
            return { ...state };
        case fromActions.ActionTypes.UPDATE_INFORMATION:
            state.patient.birthdate = action.birthdate;
            state.patient.eidCardNumber = action.eidCardNumber;
            state.patient.eidCardValidity = action.eidCardValidity;
            state.patient.firstname = action.firstname;
            state.patient.lastname = action.lastname;
            state.patient.base64Image = action.base64Image;
            state.patient.niss = action.niss;
            state.patient.gender = action.gender;
            state.stepperIndex += 1;
            persist(state);
            return { ...state };
        case fromActions.ActionTypes.UPDATE_ADDRESS:
            state.patient.address = action.address;
            persist(state);
            return { ...state };
        case fromActions.ActionTypes.ADD_CONTACT_INFO:
            state.patient.contactInformations.push(action.contactInfo);
            persist(state);
            return { ...state };
        case fromActions.ActionTypes.DELETE_CONTACT_INFO:
            state.patient.contactInformations = state.patient.contactInformations.filter((_) => {
                if (action.contactInfos.filter((__) => {
                    return _.id == __;
                }).length > 0) {
                    return false;
                }

                return true;
            });

            persist(state);
            return { ...state };
        case fromActions.ActionTypes.NEXT_STEP:
            state.stepperIndex += 1;
            persist(state);
            return { ...state };
        case fromActions.ActionTypes.PREVIOUS_STEP:
            state.stepperIndex -= 1;
            persist(state);
            return { ...state };
        default:
            return state;
    }
}