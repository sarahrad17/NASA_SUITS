using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Request library
using System.Net;
using System.IO;
//using Newtonsoft.Json;

namespace HelloWorld
{
    class Hello {
        static void Main(string[] args)
        {
           string url = @"https://api.stackexchange.com/2.2/answers?order=desc&sort=activity&site=stackoverflow";

           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
           request.AutomaticDecompression = DecompressionMethods.GZip;

           using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
           using (Stream stream = response.GetResponseStream())
           using (StreamReader reader = new StreamReader(stream))
           {
               html = reader.ReadToEnd();
           }
           Console.WriteLine(html);  
    
        }
    }
}
