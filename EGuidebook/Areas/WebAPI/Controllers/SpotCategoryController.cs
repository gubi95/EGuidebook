using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EGuidebook.Models;

namespace EGuidebook.Areas.WebAPI
{    
    public class SpotCategoryController : ApiController
    {
        public class GetAllResponse : WebAPIResponse
        {
            public List<SpotCategoryWebAPIModel> SpotCategories { get; set; }
            public GetAllResponse(bool bSuccess, WebAPIResponse.EnumWebAPIResponseCode Code, List<SpotCategoryWebAPIModel> listSpotCategoryWebAPIModel = null) : 
                base(bSuccess, Code)
            {
                this.SpotCategories = listSpotCategoryWebAPIModel;
            }
        }
        
        [HttpGet]
        [WebAPIBasicAuth]
        public GetAllResponse GetAll()
        {            
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                List<SpotCategoryModel> listSpotCategoryModel = objApplicationDbContext.SpotCategories.ToList();
                List<SpotCategoryWebAPIModel> listSpotCategoryWebAPIModel = listSpotCategoryModel.Select(x => new SpotCategoryWebAPIModel(x)).ToList();

                return new GetAllResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, listSpotCategoryWebAPIModel);
            }
            catch(Exception ex)
            {
                return new GetAllResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SEVER_ERROR);
            }
        }
    }
}
