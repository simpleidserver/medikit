// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Extensions;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Persistence.InMemory
{
    public class InMemoryMedicalfileQueryRepository : IMedicalfileQueryRepository
    {
        private static Dictionary<string, string> MAPPING_MEDICALFILE_TO_PROPERTYNAME = new Dictionary<string, string>
        {
            { "firstname", "PatientFirstname" },
            { "lastname", "PatientLastname" },
            { "niss", "PatientNiss" },
            { "createDateTime", "CreateDateTime" },
            { "updateDateTime", "UpdateDateTime" }
        };
        private readonly ConcurrentBag<MedicalfileAggregate> _medicalfiles;

        public InMemoryMedicalfileQueryRepository(ConcurrentBag<MedicalfileAggregate> medicalfiles)
        {
            _medicalfiles = medicalfiles;
        }

        public Task<MedicalfileAggregate> Get(string id, CancellationToken token)
        {
            return Task.FromResult(_medicalfiles.FirstOrDefault(_ => _.Id == id));
        }

        public Task<PagedResult<MedicalfileAggregate>> Search(SearchMedicalfileQuery parameter, CancellationToken token)
        {
            IQueryable<MedicalfileAggregate> medicalfiles = _medicalfiles.AsQueryable();
            if (MAPPING_MEDICALFILE_TO_PROPERTYNAME.ContainsKey(parameter.OrderBy))
            {
                medicalfiles = medicalfiles.InvokeOrderBy(MAPPING_MEDICALFILE_TO_PROPERTYNAME[parameter.OrderBy], parameter.Order);
            }

            if (!string.IsNullOrWhiteSpace(parameter.Niss))
            {
                medicalfiles = medicalfiles.Where(r => r.PatientNiss.StartsWith(parameter.Niss, System.StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(parameter.Firstname))
            {
                medicalfiles = medicalfiles.Where(r => r.PatientFirstname.StartsWith(parameter.Firstname, System.StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(parameter.Lastname))
            {
                medicalfiles = medicalfiles.Where(r => r.PatientLastname.StartsWith(parameter.Lastname, System.StringComparison.InvariantCultureIgnoreCase));
            }

            int totalLength = medicalfiles.Count();
            medicalfiles = medicalfiles.Skip(parameter.StartIndex).Take(parameter.Count);
            return Task.FromResult(new PagedResult<MedicalfileAggregate>
            {
                StartIndex = parameter.StartIndex,
                Count = parameter.Count,
                TotalLength = totalLength,
                Content = (ICollection<MedicalfileAggregate>)medicalfiles.ToList()
            });
        }
    }
}
