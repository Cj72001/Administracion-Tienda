using System;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class Form3 : Form
    {
        private UserControl current = null;
        private AddUser addUser = new AddUser();
        private DeleteUser deleteUser = new DeleteUser();
        
        private AddProduct addProduct = new AddProduct();
        private DeleteProduct deleteProduct = new DeleteProduct();
        
        
        
        public Form3()
        {
            InitializeComponent();
            current = addUser;
        }



        //AddUser
        private void button5_Click(object sender, EventArgs e)
        {
            tableLayoutPanel2.Controls.Remove(current);
            tableLayoutPanel2.Controls.Add(addUser, 0, 1);
            current = addUser;
            tableLayoutPanel2.SetColumnSpan(current, 4);
        }

        //DeleteUser
        private void button3_Click(object sender, EventArgs e)
        {
            tableLayoutPanel2.Controls.Remove(current);
            tableLayoutPanel2.Controls.Add(deleteUser, 0, 1);
            current = deleteUser;
            tableLayoutPanel2.SetColumnSpan(current, 4);
        }
        
        
        //AddProduct

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel5.Controls.Remove(current);
            tableLayoutPanel5.Controls.Add(addProduct, 0, 1);
            current = addProduct;
            tableLayoutPanel5.SetColumnSpan(current, 4);
        }
        
        //DeleteProduct

        private void button7_Click(object sender, EventArgs e)
        {
            tableLayoutPanel5.Controls.Remove(current);
            tableLayoutPanel5.Controls.Add(deleteProduct, 0, 1);
            current = deleteProduct;
            tableLayoutPanel5.SetColumnSpan(current, 4);
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            cmbProduct();
        }
        
        private void cmbProduct()
        {
            // Actualizar ComboBox
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "idProducto";
            comboBox1.DisplayMember = "nombre";
            comboBox1.DataSource = ProductNonQuery.getLista();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            ProductNonQuery.getLista().ForEach(product =>
            {
                if (comboBox1.SelectedValue.Equals(product.idProducto))
                {

                    p = product;

                }
            });
            
            if(p.stock_actual == 0 )
            {
                MessageBox.Show("Ya no hay producto disponible");
            }
            
            else if (Convert.ToDecimal(textBox1.Text) > p.stock_actual)
            {
                MessageBox.Show("Solamente hay: "+p.stock_actual+" de "+p.nombre+", no se pueden vender "+textBox1.Text);
            }
            
            else if (p.stock_actual != 0)
            {
                try
                {
                    ConnectionDB.ExecuteNonQuery($"UPDATE GANANCIA SET cantidad_vendida = (cantidad_vendida)+'{textBox1.Text}' WHERE idProducto = '{comboBox1.SelectedValue}';");
                    MessageBox.Show("Se vendio "+textBox1.Text+" de "+p.nombre);
            
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error actualizando");
                }
            
                try
                {
                    ConnectionDB.ExecuteNonQuery($"UPDATE GANANCIA SET stock_actual = stock_actual-{textBox1.Text} WHERE idProducto = {comboBox1.SelectedValue};");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error actualizando");
                }
            
                try
                {
                    ConnectionDB.ExecuteNonQuery($"UPDATE GANANCIA SET precio_venta_acumulado = (precio_unidad_venta * cantidad_vendida) WHERE idProducto = {comboBox1.SelectedValue};");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error actualizando");
                }
            
                try
                {
                    ConnectionDB.ExecuteNonQuery($"UPDATE GANANCIA SET ganancia = (precio_unidad_venta*cantidad_vendida)-(precio_unidad_compra*cantidad_vendida) WHERE idProducto = {comboBox1.SelectedValue};");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error actualizando");
                }
            }

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProductNonQuery.ProductQuery(dataGridView2);
            ProductNonQuery.ProductQueryGain(dataGridView1);
        }

        
    }

}