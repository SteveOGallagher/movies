using System;
namespace Movies.DB.Models
{
    public class UserRating
    {
        public int ID { get; set; }
        
        public User User { get; set; }
        
        public double Rating { get; set; }
    }
}
