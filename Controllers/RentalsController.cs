using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Controllers
{
    [Authorize(Roles = "Owner, StoreManager")]
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}