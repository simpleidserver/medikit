using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Mobile.Services
{
    public interface IPrescriptionService
    {
        Task<ICollection<string>> GetOpenedPrescriptions(string patientNiss, string assertionToken);
    }
}
