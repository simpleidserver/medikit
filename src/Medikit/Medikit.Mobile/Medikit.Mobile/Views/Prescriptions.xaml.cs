using Medikit.Mobile.Extensions;
using Medikit.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medikit.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Prescriptions : ContentPage
    {
        private PrescriptionsViewModel _viewModel;

        public Prescriptions()
        {
            _viewModel = App.ServiceProvider.GetService<PrescriptionsViewModel>();
            InitializeComponent();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // https://johnthiriet.com/mvvm-going-async-with-async-command/ : Add async command.
            _viewModel.LoadPrescriptionsCommand.ExecuteAsync().FireAndForgetSafeAsync();
        }
    }
}