using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EGuidebook.Shared
{
    public class Helper
    {
        public static string AlertBoxPath = "~/Views/Shared/AlertBox/AlertBox.cshtml";
        public static string MainLayoutPath = "~/Views/Shared/_MainLayout.cshtml";
        public static string AdminLayoutPath = "~/Views/Shared/_AdminLayout.cshtml";
        public static string NoPhotoImgPath = "/Content/assets/img/no-photo.png";
        public static string EmptyStarImgPath = "/Content/assets/img/star-empty-48.png";
        public static string YellowStarImgPath = "/Content/assets/img/star-yellow-48.png";

        public static string BaseURL
        {
            get
            {
                return  HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                    HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            }
        }

        public enum EnumSelectedMenuItem
        {
            DASHBOARD,
            USERS,
            SPOTS,
            CATEGORIES,
            SETTINGS,
            ROLES
        }

        public static string GenerateRandomPassword(int nPasswordLength, int nBigLettersCount, int nNumericLettersCount, int nNonAlphaNumericalLettersCount)
        {
            Random objRandom = new Random();
            List<char> listBig = RandLetters(objRandom, "ABCDEFGHIJKLMNOPRSTQUWXYZ", nBigLettersCount);
            List<char> listNonAlphaNumeric = RandLetters(objRandom, "!@#$%", nNonAlphaNumericalLettersCount);
            List<char> listSmall = RandLetters(objRandom, "abcdefghijklmnoprstquwxyz", nPasswordLength - (nBigLettersCount + nNonAlphaNumericalLettersCount + nNumericLettersCount));
            List<char> listNumerics = RandLetters(objRandom, "0123456789", nNumericLettersCount);
            List<char> listAllLetters = new List<char>();
            listAllLetters.AddRange(listBig);
            listAllLetters.AddRange(listNonAlphaNumeric);
            listAllLetters.AddRange(listSmall);
            listAllLetters.AddRange(listNumerics);
            listAllLetters = listAllLetters.OrderBy(x => objRandom.Next()).ToList();
            return string.Join("", listAllLetters);
        }

        private static List<char> RandLetters(Random objRandom, string strLetters, int nCount)
        {
            List<char> listChar = new List<char>();
            for(int i = 0; i < nCount; i++)
            {
                listChar.Add(strLetters[objRandom.Next(0, strLetters.Length - 1)]);
            }
            return listChar;
        }

        public static string SaveSpotImage(HttpPostedFileBase objHttpPostedFileBase)
        {
            if(objHttpPostedFileBase != null && objHttpPostedFileBase.ContentLength > 0)
            {
                string strDirectory = HttpContext.Current.Server.MapPath("/Content/assets/img/spot-images/");
                if (!Directory.Exists(strDirectory))
                {
                    Directory.CreateDirectory(strDirectory);
                }
                string strFilename = SaveUploadedFile(objHttpPostedFileBase, strDirectory);
                return "/Content/assets/img/spot-images/" + strFilename;
            }
            return null;
        }

        public static string SaveSpotCategoryIcon(HttpPostedFileBase objHttpPostedFileBase)
        {
            if (objHttpPostedFileBase != null && objHttpPostedFileBase.ContentLength > 0)
            {
                string strDirectory = HttpContext.Current.Server.MapPath("/Content/assets/img/spot-categories-icon/");
                if (!Directory.Exists(strDirectory))
                {
                    Directory.CreateDirectory(strDirectory);
                }
                string strFilename = SaveUploadedFile(objHttpPostedFileBase, strDirectory);
                return "/Content/assets/img/spot-categories-icon/" + strFilename;
            }
            return null;
        }

        public static string SaveUserAvatar(HttpPostedFileBase objHttpPostedFileBase)
        {
            if (objHttpPostedFileBase != null && objHttpPostedFileBase.ContentLength > 0)
            {
                string strDirectory = HttpContext.Current.Server.MapPath("/Content/assets/img/user-avatars/");
                if (!Directory.Exists(strDirectory))
                {
                    Directory.CreateDirectory(strDirectory);
                }
                string strFilename = SaveUploadedFile(objHttpPostedFileBase, strDirectory);
                return "/Content/assets/img/user-avatars/" + strFilename;
            }
            return null;
        }

        private static string SaveUploadedFile(HttpPostedFileBase objHttpPostedFileBase, string strCatalogDirectory)
        {
            string strFileName = Path.GetFileName(objHttpPostedFileBase.FileName);
            string strExtension = Path.GetExtension(strFileName);
            string strNewFileName = Guid.NewGuid().ToString() + "." + strExtension.TrimStart('.');
            objHttpPostedFileBase.SaveAs(Path.Combine(strCatalogDirectory, strNewFileName));
            return strNewFileName;
        }
            
        public static bool CheckIfFileExists(string strVirtualPath)
        {
            try
            {
                return File.Exists(HttpContext.Current.Server.MapPath(strVirtualPath));
            }
            catch (Exception ex) { }
            return false;
        }

        public static void DeleteFile(string strVirtualPath)
        {
            try
            {
                string strMappedPath = HttpContext.Current.Server.MapPath(strVirtualPath);
                if(File.Exists(strMappedPath))
                {
                    File.Delete(strMappedPath);
                }
            }
            catch (Exception ex) { }
        }
    }
}