using Medikit.Mobile.Services;
using Medikit.Mobile.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Medikit.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        public HomeViewModel()
        {
            _navigation = DependencyService.Get<INavigationService>();
            ConfigureEHealthSessionCommand = new Command(async () => await HandleConfigureEHealthSessionCommand());
        }

        public ICommand ConfigureEHealthSessionCommand { get; private set; }

        private async Task HandleConfigureEHealthSessionCommand()
        {
            await _navigation.PushAsync(new CertificatesPage());
        }
    }
}
