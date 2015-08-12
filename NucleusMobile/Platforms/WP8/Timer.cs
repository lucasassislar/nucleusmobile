#if WP8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Timers
{
    public class ElapsedEventArgs : EventArgs
    {
        private DateTime signalTime;
        public DateTime SignalTime
        {
            get
            {
                return signalTime;
            }
        }
    }
    public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);

    public class Timer
    {
        public double Interval { get; set; }
        public bool Enabled { get; set; }
        public event ElapsedEventHandler Elapsed;
    }
}
#endif