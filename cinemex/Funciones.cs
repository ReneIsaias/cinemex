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
    public partial class Funciones : Form
    {
        Cinemex obj = new Cinemex();
        public Funciones()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Funciones_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PROYECCIONES.clvProyeccion  AS CLAVE, PROYECCIONES.fechaInicio AS INICIA, PROYECCIONES.fechaTermino AS TERMINA, PROYECCIONES.costoProyeccion AS COSTO, PROYECCIONES.statusProyeccion AS ESTADO, PELICULA.nombrePelicula AS PELICULA, SALA.nombreSala AS SALA FROM PROYECCIONES INNER JOIN PELICULA ON PROYECCIONES.clvPelicula = PELICULA.clvPelicula INNER JOIN SALA ON PROYECCIONES.idSala = SALA.idSala");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox1.Text + "'");
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
        public void obtieneUsuario(TextBox user, TextBox sucursal)
        {
            textBox1.Text = user.Text;
            textBox5.Text = sucursal.Text;
        }       

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox6.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != ""))
                {
                    obj.ejecutaRene("EXEC procProyecciones '" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','"+comboBox3.Text+"','" +textBox5.Text+"'");
                    MessageBox.Show("Proyeccion Agregada Correctamente");
                    Funciones_Load(sender, e);
                    label8.Visible = false;
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Error en la consulta");
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox2, "SELECT PELICULA.nombrePelicula FROM PELICULA");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {            
            obj.limpiarReneFull(panel1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reservacion reservar = new Reservacion();
            //metodo envia datos
            reservar.obtieneUsuario(textBox1);
            reservar.reciveDatos(textBox2, textBox6, textBox3, comboBox2, textBox5, comboBox3);
            reservar.ShowDialog();           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox3, "SELECT SALA.nombreSala FROM SALA INNER JOIN SUCURSAL ON SALA.idSucursal = SUCURSAL.idSucursal WHERE nomSucursal='"+textBox5.Text+ "' AND statusSala=1");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            obj.ejecutaRene("SELECT getdate()");
            obj.leerRene.Read();
            textBox3.Text = obj.leerRene[0].ToString();
            textBox4.Text = obj.leerRene[0].ToString();
            obj.leerRene.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            label8.Visible = true;
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            label8.Visible = true;
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            label8.Visible = false;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            label8.Visible = false;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //general
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PROYECCIONES.clvProyeccion  AS CLAVE, PROYECCIONES.fechaInicio AS INICIA, PROYECCIONES.fechaTermino AS TERMINA, PROYECCIONES.costoProyeccion AS COSTO, PROYECCIONES.statusProyeccion AS ESTADO, PELICULA.nombrePelicula AS PELICULA, SALA.nombreSala AS SALA FROM PROYECCIONES INNER JOIN PELICULA ON PROYECCIONES.clvPelicula = PELICULA.clvPelicula INNER JOIN SALA ON PROYECCIONES.idSala = SALA.idSala");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //local
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PROYECCIONES.clvProyeccion AS CLAVE, PROYECCIONES.fechaInicio AS INICIA, PROYECCIONES.fechaTermino AS TERMINA, PROYECCIONES.costoProyeccion AS COSTO, PROYECCIONES.statusProyeccion AS ESTADO, PELICULA.nombrePelicula AS PELICULA, SALA.nombreSala AS SALA, SUCURSAL.nomSucursal AS SUCURSAL FROM PROYECCIONES INNER JOIN PELICULA ON PROYECCIONES.clvPelicula = PELICULA.clvPelicula INNER JOIN SALA ON PROYECCIONES.idSala = SALA.idSala INNER JOIN SUCURSAL ON SALA.idSucursal = SUCURSAL.idSucursal WHERE nomSucursal = '"+textBox5.Text+"'");
        }
    }
}
