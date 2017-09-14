using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.ViewModels {
    public static class Extensions {
        public static IEnumerable<SelectListItem> ToSelectListItems(
                  this IEnumerable<Genre> geners, int selectedId) {
            return
                geners.OrderBy(genre => genre.Name)
                      .Select(genre =>
                          new SelectListItem
                          {
                              Selected = (genre.Id == selectedId),
                              Text = genre.Name,
                              Value = genre.Id.ToString()
                          });
        }
    }
    public class MovieGenreViewModel {
        public MovieGenreViewModel() {
            Id = 0;
        }
        public MovieGenreViewModel(Movie movie) {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            DateAdded = movie.DateAdded;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
        public IEnumerable<SelectListItem> listOfGenres { get; set; }
        //public IEnumerable<Genre> listOfGenres1 = new List<Genre>();
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select a genre")]
        public byte? GenreId { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Select a Added Date")]
        public DateTime? DateAdded { get; set; }

        [Required()]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }

        public string Title {
            get
            {
                return (Id != 0) ? "Edit Movie" : "New Movie";
            }
        }


    }
}