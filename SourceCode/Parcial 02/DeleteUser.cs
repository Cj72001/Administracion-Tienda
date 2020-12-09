using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class DeleteUser : UserControl
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals(""))
                MessageBox.Show("No se pueden dejar espacios en blanco");

            else
            {
                UserNonQuery.DeleteUser(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserNonQuery.UserQuery(dataGridView1);
        }
    }
}