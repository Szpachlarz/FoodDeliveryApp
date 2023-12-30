namespace FoodDeliveryApp.ViewModels
{
    public class RegisterRestaurantViewModel
    {
        public string Username { get; set; }
        public string RestaurantName { get; set; }
        public string EmailAddress {  get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Description { get; set; }
        public bool IsAccepted { get; set; }
    }
}
