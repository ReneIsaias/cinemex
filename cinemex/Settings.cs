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
    public partial class Settings : Form
    {
        Cinemex obj = new Cinemex();
        public Settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obj.limpiarRene(this);
            Settings_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCheckedListBox(checkedListBox1, "SELECT descripPersmiso FROM PERMISOS");
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PERSONAL_PERMISOS.idPermiso AS NUMERO, PERSONAL_PERMISOS.clvPersonal AS CLAVE, PERSONAL.emailPersonal AS EMAIL, PERMISOS.descripPersmiso AS BOTONES FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox1.Text + "'");
            while (obj.leerRene.Read())
            {
                foreach (Control objeto in this.Controls)
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
            textBox1.Text = user.Text;
        }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox1,"SELECT PERSONAL.emailPersonal FROM PERSONAL WHERE PERSONAL.statusPersonal=1");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            if ((comboBox1.Text != "")) {
                obj.ejecutaRene("EXEC eliminaPermisos '" + comboBox1.Text + "'");
                obj.leerRene.Close();
                //MessageBox.Show("Permisos Elimindados");
                listBox1.Items.Clear();
                //en este for validamos todas los checkBox que esten seleccionados
                for (int a = 0; a < checkedListBox1.CheckedItems.Count; a++)
                {
                    string haber = "";
                    //Los pasamos al listB ox en la pocicion de la variable "a"
                    listBox1.Items.Add(checkedListBox1.CheckedItems[a].ToString());
                    haber = checkedListBox1.CheckedItems[a].ToString();
                    obj.ejecutaRene("EXEC procPermisosPersonal '" + comboBox1.Text + "','" + haber + "'");
                    obj.leerRene.Close();                
                }
                MessageBox.Show("Permisos Asignados Correctamente");
                obj.limpiarRene(this);
                Settings_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Selecciona Primero al personal!");
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox1.Text = "";
        }
    }
}
