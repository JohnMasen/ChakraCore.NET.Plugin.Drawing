using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPTest
{
    class JSDrawApp:ChakraCore.NET.Hosting.JSClassWrapperBase
    {
        public void Init()
        {
            Reference.CallMethod("Init");
        }

        public void Draw()
        {
            Reference.CallMethod("Draw");
        }
    }
}
