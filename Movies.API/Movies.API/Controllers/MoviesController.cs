using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Helpers;
using Movies.API.Models;
using Movies.API.ResponseModels;
using Movies.DB.Models;
using Newtonsoft.Json;

namespace Movies.API.Controllers
{
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
        static List<Movie> Movies = MoviesDummyDB.GenerateMoviesData(); // TODO: fetch from DB instead of memory object
        static List<User> Users = UsersDummyData.CreateUsers(); // TODO: fetch from DB instead of memory object
        
        /// GET api/movies
        /// <summary>
        /// Get the movies acccording to specified title, yearOfRelease and genres.
        /// </summary>
        /// <returns>The filtered movies.</returns>
        /// <param name="title">Title.</param>
        /// <param name="yearOfRelease">Year of release.</param>
        /// <param name="genres">Genres.</param>
        [HttpGet]
        public IActionResult Get(string title = null, string yearOfRelease = null, string[] genres = null)
        {
			var movies = MathHelpers.RoundRatings(Movies);

			if (title == null && yearOfRelease == null && genres.Length == 0)
			{
				return BadRequest("You must submit at least 1 search critera");
			}

			IEnumerable<Movie> filteredMovies = null;
            
            if (title != null)
            {
                filteredMovies = movies.Where(x => x.Title.ToLower().Contains(title.ToLower()));
            }
            
            if (yearOfRelease != null)
            {
                filteredMovies = filteredMovies == null ?
                                    movies.Where(x => x.YearOfRelease == yearOfRelease): 
                                    filteredMovies.Where(x => x.YearOfRelease == yearOfRelease);
            }
            
            if (genres.Length > 0)
            {
                filteredMovies = filteredMovies == null ?
                                    movies.Where(x => !genres.Except(x.Genres).Any()): 
                                    filteredMovies.Where(x => !genres.Except(x.Genres).Any());
            }
            
            if (filteredMovies != null)
            {
				var moviesResponse = MoviesResponse.CreateResponse(filteredMovies);
                
                return Ok(moviesResponse);
            }
            
			return NotFound();
        }

        /// GET api/movies/toprated
        /// <summary>
        /// Get the top 5 movies according to all user ratings.
        /// </summary>
        /// <returns>The top 5 rated movies</returns>
        [HttpGet("toprated")]
        public IActionResult Get()
        {
            var movies = Movies.OrderByDescending(a => a.AverageRating).ThenBy(t => t.Title).Take(5);
            
            if (movies != null)
            {
                var moviesResponse = MoviesResponse.CreateResponse(movies);
                
				return Ok(moviesResponse);
            }
            
            return NotFound();
        }

        /// GET api/movies/toprated/3
        /// <summary>
        /// Get the top 5 movies according to specified user by Id.
        /// </summary>
        /// <returns>The user's top 5 rated movies</returns>
        /// <param name="userId">User identifier.</param>
        [HttpGet("toprated/{userId}")]
        public IActionResult Get(int userId)
        {
			var user = Users.Where(i => i.ID == userId).SingleOrDefault();
            if (user == null)
            {
				return NotFound("No user has been found for this id");
            }
            
            var movies = Movies.OrderByDescending(a => a.UserRatings.Where(u => u.User.ID == userId).SingleOrDefault().Rating).ThenBy(t => t.Title).Take(5);
            
            if (movies != null)
            {
                var moviesResponse = MoviesResponse.CreateResponse(movies);
                
                return Ok(moviesResponse);
            }
            
            return NotFound("No matching movie ratings were able to be found for this user");
        }

        /// <summary>
        /// Update the specified user's rating for a specified movie.
        /// </summary>
        /// <returns>The put.</returns>
        /// <param name="movieId">Movie identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="newRating">New rating.</param>
        /// PUT api/movies/5/userrating/3
        [HttpPut("{movieId}/userrating/{userId}/{newRating}")]
        public IActionResult Put(int movieId, int userId, string newRating)
        {
			try
            {
				Convert.ToDouble(newRating);
            }
            catch (FormatException)
            {
				return BadRequest("The submitted rating is not submitted in a valid format.");
            }
            
            if (Convert.ToDouble(newRating) < 0 || Convert.ToDouble(newRating) > 5)
            {
                return BadRequest("The submitted rating is not between 0 and 5.");
            }
            
			var movie = Movies.Where(x => x.ID == movieId).SingleOrDefault();
            
            if (movie != null)
            {
                var user = Users.Where(i => i.ID == userId).SingleOrDefault();
                if (user != null)
                {
                    var rating = movie.UserRatings.Where(x => x.User.ID == userId).SingleOrDefault();
                    
                    if (rating != null)
                    {
                        Movies = MoviesDummyDB.UpdateMovieRating(Movies, movieId, userId, Convert.ToDouble(newRating));
    
                        return Ok(Movies);
                    }

                    return NotFound("No rating has been given on that movie for that user id.");
                }

				return NotFound("No user has been found for that id.");
            }

            return NotFound("No movie has been found for that id.");
        }
    }
}
