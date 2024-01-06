using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
