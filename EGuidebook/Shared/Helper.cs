using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Shared
{
    public class Helper
    {
        public static string AlertBoxPath = "~/Views/Shared/AlertBox/AlertBox.cshtml";
        public static string MainLayoutPath = "~/Views/Shared/_MainLayout.cshtml";
        public static string AdminLayoutPath = "~/Views/Shared/_AdminLayout.cshtml";

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
            SETTINGS
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
    }
}