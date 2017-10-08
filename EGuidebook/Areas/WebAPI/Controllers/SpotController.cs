using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EGuidebook.Models;
using EGuidebook.Areas.WebAPI.Models;
using System.Data.Entity;

namespace EGuidebook.Areas.WebAPI.Controllers
{
    public class SpotController : ApiController
    {
        public class GetByResponse : WebAPIResponse
        {
            public List<SpotWebAPIModel> Spots { get; private set; }

            public GetByResponse(bool bSuccess, EnumWebAPIResponseCode Code, List<SpotWebAPIModel> listSpotWebAPIModel) : base(bSuccess, Code)
            {
                this.Spots = listSpotWebAPIModel;
            }
        }

        [HttpGet]
        [WebAPIBasicAuth]
        public GetByResponse GetBy(string CategoryID, string CoorX, string CoorY)
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                var listSpot = objApplicationDbContext
                                .Spots                                
                                .AsQueryable();

                if (!string.IsNullOrEmpty(CategoryID))
                {
                    listSpot = listSpot.Where(x => x.SpotCategoryID.ToString().Equals("" + CategoryID));
                }

                //if(!string.IsNullOrEmpty(CoorX) && !string.IsNullOrEmpty(CoorY))
                //{
                //    double dCoorX = double.Parse(CoorX, System.Globalization.CultureInfo.InvariantCulture);
                //    double dCoorY = double.Parse(CoorY, System.Globalization.CultureInfo.InvariantCulture);


                //}

                List<SpotModel> listSpotModel = listSpot
                                                .Include(x => x.Grades)
                                                .ToList();

                return new GetByResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, listSpotModel.Select(x => new SpotWebAPIModel(x)).ToList());
            }
            catch(Exception ex) { }
            return new GetByResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, new List<SpotWebAPIModel>());
        }
    }
}
