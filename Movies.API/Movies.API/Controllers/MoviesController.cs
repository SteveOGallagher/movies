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
using Newtonsoft.Json;

namespace Movies.API.Controllers
{
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
		static List<Movie> Movies = MoviesDummyDB.GenerateMoviesData();
        
        // GET api/movies
        [HttpGet]
        public IActionResult Get()
        {
			var movies = Movies; // TODO: fetch from DB instead of memory object
            var jsonResponse = JsonConvert.SerializeObject(movies);
            
			return Ok(jsonResponse);
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("value");
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
			Movies[0].Title = "updated";

			return Ok(Movies);
        }
    }
}
