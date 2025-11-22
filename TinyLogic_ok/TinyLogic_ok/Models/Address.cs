using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class Address
    {
       [Key] public int IdAdress { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public int IdUser { get; set; }
        public User User { get; set; }

        
    }
}
