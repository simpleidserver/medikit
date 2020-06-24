using Xamarin.Forms;

namespace Medikit.Mobile.Services
{
    public class AlertService : IAlertService
    {
        private readonly Application _application;

        public AlertService()
        {
            _application = Application.Current;
        }

        public void DisplayAlert(string title, string message, string cancel)
        {
            _application.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
