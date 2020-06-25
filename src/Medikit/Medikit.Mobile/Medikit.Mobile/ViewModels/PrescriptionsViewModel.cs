using Medikit.EHealth.SAML;
using Medikit.Mobile.Infrastructure;
using Medikit.Mobile.Services;
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

        public PrescriptionsViewModel(ISessionService sessionService, IPrescriptionService prescriptionService)
        {
            _sessionService = sessionService;
            _prescriptionService = prescriptionService;
            LoadPrescriptionsCommand = new AsyncCommand(Load);
            Prescriptions = new ObservableCollection<PrescriptionViewModel>();
        }

        public IAsyncCommand LoadPrescriptionsCommand { get; private set; }
        public ObservableCollection<PrescriptionViewModel> Prescriptions { get; set; }

        public async Task Load()
        {
            IsBusy = true;
            Prescriptions.Clear();
            var session = _sessionService.GetSession();
            if (session == null)
            {
                try
                {
                    session = await _sessionService.BuildFallbackSession();
                }
                catch
                {
                    IsBusy = false;
                    return;
                }
            }

            var assertion = session.Body.Response.Assertion;
            var result = await _prescriptionService.GetOpenedPrescriptions("76020727360", assertion.Serialize().ToString());
            foreach(var prescription in result)
            {
                Prescriptions.Add(new PrescriptionViewModel
                {
                    RID = prescription
                });
            }
            IsBusy = false;
        }
    }
}
