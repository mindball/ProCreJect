using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace ProCreJect
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = @"C:\Users\oilaripi\source\repos\ProCreJect\ProCreJect\bin\Debug\netcoreapp3.1\swift\t.txt";

            string message = System.IO.File.ReadAllText(fileName);

            var shreder = new Shredding();

            var str = shreder.ShrederSWIFTFile(message);

            string textBody;
            if(str.TryGetValue("TextBlock", out textBody))
            {
                var shrededBody = shreder.ShrederBody(textBody);
            }

            return;
        }        
    }
}
