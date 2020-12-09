using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cmbUser();
        }

        private void cmbUser()
        {
            // Actualizar ComboBox
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "password";
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = UserNonQuery.getLista();
        }


    private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }

            else
            {
                UserNonQuery.getLista().ForEach(user =>
                {
                    if (textBox1.Text.Equals(user.password))
                    {

                        MessageBox.Show("Bienvenido : " + user.userName);

                        Form3 ventana1 = new Form3();
                        ventana1.Show();
                        this.Hide();

                    }

                    else
                    {
                        MessageBox.Show("CONTRASENA INCORRECTA");
                    }
                });
            }
        }

    private void button3_Click(object sender, EventArgs e)
    {
        Form4 ventana = new Form4();
        ventana.Show();

    }

    private void button5_Click(object sender, EventArgs e)
    {
        textBox1.Text = "";
        
    }
    }
}