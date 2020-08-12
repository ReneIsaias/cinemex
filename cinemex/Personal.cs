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
    public partial class Personal : Form
    {
        Cinemex obj = new Cinemex();
        public Personal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            obj.limpiarReneFull(panel1);
            obj.limpiarRene(this);
            Personal_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox7.Text != "") && (textBox8.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != ""))
                {
                    obj.ejecutaRene("EXEC procPersonal '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + comboBox3.Text + "','" + comboBox2.Text + "'");
                    MessageBox.Show("Personal Agregado Correctamente");
                    button6_Click(sender, e);
                    Personal_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Consulta Ejecutada Con Errores");
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PERSONAL.clvPersonal AS CLAVE, PERSONAL.nombrePersonal AS NOMBRE, PERSONAL.apeUnoPersonal AS APELLIDO_1, PERSONAL.apeDosPersonal AS APELLIDO_2, PERSONAL.telefonoPersonal AS TELEFONO, PERSONAL.emailPersonal AS EMAIL, PERSONAL.contraseniaPersonal AS CONTRASEÑA, PERSONAL.statusPersonal AS ESTADO, TIPO_PERSONA.descripTipoPersona AS CARGO FROM PERSONAL INNER JOIN TIPO_PERSONA ON PERSONAL.idTipoPersona = TIPO_PERSONA.idTipoPersona");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox6.Text + "'");
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
            textBox6.Text = user.Text;
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox2, "SELECT TIPO_PERSONA.descripTipoPersona FROM TIPO_PERSONA WHERE statusTipoPersona=1");
        }

        private void buscarPersona(object sender, KeyEventArgs e)
        {
            
            obj.llenaCuadriculaRene(dataGridView1, "EXEC buscaPersonal '" + textBox9.Text + "'");
            obj.leerRene.Close();
        }

        private void pasarDatos(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox2.Text = "";
        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox3.Text = "";
        }
    }
}
