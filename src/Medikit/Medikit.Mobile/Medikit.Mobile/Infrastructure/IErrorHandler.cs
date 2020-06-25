using System;

namespace Medikit.Mobile.Infrastructure
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
