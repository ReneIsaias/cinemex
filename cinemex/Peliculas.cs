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
    public partial class Peliculas : Form
    {
        Cinemex obj = new Cinemex();
        public Peliculas()
        {
            InitializeComponent();
        }

        private void Peliculas_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PELICULA.clvPelicula AS CLAVE, PELICULA.tituloOriginal AS TITULO, PELICULA.nombrePelicula AS NOMBRE, PELICULA.descriPelicula AS DESCRIPCION, PELICULA.anioPelicula AS AÑO, PELICULA.duracionPelicula AS DURACION, PELICULA.puntajePelicula AS PUNTAJE, PAIS.nombrePais AS PAIS, CLASIFICACION.descripClasificacion AS CLASIFICACION, GENERO.descripGenero AS GENERO, DIRECTOR.nombreDirector AS DIRECTOR, IDIOMA.nombreIdioma AS IDIOMA FROM PELICULA INNER JOIN CLASIFICACION ON PELICULA.idClasificacion = CLASIFICACION.idClasificacion INNER JOIN DIRECTOR ON PELICULA.idDirector = DIRECTOR.idDirector INNER JOIN GENERO ON PELICULA.idGenero = GENERO.idGenero INNER JOIN IDIOMA ON PELICULA.idIdioma = IDIOMA.idIdioma INNER JOIN PAIS ON PELICULA.idPais = PAIS.idPais");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox8.Text + "'");
            while (obj.leerRene.Read())
            {
                foreach (Control objeto in this.panel1.Controls)
                {
                    if ((objeto is Button))
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
            textBox8.Text = user.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != "") && (textBox7.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != "")  && (comboBox4.Text != "") && (comboBox5.Text != ""))
                {
                    obj.ejecutaRene("EXEC procPelicula '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','"+ comboBox5.Text + "'");
                    MessageBox.Show("Pelicula Agregada Correctamente");
                    button3_Click(sender, e);
                    Peliculas_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Error Inesperado, XP");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            obj.limpiarReneFull(panel1);
            obj.limpiarRene(this);
            Peliculas_Load(sender, e);
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox1, "SELECT nombrePais FROM PAIS WHERE PAIS.statusPais=1");
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox2, "SELECT descripClasificacion FROM CLASIFICACION WHERE CLASIFICACION.statusClasificacion=1");
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox3, "SELECT descripGenero FROM GENERO WHERE GENERO.statusGenero=1");
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox4, "SELECT nombreDirector FROM DIRECTOR WHERE DIRECTOR.statusDirector=1");            
        }

        private void comboBox5_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox5, "SELECT nombreIdioma FROM IDIOMA WHERE IDIOMA.statusIdioma=1");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            comboBox4.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            comboBox5.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
        }

        private void textBox9_KeyUp(object sender, KeyEventArgs e)
        {
            obj.llenaCuadriculaRene(dataGridView1, "EXEC buscaPelicula '"+ textBox9.Text +"'");
            obj.leerRene.Close();
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox1.Text = "";
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox2.Text = "";
        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox3.Text = "";
        }

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox4.Text = "";
        }

        private void comboBox5_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox5.Text = "";
        }
    }
}
