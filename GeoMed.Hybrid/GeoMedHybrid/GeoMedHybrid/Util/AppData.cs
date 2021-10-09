using System;
using System.Collections.Generic;
using System.Text;

namespace GeoMedHybrid
{
    public class AppData
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Career { get; set; }
        public string Area { get; set; }

        public const string BaseUrl = @"http://192.168.1.7:5000/mobile/";
        //public const string BaseUrl = @"http://10.0.2.2:5000/mobile/";
    }

    public class Bus
    {
        public static Action AppDataChanged;
    }
}
