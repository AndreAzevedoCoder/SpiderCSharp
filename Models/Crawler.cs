using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using HtmlAgilityPack;

namespace SpiderOficial.Models
{
    class Crawler
    {
        public int TimeBetweenSearchs = 5;
        bool UseTor = false;
        public List<string> BlackList = new List<string>();
        public static void ConfigureCrawlerNoArguments(){
            List<string> EmptyBlackList = new List<string>();
            List<string> EmptyURLS = new List<string>();
            List<string> EmptyVisitedURLS = new List<string>();
            Crawler.ConfigureCrawler(5,false,EmptyBlackList,EmptyURLS,EmptyVisitedURLS,10);
        }


        public static void ConfigureCrawler(int TimeBetweenSearchs,bool UseTor,List<string> BlackList,List<string> URLS, List<string> VisitedURLS,int HowManyLinks){
            string Option = "6";
            do
            {
                Console.Clear();
                System.Console.WriteLine("Select one of those if you want to change them");
                System.Console.WriteLine();
                System.Console.WriteLine("1- Start");
                System.Console.WriteLine("2- Time between searchs ("+TimeBetweenSearchs+" seconds)");
                System.Console.WriteLine("3- Use Tor Proxy ("+UseTor+")");
                System.Console.WriteLine("4- Configure BlackList");
                System.Console.WriteLine("5- Configure how many links visit ("+HowManyLinks+")");
                System.Console.WriteLine("6- Configure what to save");
                System.Console.WriteLine("0- Exit");
                Option = Console.ReadLine();
                switch( Option ){
                    case "1":
                        Crawler.Start(URLS,VisitedURLS,BlackList,HowManyLinks,TimeBetweenSearchs);
                        break;


                        
                    case "2":
                        System.Console.WriteLine("How much seconds do you want between searchs? (we recomend 5 seconds)");
                        string StringTime = Console.ReadLine();
                        if(General.IsNumeric(StringTime)){
                            int IntTime = int.Parse(StringTime);
                            if(IntTime > 0){
                                TimeBetweenSearchs = IntTime;
                                
                            }else{
                                System.Console.WriteLine("This value is under 0!");
                            }
                        }else{
                            System.Console.WriteLine("This value isn't numeric!");
                            Console.ReadLine();
                        }
                        break;



                    case "3":
                        System.Console.WriteLine("Do you want to use Tor Proxy? (y/n)");
                        string StringTor = Console.ReadLine();
                        StringTor = StringTor.ToLower();
                        switch(StringTor){
                            case "y":
                                UseTor = true;
                                break;
                            case "n":
                                UseTor = false;
                                break;
                            default:
                                System.Console.WriteLine("Invalid option! use \"y\" or \"n\"!");
                                break;
                        }
                        break;
                    case "4":
                        System.Console.WriteLine("This is your BlackList: ");
                        for(int i =0; i  < BlackList.Count; i++){
                            System.Console.WriteLine(i+"- "+BlackList[i]);
                        }
                        System.Console.WriteLine();
                        System.Console.WriteLine("Options: ");
                        System.Console.WriteLine("1- Add");
                        System.Console.WriteLine("2- Delete");
                        System.Console.WriteLine("0- Exit BlackList");
                        string BlackListOptions = Console.ReadLine();
                        
                        switch(BlackListOptions){
                            case "1":
                                System.Console.WriteLine("Type what link you want to avoid: (example: https://example.com/");
                                
                                string[] result = Console.ReadLine().Split(new [] { "," }, StringSplitOptions.None);
                                foreach(string item in result){
                                    BlackList.Add(item);
                                }
                                break;
                            case "2":
                                System.Console.WriteLine("                                           Type the index of the link you want to delete from the list");
                                string IndexToDelete = Console.ReadLine();  
                                if(General.IsNumeric(IndexToDelete)){
                                    BlackList.Remove(BlackList[int.Parse(IndexToDelete)]);
                                }else{
                                    System.Console.WriteLine("You didn't type a numeric!");
                                }
                                break;
                        }
                        break;
                    case "5":
                        System.Console.WriteLine("How many links visit?");
                        string StringHowManyLinskVisit = Console.ReadLine();
                        if (General.IsNumeric(StringHowManyLinskVisit)){
                            HowManyLinks = int.Parse(StringHowManyLinskVisit);
                        }else{
                            System.Console.WriteLine("You didn't type a numeric!");
                        }
                        break;
                    case "0":
                        General.Exit();
                        break;
                    default:
                        Crawler.ConfigureCrawler(TimeBetweenSearchs,UseTor,BlackList,URLS,VisitedURLS,HowManyLinks);
                        break;
                }


            }while(Option != "1");

            
        }
        

        public static void Start(List<string> URLS, List<string> VisitedURLS, List<string> BlackList,int HowManyLinks, int TimeBetweenSearchs){
            System.Console.WriteLine("Digit the initial link: (if is more than one split with \",\") ");
            string[] result = Console.ReadLine().Split(new [] { "," }, StringSplitOptions.None);
            foreach(string item in result){
                if(ValidateLink(item)){
                    URLS.Add(item);
                }
            }

            while(VisitedURLS.Count < HowManyLinks && URLS.Count > 0){
                
                try{
                    
                    

                    bool VerifyIfAlreadyVisited = VisitedURLS.Contains(URLS[0]);
                    bool VerifyIfIsInBlackList = BlackList.Contains(URLS[0]);
                    if(VerifyIfAlreadyVisited == false && VerifyIfIsInBlackList == false){
                        var wc = new WebClient();
                        string page = wc.DownloadString(URLS[0]);
                        var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(page);

                        
                        foreach (HtmlNode link in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
                        {
                        
                            string LinksCaught = link.GetAttributeValue( "href", string.Empty );

                            if(ValidateLinksCaught(LinksCaught)){
                                URLS.Add(LinksCaught);
                            }
                            
                        }
                
                    

                        VisitedURLS.Add(URLS[0]);
                        System.Console.WriteLine("I've already visited: "+VisitedURLS.Count+" links");
                        URLS.RemoveAt(0);
                        Thread.Sleep(TimeBetweenSearchs*1000);

                        
                    }else{
                        URLS.RemoveAt(0);
                    }
                


                }catch(Exception ex){//catch(ArgumentOutOfRangeException e){
                        URLS.RemoveAt(0);
                }
               
                

            
        }
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("URLS VISITADAS: ");
            foreach(string Visited in VisitedURLS){
                System.Console.WriteLine(Visited);
            }
            Console.ReadLine();
            


        }


        public static bool ValidateLink(string Link){
            bool Validated = false;
                if(Link.Substring(0,4) == "http"){
                    Validated = true;
                }else{
                    System.Console.WriteLine("The URL is invalid (try something like: https://example.com/)");
                    Console.ReadLine();
                }
            return Validated;
        }
        public static bool ValidateLinksCaught(string Link){


            if(Link.Length > 0){
                string LinkFirstLetter = Link.Substring(0,1);
                if(LinkFirstLetter == "/" && LinkFirstLetter.Length > 2){
                    Link = Link.Remove(0,1);
                    return true;
                }
                return true;
            }
            return true;
        }
    }
}
