using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        //_______________________________________________________
        [Display(Name = "Profile Picture")] //needed for the View
        [Required(ErrorMessage = "Profile Picture is required")] //needed in POST req
        public string ProfilePictureURL { get; set; }

        //_______________________________________________________
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars" )]
        public string FullName { get; set; }

        //_______________________________________________________
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        //_______________________________________________________
        //Relationships
        //many to many
        public List<Actor_Movie> Actors_Movies { get; set; }

    }
}
