using Medikit.Mobile.Services;
using Medikit.Mobile.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Medikit.Mobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly CertificatesPage _certificatesPage;
        private readonly INavigationService _navigation;

        public SettingsViewModel()
        {
            _certificatesPage = new CertificatesPage();
            _navigation = DependencyService.Get<INavigationService>();
            ConfigureEHealthSessionCommand = new Command(async () => await HandleConfigureEHealthSessionCommand());
        }

        public ICommand ConfigureEHealthSessionCommand { get; private set; }

        private async Task HandleConfigureEHealthSessionCommand()
        {
            await _navigation.PushAsync(_certificatesPage);
        }
    }
}
