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
        public Form1()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Deck test = new Deck(1);
            test.shuffle();
            foreach (Card x in test.cards)
            {
                textBox1.Text += $"{x.number} of {x.suit} || ";
            }
        }

    
    }
}
