// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient.Commands.Handlers
{
    public class AddPatientCommandHandler : IAddPatientCommandHandler
    {
        private readonly ICommitAggregateHelper _commitAggregateHelper;
        private readonly MedikitServerOptions _options;

        public AddPatientCommandHandler(ICommitAggregateHelper commitAggregateHelper, IOptions<MedikitServerOptions> options)
        {
            _commitAggregateHelper = commitAggregateHelper;
            _options = options.Value;
        }

        public async Task<string> Handle(AddPatientCommand command, CancellationToken cancellationToken)
        {
            var patientAddresses = command.Addresses == null ? new List<PatientAddress>() : command.Addresses.Select(_ =>
                new PatientAddress
                {
                    Box = _.Box,
                    Country = _.Country,
                    PostalCode = _.PostalCode,
                    Street = _.Street,
                    StreetNumber = _.StreetNumber
                }
            ).ToList();
            var contactInformations = command.ContactInformations == null ? new List<PatientContactInformation>() : command.ContactInformations.Select(_ =>
            new PatientContactInformation
            {
                Type = _.Type,
                Value = _.Value
            }).ToList();
            var img = ConvertImage(command.LogoUrl);
            string logoUrl = null;
            if (img != null)
            {
                logoUrl = Path.Combine(_options.RootPath, "images", $"{command.LogoUrl}.png");
                await System.IO.File.WriteAllBytesAsync(logoUrl, img, cancellationToken);
            }
            
            var id = Guid.NewGuid().ToString();
            var patient = PatientAggregate.New(id, command.PrescriberId, command.Firstname, command.Lastname, command.NationalIdentityNumber, command.Gender, command.BirthDate, logoUrl, command.EidCardNumber, command.EidCardValidity, patientAddresses, contactInformations);
            var streamName = patient.GetStreamName();
            await _commitAggregateHelper.Commit(patient, streamName, Constants.QueueNames.Patient);
            return id;
        }

        private byte[] ConvertImage(string img)
        {
            if (string.IsNullOrWhiteSpace(img))
            {
                return null;
            }

            var regex = new Regex("data:image\\/(.*);base64,");
            if (regex.IsMatch(img))
            {
                var base64 = regex.Replace(img, string.Empty);
                return Convert.FromBase64String(base64);
            }

            return null;
        }
    }
}
