using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string newPasword = textBox2.Text;
            UserNonQuery.UpdateUserPassword(userName, newPasword);
            this.Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}