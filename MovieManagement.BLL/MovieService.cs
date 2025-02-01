using System.Collections;
using System.Security.Policy;
using MovieManagement.DAL;
using MovieManagement.Models;

namespace MovieManagement.BLL
{
    public class MovieService
    {
        private readonly MovieDataAccess _dataAccess;

        public MovieService()
        {
            _dataAccess = new MovieDataAccess();
        }

        public Hashtable GetMovies() => _dataAccess.GetMovies();

        public void AddMovie(string id, string title, string genre, int releaseYear, string director)
        {
            var movies = _dataAccess.GetMovies();
            if (!movies.ContainsKey(id))
            {
                var movie = new Movie
                {
                    Id = id,
                    Title = title,
                    Genre = genre,
                    ReleaseYear = releaseYear,
                    Director = director
                };
                movies.Add(id, movie);
                _dataAccess.AddMovie(id, movie);  // Ensure data persistence
            }
        }


        public void DeleteMovie(string id)
        {
            _dataAccess.GetMovies().Remove(id);
        }

        public void SaveMovies(string filePath) => _dataAccess.SaveToJson(filePath);

        public void LoadMovies(string filePath) => _dataAccess.LoadFromJson(filePath);
    }
}