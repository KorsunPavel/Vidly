using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models {
    public class Movie {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Select a genre")]
        public byte GenreId { get; set; }

        [Required(ErrorMessage = "Select a Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Select a Added Date")]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1,20)]
        public int NumberInStock { get; set; }

        public byte AvailableCount { get; set; }
    }
}