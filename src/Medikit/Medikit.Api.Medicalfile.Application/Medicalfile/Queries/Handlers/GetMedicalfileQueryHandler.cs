// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Exceptions;
using Medikit.Api.Medicalfile.Application.Extensions;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using Medikit.Api.Medicalfile.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Handlers
{
    public class GetMedicalfileQueryHandler : IRequestHandler<GetMedicalfileQuery, GetMedicalfileResult>
    {
        private readonly IMedicalfileQueryRepository _medicalfileQueryRepository;

        public GetMedicalfileQueryHandler(IMedicalfileQueryRepository medicalfileQueryRepository)
        {
            _medicalfileQueryRepository = medicalfileQueryRepository;
        }

        public async Task<GetMedicalfileResult> Handle(GetMedicalfileQuery request, CancellationToken cancellationToken)
        {
            var result = await _medicalfileQueryRepository.Get(request.Id, cancellationToken); 
            if (result == null)
            {
                throw new UnknownMedicalfileException(request.Id, string.Format(Global.UnknownMedicalFile, request.Id));
            }

            return result.ToResult();
        }
    }
}
