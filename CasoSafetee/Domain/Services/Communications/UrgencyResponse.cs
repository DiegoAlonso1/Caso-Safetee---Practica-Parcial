using CasoSafetee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Domain.Services.Communications
{
    public class UrgencyResponse : BaseResponse<Urgency>
    {
        public UrgencyResponse(Urgency resource) : base(resource)
        {
        }

        public UrgencyResponse(string message) : base(message)
        {
        }
    }
}
