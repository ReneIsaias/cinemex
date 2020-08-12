using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinemex
{
    public partial class Login : Form
    {
        Cinemex obj = new Cinemex();
        public string usuario;
        public string password;
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            button2.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obj.limpiarRene(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != ""))
                {
                    obj.ejecutaRene("SELECT PERSONAL.emailPersonal, PERSONAL.contraseniaPersonal FROM PERSONAL WHERE PERSONAL.emailPersonal = '" + textBox1.Text + "' AND PERSONAL.contraseniaPersonal = '" + textBox2.Text + "'");
                    if (obj.leerRene.Read())
                    {
                        usuario = obj.leerRene[0].ToString();
                        password = obj.leerRene[1].ToString();
                        if ((textBox1.Text == usuario) && (textBox2.Text == password))
                        {
                            obj.leerRene.Close();
                            obj.ejecutaRene("SELECT CONCAT(PERSONAL.nombrePersonal,' ',PERSONAL.apeUnoPersonal,' ', PERSONAL.apeDosPersonal)AS NOMBRE FROM PERSONAL WHERE emailPersonal = '" + usuario + "'");
                            obj.leerRene.Read();
                            usuario = obj.leerRene[0].ToString();
                            MessageBox.Show("Bienvenido " + usuario);
                            this.Hide(); //Cerramos la venta actual
                            SeleccionaSala xd = new SeleccionaSala();
                            xd.XD(textBox1);
                            xd.ShowDialog();
                            obj.leerRene.Close();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Verifique los datos!!  O_o!");
                            obj.leerRene.Close();
                            button3_Click(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Verifique sus datos!!  O_o!");
                        obj.leerRene.Close();
                        button3_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Error al conectar con el servidor xP");
            }                              
        }
    }
}
