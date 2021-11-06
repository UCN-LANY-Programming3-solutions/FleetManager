using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Desktop.Data.Rest
{
    class RestDataContext
    {
        public static RestClient Client => new("https://localhost:44305/api");       
    }
}
