using System;
using System.Collections.Generic;

#nullable disable

namespace APIUnilink.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public decimal? Amount { get; set; }
    }
}
