using System;
using System.Collections.Generic;
using Movies.API.Models;

namespace Movies.API.Helpers
{
    public class MathHelpers
    {
        public static List<Movie> RoundRatings(List<Movie> movies)
        {
			for (var movie = 0; movie < movies.Count; movie++)
            {
				var rating = movies[movie].AverageRating;
				movies[movie].AverageRating = Math.Round((rating * 2), MidpointRounding.AwayFromZero)/2;
            }

			return movies;
        }
    }
}
