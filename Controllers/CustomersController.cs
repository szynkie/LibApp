using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Respositories;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersRepository _customersRepo;
        private readonly MembershipTypeRepository _MtSRepo;

        public CustomersController(ApplicationDbContext context)
        {
            _customersRepo = new CustomersRepository(context);
            _MtSRepo = new MembershipTypeRepository(context);
        }

        public ViewResult Index() => View();

        public IActionResult Details(int id)
        {
            var customer = _customersRepo.GetCustomerById(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        public IActionResult New()
        {
            var membershipTypes = _MtSRepo.GetMsTs();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customersRepo.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _MtSRepo.GetMsTs().ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _MtSRepo.GetMsTs().ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _customersRepo.Add(customer);
            }
            else
            {
                var customerInDb = _customersRepo.GetCustomerById(customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.HasNewsletterSubscribed = customer.HasNewsletterSubscribed;
            }

            _customersRepo.Save();

            return RedirectToAction("Index", "Customers");
        }
    }
}