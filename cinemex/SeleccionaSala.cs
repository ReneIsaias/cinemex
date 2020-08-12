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
    public partial class SeleccionaSala : Form
    {
        Cinemex obj = new Cinemex();
        public SeleccionaSala()
        {
            InitializeComponent();
        }

        private void SeleccionaSala_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            button2.Focus();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string vaaa = "";
                if (comboBox1.Text != "" && comboBox2.Text != "")
                {
                    vaaa = comboBox2.Text;
                    int dale = 0;
                    dale = (comboBox1.SelectedIndex) + 1;
                    this.Hide();
                    Principal objeto = new Principal();
                    objeto.reciveCartelera(comboBox1, comboBox2, textBox1);
                    objeto.ShowDialog();                                  
                }
                else
                {
                    MessageBox.Show("Selecciona Primero!");
                }
            }
            catch
            {
                MessageBox.Show("Error, Error Inesperado XD");
            }
        }        
        public void XD(TextBox usuario)
        {
            textBox1.Text = usuario.Text;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox1, "SELECT nombreEstado FROM ESTADO WHERE statusEstado=1");
            comboBox2.Text = "";
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int dale = 0;
                dale = (comboBox1.SelectedIndex) + 1;
                if ( comboBox1.Text != "")
                {                    
                    obj.llenaComboRene(comboBox2, "SELECT SUCURSAL.nomSucursal FROM SUCURSAL WHERE statusSucursal = 1 AND idEstado = '" + dale + "'");
                }
                else
                {
                    MessageBox.Show("ERROR, Selecciona un Estado");
                }
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un error inesperado alv pr XD");
            }            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox1.Text = "";
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox2.Text = "";
        }
    }
}
