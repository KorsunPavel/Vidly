using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers {
    
    public class MoviesController : Controller {
        ApplicationDbContext _context;
        public MoviesController() {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

        [Route("movies")]
        public ActionResult ShowListOfMovies() {
            List<Movie> list = _context.Movies.Include(m => m.Genre).ToList();
            MovieList movieList = new MovieList()
            {
                Movies = list
            };
            if (User.IsInRole(Roles.CAN_MANAGE_MOVIES))
                return View("Movies", movieList);
            return View("MoviesReadOnly", movieList);
        }

        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult Details(int id) {
            Movie movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult Edit(int id) {
            MovieGenreViewModel viewModel = new MovieGenreViewModel();
            viewModel.listOfGenres = _context.Genre.ToList().ToSelectListItems(id);
            if (id != 0)
            {
                Movie movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
                if (movie == null) return HttpNotFound();
                if (movie.AvailableCount <= 0)
                    return HttpNotFound();
                viewModel = new MovieGenreViewModel(movie);
                viewModel.listOfGenres = _context.Genre.ToList().ToSelectListItems(id); 
            }
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie) {

            if (!ModelState.IsValid)
            {
                MovieGenreViewModel viewModel2 = new MovieGenreViewModel(movie)
                {
                   listOfGenres = _context.Genre.ToList().ToSelectListItems(movie.Id),
                };
                return View("MovieForm", viewModel2);
            }

            //var genre = _context.Genre.Single(g => g.Id == movie.Genre.Id);
            //movie.Genre = genre;
            if (movie.Id == 0)  
            {
                //movie.AvailableCount = (byte)movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            else {
                var movieInDb = _context.Movies.Include(m => m.Genre).Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.GenreId = movie.GenreId;
            }
            _context.SaveChanges();
            MovieGenreViewModel viewModel = new MovieGenreViewModel(movie)
            {
                listOfGenres = _context.Genre.ToList().ToSelectListItems(movie.Id)
            };
            return RedirectToAction("ShowListOfMovies", "Movies");
        }
        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult Delete(int id) {
            var movie = _context.Movies.Single(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("ShowListOfMovies", "Movies");
        }

        //// GET: Movies/random
        //public ActionResult Random() {
        //    Movie movie = new Movie() { Name = "Shreak!!!" };
        //    //return View(movie);
        //    //ViewData["Movie"] = movie;
        //    ViewBag.Movie = movie;
        //    //new ViewResult().ViewData.Model = movie;
        //    //return View();

        //    List<Customer> list = new List<Customer>() {
        //        new Customer { Name = "Customer 1"},
        //        new Customer { Name = "Customer 2"}
        //    };
        //    var viewModel = new RandomMovieViewModel()
        //    {
        //        Movie = movie,
        //        Customers = list
        //    };
        //    return View(viewModel);
        //}

        //public ActionResult Edit(int id) {
        //    return Content("id - " + id);
        //}

        //public ActionResult List(int? pageIndex, string sortBy) {
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (string.IsNullOrEmpty(sortBy))
        //        sortBy = "ReleaseDate";
        //    return Content(string.Format("?pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleasedDate(int year, int month) {

        //    return Content(year + " " + month);
        //}
    }
}