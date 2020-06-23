using Medikit.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medikit.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CertificatesPage : ContentPage
    {
        public CertificatesPage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Get<CertificatesViewModel>();
        }
    }
}