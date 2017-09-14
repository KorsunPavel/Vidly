using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext _context;
        public CustomersController() {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

        [Route("customers/new")]
        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult AddNewCustomer() {
            var membershipTypes2 = _context.MemberShipTypes.ToList();
            CustomerViewModel modeView = new CustomerViewModel()
            {
                memberShipTypesList = membershipTypes2
            };
            return View("CustomerForm", modeView);
        }

        [HttpPost]
        [Route("customers/save")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult Save(Customer customer) {

            if (!ModelState.IsValid) {
                CustomerViewModel viewModel = new CustomerViewModel()
                {
                    Customer = customer,
                    memberShipTypesList = _context.MemberShipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.id == 0)
                _context.Customers.Add(customer);
            else {
                var customerInDb = _context.Customers.Single(c => c.id == customer.id);
                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewLetter = customer.IsSubscribedToNewLetter;
                customerInDb.MemberShipTypesId = customer.MemberShipTypesId;
                customerInDb.Birthday = customer.Birthday;
            }
            _context.SaveChanges();
            return RedirectToAction("ShowListOfCustomers", "customers");
        }

        [Route("customers")]
        public ActionResult ShowListOfCustomers() {
            List<Customer> listOfCustomers1 = _context.Customers.Include( c => c.MemberShipTypes).ToList();
            var viewModel = new CustmersListViewModel()
            {
                listOfCustomers = listOfCustomers1
            };
            if (User.IsInRole(Roles.CAN_MANAGE_MOVIES))
                return View("Customers", viewModel);

            return View("CustomersReadOnly", viewModel);
        }

        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        [Route("customers/{id}")]
        public ActionResult ShowCustomers(int id) {
             Customer customer = _context.Customers.Include(c => c.MemberShipTypes).SingleOrDefault( c => c.id == id);
             if (customer == null) return HttpNotFound();

              return View("Customer", customer);
        }

        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult Edit(int id) {
            var memberShipTypesList = _context.MemberShipTypes.ToList();
            CustomerViewModel viewModel = new CustomerViewModel()
            {
                Customer = new Customer(),
                memberShipTypesList = memberShipTypesList
            };
            if (id==0)
                return View("CustomerForm", viewModel);
            Customer customer = _context.Customers.Include(c => c.MemberShipTypes).SingleOrDefault(c => c.id == id);
            if (customer == null)
                return HttpNotFound();
            viewModel.Customer = customer;
            return View("CustomerForm", viewModel); 
        }

        [Authorize(Roles = Roles.CAN_MANAGE_MOVIES)]
        public ActionResult Delete(int id) {
            var customerInDb = _context.Customers.Single(c => c.id == id);
            if (customerInDb == null)
                return HttpNotFound();
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return RedirectToAction("ShowListOfCustomers", "customers");
        }
    }
}