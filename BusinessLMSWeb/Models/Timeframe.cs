using System;
using System.Collections.Generic;

namespace BusinessLMSWeb.Models
{
    public class Timeframe
    {
        public int timeframeId { get; set; }
        public string title { get; set; }
        public int days { get; set; }
        public int timeLevel { get; set; }
    }
}
