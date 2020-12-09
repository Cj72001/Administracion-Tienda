using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public class UserNonQuery
    {
        //Lista de usuarios
        public static List<User> getLista()
        {
            string sql = "select * from appuser";

            DataTable dt = ConnectionDB.ExecuteQuery(sql);

            List<User> lista = new List<User>();
            
            foreach (DataRow fila in dt.Rows)
            {
                User u = new User();
                u.userName = fila[1].ToString();
                u.password = fila[2].ToString();

                lista.Add(u);
            }
            return lista;
        }

        
        
        public static int UserQueryId(string password)
        {
            
                string query = $"SELECT idUser FROM appuser WHERE password = '{password}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                var idUser = Convert.ToInt32(dr[0].ToString());
            
                return idUser;
        }
        
        

        //Actualizando la contrasena
        public static void UpdateUserPassword(string userName, string newPassword)
        {
            var lista =  UserNonQuery.getLista();
            foreach (var user in lista)
            {
                if (userName.Equals(user.userName))
                {
                    try
                    {
                        ConnectionDB.ExecuteNonQuery($"UPDATE APPUSER " +
                                                     $"SET password = '{newPassword}' " +
                                                     $"WHERE username = '{userName}' ");
            
                        MessageBox.Show("Se ha modificado la contrasena con exito!!!");
                
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Ha ocurrido un error");
                
                    }
                }
                
                else 
                    MessageBox.Show("Usuario Incorrecto");
            }
            
        }

        //Agregando Usuario
      
        public static void AddUser(string userName, string password)
        {
            try
            {
                ConnectionDB.ExecuteNonQuery($"INSERT INTO appuser (userName, password_) VALUES("  +
                                             //usernamer
                                             $"'{userName}'," +
                                             //userType
                                             $"{password})");
            
                MessageBox.Show("Se ha creado el usuario");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
         
        }

        //Eliminando Usuario
        public static void DeleteUser(string userName)
        {
            try
            {
                
                ConnectionDB.ExecuteNonQuery($"DELETE FROM appuser " +
                                               $"WHERE username = '{userName}'"); 
            
                MessageBox.Show("Se ha eliminado el usuario");
                
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }
      
        //Mostrando Usuarios:
        public static void UserQuery(DataGridView dataGridView1)
        {
            try
            {
                var dt = ConnectionDB.ExecuteQuery($"SELECT * FROM appuser");
            
                dataGridView1.DataSource = dt;
            }
                        
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }
        
        
        
     
        
    }
}