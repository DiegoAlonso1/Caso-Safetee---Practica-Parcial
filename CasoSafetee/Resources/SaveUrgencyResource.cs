using CasoSafetee.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Resources
{
    public class SaveUrgencyResource
    {
        [Required]
        public string Title { get; set; }

        public string Summary { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
