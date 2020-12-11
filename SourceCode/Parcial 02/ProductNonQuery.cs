using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public class ProductNonQuery
    {
        public static List<Product> getLista()
        {
            string sql = "select * from GANANCIA";

            DataTable dt = ConnectionDB.ExecuteQuery(sql);

            List<Product> lista = new List<Product>();

            foreach (DataRow fila in dt.Rows)
            {
                Product p = new Product();
                p.idProducto = Convert.ToDecimal(fila[0].ToString());
                p.nombre = fila[1].ToString();
                p.stock_ingresado = Convert.ToDecimal(fila[2].ToString());
                p.stock_actual = Convert.ToDecimal(fila[3].ToString());
                p.precio_unidad_compra = Convert.ToDecimal(fila[4].ToString());
                p.precio_unidad_venta = Convert.ToDecimal(fila[5].ToString());
                p.cantidad_vendida = Convert.ToDecimal(fila[6].ToString());
                p.precio_compra_acumulado = Convert.ToDecimal(fila[7].ToString());
                p.precio_venta_acumulado = Convert.ToDecimal(fila[8].ToString());
                p.ganancia = Convert.ToDecimal(fila[9].ToString());

                lista.Add(p);
            }

            return lista;
        }

        public static int ProductQueryid(string name)
        {
            
                string query = $"SELECT idProduct FROM product WHERE name = '{name}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                var idProduct = Convert.ToInt32(dr[0]);
                return idProduct;
        }

       
        public static void AddProduct(string nombre, string stock_ingresado, string stock_actual, string precio_unidad_compra, string precio_unidad_venta, string cantidad_vendida, string precio_compra_acumulado, string precio_venta_acumulado, string ganancia)
        {
            try
            {
                ConnectionDB.ExecuteNonQuery($"INSERT INTO GANANCIA (nombre, stock_ingresado, stock_actual, precio_unidad_compra, precio_unidad_venta, cantidad_vendida, precio_compra_acumulado, precio_venta_acumulado, ganancia) VALUES("  +
                                             //name
                                             $"'{nombre}'," + 
                                             
                                             //stock ingresa
                                             $"'{stock_ingresado}'," + 
                                             
                                             //stock actual
                                             $"'{stock_actual}'," + 
                                             
                                             //precio compra
                                             $"'{precio_unidad_compra}'," + 
                                             
                                             //precio venta
                                             $"'{precio_unidad_venta}'," + 
                                             
                                             //cantidad vendida
                                             $"'{cantidad_vendida}'," + 
                                             
                                             //precio compra acumulado
                                             $"'{precio_compra_acumulado}'," + 
                                             
                                             //precio venta acumulado
                                             $"'{precio_venta_acumulado}'," + 
                                                     
                                             //ganancia
                                             $"'{ganancia}')");
            
                MessageBox.Show("Se ha ingresado el nuevo producto");
                
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un error ingresando producto");
            }
            
        }
        
        //Obteniendo id de producto:
        public static int idProduct(string name)
        {
            var idProduct = 0;
            try
            {
                string query = $"SELECT idBusiness FROM business WHERE name = '{name}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                idProduct = Convert.ToInt32(dr[0].ToString());
            }
                        
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un problema obteniendo id");
            }
            return idProduct;
        }
        
        //Actualizando Producto (stock inicial, etc)
         public static void UpdateProduct(string nombre)
        {
            try
            {
                ConnectionDB.ExecuteNonQuery($"UPDATE GANANCIA SET stock_actual = stock_ingresado, precio_compra_acumulado = (stock_ingresado*precio_unidad_compra) WHERE nombre = '{nombre}';");
            
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un error actualizando");
            }
            
        }
        

         //Eliminando Propducto
        public static void DeleteProduct(string name)
        {
            try
            {

                ConnectionDB.ExecuteNonQuery($"DELETE FROM GANANCIA " +
                                             $"WHERE nombre = '{name}'");

                MessageBox.Show("Se ha eliminado el producto");

            }
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }
        
        //Mostrando Producto:
        public static void ProductQuery(DataGridView dataGridView1)
        {
            try
            {
                var dt = ConnectionDB.ExecuteQuery($"SELECT * FROM GANANCIA");
            
                dataGridView1.DataSource = dt;
            }
                        
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }
        
        //Mostrando Ganancia Total:
        public static void ProductQueryGain(DataGridView dataGridView1)
        {
            try
            {
                var dt = ConnectionDB.ExecuteQuery($"SELECT SUM(ganancia)FROM GANANCIA;");
            
                dataGridView1.DataSource = dt;
            }
                        
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }
        
    }
}
