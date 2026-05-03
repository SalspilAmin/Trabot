using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Certification.Queries.Results;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Certification.Queries.Models
{
    public class GetCertificationByInstructorQuery : IRequest<List<GetCertificationByInstructorResponse>>
    {
        public int Id { get; set; } //InstructorId
        public GetCertificationByInstructorQuery(int id)
        {
            Id = id;
        }
    }
}
