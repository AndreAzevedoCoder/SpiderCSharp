using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using HtmlAgilityPack;

namespace SpiderOficial.Models
{
    class Crawler
    {
        public int TimeBetweenSearchs = 5000;
        bool UseTor = false;
        public List<string> BlackList = new List<string>();
        public static void ConfigureCrawlerNoArguments(){
            List<string> EmptyBlackList = new List<string>();
            List<string> EmptyURLS = new List<string>();
            List<string> EmptyVisitedURLS = new List<string>();
            List<string> EmptyWhatToSave = new List<string>();
            Crawler.ConfigureCrawler(5000,false,EmptyBlackList,EmptyURLS,EmptyVisitedURLS,EmptyWhatToSave,10);
        }


        public static void ConfigureCrawler(int TimeBetweenSearchs,bool UseTor,List<string> BlackList,List<string> URLS, List<string> VisitedURLS, List<string> WhatToSave,int HowManyLinks){
            string Option = "6";
            do
            {
                Console.Clear();
                System.Console.WriteLine("Select one of those if you want to change them");
                System.Console.WriteLine();
                System.Console.WriteLine("1- Start");
                System.Console.WriteLine("2- Time between searchs ("+TimeBetweenSearchs+" miliseconds)");
                System.Console.WriteLine("3- Use Tor Proxy ("+UseTor+")");
                System.Console.WriteLine("4- Configure BlackList");
                System.Console.WriteLine("5- Configure how many links visit ("+HowManyLinks+")");
                System.Console.WriteLine("6- Configure what to save");
                System.Console.WriteLine("0- Exit");
                Option = Console.ReadLine();
                switch( Option ){
                    case "1":
                        Crawler.Start(URLS,VisitedURLS,BlackList,HowManyLinks,TimeBetweenSearchs,WhatToSave);
                        break;


                        
                    case "2":
                        System.Console.WriteLine("How much seconds do you want between searchs? (we recomend 5000 miliseconds)");
                        string StringTime = Console.ReadLine();
                        if(General.IsNumeric(StringTime)){
                            int IntTime = int.Parse(StringTime);
                            if(IntTime >= 0){
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
                                System.Console.WriteLine("Type the index of the link you want to delete from the list");
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
                    case "6":
                        System.Console.WriteLine("This is what you want to save: ");
                        for(int i =0; i  < WhatToSave.Count; i++){
                            System.Console.WriteLine(i+"- "+WhatToSave[i]);
                        }
                        System.Console.WriteLine();
                        System.Console.WriteLine("Options: ");
                        System.Console.WriteLine("1- Add");
                        System.Console.WriteLine("2- Delete");
                        System.Console.WriteLine("0- Exit what to save list");

                        string WhatToCrawlAndSave = Console.ReadLine();
                        
                        switch(WhatToCrawlAndSave){
                            case "1":
                                System.Console.WriteLine("Type what tag and maybe the attribute you want to save: (example: img:href)");
                                    WhatToSave.Add(Console.ReadLine());
                                break;

                            case "2":
                                System.Console.WriteLine("Type the index of the tag you want to delete from the list");
                                string IndexToDelete = Console.ReadLine();  
                                if(General.IsNumeric(IndexToDelete)){
                                    WhatToSave.Remove(WhatToSave[int.Parse(IndexToDelete)]);
                                }else{
                                    System.Console.WriteLine("You didn't type a numeric!");
                                }
                                break;
                        }
                        break;
                    case "0":
                        General.Exit();
                        break;
                    default:
                        Crawler.ConfigureCrawler(TimeBetweenSearchs,UseTor,BlackList,URLS,VisitedURLS,WhatToSave,HowManyLinks);
                        break;
                }


            }while(Option != "1");

            
        }
        

        public static void Start(List<string> URLS, List<string> VisitedURLS, List<string> BlackList,int HowManyLinks, int TimeBetweenSearchs,List<string> WhatToSave){
            string resultstring = "";
            do{
                System.Console.WriteLine("Digit the initial link: (if is more than one split with \",\") or press 0 to return");
                resultstring = Console.ReadLine();
                if(resultstring == "0"){
                    ConfigureCrawler(TimeBetweenSearchs,false,BlackList,URLS,VisitedURLS,WhatToSave,HowManyLinks);
                }
                else if(resultstring != "")
                {
                    string[] result = resultstring.Split(new [] { "," }, StringSplitOptions.None);
                    foreach(string item in result){
                        if(ValidateLink(item)){
                            URLS.Add(item);
                        }
                    }
                }
            }while(resultstring == "");


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
                        string foldername = General.MakeFolderTitleByURL(URLS[0]);
                        string mainfolder = "Crawled";
                        

                        SaveCrawledInformation(foldername,mainfolder,page,WhatToSave);

                    

                        VisitedURLS.Add(URLS[0]);
                        System.Console.WriteLine("I've already visited: "+VisitedURLS.Count+" links");
                        URLS.RemoveAt(0);
                        Thread.Sleep(TimeBetweenSearchs);

                        
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


        public static void SaveCrawledInformation(string foldername,string mainfolder,string page,List<string> WhatToSave){
            System.IO.Directory.CreateDirectory(mainfolder);
            System.IO.Directory.CreateDirectory(mainfolder+"/"+foldername);
            
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(page);

            List<string> Imagens = new List<string>();
            List<string> Text = new List<string>();
            foreach(string save in WhatToSave){
                if(save == "img")
                {
                    foreach(HtmlNode link in htmlDocument.DocumentNode.SelectNodes("//"+save))
                    {
                        Imagens.Add( link.GetAttributeValue("src", "") );
                    } 
                }


                if(save == "p"){
                    foreach(HtmlNode text in htmlDocument.DocumentNode.SelectNodes("//"+save))
                    {
                        Text.Add( text.InnerText );
                    } 
                }
            }
            
            string[] TextArray = Text.ToArray();
            System.IO.File.WriteAllLines(mainfolder+'/'+foldername+'/'+General.RandomString(32), TextArray);
            
            

            foreach(string imagem in Imagens){

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(imagem);
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if ((httpWebResponse.StatusCode != HttpStatusCode.OK &&
                    httpWebResponse.StatusCode != HttpStatusCode.Moved &&
                    httpWebResponse.StatusCode != HttpStatusCode.Redirect)
                    || !httpWebResponse.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                using (var stream = httpWebResponse.GetResponseStream())
                
                using (var fileStream = File.OpenWrite(mainfolder+'/'+foldername+'/'+General.RandomString(24)))
                {
                    var bytes = new byte[4096];
                    var read=0;
                    do
                    {
                        if (stream == null) {continue;}
                        read = stream.Read(bytes, 0, bytes.Length);
                        fileStream.Write(bytes, 0, read);
                    } while (read != 0);
                    
                }
                
            }
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
