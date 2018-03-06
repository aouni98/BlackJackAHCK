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
        public int PlaceBet(int num)
        {
            if(num <= money)
            {
                bet = num;
                return bet;
            }
            bet = 0;
            return bet;
        }
        public int Hit1(Deck deck)
        {
            Card temp = new Card();
            temp = deck.Draw();
            handValue += temp.value;
            hand.Add(temp);
            if(handValue > 21)
            {
                foreach(Card x in hand)
                {
                    if(x.value == 11)
                    {
                        x.value = 1;
                        handValue -= 10;
                        return handValue;
                    }
                }
            }
            return handValue;
        }

        public int Hit2(Deck deck)
        {
            Card temp = new Card();
            temp = deck.Draw();
            hand2Value += temp.value;
            hand2.Add(temp);
            if (hand2Value > 21)
            {
                foreach (Card x in hand)
                {
                    if (x.value == 11)
                    {
                        x.value = 1;
                        hand2Value -= 10;
                        return hand2Value;
                    }
                }
            }
            return hand2Value;
        }

        public void SplitHand()
        {
            split = true;
        }
        public void RoundEnd(int dealersHand)
        {
            if((handValue > dealersHand && handValue <= 21) || (hand2Value > dealersHand && split == true && hand2Value <= 21) || (dealersHand > 21 && handValue <= 21) || (dealersHand > 21 && split == true && hand2Value <=21))
            {
                money += bet * 2;
            }
            else if((handValue > 21 && dealersHand > 21) || (hand2Value > 21 && split == true && dealersHand > 21) || (handValue == dealersHand) || (hand2Value == dealersHand && split == true))
            {
                money += bet;
            }
            bet = 0;
            handValue = 0;
            hand2Value = 0;
            split = false;
            hand = new List<Card>();
            hand2 = new List<Card>();
        }

    }
}
