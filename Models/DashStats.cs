using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPV_Mark3.Models
{
    public class DashStats
    {
        public int snr { get; set; }
        public int totalCase { get; set; }
        public int ontime { get; set; }
        public int UnderProces { get; set; }
        public int OverTime { get; set; }

    }
}