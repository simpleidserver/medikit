import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { ExtensionComponent } from './extension/extension.component';
import { ListSettingComponent } from './list/list-setting.component';
import { QRCodeModule } from 'angularx-qrcode';
import { SettingRoutes } from './setting.routes';

@NgModule({
    imports: [
        CommonModule,
        MaterialModule,
        SettingRoutes,
        SharedModule,
        QRCodeModule,
        StoreDevtoolsModule.instrument({
            maxAge: 10
        })
    ],

    providers: [],

    declarations: [
        ExtensionComponent,
        ListSettingComponent
    ],
})

export class SettingModule { }