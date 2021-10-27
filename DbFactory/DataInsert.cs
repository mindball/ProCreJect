using Dapper;
using DbFactory.Contracts;
using DbFactory.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DbFactory
{
    //TODO: refactor Datainsert into repository pattern
    public class DataInsert
    { 
        public DataInsert(IRelationDB SqlServer)
        {
            this.SqlServer = SqlServer;           
        }

        public IRelationDB SqlServer { get; set; }        

        //TODO: when needed it make async method
        public void InsertMessage(Dictionary<string, string> message,
            Dictionary<string, string> textBlocks)
        {            
            using (this.SqlServer.Connection = new SqlConnection(this.SqlServer.GetConnectionString()))
            {
                var newTextBlock = CreateMessage(new TextBlock(), textBlocks);
                var newMessage = CreateMessage(new Message(), message);

                var insertTextBlock = "INSERT INTO TextBlock(Header1, Header2, Header3) VALUES (@Header1, @Header2, @Header3)";
                
                //TODO: when needed it make async command
                var lastId = this.SqlServer.Connection.Execute(insertTextBlock, newTextBlock);
                if (lastId > 0)
                {
                    var getIdByHeader = "SELECT Id From TextBlock WHERE Header1 = @Header1 AND Header2 = @Header2 AND Header3 = @Header3";
                    //TODO: when needed it make async command
                    var textBlockId = this.SqlServer.Connection.Query<int>(getIdByHeader, newTextBlock).Single();

                    if (textBlockId > 0)
                    {
                        newMessage.TextBlockId = textBlockId;
                        var insertMessage =
                            "INSERT INTO Message(BasicHeader, ApplicationHeader, TextBlockId, Trailer) " +
                                    "VALUES (@BasicHeader, @ApplicationHeader, @TextBlockId, @Trailer)";
                        //TODO: when needed it make async command
                        var messageRow = this.SqlServer.Connection.Execute(insertMessage, newMessage);
                    }
                }
            } 
         }

        private T CreateMessage<T>(T model, Dictionary<string, string> contents)
        {            
            var propertyInfos = model.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in propertyInfos)
            {
                if (contents.ContainsKey(property.Name))
                {
                    property.SetValue(model, contents[property.Name]);
                }
            }

            return model;
        }       
    }
}
