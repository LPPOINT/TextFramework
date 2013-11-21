namespace Rose.Common
{
    public static class StringExtensions
    {
        public static string RemoveOutherSpaces(this string s)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                    s = s.Remove(0, 1);
            }

            while (s[s.Length-1] == ' ')
            {
                s = s.Remove(s.Length - 1);
            }
            return s;
        }
    }
}
