using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Core.Mapping.CertificationMapping
{
    public partial class CertificationProfile
    {
        public void AddCertificationMapping() 
        {
            CreateMap<AddInstructorCertificationCommand, Certifications>();

        }

    }
}
