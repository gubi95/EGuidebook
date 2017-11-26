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

            return View(new RouteViewModel(new RouteModel(), listSpotModel, true));
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

                List<SpotsRoutesModel> listSpotsRoutesModel = new List<SpotsRoutesModel>();

                Guid objRouteID = Guid.NewGuid();

                int i = 0;
                foreach (string strSpotID in arrSelectedSpotIDs)
                {
                    listSpotsRoutesModel.Add(new SpotsRoutesModel() { Ordinal = i, RouteID = objRouteID, SpotID = Guid.Parse(strSpotID) });
                    i++;
                }

                RouteModel objRouteModel = new RouteModel()
                {
                    RouteID = objRouteID,
                    Name = objRouteViewModel.Name,
                    Description = objRouteViewModel.Description,
                    IsSystemRoute = objRouteViewModel.IsSystemRoute,
                    CreatedByUserID = objApplicationUser.Id,
                    Spots = listSpotsRoutesModel
                };

                objApplicationDbContext.Routes.Add(objRouteModel);
                objApplicationDbContext.SaveChanges();

                return RedirectToAction("Edit", new { @id = objRouteModel.RouteID.ToString() });
            }
            return View(objRouteViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
            RouteModel objRouteModel = objApplicationDbContext
                                        .Routes
                                        .Include(x => x.Spots)
                                        .First(x => x.RouteID.Equals(id));

            if (objRouteModel != null)
            {
                List<SpotModel> listSpotModel = objApplicationDbContext
                                                .Spots
                                                .ToList();

                return View(new RouteViewModel(objRouteModel, listSpotModel, false));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid id, RouteViewModel objRouteViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                RouteModel objRouteModel = objApplicationDbContext
                                            .Routes
                                            .Include(x => x.Spots)
                                            .FirstOrDefault(x => x.RouteID.Equals(id));

                if(objRouteModel == null)
                {
                    return RedirectToAction("Index");
                }

                string[] arrSelectedSpotIDs = objRouteViewModel.SpotsIDsListFormatted.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<SpotsRoutesModel> listSpotsRoutesModel = new List<SpotsRoutesModel>();

                int i = 0;
                foreach (string strSpotID in arrSelectedSpotIDs)
                {
                    listSpotsRoutesModel.Add(new SpotsRoutesModel() { Ordinal = i, RouteID = objRouteModel.RouteID, SpotID = Guid.Parse(strSpotID) });
                    i++;
                }

                objRouteModel.Name = objRouteViewModel.Name;
                objRouteModel.Description = objRouteViewModel.Description;
                objRouteModel.IsSystemRoute = objRouteViewModel.IsSystemRoute;
                objRouteModel.Spots = listSpotsRoutesModel;

                objApplicationDbContext.SaveChanges();

                return RedirectToAction("Edit", new { @id = id.ToString() });
            }

            return View(objRouteViewModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                RouteModel objRouteModel = objApplicationDbContext
                                                    .Routes
                                                    .Include(x => x.Spots)
                                                    .FirstOrDefault(x => x.RouteID.Equals(id));

                objApplicationDbContext.Routes.Remove(objRouteModel);
                objApplicationDbContext.SaveChanges();
            }
            catch (Exception ex) { }

            return RedirectToAction("Index");
        }
    }
}