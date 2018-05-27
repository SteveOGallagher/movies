using System;
using System.Collections.Generic;
using Movies.DB.Models;

namespace Movies.API.Helpers
{
    public class UsersDummyData
    {
        public static List<User> CreateUsers ()
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
    }
}
