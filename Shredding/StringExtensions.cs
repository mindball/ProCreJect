namespace Shredding
{
    public static class StringExtensions
    { 
        public static string Between(
            this string value, 
            string openCurlyBracketWithNumber, 
            string b)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            
            int posA = value.IndexOf(openCurlyBracketWithNumber);

            if(posA == -1)
            {
                //Or throw exception when open curly bracket not found
                return "";
            }

            int posB = value.IndexOf(b, posA);
            
            if (posB == -1)
            {
                return "";
            }

            int adjustedPosA = posA + openCurlyBracketWithNumber.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }

            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }


       
    }
}
