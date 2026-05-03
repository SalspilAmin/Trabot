using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.CertificationMapping
{
    public partial class CertificationProfile : Profile
    {
        public CertificationProfile()
        {
            AddCertificationMapping();
            GetCertificationByInstructorMapping();
        }
    }
    
}
