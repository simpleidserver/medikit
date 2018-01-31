import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '@app/shared/shared.module';
import { HomeComponent } from './components/home.component';
import { HomeRoutes } from './home.routes';

@NgModule({
    imports: [
        CommonModule,
        HomeRoutes,
        MaterialModule,
        SharedModule
    ],

    declarations: [
        HomeComponent
    ],

    exports: [
        HomeComponent
    ],

    providers: [ ]
})

export class HomeModule { }
