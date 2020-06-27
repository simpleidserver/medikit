using Medikit.Mobile.Controls;
using Medikit.Mobile.ViewModels.Steps;
using Xamarin.Forms.Xaml;

namespace Medikit.Mobile.Views.Steps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectInsuredPersonStep : StepBarProgressContentControl
    {
        public SelectInsuredPersonStep()
        {
            InitializeComponent();
            BindingContext = new SelectInsuredPersonStepViewModel();
        }
    }
}