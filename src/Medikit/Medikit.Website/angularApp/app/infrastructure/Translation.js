var Translation = (function () {
    function Translation() {
    }
    Translation.fromJson = function (json) {
        var translation = new Translation();
        translation.Language = json["language"];
        translation.Value = json["value"];
        return translation;
    };
    return Translation;
}());
export { Translation };
//# sourceMappingURL=Translation.js.map