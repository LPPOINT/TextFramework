using System;

namespace Rose.Common
{
    public static class Check
    {
        public static void NotNull(object obj, string name = "")
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
