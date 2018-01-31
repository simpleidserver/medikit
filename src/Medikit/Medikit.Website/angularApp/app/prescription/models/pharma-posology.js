var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var PharmaPosology = (function () {
    function PharmaPosology(Type) {
        this.Type = Type;
    }
    return PharmaPosology;
}());
export { PharmaPosology };
var PharmaPosologyFreeText = (function (_super) {
    __extends(PharmaPosologyFreeText, _super);
    function PharmaPosologyFreeText() {
        return _super.call(this, "freetext") || this;
    }
    PharmaPosologyFreeText.fromJson = function (json) {
        var result = new PharmaPosologyFreeText();
        result.Content = json["content"];
        return result;
    };
    return PharmaPosologyFreeText;
}(PharmaPosology));
export { PharmaPosologyFreeText };
var PharmaPosologyStructured = (function (_super) {
    __extends(PharmaPosologyStructured, _super);
    function PharmaPosologyStructured() {
        return _super.call(this, "structured") || this;
    }
    return PharmaPosologyStructured;
}(PharmaPosology));
export { PharmaPosologyStructured };
var PharmaPosologyTakes = (function () {
    function PharmaPosologyTakes() {
    }
    return PharmaPosologyTakes;
}());
export { PharmaPosologyTakes };
//# sourceMappingURL=pharma-posology.js.map