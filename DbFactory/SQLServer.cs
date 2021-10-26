using DbFactory.Contracts;
using System.Data.SqlClient;
using System;

namespace DbFactory
{
    public class SQLServer : IRelationDB
    {       
        public SQLServer()
        {
            using (this.Connection =
                new SqlConnection(@"Server=.\SQLExpress;Database=SwiftDB;Trusted_Connection=True;"))
            {
                Console.WriteLine("Db connected!!!");
            }           
        }

        public SqlConnection Connection { get; set; }

        //Fa
    }
}
