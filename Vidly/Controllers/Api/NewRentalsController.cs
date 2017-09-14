using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api {
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalsController() {
            _context = new ApplicationDbContext();
        }

        // POST api/newrentals
        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRenatlDto newRentalDto) {
            var customer = _context.Customers.Single(c => c.id == newRentalDto.CustomerId);
            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            List<Rental> listOfRentals = new List<Rental>();
            foreach (var movie in movies)
            {
                if (movie.AvailableCount <= 0)
                    return BadRequest("Movie is not available.");
                movie.AvailableCount--;
                listOfRentals.Add(
                    new Rental()
                    {
                        Customer = customer,
                        DateRented = DateTime.Now,
                        Movie = movie,
                    });
            }
            _context.Rental.AddRange(listOfRentals);
            _context.SaveChanges();
            return Ok();
        }
    }
}
