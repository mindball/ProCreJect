using System.Data;

namespace DbFactory.Contracts
{
    public interface IRelationDB
    {
        IDbConnection Connection { get; set; }

        string GetConnectionString();
    }
}
