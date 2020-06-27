using Medikit.EHealth.SAML;
using Medikit.Mobile.Services;
using Medikit.Mobile.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Medikit.Mobile.ViewModels
{
    public class PrescriptionsViewModel : BaseViewModel
    {
        private readonly ISessionService _sessionService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly INavigationService _navigationService;

        public PrescriptionsViewModel(INavigationService navigationService, ISessionService sessionService, IPrescriptionService prescriptionService)
        {
            _navigationService = navigationService;
            _sessionService = sessionService;
            _prescriptionService = prescriptionService;
            LoadPrescriptionsCommand = new Command(Load);
            AddPrescriptionCommand = new Command(HandleAddPrescriptionCommand);
            Prescriptions = new ObservableCollection<PrescriptionViewModel>();
        }

        public ICommand LoadPrescriptionsCommand { get; private set; }
        public ICommand AddPrescriptionCommand { get; private set; }
        public ObservableCollection<PrescriptionViewModel> Prescriptions { get; set; }

        public void Load()
        {
            IsBusy = true;
            Prescriptions.Clear();
            Task.Factory.StartNew(async () =>
            {
                var session = _sessionService.GetSession();
                if (session == null)
                {
                    session = await _sessionService.BuildFallbackSession();
                }
                               
                var assertion = session.Body.Response.Assertion;
                var result = await _prescriptionService.GetOpenedPrescriptions("76020727360", assertion.Serialize().ToString());
                return result;

            }).ContinueWith(_ =>
            {
                if(_.Exception == null)
                {
                    var result = _.Result.Result;
                    foreach (var prescription in result)
                    {
                        Prescriptions.Add(new PrescriptionViewModel
                        {
                            RID = prescription
                        });
                    }
                }

                IsBusy = false;
            });
        }

        private async void HandleAddPrescriptionCommand()
        {
            await _navigationService.PushAsync(new AddCertificatePage());
        }
    }
}
