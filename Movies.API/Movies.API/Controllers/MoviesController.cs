﻿using System;
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
using Newtonsoft.Json;

namespace Movies.API.Controllers
{
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
		static List<Movie> Movies = MoviesDummyDB.GenerateMoviesData();
        
        // GET api/movies
        [HttpGet]
        public IActionResult Get(string title = null, string yearOfRelease = null, string[] genres = null)
        {
			var movies = MathHelpers.RoundRatings(Movies); // TODO: fetch from DB instead of memory object

			if (title == null && yearOfRelease == null && genres.Length == 0)
			{
				return BadRequest("You must submit at least 1 search critera");
			}

			IEnumerable<Movie> filteredMovies = null;
            
            if (title != null)
            {
                filteredMovies = movies.Where(x => x.Title == title);
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

        // GET api/movies/toprated
        [HttpGet("toprated")]
        public IActionResult Get()
        {
			var movies = Movies.OrderByDescending(a => a.AverageRating).ThenBy(t => t.Title);
            
            return Ok(movies.Take(5));
        }

        // PUT api/movies/5/userrating/3
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
				var rating = movie.UserRatings.Where(x => x.User.ID == userId).SingleOrDefault();
                
                if (rating != null)
                {
					Movies = MoviesDummyDB.UpdateMovieRating(Movies, movieId, userId, Convert.ToDouble(newRating));

					return Ok(Movies);
                }

				return NotFound("No user has been found for that id.");
            }

            return NotFound("No movie has been found for that id.");
        }
    }
}
