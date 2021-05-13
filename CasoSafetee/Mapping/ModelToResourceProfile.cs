using AutoMapper;
using CasoSafetee.Domain.Models;
using CasoSafetee.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Guardian, GuardianResource>();
            CreateMap<Urgency, UrgencyResource>();
        }

    }
}
