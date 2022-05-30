using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        //______________________________________________________
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        //______________________________________________________
        //display name is the same and the 
        //default err message is with the same name
        [Required]
        [DataType(DataType.Password)] //hidden by default
        public string Password { get; set; }

        //______________________________________________________
        [Display(Name = "Confirmed password")]
        [Required(ErrorMessage = "Confirm passwork is required")]
        [DataType(DataType.Password)] //hidden by default
        //we are going to check that the pw and the confirmed-pw match
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmedPassword { get; set; }

    }
}
