using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Areas.WebAPI
{
    public class WebAPIResponse
    {
        public enum EnumWebAPIResponseCode
        {
            OK = 0,
            INTERNAL_SERVER_ERROR = 1,
            UNAUTHORIZED_ACCESS = 2,
            GRADE_INCORRECT_VALUE = 3,
            SPOT_DOESNT_EXIST = 4,
            USER_ALREADY_EXISTS = 5,
            INCORRECT_USERNAME = 6,
            INCORRECT_PASSWORD = 7,
            INCORRECT_SPOT_NAME = 8,
            INCORRECT_SPOT_COORDINATES = 9,
            INCORRECT_ROUTE_NAME = 10,
            INCORRECT_ROUTE_SPOTS = 11,
            INCORRECT_ROUTE_ID = 12
        }

        public bool Success { get; private set; }
        public EnumWebAPIResponseCode Code { get; private set; }
        public string Message { get; private set; }

        public WebAPIResponse(bool bSuccess, EnumWebAPIResponseCode Code)
        {
            this.Success = bSuccess;
            this.Code = Code;
            this.Message = Code.ToString();
        }
    }
}