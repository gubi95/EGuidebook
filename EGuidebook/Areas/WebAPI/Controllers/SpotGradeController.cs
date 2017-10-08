using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EGuidebook.Models;
using System.Data.Entity;

namespace EGuidebook.Areas.WebAPI.Controllers
{    
    public class SpotGradeController : ApiController
    {
        public class CreateResponse : WebAPIResponse
        {
            public CreateResponse(bool bSuccess, WebAPIResponse.EnumWebAPIResponseCode Code) : base(bSuccess, Code) { }
        }

        public class CreateRequestData
        {
            public Guid SpotID { get; set; }
            public int Grade { get; set; }
            public string Message { get; set; }
        }

        [HttpPost]
        [WebAPIBasicAuth]
        public CreateResponse Create(CreateRequestData objCreateRequestData)
        {
            try
            {
                if (objCreateRequestData.Grade >= 1 && objCreateRequestData.Grade <= 5)
                {
                    ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                    ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));

                    SpotModel objSpotModel = objApplicationDbContext
                                                .Spots
                                                .Include(x => x.Grades.Select(y => y.User))                                                
                                                .FirstOrDefault(x => x.SpotID.Equals(objCreateRequestData.SpotID));

                    if (objSpotModel != null)
                    {
                        SpotGradeModel objSpotGradeModelExist = objSpotModel
                                                            .Grades
                                                            .FirstOrDefault(x => x.User.UserName.Equals(objApplicationUser.UserName));

                        if(objSpotGradeModelExist != null)
                        {
                            objSpotGradeModelExist.CreationDate = DateTime.Now;
                            objSpotGradeModelExist.Grade = objCreateRequestData.Grade;
                            objSpotGradeModelExist.Message = objCreateRequestData.Message;
                        }
                        else
                        {
                            SpotGradeModel objSpotGradeModel = new SpotGradeModel()
                            {
                                CreationDate = DateTime.Now,
                                Grade = objCreateRequestData.Grade,
                                Message = objCreateRequestData.Message,
                                SpotGradeID = Guid.NewGuid(),
                                User = objApplicationUser
                            };
                            objSpotModel.Grades.Add(objSpotGradeModel);                            
                        }

                        objApplicationDbContext.SaveChanges();
                        return new CreateResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK);
                    }
                    else
                    {
                        return new CreateResponse(false, WebAPIResponse.EnumWebAPIResponseCode.SPOT_DOESNT_EXIST);
                    }
                }
                else
                {
                    return new CreateResponse(false, WebAPIResponse.EnumWebAPIResponseCode.GRADE_INCORRECT_VALUE);
                }
            }
            catch (Exception ex)
            {
                return new CreateResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}