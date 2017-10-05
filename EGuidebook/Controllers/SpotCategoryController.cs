using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;
using EGuidebook.Shared;

namespace EGuidebook.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SpotCategoryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {               
            return View(new ApplicationDbContext().SpotCategories.ToList().Select(x => new SpotCategoryViewModel(x)).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new SpotCategoryViewModel());
        }

        [HttpPost]
        public ActionResult Create(SpotCategoryViewModel objSpotCategoryViewModel, HttpPostedFileBase icon)
        {
            if(ModelState.IsValid)
            {
                SpotCategoryModel objSpotCategoryModel = new SpotCategoryModel();
                objSpotCategoryModel.SpotCategoryId = Guid.NewGuid();
                objSpotCategoryViewModel.ApplyOnModel(ref objSpotCategoryModel);
                objSpotCategoryModel.IconPath = Helper.SaveSpotCategoryIcon(icon);
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                objApplicationDbContext.SpotCategories.Add(objSpotCategoryModel);
                objApplicationDbContext.SaveChanges();
                return RedirectToAction("Edit", new { id = objSpotCategoryModel.SpotCategoryId.ToString() });
            }
            return View(objSpotCategoryViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            SpotCategoryModel objSpotCategoryModel = new ApplicationDbContext()
                                                        .SpotCategories
                                                        .FirstOrDefault(x => x.SpotCategoryId.Equals(id));

            if(objSpotCategoryModel != null)
            {
                return View(new SpotCategoryViewModel(objSpotCategoryModel));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid id, SpotCategoryViewModel objSpotCategoryViewModel, HttpPostedFileBase icon)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                SpotCategoryModel objSpotCategoryModel = objApplicationDbContext
                                                            .SpotCategories
                                                            .FirstOrDefault(x => x.SpotCategoryId.Equals(id));

                if (objSpotCategoryModel != null)
                {
                    if (!string.IsNullOrEmpty(objSpotCategoryViewModel.IconPath))
                    {
                        if (icon != null && icon.ContentLength > 0 && ("" + objSpotCategoryViewModel.IconPath) != ("" + objSpotCategoryModel.IconPath))
                        {
                            Helper.DeleteFile(objSpotCategoryModel.IconPath);
                            objSpotCategoryViewModel.IconPath = Helper.SaveSpotCategoryIcon(icon);
                        }
                    }
                    else
                    {
                        Helper.DeleteFile(objSpotCategoryModel.IconPath);
                        objSpotCategoryViewModel.IconPath = null;
                    }

                    objSpotCategoryViewModel.ApplyOnModel(ref objSpotCategoryModel);
                    objApplicationDbContext.SaveChanges();
                    objSpotCategoryViewModel.SpotCategoryId = id;
                    return View(objSpotCategoryViewModel);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            objSpotCategoryViewModel.SpotCategoryId = id;
            return View(objSpotCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
            SpotCategoryModel objSpotCategoryModel = objApplicationDbContext
                                                        .SpotCategories
                                                        .FirstOrDefault(x => x.SpotCategoryId.Equals(id));

            if(objSpotCategoryModel != null)
            {
                Helper.DeleteFile(objSpotCategoryModel.IconPath);
                objApplicationDbContext.SpotCategories.Remove(objSpotCategoryModel);
                objApplicationDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}