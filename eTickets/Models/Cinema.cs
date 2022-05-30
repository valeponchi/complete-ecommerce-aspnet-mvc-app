using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Cinema Logo is required")]
        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }



        [Required(ErrorMessage = "Cinema Name is required")]
        [Display(Name = "Cinema Name")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Cinema Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }



        //Relationships
        //one to many
        public List<Movie> Movies { get; set; }
    }
}
