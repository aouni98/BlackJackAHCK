using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace BlackJackAHCK
{
    public partial class Form1 : Form
    {
        Deck deck = new Deck(6);
        Player player1 = new Player();
        Player player2 = new Player();
        Player player3 = new Player();
        Player player4 = new Player();
        Dealer dealer = new Dealer();
        AI ai = new AI();
        System.Timers.Timer timer = new System.Timers.Timer();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 100;
            timer.Elapsed += new ElapsedEventHandler(myEvent);
            timer.Enabled = true;
            timer.Start();
        }
        private void myEvent(object source, ElapsedEventArgs e)
        {
            if (odds.InvokeRequired)
            {
                odds.BeginInvoke((MethodInvoker)delegate () 
                {
                    odds.Text = $"{deck.faceCards} / {deck.totalCards}";
                });
            }
            else
            {
                odds.Text = $"{deck.faceCards} / {deck.totalCards}"; 
            }
        }
        struct Holder
        {
            public bool Stand;
            public int result;
        }
        void deal(CheckBox check, Player player, Button hit)
        {
            if (check.Checked)
            {
                player.Hit1(deck);
                player.Hit1(deck);
            }
            if (player.handValue >= 21)
            {
                hit.Enabled = false;
            }
        }


        void RunAI(int dealerUpValue)
        {
            ai.handValue = ai.Hit(ai.handValue, deck, ai.hand);
            ai.handValue = ai.Hit(ai.handValue, deck, ai.hand);
            aiHand1.Text = "";
            displayHand(ai.hand, aiHand1);
            ai.PlaceBet();
            compMoney.Text = ai.money.ToString();
            compBet.Text = ai.bet.ToString();
            Holder temp = new Holder();
            temp.result = 0;
            temp.Stand = false;
            while (!temp.Stand)
            {
                temp = Aidecide(dealerUpValue, ai.hand, ai.handValue);
                ai.handValue = temp.result;
            }
            temp.result = 0;
            temp.Stand = false;
            while(!temp.Stand && ai.split == true)
            {
                temp = Aidecide(dealerUpValue, ai.hand2, ai.hand2Value);
                ai.hand2Value = temp.result;
            }
            aiHand1.Text = "";
            displayHand(ai.hand, aiHand1);
            if (ai.split == true)
            {
                play2hand2.Text = "";
                displayHand(ai.hand2, play2hand2);
            }


        }
        Holder Aidecide(int dealerUpValue, List <Card> hand, int handvalue)
        {
            Holder temp = new Holder();
            temp.result = handvalue;
            if (handvalue >= 21)
            {
                temp.Stand = true;
                return temp;
            }
            if (dealerUpValue == 1)
            {
                dealerUpValue = 11;

            }
            if (hand.Count == 2 && ai.split == false && (hand[0].number == hand[1].number) )
            {
                if(hand[0].number== "Ace")
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                    
                }
                else if (hand[0].value == 10)
                {
                    temp.Stand = true;
                    return temp;
                }
                else if ( hand[0].value <= 9 && hand[0].value >= 6 && dealerUpValue >= 2 && dealerUpValue <= 6)
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                }
                else if (hand[0].value <= 9 && dealerUpValue >= 8 && dealerUpValue <= 9)
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                }
                else if (hand[0].value == 8 && dealerUpValue >= 7 && dealerUpValue <= 11)
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                }
                else if (hand[0].value == 7 && dealerUpValue == 7)
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                }
                else if (hand[0].value == 3 && dealerUpValue >= 4 && dealerUpValue <= 7)
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                }
                else if (hand[0].value == 2 && dealerUpValue >= 3 && dealerUpValue <= 7)
                {
                    AiSplit();
                    temp.result = ai.handValue;
                    temp.Stand = false;
                    return temp;
                }
                else
                {
                    temp.result = ai.Hit(handvalue, deck, hand);
                    temp.Stand = false;
                    return temp;
                }


            }
            else if (hand.Count == 2 && (hand[0].number == "Ace" || hand[1].number=="Ace")  )
            {
                if(hand[0].value == 9 || hand[1].value == 9)
                {
                    temp.Stand = true;
                    return temp;
                }
                else if ((hand[0].value == 8 || hand[1].value == 8) && dealerUpValue != 6)
                {
                    temp.Stand = true;
                    return temp;
                }
                else if ((hand[0].value == 7 || hand[1].value == 7) && (dealerUpValue == 2 || dealerUpValue == 7 || dealerUpValue == 8 || dealerUpValue == 11))
                {
                    temp.Stand = true;
                    return temp;
                }
                else
                {
                    temp.result = ai.Hit(handvalue, deck, hand);
                    temp.Stand = false;
                    return temp;
                }
               

            }

            else if (handvalue == 12 && dealerUpValue >= 4 && dealerUpValue <=6)
            {
                temp.Stand = true;
                return temp;
            }
            else if (handvalue >= 13 && handvalue <= 16 && dealerUpValue >= 2 && dealerUpValue <= 6)
            {
                temp.Stand = true;
                return temp;
            }
            else if (handvalue >= 17 )
            {
                temp.Stand = true;
                return temp;
            }
            else
            {
                temp.result = ai.Hit(handvalue, deck, hand);
                temp.Stand = false;
                return temp;
            }
                


        }
        void endOfRound()
        {
            bool won;
            if(deck.cards.Count < 31)
            {
                deck = new Deck(6);
            }
            won = player1.RoundEnd(dealer.handValue);
            if(won && player1.money != 0)
            {
                play1Won.Visible = true;
            }
            else if (player1.money != 0)
            {
                play1Lost.Visible = true;
            }
            play1Money.Text = player1.money.ToString();
            won = player2.RoundEnd(dealer.handValue);
            if (won && play2Enable.Checked)
            {
                play2Won.Visible = true;
            }
            else if(play2Enable.Checked)
            {
                play2Lost.Visible = true;
            }
            play2Money.Text = player2.money.ToString();
            won = player3.RoundEnd(dealer.handValue);
            if (won && play3Enable.Checked)
            {
                play3Won.Visible = true;
            }
            else if (play3Enable.Checked)
            {
                play3Lost.Visible = true;
            }
            play3Money.Text = player3.money.ToString();
            won = player4.RoundEnd(dealer.handValue);
            if (won && play4Enable.Checked)
            {
                play4Won.Visible = true;
            }
            else if(play4Enable.Checked)
            {
                play4Lost.Visible = true;
            }
            play4Money.Text = player4.money.ToString();
            won = ai.RoundEnd(dealer.handValue);
            if (won)
            {
                aiWon.Visible = true;
            }
            else
            {
                aiLost.Visible = true;
            }
            compMoney.Text = ai.money.ToString();
            dealer.RoundEnd();
        }
        void displayHand(List<Card> hand, Label label)
        {
            for (int i = 0; i < ai.hand.Count; i++)
            {
                label.Text += $"{hand[i].number} of {hand[i].suit}\n";
            }
        }
        void AiSplit()
        {
            ai.handValue -= ai.hand[0].value;
            play2hand2.Visible = true;
            ai.hand2.Add(ai.hand[0]);
            ai.hand.RemoveAt(0);
            ai.SplitHand();
            displayHand(ai.hand, aiHand1);
            displayHand(ai.hand2, play2hand2);
        }
        void RunDealer()
        {
           
            while(dealer.handValue < 16)
            {
                dealer.Hit(deck);
            }
            DisplayDealerHandFinal(dealer, dealerHand1);
            if(dealer.hand[0].value == 10)
            {
                deck.faceCards--;
            }
            endOfRound();
            startNextRound.Enabled = true;
        }
        void DisplayDealerHand(Dealer dealer, Label label)
        {
            label.Text = "";
            for(int i = 1; i < dealer.hand.Count; i++)
            {
                label.Text += $"{dealer.hand[i].number} of {dealer.hand[i].suit}\n";
            }
        }
        void DisplayDealerHandFinal(Dealer dealer, Label label)
        {
            label.Text = "";
            for (int i = 0; i < dealer.hand.Count; i++)
            {
                label.Text += $"{dealer.hand[i].number} of {dealer.hand[i].suit}\n";
            }
        }
        void DisplayHand1(Label label, Player player)
        {
            label.Text = "";
            foreach(Card x in player.hand)
            {
                label.Text += $"{x.number} of {x.suit}\n";
            }
        }
        void DisplayHand2(Label label, Player player)
        {
            label.Text = "";
            foreach (Card x in player.hand2)
            {
                label.Text += $"{x.number} of {x.suit}\n";
            }
        }
        private void playButton_Click(object sender, EventArgs e)
        {
            dealer.Hit(deck);
            if(dealer.hand[0].value == 10)
            {
                deck.faceCards++;
            }
            dealer.Hit(deck);
            DisplayDealerHand(dealer, dealerHand1);
            dealButton1.Enabled = false;
            dealButton2.Enabled = false;
            dealButton3.Enabled = false;
            dealButton4.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            dealButton1.Enabled = false;
            play1hit1.Enabled = false;
            play1Stand.Enabled = false;
            play1Split.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            player1.myTurn = true;
            playButton.Enabled = false;
            play2Enable.Enabled = false;
            play3Enable.Enabled = false;
            play4Enable.Enabled = false;
        }

        private void play1hit1_Click(object sender, EventArgs e)
        {
            
            player1.Hit1(deck);
            if(player1.handValue > 21)
            {
                play1hit1.Enabled = false;
            }
            DisplayHand1(play1hand1, player1);
        }

        private void play2hit1_Click(object sender, EventArgs e)
        {
            player2.Hit1(deck);
            if (player2.handValue > 21)
            {
                play2hit1.Enabled = false;
            }
            DisplayHand1(play2hand1, player2);
        }

        private void play3hit1_Click(object sender, EventArgs e)
        {
            player3.Hit1(deck);
            if (player3.handValue > 21)
            {
                play3hit1.Enabled = false;
            }
            DisplayHand1(play3hand1, player3);
        }

        private void play4hit1_Click(object sender, EventArgs e)
        {
            player4.Hit1(deck);
            if (player4.handValue > 21)
            {
                play4hit1.Enabled = false;
            }
            DisplayHand1(play4hand1, player4);
        }

        private void play1Stand_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            player1.myTurn = false;
            if(play2Enable.Checked)
            {
                groupBox2.Enabled = true;
                player2.myTurn = true;
                play2hit1.Enabled = false;
                play2Split.Enabled = false;
                play2Stand.Enabled = false;
            }
            else if (play3Enable.Checked)
            {
                groupBox3.Enabled = true;
                player3.myTurn = true;
                play3hit1.Enabled = false;
                play3Stand.Enabled = false;
                play3Split.Enabled = false;
            }
            else if (play4Enable.Checked)
            {
                groupBox4.Enabled = true;
                player4.myTurn = true;
                play4hit1.Enabled = false;
                play4Split.Enabled = false;
                play4Stand.Enabled = false;
            }
            else
            {
                RunAI(dealer.hand[1].value);
                RunDealer();
            }
        }

        private void play2Stand_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            player2.myTurn = false;
            if (play3Enable.Checked)
            {
                groupBox3.Enabled = true;
                player3.myTurn = true;
                play3hit1.Enabled = false;
                play3Stand.Enabled = false;
                play3Split.Enabled = false;
            }
            else if (play4Enable.Checked)
            {
                groupBox4.Enabled = true;
                player4.myTurn = true;
                play4hit1.Enabled = false;
                play4Split.Enabled = false;
                play4Stand.Enabled = false;
            }
            else
            {
                RunAI(dealer.hand[1].value);
                RunDealer();
            }
        }

        private void play3Stand_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = false;
            player3.myTurn = false;
            if(play4Enable.Checked)
            {
                groupBox4.Enabled = true;
                player4.myTurn = true;
                play4hit1.Enabled = false;
                play4Split.Enabled = false;
                play4Stand.Enabled = false;
            }
            else
            {
                RunAI(dealer.hand[1].value);
                RunDealer();
            }
        }

        private void play4Stand_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            player4.myTurn = false;
            RunAI(dealer.hand[1].value);
            RunDealer();
        }

        private void play1Split_Click(object sender, EventArgs e)
        {
            if (player1.hand[0].number == player1.hand[1].number && player1.hand.Count == 2)
            {
                player1.hand2.Add(player1.hand[0]);
                player1.handValue -= player1.hand[0].value;
                player1.hand.RemoveAt(0);
                play1hit2.Visible = true;
                play1hit2.Enabled = true;
                player1.SplitHand();
                play1Split.Enabled = false;
                play1hand2.Enabled = true;
                play1hand2.Visible = true;
                DisplayHand1(play1hand1, player1);
                DisplayHand2(play1hand2, player1);

            }
        }

        private void play2Split_Click(object sender, EventArgs e)
        {
            if (player2.hand[0].number == player2.hand[1].number && player2.hand.Count == 2)
            {
                player2.handValue -= player2.hand[0].value;
                play2hand2.Visible = true;
                player2.hand2.Add(player2.hand[0]);
                player2.hand.RemoveAt(0);
                play2hit2.Visible = true;
                play2hit2.Enabled = true;
                player2.SplitHand();
                play2Split.Enabled = false;
                play2hand2.Enabled = true;
                play2hand2.Visible = true;
                DisplayHand1(play2hand1, player2);
                DisplayHand2(play2hand2, player2);
            }
        }

        private void play3Split_Click(object sender, EventArgs e)
        {
            if (player3.hand[0].number == player3.hand[1].number && player3.hand.Count == 2)
            {
                player3.handValue -= player3.hand[0].value;
                play3hand2.Visible = true;
                player3.hand2.Add(player3.hand[0]);
                player3.hand.RemoveAt(0);
                play3hit2.Visible = true;
                play3hit2.Enabled = true;
                player3.SplitHand();
                play3Split.Enabled = false;
                play3hand2.Enabled = true;
                play3hand2.Visible = true;
                DisplayHand1(play3hand1, player3);
                DisplayHand2(play3hand2, player3);

            }
        }

        private void play4Split_Click(object sender, EventArgs e)
        {
            if (player4.hand[0].number == player4.hand[1].number && player4.hand.Count == 2)
            {
                player4.handValue -= player4.hand[0].value;
                play4hand2.Visible = true;
                player4.hand2.Add(player4.hand[0]);
                player4.hand.RemoveAt(0);
                play4hit2.Visible = true;
                play4hit2.Enabled = true;
                player4.SplitHand();
                play4Split.Enabled = false;
                play4hand2.Enabled = true;
                play4hand2.Visible = true;
                DisplayHand1(play4hand1, player4);
                DisplayHand2(play4hand2, player4);

            }
        }

        private void play1hit2_Click(object sender, EventArgs e)
        {
            player1.Hit2(deck);
            if (player1.hand2Value > 21)
            {
                play1hit2.Enabled = false;
            }
            DisplayHand2(play1hand2, player1);
        }

        private void play2hit2_Click(object sender, EventArgs e)
        {
            player2.Hit2(deck);
            if (player2.hand2Value > 21)
            {
                play2hit2.Enabled = false;
            }
            DisplayHand2(play2hand2, player2);
        }

        private void play3hit2_Click(object sender, EventArgs e)
        {
            player3.Hit2(deck);
            if (player3.hand2Value > 21)
            {
                play3hit2.Enabled = false;
            }
            DisplayHand2(play3hand2, player3);
        }

        private void play4hit2_Click(object sender, EventArgs e)
        {
            player4.Hit2(deck);
            if (player4.hand2Value > 21)
            {
                play4hit2.Enabled = false;
            }
            DisplayHand2(play4hand2, player4);
        }

        private void play1BetButtton_Click(object sender, EventArgs e)
        {
            player1.PlaceBet(Int32.Parse(textBox6.Text));
            play1Money.Text = player1.money.ToString();
            play1BetButtton.Enabled = false;
            dealButton1.Enabled = true;
        }

        private void play2BetButton_Click(object sender, EventArgs e)
        {
            player2.PlaceBet(Int32.Parse(textBox2.Text));
            play2Money.Text = player2.money.ToString();
            play2BetButton.Enabled = false;
            dealButton2.Enabled = true;
        }

        private void play3BetButton_Click(object sender, EventArgs e)
        {
            player3.PlaceBet(Int32.Parse(textBox3.Text));
            play3Money.Text = player3.money.ToString();
            play3BetButton.Enabled = false;
            dealButton3.Enabled = true;
        }

        private void play4BetButton_Click(object sender, EventArgs e)
        {
            player4.PlaceBet(Int32.Parse(textBox4.Text));
            play4Money.Text = player4.money.ToString();
            play4BetButton.Enabled = false;
            dealButton4.Enabled = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void play4Enable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dealButton1_Click(object sender, EventArgs e)
        {
            play1hit1.Enabled = true;
            play1Stand.Enabled = true;
            play1Split.Enabled = true;
            CheckBox check = new CheckBox();
            check.Checked = true;
            deal(check, player1, play1hit1);
            DisplayHand1(play1hand1, player1);
        }

        private void dealButton2_Click(object sender, EventArgs e)
        {
            play2hit1.Enabled = true;
            play2Stand.Enabled = true;
            play2Split.Enabled = true;
            deal(play2Enable, player2, play2hit1);
            DisplayHand1(play2hand1, player2);
        }

        private void dealButton3_Click(object sender, EventArgs e)
        {
            play3hit1.Enabled = true;
            play3Stand.Enabled = true;
            play3Split.Enabled = true;
            deal(play3Enable, player3, play3hit1);
            DisplayHand1(play3hand1, player3);
        }

        private void dealButton4_Click(object sender, EventArgs e)
        {
            play4hit1.Enabled = true;
            play4Stand.Enabled = true;
            play4Split.Enabled = true;
            deal(play4Enable, player4, play4hit1);
            DisplayHand1(play4hand1, player4);
        }

        private void startNextRound_Click(object sender, EventArgs e)
        {
            if(player2.money == 0)
            {
                play2Enable.Checked = false;
            }
            if(player3.money == 0)
            {
                play3Enable.Checked = false;
            }
            if(player4.money == 0)
            {
                play4Enable.Checked = false;
            }
            dealer.Hit(deck);
            dealer.Hit(deck);
            DisplayDealerHand(dealer, dealerHand1);
            DisplayHand1(play1hand1, player1);
            DisplayHand1(play2hand1, player2);
            DisplayHand1(play3hand1, player3);
            DisplayHand1(play4hand1, player4);
            DisplayDealerHand(dealer, dealerHand1);
            aiHand1.Text = "";
            displayHand(ai.hand, aiHand1);

            DisplayHand2(play1hand2, player1);
            DisplayHand2(play2hand2, player2);
            DisplayHand2(play3hand2, player3);
            DisplayHand2(play4hand2, player4);
            aiHand2.Text = "";
            displayHand(ai.hand2, aiHand2);

            play1Won.Visible = false;
            play2Won.Visible = false;
            play3Won.Visible = false;
            play4Won.Visible = false;
            aiWon.Visible = false;

            aiLost.Visible = false;
            play1Lost.Visible = false;
            play2Lost.Visible = false;
            play3Lost.Visible = false;
            play4Lost.Visible = false;

            play1hit2.Visible = false;
            play2hit2.Visible = false;
            play3hit2.Visible = false;
            play4hit2.Visible = false;

            play1hit2.Enabled = false;
            play2hit2.Enabled = false;
            play3hit2.Enabled = false;
            play4hit2.Enabled = false;

            play1hit1.Enabled = false;
            play2hit1.Enabled = false;
            play3hit1.Enabled = false;
            play4hit1.Enabled = false;

            play1Split.Enabled = false;
            play2Split.Enabled = false;
            play3Split.Enabled = false;
            play4Split.Enabled = false;

            play1Stand.Enabled = false;
            play2Stand.Enabled = false;
            play3Stand.Enabled = false;
            play4Stand.Enabled = false;

            play1BetButtton.Enabled = true;
            play2BetButton.Enabled = true;
            play3BetButton.Enabled = true;
            play4BetButton.Enabled = true;

            dealButton1.Enabled = false;
            dealButton2.Enabled = false;
            dealButton3.Enabled = false;
            dealButton4.Enabled = false;

            if (player1.money != 0)
            {
                groupBox1.Enabled = true;
                player1.myTurn = true;
            }
            else if(play2Enable.Checked)
            {
                groupBox2.Enabled = true;
                player2.myTurn = true;
            }
            else if (play3Enable.Checked)
            {
                groupBox3.Enabled = true;
                player3.myTurn = true;
            }
            else if (play4Enable.Checked)
            {
                groupBox4.Enabled = true;
                player4.myTurn = true;
            }
            startNextRound.Enabled = false;
        }
    }
}
