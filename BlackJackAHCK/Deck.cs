using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackAHCK
{
    class Deck
    {
        public int faceCards = 96;
        public int totalCards = 312;
        public List<string> suits = new List<string>() { "Clubs", "Spades", "Hearts", "Diamonds" };
        public List<string> face  = new List<string>() {"Two","Three","Four","Five","Six","Seven","Eight","Nine","Ten","Jack","Queen","King","Ace"};
        public List<Card> cards  = new List<Card>();
        public void Shuffle()
        {
            List<Card> temp = new List<Card>();
            Random rand = new Random();
            int num = 0;
            while (cards.Count() != 0)
            {
                num = rand.Next(0, cards.Count());
                temp.Add(cards[num]);
                cards.RemoveAt(num);
            }
            cards = temp;
        }
        public Deck(int numberOfDecks = 1)
        {
            faceCards = 96;
            totalCards = 312;
           Card temp = new Card();
           for(int i = 0; i < numberOfDecks; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 13; k++)
                    {
                        if (k <= 8)
                        {
                            temp = new Card(suits[j], face[k], k + 2);
                        }
                        else if(k >= 9 && k <= 11)
                        {
                            temp = new Card(suits[j], face[k], 10);
                        }
                        else
                        {
                            temp = new Card(suits[j], face[k], 11);
                        }
                        cards.Add(temp);
                    }
                }
            }
            this.Shuffle();
        }
        
        public Card Draw()
        {
            Card temp = new Card();
            temp = cards[0];
            cards.RemoveAt(0);
            if(temp.value == 10)
            {
                faceCards -= 1;
            }
            totalCards -= 1;
            return temp;
        }
    }
}
