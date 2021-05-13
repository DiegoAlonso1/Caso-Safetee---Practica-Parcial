using CasoSafetee.Domain.Models;
using System;

namespace CasoSafetee.Resources
{
    public class UrgencyResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime ReportedAt { get; set; }
        public int GuardianId { get; set; }
        public Guardian Guardian { get; set; }
    }
}
