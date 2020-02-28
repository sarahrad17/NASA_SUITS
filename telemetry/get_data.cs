using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Request library
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HelloWorld
{
    class Hello {
        static void Main(string[] args)
        {
            using (var wc = new WebClient())
            {
                var body = wc.DownloadString("http:localhost:3000/api/suit/");
                var js = Newtonsoft.Json.Linq.JObject.Parse(body);
		Console.WriteLine(js);
            }	    
    
        }
    }
}
