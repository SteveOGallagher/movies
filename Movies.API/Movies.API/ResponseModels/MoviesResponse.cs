using System;
using System.Collections.Generic;
using Movies.API.Models;

namespace Movies.API.ResponseModels
{
    public class MoviesResponse 
    {
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
