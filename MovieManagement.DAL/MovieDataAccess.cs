using MovieManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MovieManagement.DAL
{
    public class MovieDataAccess
    {
        private Dictionary<string, Movie> movieData = new Dictionary<string, Movie>();

        public Hashtable GetMovies()
        {
            var hashtable = new Hashtable();
            foreach (var pair in movieData)
            {
                hashtable[pair.Key] = pair.Value;
            }
            return hashtable;
        }

        public void AddMovie(string id, Movie movie)
        {
            if (!movieData.ContainsKey(id))
            {
                movieData[id] = movie;
            }
        }

        public void DeleteMovie(string id)
        {
            movieData.Remove(id);
        }

        public void SaveToJson(string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(filePath, JsonConvert.SerializeObject(movieData, Newtonsoft.Json.Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving to JSON: " + ex.Message);
            }
        }

        public void LoadFromJson(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    movieData = JsonConvert.DeserializeObject<Dictionary<string, Movie>>(json) ?? new Dictionary<string, Movie>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading from JSON: " + ex.Message);
            }
        }
    }
}