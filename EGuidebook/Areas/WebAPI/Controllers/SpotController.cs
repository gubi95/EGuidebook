using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EGuidebook.Models;
using EGuidebook.Areas.WebAPI.Models;
using System.Data.Entity;
using System.Web;
using System.IO;
using System.Drawing;

namespace EGuidebook.Areas.WebAPI.Controllers
{
    public class SpotController : ApiController
    {
        public class GetForGoogleMapResponse : WebAPIResponse
        {
            public SpotForGoogleMap[] Spots { get; private set; }

            public GetForGoogleMapResponse(bool bSuccess, EnumWebAPIResponseCode Code, SpotForGoogleMap[] arrSpotForGoogleMap) : base(bSuccess, Code)
            {
                this.Spots = arrSpotForGoogleMap;
            }
        }

        public GetForGoogleMapResponse GetForGoogleMap()
        {
            try
            {
                SpotForGoogleMap[] arrSpotForGoogleMap = new ApplicationDbContext()
                                                                .Spots
                                                                .Where(x => x.IsApproved)
                                                                .Select(x =>
                                                                    new
                                                                    {
                                                                        x.SpotID,
                                                                        x.Name,
                                                                        x.CoorX,
                                                                        x.CoorY
                                                                    })
                                                                .ToList()
                                                                .Select(x =>
                                                                    new SpotForGoogleMap(
                                                                        x.SpotID.ToString(),
                                                                        x.Name,
                                                                        double.Parse(x.CoorX, System.Globalization.CultureInfo.InvariantCulture),
                                                                        double.Parse(x.CoorY, System.Globalization.CultureInfo.InvariantCulture)
                                                                    )
                                                                 )
                                                                 .ToArray();

                return new GetForGoogleMapResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, arrSpotForGoogleMap);
            }
            catch (Exception ex) { }

            return new GetForGoogleMapResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, new SpotForGoogleMap[] { });
        }

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
        public GetByResponse GetBy(string CategoryID, string SpotName, string CoorX, string CoorY)
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                var listSpot = objApplicationDbContext
                                .Spots
                                .Where(x => x.IsApproved)
                                .AsQueryable();

                if (!string.IsNullOrEmpty(CategoryID))
                {
                    listSpot = listSpot.Where(x => x.SpotCategoryID.ToString().Equals("" + CategoryID));
                }

                if(!string.IsNullOrEmpty(SpotName))
                {
                    listSpot = listSpot.Where(x => ("" + x.Name).ToLower().Trim().IndexOf(("" + SpotName).Trim().ToLower()) != -1);
                }

                //if(!string.IsNullOrEmpty(CoorX) && !string.IsNullOrEmpty(CoorY))
                //{
                //    double dCoorX = double.Parse(CoorX, System.Globalization.CultureInfo.InvariantCulture);
                //    double dCoorY = double.Parse(CoorY, System.Globalization.CultureInfo.InvariantCulture);


                //}

                List<SpotModel> listSpotModel = listSpot
                                                .Include("Grades.User")
                                                .ToList();

                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));

                return new GetByResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK,
                    listSpotModel.Select(
                        x => new SpotWebAPIModel(x, x.Grades.FirstOrDefault(y => y.User.Id.Equals(objApplicationUser.Id)))).ToList());
            }
            catch (Exception ex) { }
            return new GetByResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, new List<SpotWebAPIModel>());
        }

        public class GetBySpotIDResponse : WebAPIResponse
        {
            public SpotWebAPIModel Spot { get; private set; }

            public GetBySpotIDResponse(bool bSuccess, EnumWebAPIResponseCode Code, SpotWebAPIModel objSpotWebAPIModel) : base(bSuccess, Code)
            {
                this.Spot = objSpotWebAPIModel;
            }
        }

        [HttpGet]
        [WebAPIBasicAuth]
        public GetBySpotIDResponse GetBySpotID(string SpotID)
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                SpotModel objSpotModel = objApplicationDbContext
                                            .Spots
                                            .Include("Grades.User")
                                            .FirstOrDefault(x => x.SpotID.ToString().Equals(SpotID) && x.IsApproved);

                if (objSpotModel == null)
                {
                    return new GetBySpotIDResponse(false, WebAPIResponse.EnumWebAPIResponseCode.SPOT_DOESNT_EXIST, null);
                }
                else
                {
                    ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));

                    return new GetBySpotIDResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, 
                        new SpotWebAPIModel(objSpotModel, objSpotModel.Grades.FirstOrDefault(y => y.User.Id.Equals(objApplicationUser.Id))));
                }
            }
            catch (Exception ex) { }
            return new GetBySpotIDResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, null);
        }

        public class CreateSpotModelPostData
        {
            public string Name { get; set; }
            public string ImageBase64 { get; set; }
            public double CoorX { get; set; }
            public double CoorY { get; set; }
            public int UserGrade { get; set; }
        }

        [HttpPost]
        [WebAPIBasicAuth]
        public WebAPIResponse Create(CreateSpotModelPostData objCreateSpotModelPostData)
        {
            try
            {
                if(string.IsNullOrEmpty(("" + objCreateSpotModelPostData.Name).Trim()))
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_SPOT_NAME);
                }

                if (objCreateSpotModelPostData.CoorX == 0.0 || objCreateSpotModelPostData.CoorY == 0.0)
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_SPOT_COORDINATES);
                }

                string strImage1Path = null;

                if(!string.IsNullOrEmpty(objCreateSpotModelPostData.ImageBase64))
                {
                    try
                    {
                        strImage1Path = $"/Content/assets/img/spot-images/{Guid.NewGuid().ToString()}.png";
                        byte[] arrBytes = Convert.FromBase64String(objCreateSpotModelPostData.ImageBase64);
                        using (MemoryStream objMemoryStream = new MemoryStream(arrBytes, 0, arrBytes.Length))
                        {
                            Image.FromStream(objMemoryStream, true).Save(HttpContext.Current.Server.MapPath(strImage1Path));
                        }
                    }
                    catch(Exception ex)
                    {
                        strImage1Path = null;
                    }
                }

                SpotModel objSpotModel = new SpotModel()
                {
                    SpotID = Guid.NewGuid(),
                    Name = objCreateSpotModelPostData.Name,
                    CoorX = objCreateSpotModelPostData.CoorX.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    CoorY = objCreateSpotModelPostData.CoorY.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    IsApproved = false,
                    Image1Path = strImage1Path
                };

                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                using (objApplicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (objCreateSpotModelPostData.UserGrade >= 1 && objCreateSpotModelPostData.UserGrade <= 5)
                        {
                            SpotGradeModel objSpotGradeModel = new SpotGradeModel()
                            {
                                CreationDate = DateTime.Now,
                                Grade = objCreateSpotModelPostData.UserGrade,
                                Message = null,
                                SpotGradeID = Guid.NewGuid(),
                                User = objApplicationDbContext.Users.FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name))                                
                            };

                            objSpotModel.Grades = new List<SpotGradeModel>() { objSpotGradeModel };
                        }

                        objApplicationDbContext.Spots.Add(objSpotModel);
                        objApplicationDbContext.SaveChanges();
                        objApplicationDbContext.Database.CurrentTransaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        objApplicationDbContext.Database.CurrentTransaction.Rollback();
                    }
                }

                return new WebAPIResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK);
            }
            catch (Exception ex) { }
            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
        }
    }
}
