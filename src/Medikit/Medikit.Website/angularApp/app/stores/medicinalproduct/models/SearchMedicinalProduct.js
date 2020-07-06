import { MedicinalPackage } from "./MedicinalPackage";
var SearchMedicinalProduct = (function () {
    function SearchMedicinalProduct() {
        this.Content = [];
    }
    SearchMedicinalProduct.fromJson = function (json) {
        var result = new SearchMedicinalProduct();
        result.Count = json["count"];
        result.StartIndex = json["start_index"];
        json["content"].forEach(function (r) {
            result.Content.push(MedicinalPackage.fromJson(r));
        });
        return result;
    };
    return SearchMedicinalProduct;
}());
export { SearchMedicinalProduct };
//# sourceMappingURL=SearchMedicinalProduct.js.map