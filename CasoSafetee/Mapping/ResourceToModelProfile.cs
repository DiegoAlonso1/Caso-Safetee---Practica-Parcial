using AutoMapper;
using CasoSafetee.Domain.Models;
using CasoSafetee.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveGuardianResource, Guardian>();
            CreateMap<SaveUrgencyResource, Urgency>();
        }
    }
}
