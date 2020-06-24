using System;

namespace Medikit.Mobile.ViewModels
{
    public class CertificateViewModel : BaseViewModel
    {
        private bool _isSelected;

        public string Name { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public DateTime CreateDateTime { get; set; }
    }
}
