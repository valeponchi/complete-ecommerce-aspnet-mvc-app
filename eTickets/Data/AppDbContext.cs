using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    //THE TRANSLATOR
    public class AppDbContext : IdentityDbContext<ApplicationUser>
        //in identityDbCntext you can define the userClass you want
        //if no param, by default you'll get the 
        //IdentityUser and IdentityRoleClasses
        //by the EntityframeworkCore
    {
        //DEFAULT ctor / par(what ,HowWeNameIt)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        //INSTRUCTIONS for the MANY-TO-MANY relationships 
            //override the model-creation: (par(what, howWeNameIt)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ACTOR_MOVIE TABLE define/config 
                //SQL
                //ACTOR_MOVIE IS MADE OF a combination of primary-keys //am = actor-movie
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

                //C#
                //ACTOR_MOVIE to be the joining table/model in the C# side
                //in the Actor_Movie one movie/actor can appear many times in actor_movie, with its foreignKey
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany((System.Linq.Expressions.Expression<System.Func<Movie, System.Collections.Generic.IEnumerable<Actor_Movie>>>)(am => (System.Collections.Generic.IEnumerable<Actor_Movie>)am.Actors_Movies)).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
            //needed for the default authentication table
            //no need to manually define to identifiers ❓❌❓❌❓❌
        }

        //SQL + C#
        //define the table name for each model
            //what type(DbSet<Actor>) and its table-name(Actors)
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }



    }
}
