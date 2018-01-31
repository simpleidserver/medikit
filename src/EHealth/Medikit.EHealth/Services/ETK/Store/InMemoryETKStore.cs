using Medikit.EHealth.Services.ETK.Store;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Medikit.EHealth.ETK.Store
{
    public class InMemoryETKStore : IETKStore
    {
        private readonly ConcurrentDictionary<string, ETKModel> _dic;

        public InMemoryETKStore()
        {
            _dic = new ConcurrentDictionary<string, ETKModel>();
        }

        public Task<ETKModel> Get(string type, string value, string applicationId)
        {
            ETKModel result = null;
            if (!_dic.TryGetValue(BuildId(type, value, applicationId), out result))
            {
                return Task.FromResult(result);
            }

            return Task.FromResult(result);
        }

        public Task<bool> Add(string type, string value, string applicationId, X509Certificate2 certificate, string etk)
        {
            return Task.FromResult(_dic.TryAdd(BuildId(type, value, applicationId), new ETKModel(certificate, etk)));
        }

        private static string BuildId(string type, string value, string applicationId)
        {
            return $"{type}.{value}.{applicationId}";
        }
    }
}