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
                                    .Select(x => new { x.CoorX, x.CoorY, x.Name, x.Description, x.Image1Path, x.Image2Path, x.Image3Path, x.Image4Path, x.Image5Path }).ToList();

            objHomeViewModel.SpotCoordinatesList = spotCoordinates
                                                    .Select(x => new HomeViewModel.SpotCoordinates()
                                                        {
                                                            CoorX = double.Parse(x.CoorX, System.Globalization.CultureInfo.InvariantCulture),
                                                            CoorY = double.Parse(x.CoorY, System.Globalization.CultureInfo.InvariantCulture),
                                                            Name = x.Name,
                                                            Description = x.Description,
                                                            ImagePaths = new List<string>()
                                                            {
                                                                "" + x.Image1Path,
                                                                "" + x.Image2Path,
                                                                "" + x.Image3Path,
                                                                "" + x.Image4Path,
                                                                "" + x.Image5Path
                                                            }
                                                        }
                                                    ).ToList();
            return View(objHomeViewModel);
        }
    }
}