using EGuidebook.Areas.WebAPI;
using EGuidebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EGuidebook.Areas.WebAPI.Controllers
{    
    public class UserController : ApiController
    {
        //public class RegisterRequestData
        //{
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //}

        //public class RegisterResponseData
        //{

        //}

        //[HttpPost]
        //public void Register(RegisterRequestData objRegisterRequestData)
        //{

        //}

        public class LoginResponse : WebAPIResponse
        {
            public List<SpotCategoryModel> SpotCategories { get; private set; }

            public LoginResponse(bool bSuccess, EnumWebAPIResponseCode eCode, List<SpotCategoryModel> listSpotCategoryModel) : base(bSuccess, eCode)
            {
                this.SpotCategories = listSpotCategoryModel;
            }
        }

        [WebAPIBasicAuth]
        [HttpPost]
        public LoginResponse Login()
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                List<SpotCategoryModel> listSpotCategoryModel = objApplicationDbContext
                                                                    .SpotCategories
                                                                    .ToList();

                return new LoginResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, listSpotCategoryModel);
            }
            catch (Exception ex) { }
            return new LoginResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SEVER_ERROR, null);
        }
    }
}
