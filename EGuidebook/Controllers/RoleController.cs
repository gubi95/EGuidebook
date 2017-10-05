using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;

namespace EGuidebook.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        [HttpGet]        
        public ActionResult Index()
        {
            return View(new ApplicationDbContext().Roles.ToList().Select(x => new RoleViewModel(x)).ToList());
        }
    }
}