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
    public partial class Reservacion : Form
    {
        Cinemex obj = new Cinemex();
        public double cantidad;
        public double precio;
        public double total;
        public double subtotal;
        public double pago;
        public Reservacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Reservacion_Load(object sender, EventArgs e)
        {
            obj.conectaRene();
            obj.llenaCuadriculaRene(dataGridView2, "SELECT FUNCION.clvFuncion AS FUNCION, FUNCION.cantidadVenta AS CANTIDAD, FUNCION.precioVendido AS PRECIO, RESERVACION.clvReservacion AS RESERVACION, PROYECCIONES.clvProyeccion AS PROYECCION FROM FUNCION INNER JOIN RESERVACION ON FUNCION.clvReservacion = RESERVACION.clvReservacion INNER JOIN PROYECCIONES ON FUNCION.clvProyeccion = PROYECCIONES.clvProyeccion");            
            obj.llenaCuadriculaRene(dataGridView3, "SELECT RESERVACION.clvReservacion AS RESERVACION, RESERVACION.fechaReservacion AS FECHA, RESERVACION.costoTotal AS COSTO, SALA.nombreSala AS SALA, PERSONAL.emailPersonal AS PERSONAL FROM RESERVACION INNER JOIN SALA ON RESERVACION.idSala = SALA.idSala INNER JOIN PERSONAL ON RESERVACION.clvPersonal = PERSONAL.clvPersonal");
            obj.llenaCuadriculaRene(dataGridView1, "SELECT BOLETO.clvBoleto AS BOLETO, BOLETO.montoPagado AS COSTO, BOLETO.clvReservacion AS RESERVACION, CLIENTE.emailCliente AS CLIENTE FROM BOLETO INNER JOIN RESERVACION ON BOLETO.clvReservacion = RESERVACION.clvReservacion INNER JOIN CLIENTE ON BOLETO.rfcCliente = CLIENTE.rfcCliente");
            validarUsuario();
        }
        public void validarUsuario()
        {
            obj.ejecutaRene("SELECT PERSONAL_PERMISOS.idPermiso, PERSONAL_PERMISOS.clvPersonal, PERSONAL.emailPersonal, PERMISOS.descripPersmiso FROM PERSONAL_PERMISOS INNER JOIN PERSONAL ON PERSONAL_PERMISOS.clvPersonal = PERSONAL.clvPersonal INNER JOIN PERMISOS ON PERSONAL_PERMISOS.idPermiso = PERMISOS.idPermiso WHERE PERSONAL.emailPersonal = '" + textBox1.Text + "'");
            while (obj.leerRene.Read())
            {
                foreach (Control objeto in this.panel2.Controls)
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

        public void reciveDatos(TextBox pelicula, TextBox costo, TextBox fecha, ComboBox peli, TextBox sucursal, ComboBox sala)
        {
            comboBox4.Text = pelicula.Text;
            textBox8.Text = costo.Text;
            textBox10.Text = fecha.Text;
            label29.Text = peli.Text;
            comboBox2.Text = sucursal.Text;
            comboBox3.Text = sala.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox7.Text != "") && (textBox8.Text != "") && (textBox10.Text != "") && (comboBox3.Text != "") && (comboBox1.Text != "") && (comboBox4.Text != ""))
                {
                    if (total > pago)
                    {
                        subtotal = total - pago;
                        MessageBox.Show("Dinero no suficiente, falta -$" + subtotal);
                    }
                    else
                    {
                        obj.ejecutaRene("EXEC procReservaciones '" + textBox2.Text + "','" + textBox10.Text + "','" + textBox9.Text + "','" + comboBox3.Text + "','" + textBox1.Text + "'");
                        //MessageBox.Show("Reservacion hecha");
                        //obj.leerRene.Close();
                        //obj.ejecutaRene("EXEC procFuncion '" + textBox5.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + comboBox4.Text + "','" + textBox2.Text + "'");
                        //MessageBox.Show("Funcion Agregada Correctamente");
                        //obj.leerRene.Close();
                        //obj.ejecutaRene("EXEC procBoleto '" + textBox4.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "'");
                        //MessageBox.Show("Boleto registrado");
                        //obj.leerRene.Close();
                        int lista = checkedListBox1.CheckedItems.Count;
                        int cantidad = 0;
                        cantidad = int.Parse(textBox7.ToString());
                        MessageBox.Show("Cantidad " + cantidad);
                        MessageBox.Show("Lista " + lista);
                        if (lista == cantidad)
                        {
                            obj.ejecutaRene("EXEC eliminaAsientos '" + comboBox3.Text + "'");
                            obj.leerRene.Close();
                            MessageBox.Show("Asientos Modificados");
                            //en este for validamos todas los checkBox que esten seleccionados
                            for (int a = 0; a < checkedListBox1.CheckedItems.Count; a++)
                            {
                                string haber = "";
                                //Los pasamos al listB ox en la pocicion de la variable "a"
                                haber = checkedListBox1.CheckedItems[a].ToString();
                                obj.ejecutaRene("EXEC procSalaAsiento '" + comboBox3.Text + "','" + haber + "'");
                                label31.Text = haber.ToString();
                                panel1.Visible = true;
                                MessageBox.Show("Boleto "+haber);
                                obj.leerRene.Close();
                            }
                            MessageBox.Show("Datos Correctos");
                        }
                        else
                        {
                            MessageBox.Show("Seleccione solo " + cantidad + " boletos");
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Llena los campos primero");
                }
            }
            catch
            {
                MessageBox.Show("Has echo algo mal XP");                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obj.limpiarRene(this);
            obj.limpiarReneFull(panel2);
            checkedListBox1.Items.Clear();
            panel1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox3, "SELECT SALA.nombreSala FROM SALA INNER JOIN SUCURSAL ON SALA.idSucursal = SUCURSAL.idSucursal WHERE nomSucursal='" + comboBox2.Text + "' AND statusSala=1");
            String salas = "";
            salas = comboBox3.Text;
            obj.llenaCheckedListBox(checkedListBox1, "SELECT ASIENTO.nombreAsiento FROM DETALLE_ASIENTO INNER JOIN ASIENTO ON DETALLE_ASIENTO.idAsiento = ASIENTO.idAsiento INNER JOIN SALA ON DETALLE_ASIENTO.idSala = SALA.idSala WHERE nombreSala = '" + salas + "'");
                //Valida el precio
            if ((textBox8.Text != "") && (textBox7.Text != "") && (textBox3.Text != ""))
            {
                precio = double.Parse(textBox8.Text);
                cantidad = double.Parse(textBox7.Text);
                pago = double.Parse(textBox3.Text);
                total = precio * cantidad;
                textBox9.Text = total.ToString();
                subtotal = pago - total;
                textBox9.Visible = true;
                label14.Visible = true;
                if (total > pago)
                {
                    subtotal = total - pago;
                    MessageBox.Show("Dinero no suficiente, falta -$" + subtotal);
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Llena los campos");
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox10.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            textBox9.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
            comboBox3.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
            textBox1.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox7.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            comboBox4.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {           
            obj.llenaComboRene(comboBox4, "SELECT clvProyeccion FROM PROYECCIONES");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {            
        }

        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            label25.Text = textBox4.Text;
            label26.Text = textBox7.Text;
            label27.Text = textBox8.Text;
            label28.Text = comboBox3.Text;
            label30.Text = textBox10.Text;            
            label32.Text = textBox1.Text;
            label33.Text = comboBox4.Text;
            label34.Text = comboBox1.Text;
            label35.Text = textBox3.Text;
            label36.Text = total.ToString();
            label41.Text = subtotal.ToString();
            label42.Text = textBox10.Text;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox1, "SELECT emailCliente FROM CLIENTE WHERE statusCliente = 1");
            label45.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void textBox8_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox10_KeyUp(object sender, KeyEventArgs e)
        {
            if ((textBox8.Text != "") && (textBox7.Text != "") && (textBox3.Text != ""))
            {
                precio = double.Parse(textBox8.Text);
                cantidad = double.Parse(textBox7.Text);
                pago = double.Parse(textBox3.Text);
                total = precio * cantidad;
                textBox9.Text = total.ToString();
                subtotal = pago - total;
                textBox9.Visible = true;
                label14.Visible = true;
                if (total > pago)
                {
                    subtotal = total - pago;
                    MessageBox.Show("Dinero no suficiente, falta -$" + subtotal);
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Llena los campos");
            }
        }

        private void textBox10_MouseClick(object sender, MouseEventArgs e)
        {
            label45.Visible = true;
        }

        private void label45_Click(object sender, EventArgs e)
        {
            obj.ejecutaRene("SELECT getdate()");
            obj.leerRene.Read();
            textBox10.Text = obj.leerRene[0].ToString();            
            obj.leerRene.Close();
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            obj.llenaComboRene(comboBox2, "SELECT SUCURSAL.nomSucursal FROM SUCURSAL WHERE statusSucursal = 1");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox7.Text != "") && (textBox8.Text != "") && (textBox10.Text != "") && (comboBox3.Text != "") && (comboBox1.Text != "") && (comboBox4.Text != "")&&(comboBox2.Text!="")&&(checkedListBox1.Items.Count>1))
                {
                    precio = double.Parse(textBox8.Text);
                    cantidad = double.Parse(textBox7.Text);
                    pago = double.Parse(textBox3.Text);
                    total = precio * cantidad;
                    textBox9.Text = total.ToString();
                    subtotal = pago - total;
                    textBox9.Visible = true;
                    label14.Visible = true;
                    if (total > pago)
                    {
                        subtotal = total - pago;
                        MessageBox.Show("Dinero no suficiente, falta -$" + subtotal);
                    }
                    else
                    {
                        string cadena = "";
                        cadena = textBox7.Text;
                        //MessageBox.Show("cadena "+cadena);
                        int cantidad = 0;
                        cantidad = int.Parse(cadena);
                        //MessageBox.Show("cantidad "+cantidad.ToString());
                        int lista;
                        lista = checkedListBox1.CheckedItems.Count;
                        //MessageBox.Show("lista "+lista.ToString());
                        if (cantidad == lista)
                        {
                            obj.ejecutaRene("EXEC procReservaciones '" + textBox2.Text + "','" + textBox10.Text + "','" + textBox9.Text + "','" + comboBox3.Text + "','" + textBox1.Text + "'");
                            //MessageBox.Show("Reservacion hecha");
                            obj.leerRene.Close();
                            obj.ejecutaRene("EXEC procFuncion '" + textBox5.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + comboBox4.Text + "','" + textBox2.Text + "'");
                            //MessageBox.Show("Funcion Agregada Correctamente");
                            obj.leerRene.Close();
                            obj.ejecutaRene("EXEC procBoleto '" + textBox4.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "'");
                            //MessageBox.Show("Boleto registrado");
                            obj.leerRene.Close();
                            //obj.ejecutaRene("EXEC eliminaAsientos '" + comboBox3.Text + "'");
                            obj.leerRene.Close();
                            //MessageBox.Show("Asientos Modificados");
                            //en este for validamos todas los checkBox que esten seleccionados
                            for (int a = 0; a < checkedListBox1.CheckedItems.Count; a++)
                            {
                                string haber = "";
                                //Los pasamos al listB ox en la pocicion de la variable "a"
                                haber = checkedListBox1.CheckedItems[a].ToString();
                                //obj.ejecutaRene("EXEC procSalaAsiento '" + comboBox3.Text + "','" + haber + "'");
                                label31.Text = haber.ToString();
                                panel1.Visible = true;
                                MessageBox.Show("Boleto " + haber);
                                obj.leerRene.Close();
                                Reservacion_Load(sender, e);
                            }                            
                        }
                        else
                        {
                            MessageBox.Show("Seleccione "+cantidad+" boletos");
                        }
                    }                  
                }
                else
                {
                    MessageBox.Show("Llene primero todos los campos");
                }
            }
            catch
            {
                MessageBox.Show("Algo salio mal, espera un momento xP");
            }
        }

        private void label46_Click(object sender, EventArgs e)
        {
            string jaja = "";
            jaja = textBox7.Text;
            MessageBox.Show(jaja);
            int vaa = 0;
            vaa = int.Parse(jaja);
            MessageBox.Show(vaa.ToString());
            int lista;
            lista = checkedListBox1.CheckedItems.Count;
            MessageBox.Show(lista.ToString());
            if (vaa == lista)
            {
                MessageBox.Show("Iguales");
            }
            else
            {
                MessageBox.Show("No iguales");
            }

        }
    }
}
