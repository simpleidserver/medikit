// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Services.Parameters;
using Medikit.Api.Application.Services.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Services.InMemory
{
    public class InMemoryAmpService : IAmpService
    {
        private static List<AmpResult> AMP_LST = new List<AmpResult>
        {
            new AmpResult
            {
                Code = "137742-01",
                OfficialName = "Dafalgan 500 mg",
                AmppLst = new List<AmppResult>
                {
                    new AmppResult
                    {
                        DeliveryMethods = new List<DmppResult>
                        {
                            new DmppResult
                            {
                                DeliveryEnvironment = "P",
                                CodeType = "CNK",
                                Code = "2933901"
                            }
                        },
                        PrescriptionNames = new List<TranslationResult>
                        {
                            new TranslationResult
                            {
                                Language = "fr",
                                Value = "Dafalgan compr. efferv. (séc.) 20x 500mg"
                            },
                            new TranslationResult
                            {
                                Language = "nl",
                                Value = "Dafalgan compr. efferv. (séc.) 20x 500mg"
                            }
                        }
                    }
                }
            },
        };

        public Task<SearchResult<AmpResult>> SearchByMedicinalPackageName(SearchAmpRequest request, CancellationToken token)
        {
            ICollection<AmpResult> result = AMP_LST.Where(amp => amp.Names.Any(n => n.Value.ToLowerInvariant().Contains(request.ProductName.ToLowerInvariant()))).ToList();
            return Task.FromResult(new SearchResult<AmpResult>
            {
                StartIndex = 0,
                Count = 2,
                Content = result
            });
        }

        public Task<AmpResult> SearchByCnkCode(string deliveryEnvironment, string cnk, CancellationToken token)
        {
            var result = AMP_LST.FirstOrDefault(amp => amp.AmppLst.Any(a => a.DeliveryMethods.Any(d => d.CodeType == "CNK" && d.DeliveryEnvironment == deliveryEnvironment && d.Code == cnk)));
            return Task.FromResult(result);
        }
    }
}
