using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Medikit.Mobile.ViewModels
{
    public class BaseStepProgressBarViewModel : BaseViewModel
    {
        public BaseStepProgressBarViewModel()
        {
            PreviousCommand = new Command(HandlePrevious);
            NextCommand = new Command(HandleNext);
        }

        public event EventHandler PreviousStepNavigated;
        public event EventHandler NextStepNavigated;

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

        private void HandlePrevious()
        {
            if (PreviousStepNavigated != null)
            {
                PreviousStepNavigated(this, EventArgs.Empty);
            }
        }

        private void HandleNext()
        {
            if (NextStepNavigated != null)
            {
                NextStepNavigated(this, EventArgs.Empty);
            }
        }
    }
}
