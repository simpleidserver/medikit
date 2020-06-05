// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Medikit.Api.Acceptance.Tests.Steps
{
    [Binding]
    public class WebApiSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private static object _obj = new object();
        private static CustomWebApplicationFactory<FakeStartup> _factory;
        private static HttpClient _client;
        private static EventWaitHandle Evt = new EventWaitHandle(true, EventResetMode.AutoReset);

        public WebApiSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            lock(_obj)
            {
                Evt.WaitOne();
                if (_factory == null)
                {
                    _factory = new CustomWebApplicationFactory<FakeStartup>(c => { });
                    _client = _factory.CreateClient();
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Evt.Set();
        }

        [When("execute HTTP GET request '(.*)'")]
        public async Task WhenExecuteHTTPGETRequest(string url)
        {
            url = Parse(url);
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            var httpResponseMessage = await _client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            _scenarioContext.Set(httpResponseMessage, "httpResponseMessage");
        }

        [When("execute HTTP DELETE request '(.*)'")]
        public async Task WhenExecuteHTTPDELETERequest(string url)
        {
            url = Parse(url);
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };
            var httpResponseMessage = await _client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            _scenarioContext.Set(httpResponseMessage, "httpResponseMessage");
        }

        [When("execute HTTP POST JSON request '(.*)'")]
        public async Task WhenExecuteHTTPPostJSONRequest(string url, Table table)
        {
            var jObj = new JObject();
            foreach (var record in table.Rows)
            {
                var key = record["Key"];
                var value = Parse(record["Value"]);
                try
                {
                    jObj.Add(key, JToken.Parse(value));
                }
                catch
                {
                    jObj.Add(key, value.ToString());
                }
            }

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Parse(url)),
                Content = new StringContent(jObj.ToString(), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await _client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            _scenarioContext.Set(httpResponseMessage, "httpResponseMessage");
        }

        [When("execute HTTP PUT JSON request '(.*)'")]
        public async Task WhenExecuteHTTPPutJSONRequest(string url, Table table)
        {
            var jObj = new JObject();
            foreach (var record in table.Rows)
            {
                var key = record["Key"];
                var value = Parse(record["Value"]);
                try
                {
                    jObj.Add(key, JToken.Parse(value));
                }
                catch
                {
                    jObj.Add(key, value.ToString());
                }
            }

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(Parse(url)),
                Content = new StringContent(jObj.ToString(), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await _client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            _scenarioContext.Set(httpResponseMessage, "httpResponseMessage");
        }

        private string Parse(string val)
        {
            var regularExpression = new Regex(@"\$([a-zA-Z]|_)*\$");
            var result = regularExpression.Replace(val, (m) =>
            {
                if (string.IsNullOrWhiteSpace(m.Value))
                {
                    return string.Empty;
                }

                return _scenarioContext.Get<string>(m.Value.TrimStart('$').TrimEnd('$'));
            });
            return result;
        }
    }
}
