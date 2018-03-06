using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackAHCK
{
    class Player
    {
        public List<Card> hand { get; private set; }
        public int handValue { get; private set; }
        public List<Card> hand2 { get; private set; }
        public int hand2Value { get; private set; }
        public int money { get; private set; }
        public int bet { get; private set; }
        public bool split { get; private set; }
        public bool myTurn { get; private set; }
        public Player(int money = 100, bool myTurn = false)
        {
            hand = new List<Card>();
            handValue = 0;
            hand2 = new List<Card>();
            hand2Value = 0;
            this.money = money;
            bet = 0;
            split = false;
            this.myTurn = myTurn;
        }


    }
}
