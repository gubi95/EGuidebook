using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;
using System.Data.Entity;
using System.Data.Entity;

namespace EGuidebook.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SpotController : Controller
    {    
        [HttpGet]        
        public ActionResult Index()
        {
            return View(new ApplicationDbContext()
                .Spots
                .Include(x => x.ApprovedBy)
                .Include(x => x.Grades)
                .ToList()
                .Select(x => new SpotViewModel(x))
                .ToList());
        }

        [HttpGet]        
        public ActionResult Create()
        {
            return View(new SpotViewModel());
        }

        [HttpPost]
        public ActionResult Create(SpotViewModel objSpotViewModel)
        {
            if(ModelState.IsValid)
            {
                SpotModel objSpotModel = new SpotModel();
                objSpotModel.SpotID = Guid.NewGuid();
                objSpotViewModel.ApplyToModel(ref objSpotModel, true);
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                objApplicationDbContext.Spots.Add(objSpotModel);
                objApplicationDbContext.SaveChanges();
                return RedirectToAction("Edit", new { @id = objSpotModel.SpotID.ToString() });
            }
            return View(objSpotViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
            SpotModel objSpotModel = objApplicationDbContext
                                        .Spots
                                        .Include(x => x.Grades.Select(y => y.User))
                                        .FirstOrDefault(x => x.SpotID.Equals(id));

            if(objSpotModel != null)
            {
                return View(new SpotViewModel(objSpotModel));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid id, SpotViewModel objSpotViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                SpotModel objSpotModel = objApplicationDbContext
                                            .Spots
                                            .Include(x => x.Grades)
                                            .FirstOrDefault(x => x.SpotID.Equals(id));

                if (objSpotModel != null)
                {
                    objSpotViewModel.ApplyToModel(ref objSpotModel, false);
                    objApplicationDbContext.SaveChanges();
                    return RedirectToAction("Edit", new { @id = objSpotModel.SpotID.ToString() });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(objSpotViewModel);
        }

        [HttpPost]
        public ActionResult Approve(SpotViewModel objSpotViewModel)
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

            SpotModel objSpotModel = objApplicationDbContext
                                        .Spots
                                        .FirstOrDefault(x => x.SpotID.Equals(objSpotViewModel.SpotID));

            if(objSpotModel != null)
            {
                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(HttpContext.User.Identity.Name));

                objSpotModel.IsApproved = true;
                objSpotModel.ApprovalDate = DateTime.Now;
                objSpotModel.ApprovedByUserID = objApplicationUser.Id;
                objApplicationDbContext.SaveChanges();
            }

            return RedirectToAction("Edit", new { @id = objSpotModel.SpotID.ToString() });
        }
    }
}