

namespace APIUnilink.Models
{
    public class BetUser 
    {
     
        public string Username { get; set; }
        public decimal? Initial_Balance { get; set; }
        public decimal? Amount { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public bool Is_Save { get; set; }
    }
}
