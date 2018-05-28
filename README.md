
# Movie API

This is an example Web API solution built in .Net Core which allows users to search for movies according to Title, Year Of Release, and Genre. It also allows users to rate the movies and provides Top 5 rankings for all movies and for movies rated by a particular user.

## Setup

### Prerequisites

- [Visual Studio Community](https://www.visualstudio.com/downloads/)
- [.Net Core 2.0](https://www.microsoft.com/net/download/)

### Installation

- Clone this repo. (`git clone git@github.com:SteveOGallagher/movies.git`)
- Either open the solution in Visual Studio or change directory to the _Movies.API_ solution folder in your terminal and run `dotnet restore` to install all NuGet dependancies.

## Running the Application

### Running Locally

- Change directory into the _Movies.API_ project folder
- Run `dotnet run`
- Note the url logged out in the console line: `Now listening on: http://localhost:22827`
- Copy this url into your browser window or an equivalent API tool to Postman and add `/api/movies` so that the full url becomes for example `http://localhost:22827/api/movies`
- Provided that the project is running correctly, a GET request for this url should return an error message stating `You must submit at least 1 search critera`

### API Usage

The following endpoints are available:

- `GET api/movies` : This endpoint returns all movies matching certain criteria which must be submitted as url query parameters. 
  - `api/movies?title=Anchorman%202` : The _title_ paramater will filter the movies to ones matching the submitted title exactly. This example query will return the film with the title "Anchorman 2".
  - `api/movies?yearOfRelease=2015` : The _yearOfRelease_ paramater will filter the movies to ones matching the submitted year of release exactly. This example query will return the films which were released in 2015. _( Note: the years in this example project are not fact-checked, they are guessed at by the developer, probably poorly...)_
  -  `api/movies?genres=thriller&genres=sci-fi` : The _genres_ paramater will filter the movies to ones tagged with all of the submitted genres. This example query will return any films which are both thriller and sci-fi genres.
  - It is possible to submit multiple query parameters at once; the API will return any movies that match all of the criteria submitted.
 - `PUT api/movies/{movieId}/userrating/{userId}` : This endpoint updates a user's rating of a movie according to the movie's API Id and the user's API Id. The rating must be passed in the body of the request in the format: ``` {   "newRating": "4" }```. The number must be a valid double value when converted from a string and must be between 0 and 5.
 - `GET api/movies/toprated` : This endpoint will return the top 5 rated movies as an average of all user ratings.
 - `GET api/movies/toprated/{userId}` : This endpoint will return the top 5 rated movies according to a particular user identified by their API Id.

## Development Notes

### Database 

For the purpose of this example, no database has been configured. Two helper scripts called `MoviesDummyDB.cs` and `UsersDummyDB.cs` have been created to generate a simulation of the movies and users data for use by the _Movie.API_ project. 

### Future Refactoring

##### LINQ Services

The API endpoints contain all of their LINQ processing data currently for the sake of development time, but ideally this business logic would be moved into service scripts elsewhere in the project or solution. This would allow the API endpoint code to be reduced to the essential Found/Not Found/Bad Request logic steps with the results of simple and reusable function calls to inform the return logic.

##### UserRatings Dataset

Currently each Movie in the _Movies_ dummy data contains a list of UserRatings within itself which have been applied to that movie. If an actual database were being used for this API, the UserRatings ought to have its own database table with Id keys to link them to their associated Movie and User. This would make it easier to get all UserRatings matching a particular movie as well as all UserRatings matching a particular user, without the latter requiring a filtering process through all Movies to check all UserRatings on them for matching user Ids as is currently in place.

##### Dependancy Injection

Currently there is no DI within the project which makes future maintainability problematic. Classes in the code are currently new'd up each time an instance of it is required; whereas, ideally each class would have a corresponding interface to code against so that any future alterations to the classes themselves shouldn't require refactoring wherever that class is new'd throughout the codebase.

##### Movie Filtering

Currently the search for movies by title requires an exact title match to filter movies correctly. Ideally, the API should be able to filter according to movie titles which contain part of the submitted title text. Also, even more ideally, the API could make use of some AI by using Azure Cognitive Services for example so that if a genre of 'thrills' was submitted, the tag of 'thriller' could be matched for this and all thriller films could be returned to the request rather than 0 results.
