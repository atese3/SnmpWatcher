using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherUI
{
    public class Logger
    {
        public static void WriteLine(string input)
        {
            /// Project step logger
            File.AppendAllText("WatcherUI.log", DateTime.Now + " ### " + input + Environment.NewLine);
            UserInterface.GetInst().Log = DateTime.Now + " ### " + input;
        }
    }
}
