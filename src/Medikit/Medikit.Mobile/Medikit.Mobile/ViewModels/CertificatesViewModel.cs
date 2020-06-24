using Medikit.Mobile.Models;
using Medikit.Mobile.Resources;
using Medikit.Mobile.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Medikit.Mobile.ViewModels
{
    public class CertificatesViewModel : BaseViewModel
    {
        private CertificateViewModel _activeCertificate;
        private readonly INavigationService _navigation;
        private readonly ICertificateStore _certificateStore;
        private readonly IAlertService _alertService;

        public CertificatesViewModel()
        {
            _navigation = DependencyService.Resolve<INavigationService>();
            _certificateStore = DependencyService.Resolve<ICertificateStore>();
            _alertService = DependencyService.Resolve<IAlertService>();
            UploadCertificateCommand = new Command(async () => await HandleUploadCertificateCommand());
            LoadCertificatesCommand = new Command(async () => await Load());
            DeleteCertificateCommand = new Command(async () => await HandleDeleteCertificateCommand(), CanDelete);
            ChooseCertificateCommand = new Command(async () => await HandleChooseCertificateCommand(), CanChooseCertificate);
            Certificates = new ObservableCollection<CertificateViewModel>();
        }

        public ICommand UploadCertificateCommand { get; private set; }
        public ICommand LoadCertificatesCommand { get; private set; }
        public ICommand DeleteCertificateCommand { get; private set; }
        public ICommand ChooseCertificateCommand { get; private set; }
        public ObservableCollection<CertificateViewModel> Certificates { get; private set; }
        public CertificateViewModel ActiveCertificate
        {
            get { return _activeCertificate; }
            set 
            { 
                SetProperty(ref _activeCertificate, value);
                ((Command)ChooseCertificateCommand).ChangeCanExecute();
            }
        }

        private bool CanDelete()
        {
            return Certificates.Any(_ => _.IsSelected);
        }

        private bool CanChooseCertificate()
        {
            return _activeCertificate != null;
        }

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
                    var value = result?.Text ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }

                    var splitted = value.Split('$');
                    if (splitted.Count() != 3)
                    {
                        return;
                    }

                    var url = splitted.First();
                    var password = splitted[1];
                    var name = splitted[2];
                    using (var httpClient = new HttpClient())
                    {
                        var request = new HttpRequestMessage
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Get
                        };
                        var httpResult = await httpClient.SendAsync(request);
                        var json = await httpResult.Content.ReadAsStringAsync();
                        var base64 = JsonConvert.DeserializeObject<JObject>(json)["file"].ToString();
                        await _certificateStore.Add(new MedikitCertificate
                        {
                            CreateDateTime = DateTime.UtcNow,
                            IsSelected = false,
                            Name = name,
                            Password = password,
                            Payload = base64,
                            Type = MedikitCertificateTypes.IDENTITY
                        });
                        Certificates.Add(new CertificateViewModel
                        {
                            IsSelected = false,
                            Name = name
                        });
                        await _navigation.PopAsync();
                        _alertService.DisplayAlert(AppResources.Success, AppResources.CertificateAdded, AppResources.Ok);
                    }
                });
            };
            scanPage.Title = AppResources.ScanCertificate;
            await _navigation.PushAsync(scanPage);
        }

        private async Task HandleChooseCertificateCommand()
        {
            if (_activeCertificate == null)
            {
                return;
            }

            var certificate = await _certificateStore.Get(_activeCertificate.Name);
            if (certificate == null)
            {
                return;
            }

            certificate.IsSelected = true;
            await _certificateStore.Update(certificate);
        }

        private async Task Load()
        {
            IsBusy = true;
            var certificates = await _certificateStore.GetIdentityCertificates();
            Certificates.Clear();
            foreach(var certificate in certificates)
            {
                var record = new CertificateViewModel
                {
                    IsSelected = false,
                    Name = certificate.Name,
                    CreateDateTime = certificate.CreateDateTime
                };
                Certificates.Add(record);
                if (certificate.IsSelected)
                {
                    ActiveCertificate = record;
                }
            }

            ((Command)DeleteCertificateCommand).ChangeCanExecute();
            IsBusy = false;
        }

        private async Task HandleDeleteCertificateCommand()
        {
            var names = Certificates.Where(_ => _.IsSelected).Select(_ => _.Name);
            if (!names.Any())
            {
                return;
            }

            foreach(var name in names)
            {
                await _certificateStore.Remove(name);
                Certificates.Remove(Certificates.First(_ => _.Name == name));
            }

            foreach (var certificate in Certificates)
            {
                certificate.IsSelected = false;
            }

            ((Command)DeleteCertificateCommand).ChangeCanExecute();
        }
    }
}
