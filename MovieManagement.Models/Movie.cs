using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Genre: {Genre}, Year: {ReleaseYear}, Director: {Director}";
        }
    }
}
