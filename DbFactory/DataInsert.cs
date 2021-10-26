using DbFactory.Contracts;
using DbFactory.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
            //Some validations
            if (message.Count == 0 ||
                textBlocks.Count == 0)
            {
                return;
            }

            var newTextBlock = CreateNewTextBlock(textBlocks);
        }

        private TextBlock CreateNewTextBlock(Dictionary<string, string> textBlocks)
        {
            var newTextBlock = new TextBlock();
            var propertyInfos = newTextBlock.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in propertyInfos)
            {
                if (textBlocks.ContainsKey(property.Name))
                {
                    property.SetValue(newTextBlock, textBlocks[property.Name]);
                }
            }


            return null;
        }
    }
}
