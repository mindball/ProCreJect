namespace ProCreJect
{
    public static class StringExtensions
    { 
        public static string Between(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.IndexOf(b, posA);

            if (posA == -1)
            {
                return "";
            }

            if (posB == -1)
            {
                return "";
            }

            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }


       
    }
}
