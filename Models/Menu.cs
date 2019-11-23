using System;

namespace SpiderOficial.Models
{
    class Menu
    {
        public static void StartMenu()
        {
            Console.Clear();
            System.Console.WriteLine("           (");
            System.Console.WriteLine("           )");
            System.Console.WriteLine("     /\\  .-\"\"\"-.  /\\ ");
            System.Console.WriteLine("    //\\\\/  ,,,  \\//\\");
            System.Console.WriteLine("    |/\\| ,;;;;;, |/\\|        ");
            System.Console.WriteLine("    |/\\| ,;;;;;, |/\\|          _________      .__    .___             _________        .__                         ");
            System.Console.WriteLine("    //\\\\\\;-\"\"\"-;///\\\\         /   _____/_____ |__| __| _/___________  \\_   ___ \\  _____|  |__ _____ _____________  ");
            System.Console.WriteLine("  //  \\/   .   \\/  \\          \\_____  \\\\____ \\|  |/ __ |/ __ \\_  __ \\ /    \\  \\/ /  ___/  |  \\\\__  \\_  __ \\____  \\ ");
            System.Console.WriteLine("  (| ,-_| \\ | / |_-, |)       /        \\  |_> >  / /_/ \\  ___/|  | \\/ \\     \\____\\___ \\|   Y  \\/ __ \\|  | \\/  |_> >");
            System.Console.WriteLine("  //`__\\.-.-./__`\\\\          /_______  /   __/|__\\____ |\\___  >__|     \\______  /____  >___|  (____  /__|  |   __/ ");
            System.Console.WriteLine("  // /.-(() ())-.\\ \\\\                \\/|__|           \\/    \\/                \\/     \\/     \\/     \\/      |__|");
            System.Console.WriteLine("  // /.-(() ())-.\\ \\\\       ");
            System.Console.WriteLine(" (\\ |)   '---'   (| /)                                   Crawler created by: André S. Azevedo");
            System.Console.WriteLine(" ` (|             |) `");
            System.Console.WriteLine("  \\)             (/");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("                                                          1- Start");
            System.Console.WriteLine("                                                          0- Exit");
            System.Console.WriteLine();
            string Option = Console.ReadLine();
            do
            {
                switch( Option ){
                    case "1":
                        Crawler.ConfigureCrawlerNoArguments();
                        break;
                    case "0":
                        General.Exit();
                        break;
                    default:
                        Menu.StartMenu();
                        break;
                }

            }while(Option != "1");
        }
    }
}
