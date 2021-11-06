using FleetManager.DataAccessLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Desktop.Data.Rest
{
    class RestDataContext : IDataContext<IRestClient>
    {
        public static string _baseAddress = "https://localhost:44305/api";

        public IRestClient Open()
        {
            return new RestClient(_baseAddress);
        }
    }
}
