using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackAHCK
{
    class Dealer
    {
        public List<Card> hand { get; private set; }
        public int handValue { get; private set; }
        public Dealer()
        {
            hand = new List<Card>();
            handValue = 0;
        }
       public void RoundEnd()
        {
            hand = new List<Card>();
            handValue = 0;
        }
        public int Hit(Deck deck)
        {
            Card temp = new Card();
            temp = deck.Draw();
            handValue += temp.value;
            hand.Add(temp);
            if (handValue > 21)
            {
                foreach (Card x in hand)
                {
                    if (x.value == 11)
                    {
                        x.value = 1;
                        handValue -= 10;
                        return handValue;
                    }
                }
            }
            return handValue;
        }
    }
}
