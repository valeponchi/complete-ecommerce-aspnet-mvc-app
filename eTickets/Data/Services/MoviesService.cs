using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{   //WE IMPORT IN HERE ALL THE METHODS FROM IMOVIESSERVICE
    //CONNECTION WITH DB
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //___________________________________________________________________
        //CREATE - ADD NEW MOVIE
        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            //add data to DB
            //(1)create a NewMovie object:
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            //(2)now we add the movie into the (DB)
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync(); //EF generates the new Id

            //(3)now we add the movie-actors
            //to do that we use the joining table/entity Actors_Movies
            foreach (var actorId in data.ActorIds)
            {
                //we are going to create a new actor_movie
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        //___________________________________________________________________
        //READ - GET A MOVIE/ID
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        //___________________________________________________________________
        //READ - GET THE DROPDOWNS FOR A NEW MOVIE
        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
            };
            return response;
        }

        //___________________________________________________________________
        //UPDATE A MOVIE/ID
        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            //FIRST WE GET THE DB MOVIE
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbMovie != null) //then we UPDATE the DB
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                //now we add the movie into the (DB)
                await _context.SaveChangesAsync(); 
            };
            //Remove the existing Actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
             _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //NOW WE ADD ALL THE NEW ACTORS
            //to do that we use the joining table/entity Actors_Movies
            foreach (var actorId in data.ActorIds)
            {
                //we are going to create a new actor_movie
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
