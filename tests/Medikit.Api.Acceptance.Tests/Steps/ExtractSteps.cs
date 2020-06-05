// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Medikit.Api.Acceptance.Tests.Steps
{
    [Binding]
    public class ExtractSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ExtractSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [When("extract JSON from body")]
        public async Task WhenExtractFromBody()
        {
            var httpResponseMessage = _scenarioContext["httpResponseMessage"] as HttpResponseMessage;
            var json = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            _scenarioContext.Set(JsonConvert.DeserializeObject<JObject>(json), "jsonHttpBody");
        }

        [When("extract JSON from body into '(.*)'")]
        public async Task WhenExtractFromBodyIntoKey(string key)
        {
            var httpResponseMessage = _scenarioContext["httpResponseMessage"] as HttpResponseMessage;
            var json = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            _scenarioContext.Set(JsonConvert.DeserializeObject<JObject>(json), key);
        }

        [When("extract '(.*)' from JSON body into '(.*)'")]
        public void WhenExtractJSONKeyFromBody(string selector, string key)
        {
            var jsonHttpBody = _scenarioContext["jsonHttpBody"] as JObject;
            var val = jsonHttpBody.SelectToken(selector);
            if (val != null)
            {
                _scenarioContext.Set(val.ToString(), key);
            }
        }
    }
}
