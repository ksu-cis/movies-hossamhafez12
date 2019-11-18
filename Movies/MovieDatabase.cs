using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }
        public List<Movie> SearchAndFilter(string searchString, List<string> rating)
        {
            if (searchString == null && rating.Count == 0) return null;
            List<Movie> res = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (searchString != null && rating.Count > 0)
                {
                    if (movie.Title != null
                       && movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                        && rating.Contains(movie.MPAA_Rating))
                    {
                        res.Add(movie);
                    }
                }
                else if (searchString != null)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        res.Add(movie);
                    }
                }
                else if (rating.Count > 0)
                {
                    if (rating.Contains(movie.MPAA_Rating))
                    {
                        res.Add(movie);
                    }
                }


            }
            return res;
        }
    }
}
