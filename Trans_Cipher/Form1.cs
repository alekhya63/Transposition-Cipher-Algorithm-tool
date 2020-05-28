using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trans_Cipher
{
    public partial class TranspositionCipher : Form
    {

        public TranspositionCipher()
        {
            InitializeComponent();
            intor1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            intor1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tutorial1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pratise1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cipherwork1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            contactus1.BringToFront();
        }
    }
}
