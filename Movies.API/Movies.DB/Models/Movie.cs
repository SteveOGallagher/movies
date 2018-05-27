namespace Movies.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Movie
    {
        [Key]
        [Column("movie_id")]
        public int MovieId { get; set; }

        [Column("movie_title")]
        public string MovieTitle { get; set; }

        [Column("movie_rating")]
        public double MovieRating { get; set; }
    }
}
