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
    public partial class Ventas : Form
    {
        Cinemex obj = new Cinemex();
        public Ventas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
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

        private void button3_Click(object sender, EventArgs e)
        {
            obj.limpiarRene(this);
            Ventas_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guardado");
        }
    }
}
