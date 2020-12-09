using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class DeleteProduct : UserControl
    {
        public DeleteProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals(""))
                MessageBox.Show("No se pueden dejar campos vacios");
            
            else
                ProductNonQuery.DeleteProduct(textBox1.Text);
               
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            ProductNonQuery.ProductQuery(dataGridView2);
        }
    }
}