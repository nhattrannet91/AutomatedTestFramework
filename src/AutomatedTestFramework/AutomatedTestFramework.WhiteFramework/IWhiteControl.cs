using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using White.Core.UIItems;

namespace AutomatedTestFramework.WhiteFramework
{
    internal interface IWhiteControl
    {
        UIItem WhiteItem { get; }
    }
}
