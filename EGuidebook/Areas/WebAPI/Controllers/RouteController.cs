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
