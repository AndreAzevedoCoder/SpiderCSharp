using System;
using System.Linq;

namespace SpiderOficial.Models
{
    class General
    {
        public static void Exit(){
            System.Console.WriteLine("Bye!");
            System.Environment.Exit(1);
        }
        public static bool IsNumeric(string IsThisStringNumeric){
            return int.TryParse(IsThisStringNumeric, out int n);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string MakeFolderTitleByURL(string url){
            url = url.Replace("https:", ""); 
            url = url.Replace("http:", ""); 
            url = url.Replace("//www.", ""); 
            url = url.Replace("//", ""); 
            if(url.IndexOf(".") > 0){
                url = url.Substring(0, url.IndexOf("."));
            }
            return url;
        }
    }
}
