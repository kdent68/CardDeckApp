using System;
using System.Collections.Generic;
using System.Text;

namespace CardDeckApp.Models
{
    class DrawnCardResult : NewDeckResult
    {
        public List<Card> Cards { get; set; }
    }
}
