using Medikit.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medikit.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CertificatesPage : ContentPage
    {
        private CertificatesViewModel _viewModel;

        public CertificatesPage()
        {
            _viewModel = App.ServiceProvider.GetService<CertificatesViewModel>();
            InitializeComponent();
            BindingContext =  _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadCertificatesCommand.Execute(null);
        }

        private void HandleCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ((Command)_viewModel.DeleteCertificateCommand).ChangeCanExecute();
        }
    }
}