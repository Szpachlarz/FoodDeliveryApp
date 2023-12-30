using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [StringLength(20, ErrorMessage = "Maksymalnie 20 znaków")]
        public string Username { get; set; }
        [Display(Name = "Adres e-mail")]
        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Nieprawidłowy format adresu e-mail")]
        [StringLength(40, ErrorMessage = "Maksymalnie 40 znaków")]
        public string EmailAddress { get; set; }
        [Display(Name = "Hasło")]
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Maksymalnie 20 znaków")]
        public string Password { get; set; }
        [Display(Name = "Powtórz hasło")]
        [Required(ErrorMessage = "Ponowne podanie hasła jest wymagane")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Maksymalnie 20 znaków")]
        [Compare("Password", ErrorMessage = "Podane hasła różnią się")]
        public string ConfirmPassword { get; set; }
        //[Display(Name = "Zapoznaj się z regulaminem")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Akceptacja regulaminu jest wymagana")]
        public bool IsAccepted { get; set; }
    }
}
