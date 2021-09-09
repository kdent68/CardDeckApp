using System;
using System.Collections.Generic;
using System.Text;

namespace CardDeckApp.Models
{
    class NewDeckResult
    {
        public bool Success { get; set; }
        public string Deck_id { get; set; }
        public int Remaining { get; set; }
        public bool Shuffled { get; set; }
    }
}
