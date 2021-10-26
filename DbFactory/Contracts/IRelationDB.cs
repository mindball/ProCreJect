using System.Data.SqlClient;

namespace DbFactory.Contracts
{
    public interface IRelationDB
    {        
        SqlConnection Connection { get; set; }
    }
}
