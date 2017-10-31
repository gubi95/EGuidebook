using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;

namespace EGuidebook.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

            HomeViewModel objHomeViewModel = new HomeViewModel();

            objHomeViewModel.SpotCategoriesCount = objApplicationDbContext.SpotCategories.Count();
            objHomeViewModel.SpotsCount = objApplicationDbContext.Spots.Count();
            objHomeViewModel.UsersCount = objApplicationDbContext.Users.Count();

            var spotCoordinates = objApplicationDbContext
                                    .Spots
                                    .ToList();

            objHomeViewModel.SpotCoordinatesList = spotCoordinates
                                                    .Select(x => new HomeViewModel.SpotCoordinates(x))
                                                    .ToList();
            return View(objHomeViewModel);
        }
    }
}