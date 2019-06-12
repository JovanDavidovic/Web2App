using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApp.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        public string Birthday { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Acctype")]
        public string Acctype { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class PricelistBindingModel
    {
        [Required]
        [Display(Name = "From")]
        public string From { get; set; }
        [Required]
        [Display(Name = "To")]
        public string To { get; set; }

        [Required]
        [Display(Name = "Hour")]
        public int Hour { get; set; }
        [Required]
        [Display(Name = "Day")]
        public int Day { get; set; }
        [Required]
        [Display(Name = "Month")]
        public int Month { get; set; }
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }

    public class PricelistWithIdModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "From")]
        public string From { get; set; }
        [Required]
        [Display(Name = "To")]
        public string To { get; set; }

        [Required]
        [Display(Name = "Hour")]
        public int Hour { get; set; }
        [Required]
        [Display(Name = "Day")]
        public int Day { get; set; }
        [Required]
        [Display(Name = "Month")]
        public int Month { get; set; }
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }

    public class BuyTicketBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Hour")]
        public int Hour { get; set; }
        [Required]
        [Display(Name = "Day")]
        public int Day { get; set; }
        [Required]
        [Display(Name = "Month")]
        public int Month { get; set; }
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }

    public class BoughtTicketBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "TicketType")]
        public string TicketType { get; set; }

        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }
    }

    public class RouteBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public int Name { get; set; }
        [Required]
        [Display(Name = "RouteStations")]
        public string RouteStations { get; set; }
        
    }

    public class DepartureTimeBindingModel
    {
        [Required]
        [Display(Name = "Hour")]
        public int Hour { get; set; }
        [Required]
        [Display(Name = "Min")]
        public int Min { get; set; }
        [Required]
        [Display(Name = "DayType")]
        public string DayType { get; set; }
        [Required]
        [Display(Name = "RouteName")]
        public int RouteName { get; set; }
    }
}
