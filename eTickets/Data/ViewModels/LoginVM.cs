using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        //______________________________________________________
        //display name is the same and the 
        //default err message is with the same name
        [Required]
        [DataType(DataType.Password)] //hidden by default
        public string Password { get; set; }

    }
}
