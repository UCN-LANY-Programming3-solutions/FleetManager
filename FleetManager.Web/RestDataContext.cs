using FleetManager.DataAccessLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web
{
    public class RestDataContext : IDataContext<IRestClient>
    {
        public SupportedContextTypes SupportedContext => SupportedContextTypes.Rest;

        private string _baseUri = "https://localhost:44305";

        public IRestClient Open()
        {
            return new RestClient(_baseUri);
        }
    }
}
