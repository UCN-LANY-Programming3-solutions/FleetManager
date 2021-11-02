using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer
{
    public interface IDataContext
    {

    }

    public interface IDataContext<TConnection> : IDataContext
    {
        TConnection Open();
    }
}
