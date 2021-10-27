using DbFactory.Contracts;
using DbFactory.Models;
using System.Collections.Generic;
using System.Reflection;
using Dapper;
using System;

using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace DbFactory
{
    public class DataInsert
    { 
        public DataInsert(IRelationDB SqlServer)
        {
            this.SqlServer = SqlServer;
        }

        public IRelationDB SqlServer { get; set; }

        public void InsertMessage(Dictionary<string, string> message,
            Dictionary<string, string> textBlocks)
        {
            var newTextBlock = CreateMessage(new TextBlock(), textBlocks);
            var newMessage = CreateMessage(new Message(), message);

            //TODO: make validation when header exist
            var insertTextBlock = "INSERT INTO TextBlock(Header1, Header2, Header3) VALUES (@Header1, @Header2, @Header3)";
            
            using (this.SqlServer.Connection = new SqlConnection("Server=10.148.73.5;Database=SwiftDB;User=sa;Password = Q1w2e3r4"))
            {
                var lastId = this.SqlServer.Connection.Execute(insertTextBlock, newTextBlock);
                if (lastId > 0)
                {
                    var getIdByHeader = "SELECT Id From TextBlock WHERE Header1 = @Header1 AND Header2 = @Header2 AND Header3 = @Header3";
                    var textBlockId = this.SqlServer.Connection.Query<int>(getIdByHeader, newTextBlock).Single();

                    if(textBlockId > 0)
                    {
                        newMessage.TextBlockId = textBlockId;
                        var insertMessage =
                            "INSERT INTO Message(BasicHeader, ApplicationHeader, TextBlockId, Trailer) " +
                                    "VALUES (@BasicHeader, @ApplicationHeader, @TextBlockId, @Trailer)";

                        var messageRow = this.SqlServer.Connection.Execute(insertMessage, newMessage);
                    }
                }
            } 
         }

        private T CreateMessage<T>(T model, Dictionary<string, string> textBlocks)
        {            
            var propertyInfos = model.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in propertyInfos)
            {
                if (textBlocks.ContainsKey(property.Name))
                {
                    property.SetValue(model, textBlocks[property.Name]);
                }
            }

            return model;
        }       
    }
}
