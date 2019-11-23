using System;

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
    }
}
