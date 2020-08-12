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
    public partial class Sucursales : Form
    {
        Cinemex obj = new Cinemex();
        public Sucursales()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != ""))
                {
                    obj.ejecutaRene("EXEC procSucursal '" + textBox3.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text +"'");
                    MessageBox.Show("Sucursal Agregada Correctamente");
                    button3_Click(sender, e);
                    Sucursales_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Error datos no validos");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obj.limpiarReneFull(panel1);
            obj.limpiarRene(this);
            Sucursales_Load(sender, e);
        }

        private void Sucursales_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT SUCURSAL.nomSucursal AS SUCURSAL, SUCURSAL.telefonoSucursal AS TELEFONO, SUCURSAL.statusSucursal AS ESTADO, ESTADO.nombreEstado AS CIUDAD FROM SUCURSAL INNER JOIN ESTADO ON SUCURSAL.idEstado = ESTADO.idEstado WHERE SUCURSAL.statusSucursal=1");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox4.Text + "'");
            while (obj.leerRene.Read())
            {
                foreach (Control objeto in this.panel1.Controls)
                {
                    if (objeto is Button)
                    {
                        if (objeto.Tag.Equals(obj.leerRene[0].ToString()))
                        {
                            objeto.Enabled = true;
                            objeto.Visible = true;
                        }
                    }
                }
            }
            obj.leerRene.Close();
        }
        public void obtieneUsuario(TextBox user)
        {
            textBox4.Text = user.Text;
        }
        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox2, "SELECT ESTADO.nombreEstado FROM ESTADO WHERE statusEstado=1");
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            obj.llenaCuadriculaRene(dataGridView1, "EXEC buscaSucursal '" + textBox5.Text + "'");
            obj.leerRene.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();            
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
