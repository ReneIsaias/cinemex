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
    public partial class Sala : Form
    {
        Cinemex obj = new Cinemex();
        public Sala()
        {
            InitializeComponent();
        }

        private void Sala_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            validarUsuario();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT SALA.nombreSala AS SALA, SALA.numAsientos AS ASIENTOS, SALA.statusSala AS ESTADO,ESTADO.nombreEstado AS CIUDAD, SUCURSAL.nomSucursal AS SUCURSAL FROM SALA INNER JOIN SUCURSAL ON SALA.idSucursal = SUCURSAL.idSucursal INNER JOIN ESTADO ON SUCURSAL.idEstado = ESTADO.idEstado ORDER BY nomSucursal");
            obj.llenaCheckedListBox(checkedListBox1, "SELECT ASIENTO.nombreAsiento FROM ASIENTO WHERE statusAsiento=1");
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox4.Text + "'");
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
            textBox4.Text = user.Text;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            obj.limpiarReneFull(panel1);
            textBox5.Text = "";
            Sala_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "")&&(textBox2.Text != "")&&(comboBox2.Text != "")&&(comboBox4.Text != "") && (comboBox1.Text != ""))
                {
                    obj.ejecutaRene("EXEC procSala '" + textBox3.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox4.Text + "','" + comboBox2.Text + "'");
                    MessageBox.Show("Sala Guardada Correctamente");
                    obj.limpiarRene(this);
                    Sala_Load(sender, e);                    
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado XD");
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int dale = 0;
                dale = (comboBox1.SelectedIndex) + 1;
                if (comboBox1.Text != "")
                {
                    obj.llenaComboRene(comboBox2, "SELECT SUCURSAL.nomSucursal FROM SUCURSAL WHERE statusSucursal = 1 AND idEstado = '" + dale + "'");
                }
                else
                {
                    MessageBox.Show("Selecciona una ciudad");
                }
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un error inesperado alv pr XD");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            obj.llenaCuadriculaRene(dataGridView1, "EXEC buscaSala '"+textBox5.Text+"'");
            obj.leerRene.Close();
        }

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox4.Text = "";
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox2.Text = "";
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox1.Text = "";
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox1, "SELECT nombreEstado FROM ESTADO WHERE statusEstado=1");
            comboBox2.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (comboBox2.Text != "") && (comboBox4.Text != "") && (comboBox1.Text != ""))
                {
                    string jaja = "";
                    jaja = textBox2.Text;
                    MessageBox.Show(jaja);
                    int vaa = 0;
                    vaa = int.Parse(jaja);
                    MessageBox.Show(vaa.ToString());
                    int lista;
                    lista = checkedListBox1.CheckedItems.Count;
                    MessageBox.Show(lista.ToString());
                    if (vaa == lista)
                    {
                        obj.ejecutaRene("EXEC eliminaAsientos '" + textBox1.Text + "','" + comboBox2.Text + "'");
                        obj.leerRene.Close();
                        for (int a = 0; a < checkedListBox1.CheckedItems.Count; a++)
                        {
                            string haber = "";
                            haber = checkedListBox1.CheckedItems[a].ToString();
                            obj.ejecutaRene("EXEC procSalaAsiento '" + textBox1.Text + "','" + comboBox2.Text + "','" + haber + "'");
                            obj.leerRene.Close();
                        }
                        MessageBox.Show("Asientos Guardados Correctamente");
                        obj.limpiarRene(this);
                        Sala_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("No son iguales los asientos XD");
                    }                    
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Ocurrio un error inesperado XD");
            }
        }
    }
}
