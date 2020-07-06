// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using Medikit.EHealth.Services.DICS;
using Medikit.EHealth.Services.DICS.Request;
using Medikit.EHealth.Services.DICS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.EHealth.EHealthServices
{
    public class EHealthAmpService : IEHealthAmpService
    {
        private readonly IDicsService _dicsService;

        public EHealthAmpService(IDicsService dicsService)
        {
            _dicsService = dicsService;
        }

        public async Task<SearchEHealthQueryResult<AmppResult>> SearchMedicinalPackage(SearchAmpRequest request, CancellationToken token)
        {
            var issueInstant = DateTime.UtcNow;
            var soapResponse = await _dicsService.FindAmp(new DICSFindAmpRequest
            {
                FindByPackage = new DICSFindByPackage
                {
                    AnyNamePart = request.ProductName
                }
            });
            var amppLst = soapResponse.Body.Response.Amp.SelectMany(_ => _.AmppLst).Where(_ => _.Status == "AUTHORIZED").ToList();
            for(var y = amppLst.Count() - 1; y >= 0; y--)
            {
                var ampp = amppLst.ElementAt(y);
                if (request.IsCommercialised != null && ((ampp.Commercialization == null && request.IsCommercialised.Value) || (ampp.Commercialization != null && !request.IsCommercialised.Value)))
                {
                    amppLst.RemoveAt(y);
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(request.DeliveryEnvironment))
                {
                    // Note : Only public !
                    ampp.DmppLst = ampp.DmppLst.Where(p => request.DeliveryEnvironment == p.DeliveryEnvironment && p.CodeType == "CNK").ToList();
                    if (!ampp.DmppLst.Any())
                    {
                        amppLst.RemoveAt(y);
                    }
                }
            }

            var count = amppLst.Count();
            amppLst = amppLst.OrderBy(a => a.PackDisplayValue).Skip(request.StartIndex).Take(request.Count).ToList();
            return new SearchEHealthQueryResult<AmppResult>
            {
                StartIndex = request.StartIndex,
                Count = count,
                Content = ToResult(amppLst)
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
                           Names = a.Name.Select(t => new EHealthTranslationResult
                           {
                               Language = t.Lang,
                               Value = t.Content
                           }).ToList(),
                           OfficialName = a.OfficialName,
                           AmppLst = ToResult(a.AmppLst).ToList()
                       }
                   ).ToList();
        }    

        public ICollection<AmppResult> ToResult(List<DICSAmpp> amppLst)
        {
            return amppLst.Select(b => new AmppResult
            {
                CrmUrlLst = b.CrmUrls == null ? new List<EHealthTranslationResult>() : b.CrmUrls.Select(_ => new EHealthTranslationResult { Language = _.Lang, Value = _.Content }).ToList(),
                SpcUrlLst = b.SpcUrls == null ? new List<EHealthTranslationResult>() : b.SpcUrls.Select(_ => new EHealthTranslationResult { Language = _.Lang, Value = _.Content }).ToList(),
                LeafletUrlLst = b.LeafletUrls == null ? new List<EHealthTranslationResult>() : b.LeafletUrls.Select(_ => new EHealthTranslationResult { Language = _.Lang, Value = _.Content }).ToList(),
                PrescriptionNames = b.PrescriptionNames.Select(t => new EHealthTranslationResult
                {
                    Language = t.Lang,
                    Value = t.Content
                }).ToList(),
                DeliveryMethods = b.DmppLst.Select(d => new DmppResult
                {
                    Code = d.Code,
                    CodeType = d.CodeType,
                    DeliveryEnvironment = d.DeliveryEnvironment,
                    Price = d.Price,
                    Reimbursable = d.Reimbursable
                }).ToList()
            }).ToList();
        }
    }
}
