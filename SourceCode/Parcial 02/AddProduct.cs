using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class AddProduct : UserControl
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        public delegate void MyDelegate(ComboBox c);
        public MyDelegate AccionesCmb;

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("") ||
                textBox2.Text.Equals("") ||
                textBox3.Text.Equals("") ||
                textBox4.Text.Equals(""))

            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }

            else
            {

                //Agregando producto:
                ProductNonQuery.AddProduct(textBox1.Text, textBox2.Text, "0", textBox3.Text, textBox4.Text, "0", "0", "0", "0");
                //int idProduct = ProductNonQuery.idProduct(textBox1.Text);
                ProductNonQuery.UpdateProduct(textBox1.Text);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}