using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EGuidebook.Models;

namespace EGuidebook.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RouteController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(
                new ApplicationDbContext()
                    .Routes
                    .ToList()
                    .Select(x => new RouteViewModel(x))
                    .ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

            List<SpotModel> listSpotModel = objApplicationDbContext
                                                    .Spots
                                                    .ToList();

            return View(new RouteViewModel(new RouteModel(), listSpotModel));
        }

        [HttpPost]
        public ActionResult Create(RouteViewModel objRouteViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(this.User.Identity.Name));

                string[] arrSelectedSpotIDs = objRouteViewModel.SpotsIDsListFormatted.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                RouteModel objRouteModel = new RouteModel()
                {
                    RouteID = Guid.NewGuid(),
                    Name = objRouteViewModel.Name,
                    Description = objRouteViewModel.Description,
                    CreatedByUserID = objApplicationUser.Id,
                    Spots = objApplicationDbContext
                            .Spots
                            .Where(x => arrSelectedSpotIDs.Contains(x.SpotID.ToString()))
                            .ToList()
                };

                objApplicationDbContext.Routes.Add(objRouteModel);
                objApplicationDbContext.SaveChanges();
            }
            return View(objRouteViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            RouteModel objRouteModel = new ApplicationDbContext()
                                            .Routes
                                            .First(x => x.RouteID.Equals(id));

            if (objRouteModel != null)
            {
                return View(new RouteViewModel(objRouteModel));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid id, RouteViewModel objRouteViewModel)
        {
            return null;
        }
    }
}