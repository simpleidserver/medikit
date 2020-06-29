// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.DICS;
using Medikit.EHealth.Services.DICS.Request;
using Medikit.EHealth.Services.DICS.Response;
using Medikit.EHealth.SOAP.DTOs;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        private static async Task<SOAPEnvelope<DICSFindAmpResponseBody>> FindAmpByPackageName()
        {
            var dicsClient = (IDicsService)_serviceProvider.GetService(typeof(IDicsService));
            var result = await dicsClient.FindAmp(new DICSFindAmpRequest
            {
                FindByPackage = new DICSFindByPackage
                {
                    AnyNamePart = "clamoxyl"
                }
            });
            return result;
        }
        private static async Task FindAmppByPackageName()
        {
            var dicsClient = (IDicsService)_serviceProvider.GetService(typeof(IDicsService));
            await dicsClient.FindAmpp(new DICSFindAmppRequest
            {
                FindByPackage = new DICSFindByPackage
                {
                    AnyNamePart = "clamoxyl"
                }
            });
        }

        private static async Task FindReimbursement()
        {
            var dicsClient = (IDicsService)_serviceProvider.GetService(typeof(IDicsService));
            await dicsClient.FindReimbursement(new DICSFindReimbursementRequest
            {
                FindByPackage = new DICSFindByPackage
                {
                    AnyNamePart = "clamoxyl"
                }
            });
        }

        private static async Task<SOAPEnvelope<DICSFindAmpResponseBody>> FindAmpByDelivery()
        {
            var dicsClient = (IDicsService)_serviceProvider.GetService(typeof(IDicsService));
            var result = await dicsClient.FindAmp(new DICSFindAmpRequest
            {
                FindByDmpp = new DICSFindByDmpp
                {
                    DeliveryEnvironment = "P",
                    CodeType = "CNK",
                    Code = "0895540"
                }
            });
            return result;
        }
    }
}
