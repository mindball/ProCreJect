using System.Data;
using System.Data.SqlClient;

namespace DbFactory.Contracts
{
    public interface IRelationDB
    {
        IDbConnection Connection { get; set; }
    }
}
