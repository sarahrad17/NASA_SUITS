using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Request library
using System.Net;
using System.IO;
using System.Security.Authentication;
//using Newtonsoft.Json;

namespace HelloWorld
{
    class Hello {
        static void Main(string[] args)
        {
            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;
            string url = @"http://localhost:3000/api/suit";

           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
           request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            { 
                string html = reader.ReadToEnd();
                Console.WriteLine(html);  
            }
        }
    }
}
