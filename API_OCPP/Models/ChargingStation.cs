using System;
using System.Collections.Generic;

#nullable disable

namespace API_OCPP.Models
{
    public partial class ChargingStation
    {
        public ChargingStation()
        {
            Heartbeats = new HashSet<Heartbeat>();
        }

        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string VendorName { get; set; }
        public string FirmwareVersion { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Available { get; set; }

        public virtual ICollection<Heartbeat> Heartbeats { get; set; }
    }
}
