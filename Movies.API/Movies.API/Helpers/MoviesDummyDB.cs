using System;
using System.Collections.Generic;
using Movies.API.Models;
using Movies.DB.Models;

namespace Movies.API.Helpers
{
    public class MoviesDummyDB
    {
        public static List<Movie> GenerateMoviesData()
        {
			var users = CreateUsers();
			var movieTitles = new List<Movie>
			{
				new Movie { Title = "Armagheddon", YearOfRelease = "1994", Genres = new string[] { "sci-fi", "thriller" }},
				new Movie { Title = "Anchorman 2", YearOfRelease = "2016", Genres = new string[] { "comedy" }},
				new Movie { Title = "Edge Of Tomorrow", YearOfRelease = "2015", Genres = new string[] { "sci-fi", "thriller" }},
				new Movie { Title = "Guardians of the Galaxy", YearOfRelease = "2015", Genres = new string[] { "sci-fi", "superhero" }},
				new Movie { Title = "The Matrix", YearOfRelease = "1999", Genres = new string[] { "sci-fi", "thriller" }},
				new Movie { Title = "Shaun of the Dead", YearOfRelease = "2004", Genres = new string[] { "comedy", "thriller", "horror" }},
				new Movie { Title = "Citizen Kane", YearOfRelease = "1970", Genres = new string[] { "drama" }}
			};
            
			var movies = new List<Movie>();
			var id = 1;
            
            foreach (var movie in movieTitles)
            {
                var userRatings = CreateRatings(users);
				
				movies.Add(new Movie { ID = id, Title = movie.Title, Genres = movie.Genres, YearOfRelease = movie.YearOfRelease, UserRatings = userRatings, Rating = AverageRating(userRatings) });
				id++;
            }

			return movies;
        }
        
        public static List<Movie> UpdateMovieRating(List<Movie> movies, int movieId, int userId, double rating)
        {
			for (var movieIndex = 0; movieIndex < movies.Count; movieIndex++)
            {
                if (movies[movieIndex].ID == movieId)
                {
					for (var ratingIndex = 0; ratingIndex < movies[movieIndex].UserRatings.Count; ratingIndex++)
                    {
                        if (movies[movieIndex].UserRatings[ratingIndex].User.ID == userId)
                        {
							movies[movieIndex].UserRatings[ratingIndex].Rating = rating;
                        }
                    }
                }
            }

			return movies;
        }
    
        static List<User> CreateUsers()
        {
            return new List<User>
            {
                new User { ID = 1, UserName = "DaveGrohl" },
                
                new User { ID = 2, UserName = "RiversCuomo" },
                
                new User { ID = 3, UserName = "MikeDirnt" },
                
                new User { ID = 4, UserName = "TomMorello" },
                
                new User { ID = 5, UserName = "CarsonWentz" } 
            };
        }
    
        static List<UserRating> CreateRatings(List<User> users)
        {
			var userRatings = new List<UserRating>();
			var id = 1;
            
            foreach(var user in users)
            {
				userRatings.Add(new UserRating
				{
                    ID = id,
					User = user,
					Rating = new Random().Next(0, 5)
				});
				id++;
            }

			return userRatings;
        }
        
        static double AverageRating(List<UserRating> userRatings)
        {
            double totalRating = 0;
            foreach (var rating in userRatings)
            {
                totalRating += rating.Rating;
            }
            return totalRating / userRatings.Count;
        }
    }
}
