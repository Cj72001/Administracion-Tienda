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
            // Actualizar ComboBox 1
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "idProducto";
            comboBox1.DisplayMember = "nombre";
            comboBox1.DataSource = ProductNonQuery.getLista();
            
            // Actualizar ComboBox 2
            comboBox2.DataSource = null;
            comboBox2.ValueMember = "idProducto";
            comboBox2.DisplayMember = "nombre";
            comboBox2.DataSource = ProductNonQuery.getLista();
        }
        

        //Sell Button (Sell Tab) 
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
                MessageBox.Show("Ya no hay " + p.nombre + " disponible", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
            else if (Convert.ToDecimal(textBox1.Text) > p.stock_actual)
            {
                MessageBox.Show("Solamente hay: "+p.stock_actual+" de "+p.nombre+", no se pueden vender "+textBox1.Text, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //Agregando a t3
                try
                {

                    decimal ganancia;
                    ganancia = (Convert.ToDecimal(textBox1.Text)*p.precio_unidad_venta)- (Convert.ToDecimal(textBox1.Text)*p.precio_unidad_compra);
                    ConnectionDB.ExecuteNonQuery($"INSERT INTO GANANCIADIAARTICULO (nombre, cantidad_vendida, ganancia) VALUES('{p.nombre}', '{textBox1.Text}', '{ganancia}');");
                    
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error actualizando T2 ");
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


        //Vendido Tab
        private void button8_Click(object sender, EventArgs e)
        {
            //Updating Producto Vendido
            ProductNonQuery.ProductQueryStock0(dataGridView3);
            
            //Updating ganancias
            ProductNonQuery.ProductQueryGain(dataGridView1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Updating Producto Vendido
            ProductNonQuery.ProductQueryStock0(dataGridView5);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Updating Product Table
            ProductNonQuery.ProductQueryAll(dataGridView5);
        }

        //Update Product btn
        private void button10_Click(object sender, EventArgs e)
        {
            
            ProductNonQuery.UpdateProductStock(Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(textBox2.Text));
        }

        //Actualizar ventas del dia btn
        private void button12_Click(object sender, EventArgs e)
        {
            //Updating Ventas del dia table
            ProductNonQuery.ProductQuerySecondT(dataGridView2);
            
            //Ganancias
            //Updating ganancias
            ProductNonQuery.ProductQueryGainT2(dataGridView6);
        }
        
        //Cerrar caja btn
        private void button13_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        //Actualizar Historial de ventas 
        private void button4_Click(object sender, EventArgs e)
        {
            //Updating Ventas historial
            ProductNonQuery.ProductQueryT3(dataGridView4);
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro de CERRAR DIA ?", "Corte de caja", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                //Insertando ganancias en T3
                try
                {
                    ConnectionDB.ExecuteNonQuery($"INSERT INTO GANANCIADIA (select current_date, SUM(ganancia)FROM GANANCIADIAARTICULO);");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error actualizando suma de ganancias");
                }
            
                //Vaciando datos de T2
                try
                {
                    ConnectionDB.ExecuteNonQuery($"truncate table GANANCIADIAARTICULO;");
                    MessageBox.Show("Se ha cortado caja!!");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error BORRANDO  T2");
                }
            }

        }

//Limpiar ganancias
        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro de LIMPIAR HISTORIAL ?", "Historial de ventas", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    ConnectionDB.ExecuteNonQuery($"TRUNCATE TABLE GANANCIADIA;");
                    MessageBox.Show("Se ha limpiado el historial!!");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error BORRANDO HISOTRIAL T3");
                }
            }
            
            
        }

        //lIMPIAR GANANCIAS BTN
        private void button15_Click(object sender, EventArgs e)
        {
            //Insertando ganancias en T3
            try
            {
                ConnectionDB.ExecuteNonQuery($"UPDATE GANANCIA SET ganancia = 0 WHERE idProducto = {Convert.ToInt32(comboBox2.SelectedValue)};");
                MessageBox.Show("Se han limpiado las ganancias del producto");
            }
            
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un error LIMPIANDO GANANCIAS");
            }
            
        }
    }

}