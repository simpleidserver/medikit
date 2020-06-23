using Medikit.Mobile.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Medikit.Mobile.ViewModels
{
    public class CertificatesViewModel
    {
        private readonly INavigationService _navigation;

        public CertificatesViewModel()
        {
            _navigation = DependencyService.Resolve<INavigationService>();
            UploadCertificateCommand = new Command(async() => await HandleUploadCertificateCommand());
        }

        public ICommand UploadCertificateCommand { get; private set; }

        private async Task HandleUploadCertificateCommand()
        {
            var expectedFormat = ZXing.BarcodeFormat.QR_CODE;
            var opts = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                PossibleFormats = new List<ZXing.BarcodeFormat> { expectedFormat }
            };
            var scanPage = new ZXingScannerPage(opts);
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _navigation.PopAsync();
                    var value = result?.Text ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }

                    var splitted = value.Split('.');
                    if (splitted.Count() != 2)
                    {
                        return;
                    }

                    var url = splitted.First();
                    var password = splitted.Last();
                    using (var httpClient = new HttpClient())
                    {
                        var request = new HttpRequestMessage
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Get
                        };
                        var httpResult = await httpClient.SendAsync(request);
                        var json = await httpResult.Content.ReadAsStringAsync();
                        var file = JsonConvert.DeserializeObject<JObject>(json)["file"];

                    }
                    // var format = result?.BarcodeFormat.ToString() ?? string.Empty;
                    // var value = result?.Text ?? string.Empty;
                    // 
                    // MainPage.Navigation.PopAsync();
                    // MainPage.DisplayAlert("Barcode Result", format + "|" + value, "OK");
                });
            };
            scanPage.Title = "Scan certificate";
            await _navigation.PushAsync(scanPage);
        }
    }
}
