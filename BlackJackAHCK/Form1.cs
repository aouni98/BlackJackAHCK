using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJackAHCK
{
    public partial class Form1 : Form
    {
        Deck deck = new Deck();
        Player player1 = new Player();
        Player player2 = new Player();
        Player player3 = new Player();
        Player player4 = new Player();
        Dealer dealer = new Dealer();
        public Form1()
        {
            InitializeComponent();
            
        }
         void deal(CheckBox check, Player player, Button hit)
        {
            if(check.Checked)
            {
                player.Hit1(deck);
                player.Hit1(deck);
            }
            if (player.handValue >= 21)
            {
                hit.Enabled = false;
            }
        }
        void RunDealer()
        {
            while(dealer.handValue < 16)
            {
                dealer.Hit(deck);
            }
            DisplayDealerHandFinal(dealer, dealerHand1);
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
                RunDealer();
            }
        }

        private void play4Stand_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            player4.myTurn = false;
            RunDealer();
        }

        private void play1Split_Click(object sender, EventArgs e)
        {
            if (player1.hand[0].number == player1.hand[1].number)
            {
                player1.hand2.Add(player1.hand[0]);
                player1.handValue -= player1.hand[0].value;
                player1.hand.RemoveAt(0);
                play1hit2.Visible = true;
                play1hit2.Enabled = true;
                player1.SplitHand();
                play1Split.Enabled = false;
                play1hand2.Enabled = true;
                DisplayHand1(play1hand1, player1);
                DisplayHand2(play1hand2, player1);

            }
        }

        private void play2Split_Click(object sender, EventArgs e)
        {
            if (player2.hand[0].number == player2.hand[1].number)
            {
                player2.handValue -= player2.hand[0].value;
                play2hand2.Visible = true;
                player2.hand2.Add(player2.hand[0]);
                player2.hand.RemoveAt(0);
                play2hit2.Visible = true;
                play2hit2.Enabled = true;
                player2.SplitHand();
                play2Split.Enabled = false;
                DisplayHand1(play2hand1, player2);
                DisplayHand2(play2hand2, player2);
            }
        }

        private void play3Split_Click(object sender, EventArgs e)
        {
            if (player3.hand[0].number == player3.hand[1].number)
            {
                player3.handValue -= player3.hand[0].value;
                play3hand2.Visible = true;
                player3.hand2.Add(player3.hand[0]);
                player3.hand.RemoveAt(0);
                play3hit2.Visible = true;
                play3hit2.Enabled = true;
                player3.SplitHand();
                play3Split.Enabled = false;
                DisplayHand1(play3hand1, player3);
                DisplayHand2(play3hand2, player3);

            }
        }

        private void play4Split_Click(object sender, EventArgs e)
        {
            if (player4.hand[0].number == player4.hand[1].number)
            {
                player4.handValue -= player4.hand[0].value;
                play4hand2.Visible = true;
                player4.hand2.Add(player4.hand[0]);
                player4.hand.RemoveAt(0);
                play4hit2.Visible = true;
                play4hit2.Enabled = true;
                player4.SplitHand();
                play4Split.Enabled = false;
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
            DisplayHand2(play1hand1, player1);
        }

        private void play2hit2_Click(object sender, EventArgs e)
        {
            player2.Hit2(deck);
            if (player2.hand2Value > 21)
            {
                play2hit2.Enabled = false;
            }
            DisplayHand2(play2hand1, player2);
        }

        private void play3hit2_Click(object sender, EventArgs e)
        {
            player3.Hit2(deck);
            if (player3.hand2Value > 21)
            {
                play3hit2.Enabled = false;
            }
            DisplayHand2(play3hand1, player3);
        }

        private void play4hit2_Click(object sender, EventArgs e)
        {
            player4.Hit2(deck);
            if (player4.hand2Value > 21)
            {
                play4hit2.Enabled = false;
            }
            DisplayHand2(play4hand1, player4);
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
    }
}
