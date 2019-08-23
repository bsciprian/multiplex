using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultiplexData.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        [Required]
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<MovieCategory> MovieCategories { get; set; }
        public virtual IEnumerable<Run> Runs { get; set; }
    }
}
