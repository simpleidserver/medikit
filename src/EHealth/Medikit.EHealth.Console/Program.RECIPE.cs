// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.Recipe;
using Medikit.EHealth.Services.Recipe.Request;
using System;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        public static async Task GetPrescription()
        {
            var recipeService = (IRecipeService)_serviceProvider.GetService(typeof(IRecipeService));
            await recipeService.GetPrescription("BEP0SGEZ8L77");
        }

        public static async Task GetOpenedPrescriptions()
        {
            var recipeService = (IRecipeService)_serviceProvider.GetService(typeof(IRecipeService));
            await recipeService.GetOpenedPrescriptions("76020727360", new Page());
        }

        public static async Task AddPrescription(SAMLAssertion assertion)
        {
            var recipeService = (IRecipeService)_serviceProvider.GetService(typeof(IRecipeService));
            await recipeService.CreatePrescription("P0", "76020727360", DateTime.Parse("2020-06-25"), null, assertion);
        }

        public static async Task RejectPrescription()
        {
            var recipeService = (IRecipeService)_serviceProvider.GetService(typeof(IRecipeService));
            await recipeService.RevokePrescription("BEP011D2G1BZ", "bad");
        }
    }
}
