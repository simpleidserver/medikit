namespace Medikit.Mobile.Services
{
    public interface IAlertService
    {
        void DisplayAlert(string title, string message, string cancel);
    }
}
