using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackAHCK
{
    class Deck
    {
        public List<string> suits = new List<string>() { "Clubs", "Spades", "Hearts", "Diamonds" };
        public List<string> face  = new List<string>() {"Two","Three","Four","Five","Six","Seven","Eight","Nine","Ten","Jack","Queen","King","Ace"};
        public List<Card> cards = new List<Card>();
        public Deck(int numberOfDecks = 1)
        {
           Card temp = new Card();
           for(int i = 0; i < numberOfDecks; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 13; k++)
                    {
                        temp = new Card(suits[j], face[k]);
                        cards.Add(temp);
                    }
                }
            }
        }
        public void shuffle()
        {
            List<Card> temp = new List<Card>();
            Random rand = new Random();
            int num = 0;
            while(cards.Count() != 0)
            {
                num = rand.Next(0, cards.Count());
                temp.Add(cards[num]);
                cards.RemoveAt(num);
            }
            cards = temp;
        }
    }
}
