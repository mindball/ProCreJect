using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shredding
{
    public class ShreddingFile
    {
        private const string CrLf = "\r\n";
        private readonly int patternSize = ":00:".Length;
        
        private const int fieldOne = 1;
        private const int fieldTwo = 2;
        private const int fieldThree = 3;
        private const int fieldFour = 4;
        private const int fieldFive = 5;

        private readonly string openCurlyBracket = $"{{{0}}}:";
        private const string clossedCurlyBracket = "}";

        private const string header1 = "Header1";
        private const string header2 = "Header2";
        private const string header3 = "Header3";

        public Dictionary<string, string> ShrederSWIFTFile(string message)
        {
            Dictionary<string, string> swiftMessage = new Dictionary<string, string>();

            string matchPattern = String.Format(openCurlyBracket, fieldOne);
            if (message.Contains(matchPattern))
            {
                string Block1 = message.Between(matchPattern, clossedCurlyBracket);
                swiftMessage.Add("BasicHeader", Block1);
            }   

            matchPattern = String.Format(openCurlyBracket, fieldTwo);
            if (message.Contains(matchPattern))
            {
                string Block2 = message.Between(matchPattern, clossedCurlyBracket);
                swiftMessage.Add("ApplicationHeader", Block2);
            }

            //Това го няма в файла
            //matchPattern = String.Format(openCurlyBracket, fieldThree);
            //if (message.Contains(matchPattern)
            //{
            //    string Block3 = message.Between(":{", "}");
            //    swiftMessage.Add("UserHeader", Block3);
            //}

            matchPattern = String.Format(openCurlyBracket, fieldFour);
            if (message.Contains(matchPattern))
            {
                string Block4 = message.Between(matchPattern, clossedCurlyBracket)
                    .Replace(CrLf, string.Empty).TrimStart();
                swiftMessage.Add("TextBlock", Block4);
            }

            matchPattern = String.Format(openCurlyBracket, fieldFive);
            if (message.Contains(matchPattern))
            {
                string Block5 = message.Between(matchPattern, clossedCurlyBracket);
                swiftMessage.Add("Trailer", Block5);
            }

            return swiftMessage;
        }                

        public Dictionary<string, string> ShrederBody(string body)
        {
            var s = body.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var bodyMessages = new Dictionary<string, string>();

            var pattern = @"(?m):[0-9][0-9]:";
            //var pattern = @":\d+:(.*?)(?=:)";
            //var pattern = @":\d+:(.*?)(?=:(.*))"; 

            var indexes = Regex.Matches(body, pattern).Select(i => i.Index).ToList();
            bodyMessages.Add(header1, 
                body.Substring(indexes[0] + patternSize, indexes[1] - patternSize).TrimEnd());
            bodyMessages.Add(header2,
                body.Substring(indexes[1] + patternSize, indexes[2] - (indexes[1] + patternSize)));
            bodyMessages.Add(header3,
                body.Substring(indexes[2] + patternSize, (body.Length - indexes[2] - patternSize)));

            return bodyMessages;
        }
    }
}
