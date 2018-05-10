using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WatcherUI
{
    public class Listener
    {
        public Timer cycleTimer;
        public void start(int cycleTime)
        {
            GetValue();
            cycleTimer = new Timer();
            cycleTimer.Elapsed += new ElapsedEventHandler(CycleTimedEvent);
            cycleTimer.Interval = cycleTime;
            cycleTimer.Enabled = true;
        }

        private void CycleTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                TrigRefreshCycle();
            }
            catch (Exception ex)
            {
                Logger.WriteLine("SNMP CycleTimedEvent" + ex.Message);
            }
        }
        private void GetValue()
        {
            try
            {
                /// SharpSnmpLib Connection Function
                var result = Messenger.Get(VersionCode.V1,
                                           new IPEndPoint(IPAddress.Parse(Parameters.Params.IP), Parameters.Params.Port),
                                           new OctetString(Parameters.Params.Content),
                                           new List<Variable> { new Variable(new ObjectIdentifier(Parameters.Params.Oid)) },
                                           Parameters.Params.Timeout);
                Logger.WriteLine("-------------------------");
                foreach (Variable item in result)
                {
                    Logger.WriteLine(DateTime.Now + " ### " + item.Data.ToString());
                }
                Logger.WriteLine("-------------------------");
            }
            catch (Exception)
            {
                Logger.WriteLine("### Time Out Exception ###");
            }
        }
        private void TrigRefreshCycle()
        {
            GetValue();
        }
    }
}
