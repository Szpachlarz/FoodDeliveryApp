using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
        public string Voivodeship { get; set; }
        public string ZipCode { get; set; }
    }
}
