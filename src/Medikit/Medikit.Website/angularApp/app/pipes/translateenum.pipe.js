var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, Pipe, ChangeDetectorRef } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
var TranslateEnumPipe = (function () {
    function TranslateEnumPipe(translateService, _ref) {
        this.translateService = translateService;
        this._ref = _ref;
        this.lastValue = null;
        this.value = null;
    }
    TranslateEnumPipe.prototype.transform = function (translations) {
        var _this = this;
        if (!translations || translations.length === 0) {
            return;
        }
        if (this.lastValue && this.value && this.lastValue == this.value) {
            return this.value;
        }
        var onTranslation = function (translations, lng) {
            var filteredTranslations = translations.filter(function (tr) {
                return tr.Language == lng;
            });
            if (filteredTranslations.length == 0) {
                _this.value = "unknown";
                return;
            }
            _this.value = filteredTranslations[0].Value;
            _this._ref.markForCheck();
        };
        this._dispose();
        var defaultLang = this.translateService.getDefaultLang();
        if (this.value == null) {
            onTranslation(translations, defaultLang);
        }
        if (!this.onLangChange) {
            this.onLangChange = this.translateService.onLangChange.subscribe(function (lng) {
                if (_this.lastValue) {
                    _this.lastValue = null;
                    onTranslation(translations, lng.lang);
                }
            });
        }
        if (!this.onDefaultLangChange) {
            this.onDefaultLangChange = this.translateService.onDefaultLangChange.subscribe(function (lng) {
                if (_this.lastValue) {
                    _this.lastValue = null;
                    onTranslation(translations, lng.lang);
                }
            });
        }
        this.lastValue = this.value;
        return this.value;
    };
    TranslateEnumPipe.prototype._dispose = function () {
        if (typeof this.onLangChange !== 'undefined') {
            this.onLangChange.unsubscribe();
            this.onLangChange = undefined;
        }
        if (typeof this.onDefaultLangChange !== 'undefined') {
            this.onDefaultLangChange.unsubscribe();
            this.onDefaultLangChange = undefined;
        }
    };
    TranslateEnumPipe.prototype.ngOnDestroy = function () {
        this._dispose();
    };
    TranslateEnumPipe = __decorate([
        Injectable(),
        Pipe({
            name: 'translateenum',
            pure: false
        }),
        __metadata("design:paramtypes", [TranslateService, ChangeDetectorRef])
    ], TranslateEnumPipe);
    return TranslateEnumPipe;
}());
export { TranslateEnumPipe };
//# sourceMappingURL=translateenum.pipe.js.map