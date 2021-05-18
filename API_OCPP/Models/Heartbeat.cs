using System;
using System.Collections.Generic;

#nullable disable

namespace API_OCPP.Models
{
    public partial class Heartbeat
    {
        public int Id { get; set; }
        public DateTime Hbtime { get; set; }
        public int ChargingStationsId { get; set; }

        public virtual ChargingStation ChargingStations { get; set; }
    }
}
