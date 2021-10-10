using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer
{

    [Serializable]
    public class DaoException : Exception
    {
        public DaoException() { }
        public DaoException(object model) : base($"An error ocurred when handling model {model}") { }
        public DaoException(object model, Exception inner) : base($"An error ocurred when handling model {model}", inner) { }
        protected DaoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
