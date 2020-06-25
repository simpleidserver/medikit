using System.Threading.Tasks;
using System.Windows.Input;

namespace Medikit.Mobile.Infrastructure
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
