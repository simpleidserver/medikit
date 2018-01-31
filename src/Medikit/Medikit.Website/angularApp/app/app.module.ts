import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppComponent } from './app.component';
import { routes } from './app.routes';
import { HomeModule } from './home/home.module';
import { MedicinalProductService } from './medicinalproduct/services/medicinalproduct-service';
import { PatientService } from './patient/services/patient-service';
import { ReferenceTableService } from './referencetable/services/reference-table-service';
import { MaterialModule } from './shared/material.module';
import { SharedModule } from './shared/shared.module';
import { MedikitExtensionService } from './services/medikitextension.service';

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
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
        AppComponent
    ],
    bootstrap: [AppComponent],
    providers: [PatientService, MedicinalProductService, ReferenceTableService, MedikitExtensionService ]
})
export class AppModule { }