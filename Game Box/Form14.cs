using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace Game_Box
{
    public partial class Form14 : Form
    {
        public Form14(String idpasas, int modos)
        {
            InitializeComponent();
            this.idventa = idpasas;
            this.modoz = modos;
            
            
        }

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador, Adaptador1;
        static DataTable Fuente, Fuente1;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();
        DataTable datos = new DataTable();  
      


        //Declaración de Variables
        String idventa, idpasable, sql, sql1, id_venta, desc, total, cliente, empleado, fecha, idprod, precio, nomprod;
        int modoz,modo,cont;
        string [] coma;
       

        //Conexión con la BD
        private void conectar()
        {
            StrConexion = @"Server=127.0.0.1;Port=5432;" + "User id=mediabox; password=mediabox; Database=MediaContent; Encoding=UTF8;";

            //Objeto conexion
            con = new NpgsqlConnection(StrConexion);

            //con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MediaContent.accdb";

            try
            {
                con.Open();
                // pictureBox2.Visible = true;
               // MessageBox.Show("Inicio Correcto");
            }

            catch (Exception)
            {
                MessageBox.Show("Error de Inicio \nLa Aplicacion se cerrará");
                this.Close();
            }
        }
        
        public void Consultar()
        {
            Adaptador = new NpgsqlDataAdapter(sql, con);
            DataSet.Reset();
            Fuente = new DataTable();
            Adaptador.Fill(DataSet);
            Fuente = DataSet.Tables[0];
        }

        public void Consultar1()
        {
            Adaptador1 = new NpgsqlDataAdapter(sql1, con);
            DataSet1.Reset();
            Fuente1 = new DataTable();
            Adaptador1.Fill(DataSet1);
            Fuente1 = DataSet1.Tables[0];
        }

        //----Inicializar Ventas
        public void Sacarinfo()
        {
            try
            {
                sql = "SELECT \"Id_ventas\",\"Descripcion\",\"Total_venta\",\"Id_cliente\",\"Id_empleado\",\"Fecha_venta\" FROM \"Ventas\" WHERE \"Id_ventas\" = "+idventa;
                Consultar();
                dataGridView3.DataSource = Fuente;

                id_venta = dataGridView3.Rows[0].Cells[0].Value.ToString();
                desc = dataGridView3.Rows[0].Cells[1].Value.ToString();
                total = dataGridView3.Rows[0].Cells[2].Value.ToString();
                cliente = dataGridView3.Rows[0].Cells[3].Value.ToString();
                empleado = dataGridView3.Rows[0].Cells[4].Value.ToString();
                fecha = dataGridView3.Rows[0].Cells[5].Value.ToString();

                label3.Text = total;
                label6.Text = cliente;
                label5.Text = empleado;
                label9.Text = fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error en el proceso:\n\n" + ex);
            }

        }

        //----Inicializar Productos
        public void Inicializar()
        {
            try
            {
                coma = desc.Split(',');

                foreach (string prod in coma)
                {
                    sql1 = "SELECT \"Id_producto\",\"Nombre\",\"Precio_publico\" FROM \"Productos\" WHERE \"Id_producto\" = " + prod;
                    Consultar1();
                    dataGridView2.DataSource = Fuente1;
                    //añadir valores al grid principal
                    idprod = dataGridView2.Rows[0].Cells[0].Value.ToString();
                    nomprod = dataGridView2.Rows[0].Cells[1].Value.ToString();
                    precio = dataGridView2.Rows[0].Cells[2].Value.ToString();

                    foreach (string prod1 in coma)
                    {
                        if (prod.Equals(prod1))
                        {
                            cont = cont + 1;
                        }
                    }

                    datos.Rows.Add(idprod, nomprod, cont, precio);
                    cont = 0;
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Ha ocurrido un error en el proceso:\n\n" + ex1);
            }

                   
                     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modo = 3;
            if (dataGridView1.SelectedRows.Count > 0 &&
                      dataGridView1.SelectedRows[0].Index !=
                         dataGridView1.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form11 formu11_6 = new Form11(modo, idpasable);
                formu11_6.Show();
                modo = 0;
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        public void campos()
        {
            datos.Columns.Add("Id Producto");
            datos.Columns.Add("Nombre");
            datos.Columns.Add("Cantidad");
            datos.Columns.Add("Precio");
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            conectar(); 
            campos();
            dataGridView1.DataSource = datos;
            Sacarinfo();
            Inicializar();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
    
    }
}
