using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Medikit.Mobile.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MedikitMobileOptions _options;

        public PrescriptionService(IHttpClientFactory httpClientFactory, IOptions<MedikitMobileOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<ICollection<string>> GetOpenedPrescriptions(string patientNiss, string assertionToken)
        {
            using (var httpClient = _httpClientFactory.CreateClient("apiClient"))
            {
                var json = new JObject
                {
                    { "patient_niss", patientNiss },
                    { "assertion_token", assertionToken }
                };
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"{_options.ApiUrl}/prescriptions/opened"),
                    Method = HttpMethod.Post,
                    Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json")
                };
                var httpResult = await httpClient.SendAsync(request);
                var jsonResult = await httpResult.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<string>>(jsonResult);
            }
        }
    }
}
