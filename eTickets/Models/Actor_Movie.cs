namespace eTickets.Models
{
    public class Actor_Movie //JOINT TABLE - many to many relationship
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }



    }
}
