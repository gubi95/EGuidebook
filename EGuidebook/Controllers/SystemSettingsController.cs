using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;

namespace EGuidebook.Controllers
{
    [Authorize]
    public class SystemSettingsController : Controller
    {
        [HttpGet]
        public ActionResult Edit()
        {
            SystemSettingsModel objSystemSettingsModel = new ApplicationDbContext().SystemSettings.FirstOrDefault();
            return View(objSystemSettingsModel != null ? new SystemSettingsViewModel(objSystemSettingsModel) : new SystemSettingsViewModel());
        }

        [HttpPost]
        public ActionResult Edit(SystemSettingsViewModel objSystemSettingsViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                SystemSettingsModel objSystemSettingsModel = objApplicationDbContext.SystemSettings.FirstOrDefault();

                if(objSystemSettingsModel == null)
                {
                    objSystemSettingsModel = new SystemSettingsModel();
                    objSystemSettingsModel.SystemSettingsId = Guid.NewGuid();
                    objSystemSettingsViewModel.ApplyOnModel(ref objSystemSettingsModel);
                    objApplicationDbContext.SystemSettings.Add(objSystemSettingsModel);
                }                
                else
                {
                    objSystemSettingsViewModel.ApplyOnModel(ref objSystemSettingsModel);
                }

                objApplicationDbContext.SaveChanges();
            }
            return View(objSystemSettingsViewModel);
        }
    }
}