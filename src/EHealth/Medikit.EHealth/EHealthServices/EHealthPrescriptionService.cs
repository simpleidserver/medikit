// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using Medikit.EHealth.Enums;
using Medikit.EHealth.Services.Recipe;
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using Medikit.EHealth.Services.Recipe.Request;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.EHealth.EHealthServices
{
    public class EHealthPrescriptionService : IEHealthPrescriptionService
    {
        private readonly IRecipeService _recipeService;

        public EHealthPrescriptionService(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPrescriptionsParameter parameter, CancellationToken token)
        {
            var result = await  _recipeService.GetOpenedPrescriptions(parameter.PatientNiss, new Page { PageNumber = parameter.PageNumber }, parameter.Assertion);
            return result.Prescriptions;
        }

        public async Task<PharmaceuticalPrescriptionResult> GetPrescription(GetPrescriptionParameter parameter, CancellationToken token)
        {
            var result = await _recipeService.GetPrescription(parameter.PrescriptionId, parameter.Assertion);
            var folder = result.KmehrmessageType.Items.First() as folderType;
            return new PharmaceuticalPrescriptionResult
            {
                Id = result.Rid,
                CreateDateTime = result.CreationDate,
                EndExecutionDate = result.ExpirationDate,
                PatientNiss = folder.patient.id.First(_ => _.S == IDPATIENTschemes.IDPATIENT).Value,
                Medications = folder.transaction.Select(_ => ToPharmaMedication(_)).ToList(),
                PrescriptionType = PrescriptionTypes.P0
            };
        }


        private static PharmaceuticalPrescriptionMedication ToPharmaMedication(transactionType transaction)
        {
            var headingType = transaction.Items.First() as headingType;
            var itemType = headingType.Items.First() as itemType;
            var medicinalProduct = itemType.content.First().Items.First() as medicinalProductType;
            var result = new PharmaceuticalPrescriptionMedication
            {
                PackageCode = medicinalProduct.intendedcd.First().Value
            };
            if (itemType.posology.ItemsElementName.Any(_ => _ == ItemsChoiceType3.text))
            {
                var textType = itemType.posology.Items.First() as textType;
                result.Posology = new PharmaceuticalPrescriptionFreeTextPosologyResult
                {
                    Content = textType.Value
                };
            }
            else
            {

            }

            return result;
        }
    }
}
