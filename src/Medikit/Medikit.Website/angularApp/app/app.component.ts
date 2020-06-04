import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';

const medikitLanguageName: string = "medikitLanguage";

@Component({
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
            transition('expanded <=> collapsed',
                animate('225ms cubic-bezier(0.4,0.0,0.2,1)')
            ),
        ])
    ]
})
export class AppComponent implements OnInit {
    sessionCheckTimer: any;
    isConnected: boolean = false;
    name: string;
    roles: any;
    expanded: boolean = false;
    activeLanguage: string = 'en';

    constructor(private route: Router, private translate: TranslateService, private router: Router, private oauthService: OAuthService) {
        this.activeLanguage = sessionStorage.getItem(medikitLanguageName);
        if (!this.activeLanguage) {
            this.activeLanguage = 'en';
        }

        translate.setDefaultLang(this.activeLanguage);
        translate.use(this.activeLanguage);
        this.configureOAuth();
    }

    configureOAuth() {
        this.oauthService.configure(authConfig);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        let self = this;
        this.oauthService.loadDiscoveryDocumentAndTryLogin({
            disableOAuth2StateCheck: true
        });
        this.sessionCheckTimer = setInterval(function () {
            if (!self.oauthService.hasValidIdToken()) {
                self.oauthService.logOut();
                self.route.navigate(["/"]);
            }
        }, 3000);
    }

    login() {
        console.log("BINGO");
        this.oauthService.customQueryParams = {
            'prompt': 'login'
        };
        this.oauthService.initImplicitFlow();
        return false;        
    }

    chooseSession() {
        this.oauthService.customQueryParams = {
            'prompt': 'select_account'
        };
        this.oauthService.initImplicitFlow();
        return false;
    }

    disconnect() {
        this.oauthService.logOut();
        this.router.navigate(['/home']);
        return false;
    }

    init() {
        var claims: any = this.oauthService.getIdentityClaims();
        if (!claims) {
            this.isConnected = false;;
            return;
        }

        this.name = claims.given_name;
        this.roles = claims.role;
        this.isConnected = true;
    }


    ngOnInit(): void {
        this.init();
        this.oauthService.events.subscribe((e: any) => {
            if (e.type === "logout") {
                this.isConnected = false;
            } else if (e.type === "token_received") {
                this.init();
            }
        });


        this.router.events.subscribe((opt: any) => {
            var url = opt.urlAfterRedirects;
            if (!url || this.expanded) {
                return;
            }

            if (url.startsWith('/prescription')) {
                this.expanded = true;
            }
        });
    }

    chooseLanguage(lng: string) {
        this.translate.use(lng);
        sessionStorage.setItem(medikitLanguageName, lng);
        this.activeLanguage = lng;
    }

    togglePrescriptions() {
        this.expanded = !this.expanded;
    }
}
