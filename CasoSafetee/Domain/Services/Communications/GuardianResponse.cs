using CasoSafetee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Domain.Services.Communications
{
    public class GuardianResponse : BaseResponse<Guardian>
    {
        public GuardianResponse(Guardian resource) : base(resource)
        {
        }

        public GuardianResponse(string message) : base(message)
        {
        }
    }
}
