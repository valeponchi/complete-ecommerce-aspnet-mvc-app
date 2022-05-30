using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    //useful when we need the user as a parameter
    public class ApplicationUser:IdentityUser
        //the :IdentityUser has already some prop/Cols in the userDB
        //email & Normalised email & email confirmed
        //username & normalized username
        //phone and phoneConfirmed
        //Id
    {
        //any addition prop in here is used to add a column in Db
        //by EntityFrameworkCore
        [Display(Name = "FullName")]
        public string FullName { get; set; }
    }
}
