// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        private static IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            var userProfile = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "ehealth", "keystore");
            var serviceCollection = new ServiceCollection().AddEHealth(o =>
            {
                o.IdentityCertificateStore = Path.Combine(userProfile, "SSIN=89100739573 20200508-155826.acc-p12");
                o.IdentityCertificateStorePassword = "AJH9ka/fh%.?75WF";
                o.OrgCertificateStore = Path.Combine(userProfile, "CBE=0543979265 20200417-143522.acc-p12");
                o.OrgCertificateStorePassword = "AJH9ka/fh%.?75WF";
            });
            _serviceProvider = serviceCollection.BuildServiceProvider();
            /*
            BuildSTSIdentityRequestEID().ContinueWith((s) =>
            {
                string sss = "";
            });
            */
            BuildSTSIdentityRequest().ContinueWith((s) =>
            {
                // AddPrescription(s.Result.Body.Response.Assertion);
                // GetPrescription();
                // GetOpenedPrescriptions();
                // RejectPrescription();
                // GetPrescriptionsHistory();
                // GetBoxInfo();
                // GetMessagesList();
                // SendMessage();
            });
            /*
            GetKGSS().ContinueWith((s) =>
            {

            });
            */
            /*
            FindAmpByPackageName().ContinueWith((s) =>
            {

            });
            /*
            FindCNKCivics().ContinueWith((_) =>
            {

            });
            FindReimbursement().ContinueWith((_) =>
            {

            });
            FindAmppByPackageName().ContinueWith((_) =>
            {
                
            });
            */
            System.Console.ReadLine();
        }
    }
}
