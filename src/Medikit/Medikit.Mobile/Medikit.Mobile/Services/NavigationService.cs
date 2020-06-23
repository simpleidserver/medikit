using System.Threading.Tasks;
using Xamarin.Forms;

namespace Medikit.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Application _application;

        public NavigationService()
        {
            _application = Application.Current;
        }


        public void InsertPageBefore(Page page, Page before)
        {
            _application.MainPage.Navigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return _application.MainPage.Navigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return _application.MainPage.Navigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return _application.MainPage.Navigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return _application.MainPage.Navigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return _application.MainPage.Navigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return _application.MainPage.Navigation.PopToRootAsync();
        }

        public Task PushAsync(Page page)
        {
            return _application.MainPage.Navigation.PushAsync(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            return _application.MainPage.Navigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(Page page)
        {
            return _application.MainPage.Navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return _application.MainPage.Navigation.PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            _application.MainPage.Navigation.RemovePage(page);
        }
    }
}
