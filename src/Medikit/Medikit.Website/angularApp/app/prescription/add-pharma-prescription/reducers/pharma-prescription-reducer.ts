import * as fromActions from '../actions/pharma-prescription';
import { PharmaPrescription } from "@app/prescription/models/pharma-prescription";
import { AddPharmaPrescriptionFormState } from "../states/pharma-prescription-state";

const sessionStorageKey: string = 'pharma-prescription';

var persist = (state: any) => {
    sessionStorage.setItem(sessionStorageKey, JSON.stringify(state));
};

var getInitialState = (): AddPharmaPrescriptionFormState => {
    var json : any = sessionStorage.getItem(sessionStorageKey);
    if (!json) {
        return {
            prescription: new PharmaPrescription(),
            stepperIndex: 0,
            nextPatientFormBtnDisabled : true
        } as AddPharmaPrescriptionFormState;
    }

    return JSON.parse(json) as AddPharmaPrescriptionFormState;

};

export const initialState: AddPharmaPrescriptionFormState = getInitialState();

export function PharmaPrescriptionFormReducer(state = initialState, action: fromActions.ActionsUnion) {
    switch (action.type) {
        case fromActions.ActionTypes.LOAD_PHARMA_PRESCRIPTION:
            var loaded = getInitialState();
            state.stepperIndex = loaded.stepperIndex;
            state.prescription = loaded.prescription;
            state.nextPatientFormBtnDisabled = loaded.nextPatientFormBtnDisabled;
            return { ...state };
        case fromActions.ActionTypes.NISS_CHECKED:
            state.prescription.Patient = action.patient;
            state.nextPatientFormBtnDisabled = false;
            persist(state);
            return { ...state };
        case fromActions.ActionTypes.NISS_UNKNOWN:
            state.nextPatientFormBtnDisabled = true;
            state.prescription.Patient.niss = action.niss;
            state.prescription.Patient.birthdate = null;
            state.prescription.Patient.firstname = null;
            state.prescription.Patient.lastname = null;
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
        case fromActions.ActionTypes.ADD_DRUG_PRESCRIPTION:
            state.prescription.DrugsPrescribed.push(action.drugPrescription);
            persist(state);
            return { ...state };
        case fromActions.ActionTypes.DELETE_DRUG_PRESCRIPTION:
            state.prescription.DrugsPrescribed = state.prescription.DrugsPrescribed.filter(function (dp) {
                return dp.TechnicalId != action.technicalId;
            });
            persist(state);
            return { ...state };
        default:
            return state;
    }
}