using System;
using System.Collections.Generic;

#nullable disable

namespace API_OCPP.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TagId { get; set; }
    }
}
