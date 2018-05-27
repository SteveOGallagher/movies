﻿namespace Movies.API.Models
{
    using System;
	using System.Collections.Generic;
	using Movies.DB.Models;

	public class Movie
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public double Rating { get; set; }

		public List<UserRating> UserRatings { get; set; }
    }
}
