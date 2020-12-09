using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class AddUser : UserControl
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (
                textBox2.Text.Equals("") ||
                textBox3.Text.Equals(""))
                
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }

            else
            {
                UserNonQuery.AddUser( textBox2.Text, textBox3.Text);
            }
        }
    }
    
    
}