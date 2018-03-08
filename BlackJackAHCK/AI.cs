using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackAHCK
{
    class AI
    {
        public List<Card> hand { get; set; }
        public int handValue { get; set; }
        public List<Card> hand2 { get; set; }
        public int hand2Value { get; set; }
        public int money { get; private set; }
        public int bet { get; private set; }
        public bool split { get; private set; }
        public bool myTurn { get; set; }
        public AI(int money = 100, bool myTurn = false)
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
        public int PlaceBet()
        {
           
                bet = money / 10;
                money -= bet;
                return bet;
        }
        public int Hit( int tempnum, Deck deck, List <Card> hand)
        {
            Card temp = new Card();
            temp = deck.Draw();
            tempnum += temp.value;
            hand.Add(temp);
            if (handValue > 21)
            {
                foreach (Card x in hand)
                {
                    if (x.value == 11)
                    {
                        x.value = 1;
                        handValue -= 10;
                        return tempnum;
                    }
                }
            }
            return tempnum;
        }

        

        public void SplitHand()
        {
            split = true;
        }
        public bool RoundEnd(int dealersHand)
        {
            bool temp = false;
            if ((handValue > dealersHand && handValue <= 21) || (hand2Value > dealersHand && split == true && hand2Value <= 21) || (dealersHand > 21 && handValue <= 21) || (dealersHand > 21 && split == true && hand2Value <= 21))
            {
                money += bet * 2;
                temp = true;
            }
            else if ((handValue > 21 && dealersHand > 21) || (hand2Value > 21 && split == true && dealersHand > 21) || (handValue == dealersHand) || (hand2Value == dealersHand && split == true))
            {
                money += bet;
                temp = true;
            }
            bet = 0;
            handValue = 0;
            hand2Value = 0;
            split = false;
            hand = new List<Card>();
            hand2 = new List<Card>();
            return temp;
        }

    }
}

