using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos {
    public class MovieDto {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select a genre")]
        public byte GenreId { get; set; }

        [Required(ErrorMessage = "Select a Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Select a Added Date")]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }
    }
}