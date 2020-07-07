import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AppComponent, AuthPinDialog, InstallExtensionHelpDialog } from './app.component';
import { routes } from './app.routes';
import { HomeModule } from './home/home.module';
import { MaterialModule } from './infrastructure/material.module';
import { AddressService } from './infrastructure/services/address.service';
import { AuthGuard } from './infrastructure/services/auth-guard.service';
import { MedikitExtensionService } from './infrastructure/services/medikitextension.service';
import { SharedModule } from './infrastructure/shared.module';
import { ReferenceTableService } from './referencetable/services/reference-table-service';
import { appReducer } from './stores/appstate';
import { MedicinalProductService } from './stores/medicinalproduct/services/medicinalproduct-service';
import { PatientEffects } from './stores/patient/patient-effects';
import { PatientService } from './stores/patient/services/patient-service';
import { PharmaPrescriptionEffects } from './stores/pharmaprescription/prescription-effects';
import { MedicalfileEffects } from './stores/medicalfile/medicalfile-effects';
import { MedicalfileService } from './stores/medicalfile/services/medicalfile-service';

export function createTranslateLoader(http: HttpClient) {
    let url = process.env.BASE_URL + 'assets/i18n/';
    return new TranslateHttpLoader(http, url, '.json');
}

@NgModule({
    imports: [
        RouterModule.forRoot(routes),
        SharedModule,
        MaterialModule,
        HomeModule,
        MatFormFieldModule,
        FlexLayoutModule,
        BrowserAnimationsModule,
        HttpClientModule,
        OAuthModule.forRoot(),
        EffectsModule.forRoot([PatientEffects, PharmaPrescriptionEffects, MedicalfileEffects]),
        StoreModule.forRoot(appReducer),
        StoreDevtoolsModule.instrument({
            maxAge: 10
        }),
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
        AppComponent, AuthPinDialog, InstallExtensionHelpDialog
    ],
    entryComponents: [AuthPinDialog, InstallExtensionHelpDialog],
    bootstrap: [AppComponent],
    providers: [PatientService, MedicinalProductService, ReferenceTableService, MedikitExtensionService, AddressService, MedicalfileService, AuthGuard ]
})
export class AppModule { }