using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackAHCK
{
    class Card
    {
        public string suit { get; private set;}
        public string number { get; private set; }
        public Card(string suit, string number)
        {
            this.suit = suit;
            this.number = number;
        }
        public Card()
        {
            suit = "";
            number = "";
        }
    }
}
