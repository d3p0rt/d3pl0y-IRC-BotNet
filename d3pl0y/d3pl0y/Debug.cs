using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3pl0y
{
    class Debug
    {
        public static void  Write(string err)
        {
            if (Config.debug == true)
                Console.WriteLine("[DEBUG] " + err);
            else { }

        }
    }
}
