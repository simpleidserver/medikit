var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { ExtensionComponent } from './extension/extension.component';
import { ListSettingComponent } from './list/list-setting.component';
import { SettingRoutes } from './setting.routes';
var SettingModule = (function () {
    function SettingModule() {
    }
    SettingModule = __decorate([
        NgModule({
            imports: [
                CommonModule,
                MaterialModule,
                SettingRoutes,
                SharedModule,
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
    ], SettingModule);
    return SettingModule;
}());
export { SettingModule };
//# sourceMappingURL=setting.module.js.map