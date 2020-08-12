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
    public partial class Producto : Form
    {
        Cinemex obj = new Cinemex();
        public Producto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obj.limpiarRene(this);
            obj.limpiarReneFull(panel1);
            Producto_Load(sender, e);
        }
        private void Producto_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCuadriculaRene(dataGridView1, "SELECT PRODUCTO.clvProducto AS CLAVE, PRODUCTO.nombreProducto AS NOMBRE, PRODUCTO.descriProducto AS DESCRIPCION , PRODUCTO.cantidadProducto AS CANTIDAD, PRODUCTO.precioProducto AS PRECIO,statusProducto AS ESTADO, CATEGORIA.descripCategoria AS CATEGORIA, MARCA.descripMarca AS MARCA, TAMANIO.descripTamanio AS TAMANO FROM PRODUCTO INNER JOIN CATEGORIA ON PRODUCTO.idCategoria = CATEGORIA.idCaregoria INNER JOIN TAMANIO ON PRODUCTO.idTamanio = TAMANIO.idTamanio INNER JOIN MARCA ON PRODUCTO.idMarca = MARCA.idMarca WHERE PRODUCTO.statusProducto=1");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox7.Text + "'");
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
            textBox7.Text = user.Text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != "") && (comboBox4.Text != ""))
                {
                    obj.ejecutaRene("EXEC procProducto " + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "'");
                    MessageBox.Show("Producto Agregado Correctamente");
                    button3_Click(sender, e);
                    Producto_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Llene los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Ocurrio un error insesperado XP");
            }
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox2, "SELECT CATEGORIA.descripCategoria FROM CATEGORIA WHERE CATEGORIA.statusCategoria=1");
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox3, "SELECT MARCA.descripMarca FROM MARCA WHERE MARCA.statusMarca=1");
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox4, "SELECT TAMANIO.descripTamanio FROM TAMANIO WHERE TAMANIO.statusTamanio=1");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox4.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            obj.llenaCuadriculaRene(dataGridView1, "EXEC buscaProducto '" + textBox6.Text + "'");
            obj.leerRene.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox3.Text = "";
        }

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            comboBox4.Text = "";
        }
    }
}