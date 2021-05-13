using System.Collections.Generic;

namespace CasoSafetee.Resources
{
    public class GuardianResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Adress { get; set; }
        public IList<UrgencyResource> Urgencies { get; set; }
    }
}
