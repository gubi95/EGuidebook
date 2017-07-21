using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;

namespace EGuidebook.Controllers
{
    public class SpotController : Controller
    {        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View(new SpotViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(SpotViewModel objSpotViewModel)
        {
            if(ModelState.IsValid)
            {

            }

            return View(objSpotViewModel);
        }
    }
}