using System;
using System.Collections.Generic;
using Movies.API.Models;

namespace Movies.API.ResponseModels
{
    public class MoviesResponse 
    {
        /// <summary>
        /// Creates the reduced Movies response using MoviesDetails instead of Movie class.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="movies">Movies.</param>
        public static List<MoviesDetails> CreateResponse(IEnumerable<Movie> movies)
        {
			var moviesResponse = new List<MoviesDetails>();
            
			foreach (var movie in movies)
            {
				moviesResponse.Add(new MoviesDetails(movie));
            }

			return moviesResponse;
        }
    }
    
    public class MoviesDetails
    {
        public MoviesDetails (Movie movie)
        {
			ID = movie.ID;
			AverageRating = movie.AverageRating;
			RunningTime = movie.RunningTime;
			Title = movie.Title;
			YearOfRelease = movie.YearOfRelease;
        }
        
        public int ID { get; set; }

        public double AverageRating { get; set; }

        public string RunningTime { get; set; }

        public string Title { get; set; }

        public string YearOfRelease { get; set; }
    }
}
