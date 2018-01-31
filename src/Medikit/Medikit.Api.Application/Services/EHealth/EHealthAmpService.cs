// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Services.Parameters;
using Medikit.Api.Application.Services.Results;
using Medikit.EHealth.Services.DICS;
using Medikit.EHealth.Services.DICS.Request;
using Medikit.EHealth.Services.DICS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Services.EHealth
{
    public class EHealthAmpService : IAmpService
    {
        private readonly IDicsService _dicsService;

        public EHealthAmpService(IDicsService dicsService)
        {
            _dicsService = dicsService;
        }

        public async Task<SearchResult<AmpResult>> SearchByMedicinalPackageName(SearchAmpRequest request, CancellationToken token)
        {
            var issueInstant = DateTime.UtcNow;
            var soapResponse = await _dicsService.FindAmp(new DICSFindAmpRequest
            {
                FindByPackage = new DICSFindByPackage
                {
                    AnyNamePart = request.ProductName
                }
            });
            var ampLst = soapResponse.Body.Response.Amp;
            for(var y = ampLst.Count() - 1; y >= 0; y--)
            {
                var amp = ampLst.ElementAt(y);
                if (request.IsCommercialised != null)
                {
                    amp.AmppLst = amp.AmppLst.Where(a => a.Commercialization != null && request.IsCommercialised.Value || a.Commercialization == null && !request.IsCommercialised.Value).ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.DeliveryEnvironment))
                {
                    for (var i = amp.AmppLst.Count() - 1; i >= 0; i--)
                    {
                        var ampp = amp.AmppLst[i];
                        ampp.DmppLst = ampp.DmppLst.Where(p => request.DeliveryEnvironment == p.DeliveryEnvironment && p.CodeType == "CNK").ToList();
                        if (!ampp.DmppLst.Any())
                        {
                            amp.AmppLst.RemoveAt(i);
                        }
                    }
                }

                if (!amp.AmppLst.Any())
                {
                    ampLst.RemoveAt(y);
                }
            }

            var count = ampLst.Count();
            ampLst = ampLst.OrderBy(a => a.OfficialName).Skip(request.StartIndex).Take(request.Count).ToList();
            return new SearchResult<AmpResult>
            {
                StartIndex = request.StartIndex,
                Count = count,
                Content = ToResult(ampLst)
            };
        }

        public async Task<AmpResult> SearchByCnkCode(string deliveryEnvironment, string cnk, CancellationToken token)
        {
            var soapResponse = await _dicsService.FindAmp(new DICSFindAmpRequest
            {
                FindByDmpp = new DICSFindByDmpp
                {
                    DeliveryEnvironment = deliveryEnvironment,
                    CodeType = "CNK",
                    Code = cnk
                }
            });
            if (!soapResponse.Body.Response.Amp.Any())
            {
                return null;
            }

            return ToResult(soapResponse.Body.Response.Amp).FirstOrDefault();
        }

        private ICollection<AmpResult> ToResult(List<DICSAmp> ampLst)
        {
            return ampLst.Select(a =>
                       new AmpResult
                       {
                           Code = a.Code,
                           Names = a.Name.Select(t => new TranslationResult
                           {
                               Language = t.Lang,
                               Value = t.Content
                           }).ToList(),
                           OfficialName = a.OfficialName,
                           AmppLst = a.AmppLst.Select(b => new AmppResult
                           {
                               PrescriptionNames = b.PrescriptionNames.Select(t => new TranslationResult
                               {
                                   Language = t.Lang,
                                   Value = t.Content
                               }).ToList(),
                               DeliveryMethods = b.DmppLst.Select(d => new DmppResult
                               {
                                   Code = d.Code,
                                   CodeType = d.CodeType,
                                   DeliveryEnvironment = d.DeliveryEnvironment
                               }).ToList()
                           }).ToList()
                       }
                   ).ToList();
        }    
    }
}
