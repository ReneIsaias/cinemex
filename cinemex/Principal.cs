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
    public partial class Principal : Form
    {
        Cinemex obj = new Cinemex();
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            validarUsuario();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PERMISOS.descripPersmiso AS BOTONES FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox1.Text + "'");
            obj.llenaCuadriculaRene(dataGridView2, "SELECT PROYECCIONES.clvProyeccion AS CLAVE, PROYECCIONES.fechaInicio AS INICIA, PROYECCIONES.fechaTermino AS TERMINA, PROYECCIONES.costoProyeccion AS COSTO, PROYECCIONES.statusProyeccion AS ESTADO, PELICULA.nombrePelicula AS PELICULA, SALA.nombreSala AS SALA FROM PROYECCIONES INNER JOIN PELICULA ON PROYECCIONES.clvPelicula = PELICULA.clvPelicula INNER JOIN SALA ON PROYECCIONES.idSala = SALA.idSala INNER JOIN SUCURSAL ON SALA.idSucursal = SUCURSAL.idSucursal where SUCURSAL.nomSucursal = '" + textBox3.Text + "'");
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox1.Text + "'");
            while (obj.leerRene.Read())
            {
                foreach (Control objeto in this.panel2.Controls)
                {
                    if (objeto is Button || objeto is PictureBox)
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
        public void reciveCartelera(ComboBox estado, ComboBox sucursal, TextBox user)
        {            
            textBox2.Text = estado.Text;
            textBox3.Text = sucursal.Text;
            textBox1.Text = user.Text;
            label7.Text = "SUCURSAL " + sucursal.Text;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (panel2.Width == 70)
            {
                panel2.Width = 250;
            }
            else
            {
                panel2.Width = 70;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Peliculas peli = new Peliculas();
            peli.obtieneUsuario(textBox1);
            peli.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Funciones func = new Funciones();
            func.obtieneUsuario(textBox1, textBox3);
            func.ShowDialog();
            Principal_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reservacion rese = new Reservacion();
            rese.obtieneUsuario(textBox1);
            rese.ShowDialog();
            Principal_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sucursales sucu = new Sucursales();
            sucu.obtieneUsuario(textBox1);
            sucu.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Producto prod = new Producto();
            prod.obtieneUsuario(textBox1);
            prod.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Personal person = new Personal();
            person.obtieneUsuario(textBox1);
            person.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            venta.obtieneUsuario(textBox1);
            venta.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Settings sett = new Settings();
            sett.obtieneUsuario(textBox1);
            sett.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Sala sala = new Sala();
            sala.obtieneUsuario(textBox1);
            sala.ShowDialog();
            Principal_Load(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Aqui
            this.Hide();
            Login logear = new Login();
            logear.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            button8_Click_1(sender, e);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
