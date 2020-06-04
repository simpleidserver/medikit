var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';
import { MedikitExtensionService } from './infrastructure/services/medikitextension.service';
var medikitLanguageName = "medikitLanguage";
var AppComponent = (function () {
    function AppComponent(route, translate, router, oauthService, medikitExtensionService) {
        this.route = route;
        this.translate = translate;
        this.router = router;
        this.oauthService = oauthService;
        this.medikitExtensionService = medikitExtensionService;
        this.isConnected = false;
        this.expanded = false;
        this.activeLanguage = 'en';
        this.activeLanguage = sessionStorage.getItem(medikitLanguageName);
        if (!this.activeLanguage) {
            this.activeLanguage = 'en';
        }
        translate.setDefaultLang(this.activeLanguage);
        translate.use(this.activeLanguage);
        this.configureOAuth();
    }
    AppComponent.prototype.configureOAuth = function () {
        this.oauthService.configure(authConfig);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        var self = this;
        this.oauthService.loadDiscoveryDocumentAndTryLogin({
            disableOAuth2StateCheck: true
        });
        this.sessionCheckTimer = setInterval(function () {
            if (!self.oauthService.hasValidIdToken()) {
                self.oauthService.logOut();
                self.route.navigate(["/"]);
            }
        }, 3000);
    };
    AppComponent.prototype.login = function () {
        console.log("BINGO");
        this.oauthService.customQueryParams = {
            'prompt': 'login'
        };
        this.oauthService.initImplicitFlow();
        return false;
    };
    AppComponent.prototype.chooseSession = function () {
        this.oauthService.customQueryParams = {
            'prompt': 'select_account'
        };
        this.oauthService.initImplicitFlow();
        return false;
    };
    AppComponent.prototype.disconnect = function () {
        this.oauthService.logOut();
        this.router.navigate(['/home']);
        return false;
    };
    AppComponent.prototype.init = function () {
        var claims = this.oauthService.getIdentityClaims();
        if (!claims) {
            this.isConnected = false;
            ;
            return;
        }
        this.name = claims.given_name;
        this.roles = claims.role;
        this.isConnected = true;
    };
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.init();
        this.oauthService.events.subscribe(function (e) {
            if (e.type === "logout") {
                _this.isConnected = false;
            }
            else if (e.type === "token_received") {
                _this.init();
            }
        });
        this.router.events.subscribe(function (opt) {
            var url = opt.urlAfterRedirects;
            if (!url || _this.expanded) {
                return;
            }
            if (url.startsWith('/prescription')) {
                _this.expanded = true;
            }
        });
        this.medikitExtensionService.
        ;
    };
    AppComponent.prototype.chooseLanguage = function (lng) {
        this.translate.use(lng);
        sessionStorage.setItem(medikitLanguageName, lng);
        this.activeLanguage = lng;
    };
    AppComponent.prototype.togglePrescriptions = function () {
        this.expanded = !this.expanded;
    };
    AppComponent = __decorate([
        Component({
            selector: 'app-component',
            templateUrl: './app.component.html',
            styleUrls: [
                './app.component.scss'
            ],
            encapsulation: ViewEncapsulation.None,
            animations: [
                trigger('indicatorRotate', [
                    state('collapsed', style({ transform: 'rotate(0deg)' })),
                    state('expanded', style({ transform: 'rotate(180deg)' })),
                    transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4,0.0,0.2,1)')),
                ])
            ]
        }),
        __metadata("design:paramtypes", [Router, TranslateService, Router, OAuthService, MedikitExtensionService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map