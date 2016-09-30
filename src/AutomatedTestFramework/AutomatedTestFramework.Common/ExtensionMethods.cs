using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestFramework.Common
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            if (collection != null)
            {
                return !collection.GetEnumerator().MoveNext();
            }

            return true;
        }
    }
}
