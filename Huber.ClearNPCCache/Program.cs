using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huber.ClearNPCCache
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Clearing NPCScan WoW Addon Cache...");

            TextWriter tw = new StreamWriter("Huber_Clear_NPC_Task.txt",true);
            tw.WriteLine(String.Format("-------- New job {0} --------", DateTime.Now));
            try
            {
                NameValueCollection settings = ConfigurationManager.AppSettings;
                var WoWCacheFiles = Directory.EnumerateFiles(settings["WoWCacheDir"], "*", SearchOption.AllDirectories).Select(Path.GetFileName);

                tw.WriteLine("Found Directory");

                if (WoWCacheFiles.Contains("creaturecache.wdb"))
                {
                    Console.WriteLine("Found the CreatureCache file");
                    File.Delete(String.Format("{0}\\{1}", settings["WoWCacheDir"], "creaturecache.wdb"));
                    Console.WriteLine("Deleted the CreatureCache file");
                    tw.WriteLine("Deleted the CreatureCache file");
                }
                if (WoWCacheFiles.Contains("npccache.wdb"))
                {
                    Console.WriteLine("Found the NpcCache file");
                    File.Delete(String.Format("{0}\\{1}", settings["WoWCacheDir"], "npccache.wdb"));
                    Console.WriteLine("Deleted the NpcCache file");
                    tw.WriteLine("Deleted the NpcCache file");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                tw.WriteLine(String.Format("{0}", ex.ToString()));
            }
            finally
            {
                tw.WriteLine(String.Format("-------- End job {0} --------", DateTime.Now));
                tw.Close();
            }
        }
    }
}
