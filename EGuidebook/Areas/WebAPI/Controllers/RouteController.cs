using EGuidebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;

namespace EGuidebook.Areas.WebAPI.Controllers
{
    public class RouteController : ApiController
    {
        public class Route
        {
            public string RouteID { get; private set; }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public bool IsSystemRoute { get; private set; }
            public RouteSpot[] Spots { get; private set; }

            public Route(RouteModel objRouteModel)
            {
                this.RouteID = objRouteModel.RouteID.ToString();
                this.Name = objRouteModel.Name;
                this.Description = objRouteModel.Description;
                this.IsSystemRoute = objRouteModel.IsSystemRoute;
                this.Spots = objRouteModel.Spots != null ? objRouteModel.Spots.Select(x => new RouteSpot(x)).ToArray() : new RouteSpot[] { };
            }
        }

        public class RouteSpot
        {
            public string SpotID { get; private set; }
            public string Name { get; private set; }
            public double CoorX { get; private set; }
            public double CoorY { get; private set; }

            public RouteSpot(SpotModel objSpotModel)
            {
                this.SpotID = objSpotModel.SpotID.ToString();
                this.Name = objSpotModel.Name;
                this.CoorX = double.Parse(objSpotModel.CoorX, System.Globalization.CultureInfo.InvariantCulture);
                this.CoorY = double.Parse(objSpotModel.CoorY, System.Globalization.CultureInfo.InvariantCulture); 
            }
        }

        public class GetAllResponse : WebAPIResponse
        {
            public Route[] Routes { get; private set; }

            public GetAllResponse(bool bSuccess, EnumWebAPIResponseCode Code, Route[] arrRoute) : base(bSuccess, Code)
            {
                this.Routes = arrRoute;
            }
        }

        [HttpGet]
        [WebAPIBasicAuth]
        public GetAllResponse GetAll()
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));

                List<Route> listRoute = objApplicationDbContext
                                            .Routes
                                            .Include(x => x.Spots)
                                            .Where(x => x.CreatedByUserID.Equals(objApplicationUser.Id.ToString()) || x.IsSystemRoute)
                                            .ToList()
                                            .Select(x => new Route(x))
                                            .ToList();

                return new GetAllResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, listRoute.ToArray());
            }
            catch (Exception ex) { }
            return new GetAllResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, new Route[] { });
        }

        public class CreateRoutePostData
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string[] SpotIDs { get; set; }
        }
                
        [HttpPost]
        [WebAPIBasicAuth]
        public WebAPIResponse Create(CreateRoutePostData objCreateRoutePostData)
        {
            try
            {
                if (objCreateRoutePostData == null)
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
                }

                if(string.IsNullOrEmpty(("" + objCreateRoutePostData.Name).Trim()))
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_NAME);
                }

                if (objCreateRoutePostData.SpotIDs == null || objCreateRoutePostData.SpotIDs.Length < 2)
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_SPOTS);
                }

                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                using (DbContextTransaction objDbContextTransaction = objApplicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        List<SpotModel> listSpotModel = objApplicationDbContext
                                                            .Spots
                                                            .Where(x => objCreateRoutePostData.SpotIDs.Contains(x.SpotID.ToString()))
                                                            .ToList();

                        if (listSpotModel.Count != objCreateRoutePostData.SpotIDs.Length)
                        {
                            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_SPOTS);
                        }

                        ApplicationUser objApplicationUser = objApplicationDbContext
                                                                .Users
                                                                .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));

                        RouteModel objRouteModel = new RouteModel()
                        {
                            RouteID = Guid.NewGuid(),
                            CreatedByUserID = objApplicationUser.Id,
                            Description = objCreateRoutePostData.Description,
                            Name = objCreateRoutePostData.Name,
                            IsSystemRoute = false,
                            Spots = listSpotModel
                        };

                        objApplicationDbContext.Routes.Add(objRouteModel);
                        objApplicationDbContext.SaveChanges();

                        objDbContextTransaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        objDbContextTransaction.Rollback();
                    }
                }
            }
            catch (Exception ex) { }
            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
        }

        public class EditRoutePostData
        {
            public string RouteID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string[] SpotIDs { get; set; }
        }

        [HttpPost]
        [WebAPIBasicAuth]
        public WebAPIResponse Edit(EditRoutePostData objEditRoutePostData)
        {
            try
            {
                if (objEditRoutePostData == null)
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
                }

                if (string.IsNullOrEmpty(("" + objEditRoutePostData.Name).Trim()))
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_NAME);
                }

                if (objEditRoutePostData.SpotIDs == null || objEditRoutePostData.SpotIDs.Length < 2)
                {
                    return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_SPOTS);
                }

                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                using (DbContextTransaction objDbContextTransaction = objApplicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        RouteModel objRouteModel = objApplicationDbContext
                                                    .Routes
                                                    .FirstOrDefault(x => x.RouteID.ToString().Equals(objEditRoutePostData.RouteID));

                        if (objRouteModel == null)
                        {
                            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_ID);
                        }

                        List<SpotModel> listSpotModel = objApplicationDbContext
                                                            .Spots
                                                            .Where(x => objEditRoutePostData.SpotIDs.Contains(x.SpotID.ToString()))
                                                            .ToList();

                        if (listSpotModel.Count != objEditRoutePostData.SpotIDs.Length)
                        {
                            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_ROUTE_SPOTS);
                        }

                        ApplicationUser objApplicationUser = objApplicationDbContext
                                                                .Users
                                                                .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));                        

                        objRouteModel.Name = objEditRoutePostData.Name;
                        objRouteModel.Description = objEditRoutePostData.Description;
                        objRouteModel.IsSystemRoute = false;
                        objRouteModel.Spots = listSpotModel;
                        
                        objApplicationDbContext.SaveChanges();

                        objDbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        objDbContextTransaction.Rollback();
                    }
                }
            }
            catch (Exception ex) { }
            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
        }

        public class DeletePostData
        {
            public string RouteID { get; set; }
        }

        [WebAPIBasicAuth]
        [HttpPost]
        public WebAPIResponse Delete(DeletePostData objDeletePostData)
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                RouteModel objRouteModel = new RouteModel()
                {
                    RouteID = Guid.Parse(objDeletePostData.RouteID)
                };
                objApplicationDbContext.Routes.Attach(objRouteModel);
                objApplicationDbContext.Routes.Remove(objRouteModel);
                objApplicationDbContext.SaveChanges();

                return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.OK);
            }

            catch (Exception ex) { }
            return new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
        }
    }    
}
