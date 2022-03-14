using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using System.IO;

namespace Game_Box
{
    public partial class Form5 : Form
    {
        public Form5(String usuario,String password)
        {
            InitializeComponent();
            this.user = usuario;
            this.pass = password;
        }
        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador, Adaptador1, Adaptador2, Adaptador3;
        static DataTable Fuente;
        static DataTable Fuente1;
        static DataTable Fuente2;
        static DataTable Fuente3;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();
        private DataSet DataSet2 = new DataSet();
        private DataSet DataSet3 = new DataSet();
       
        String sql, sql1, sql2, sql3, idpasable,user,pass,ruta,extension;
        int modo = 0;
        

        private void conectar()
        {
            StrConexion = @"Server=127.0.0.1;Port=5432;" + "User id=mediabox; password=mediabox; Database=MediaContent; Encoding=UTF8;";

            //Objeto conexion
            con = new NpgsqlConnection(StrConexion);

          /*  StrConexion = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=|DataDirectory|\MediaContent.accdb; Jet OLEDB:Database Password=mediabox;";

            //Objeto conexion
            con = new OleDbConnection(StrConexion);

            //con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MediaContent.accdb";*/

            try
            {
                con.Open();
                //MessageBox.Show("Inicio Correcto");
            }

            catch (Exception)
            {
                MessageBox.Show("Error de Inicio \nLa Aplicacion se cerrará");
                this.Close();
            }
        }

       /* public static BindingSource Source
        {
            get
            {
                return Fuente;
            }
        }
        public static BindingSource Source1
        {
            get
            {
                return Fuente1;
            }
        }
        public static BindingSource Source2
        {
            get
            {
                return Fuente2;
            }
        }*/
        //Conexion a la BD     
        public void Consultar()
        {

            Adaptador = new NpgsqlDataAdapter(sql, con);
            DataSet.Reset();
            Fuente = new DataTable();
            Adaptador.Fill(DataSet);
            Fuente = DataSet.Tables[0];

            /*Adaptador = new OleDbDataAdapter(sql, con);
            Constructor = new OleDbCommandBuilder(Adaptador);
            tabla = "ventas";
            Tabla = new DataTable(tabla);
            Adaptador.Fill(Tabla);
            Fuente = new BindingSource();
            Fuente.DataSource = Tabla;*/

        }

        public void Consultar1()
        {
            Adaptador1 = new NpgsqlDataAdapter(sql1, con);
            DataSet1.Reset();
            Fuente1 = new DataTable();
            Adaptador1.Fill(DataSet1);
            Fuente1 = DataSet1.Tables[0];
            /*Adaptador1 = new OleDbDataAdapter(sql1, con);
            Constructor1 = new OleDbCommandBuilder(Adaptador1);
            tabla1 = "usuarios";
            Tabla1 = new DataTable(tabla1);
            Adaptador1.Fill(Tabla1);
            Fuente1 = new BindingSource();
            Fuente1.DataSource = Tabla1;*/

        }

        public void Consultar2()
        {
            Adaptador2 = new NpgsqlDataAdapter(sql2, con);
            DataSet2.Reset();
            Fuente2 = new DataTable();
            Adaptador2.Fill(DataSet2);
            Fuente2 = DataSet2.Tables[0];
            /* Adaptador2 = new OleDbDataAdapter(sql2, con);
            Constructor2 = new OleDbCommandBuilder(Adaptador2);
            tabla2 = "JuegosPSP";
            Tabla2 = new DataTable(tabla2);
            Adaptador2.Fill(Tabla2);
            Fuente2 = new BindingSource();
            Fuente2.DataSource = Tabla2;*/

        }

        public void Consultar3()
        {
            Adaptador3 = new NpgsqlDataAdapter(sql3, con);
            DataSet3.Reset();
            Fuente3 = new DataTable();
            Adaptador3.Fill(DataSet3);
            Fuente3 = DataSet3.Tables[0];
        }

        public void inicializar()
        {
            //----Inicializar Ventas
            sql = "SELECT \"Id_ventas\",\"Descripcion\",\"Total_venta\",\"Id_cliente\",\"Id_empleado\",\"Fecha_venta\" FROM \"Ventas\" ORDER BY \"Id_ventas\"";
            Consultar();
            dataGridView1.DataSource = Fuente;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            //----Inicializar Usuarios
            sql1 = "SELECT \"Id_empleado\",\"Nombre\",\"Puesto\",\"Usuario\" FROM \"Empleados\" ORDER BY \"Id_empleado\" ";
            Consultar1();
            dataGridView3.DataSource = Fuente1;
            this.dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.MultiSelect = false;
            //-----Inicializar Productos
            sql2 = "SELECT \"Id_producto\",\"Nombre\",\"Existencias\",\"Precio_lista\" FROM \"Productos\" ORDER BY \"Id_producto\" DESC";
            Consultar2();
            dataGridView2.DataSource = Fuente2;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.MultiSelect = false;
            //-----Inicializar Productos
            sql3 = "SELECT \"Id_clientes\",\"Nombre\",\"Direccion\",\"Saldo\" FROM \"Clientes\" ORDER BY \"Id_clientes\" ";
            Consultar3();
            dataGridView4.DataSource = Fuente3;
            this.dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.MultiSelect = false;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;

            conectar();
            inicializar();

           toolTip1.SetToolTip(this.button12,"Actualizar Conexión con la BD");
           //dataGridView2.SelectedRows.Count
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            con.Close();
          //  MessageBox.Show("Conexion Terminada");
            conectar();
            inicializar();
            MessageBox.Show("Conexion Actualizada");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 formu7 = new Form7();
            formu7.Show();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            //toolTip1.Show("Actualizar", button12);
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0 &&
           dataGridView3.SelectedRows[0].Index !=
              dataGridView3.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form8 formu8 = new Form8(idpasable);
                formu8.Show();
                
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0 &&
                      dataGridView3.SelectedRows[0].Index !=
                         dataGridView3.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form8 formu8 = new Form8(idpasable);
                formu8.Show();
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0 &&
                      dataGridView3.SelectedRows[0].Index !=
                         dataGridView3.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form9 formu9 = new Form9(idpasable,user);
                formu9.Show();
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0 &&
                      dataGridView3.SelectedRows[0].Index !=
                         dataGridView3.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form10 formu10 = new Form10(idpasable,pass,user);
                formu10.Show();
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modo = 1;
            Form11 formu11 = new Form11(modo,idpasable);
            formu11.Show();
            modo = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            modo = 2;
            if (dataGridView2.SelectedRows.Count > 0 &&
                      dataGridView2.SelectedRows[0].Index !=
                         dataGridView2.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form11 formu11_5 = new Form11(modo, idpasable);
                formu11_5.Show();
                modo = 0;

            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modo = 3;
            if (dataGridView2.SelectedRows.Count > 0 &&
                      dataGridView2.SelectedRows[0].Index !=
                         dataGridView2.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView2.CurrentRow.Cells[0].Value.ToString();
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

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            modo = 3;
            if (dataGridView2.SelectedRows.Count > 0 &&
                      dataGridView2.SelectedRows[0].Index !=
                         dataGridView2.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form11 formu11_5 = new Form11(modo, idpasable);
                formu11_5.Show();
                modo = 0;
            }
        }

        private void Personas_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            modo = 0;
            if (dataGridView2.SelectedRows.Count > 0 &&
                     dataGridView2.SelectedRows[0].Index !=
                        dataGridView2.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form12 formu12 = new Form12(idpasable,pass,modo);
                formu12.Show();
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            modo = 0;
           
                Form13 formu13 = new Form13(idpasable,modo);
                formu13.Show();
            
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            modo = 1;
            if (dataGridView4.SelectedRows.Count > 0 &&
                     dataGridView4.SelectedRows[0].Index !=
                        dataGridView4.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form13 formu13_1 = new Form13(idpasable, modo);
                formu13_1.Show();
                modo = 0;
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            modo = 1;
            if (dataGridView4.SelectedRows.Count > 0 &&
                     dataGridView4.SelectedRows[0].Index !=
                        dataGridView4.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form12 formu12_1 = new Form12(idpasable,pass,modo);
                formu12_1.Show();
                modo = 0;
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivo de Word (*.DOC)|*.DOC";
            saveFileDialog1.ShowDialog();
            try
            {

                if ((saveFileDialog1.FileName != ""))
                {

                    ruta = saveFileDialog1.FileName;
                    extension = ruta.Substring(ruta.LastIndexOf('.') + 1).ToLower();
                    TextWriter sw = new StreamWriter(ruta);
                    int rowcount = dataGridView1.Rows.Count;
                    
                        sw.WriteLine("Id ventas\t|Descripcion\t|Total venta\t|Id cliente\t|Id empleado");
                        for (int i = 0; i < rowcount - 1; i++)
                        {
                            sw.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString() + "\t|" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "\t|" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\t|" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "\t|" + dataGridView1.Rows[i].Cells[4].Value.ToString());
                        }
                        sw.Close(); //Don't Forget Close the TextWriter Object(sw)
                        MessageBox.Show("Archivo creado con Éxito");
                                        

                } MessageBox.Show("Exportación Cancelada");

            }
            catch
            {
                MessageBox.Show("Error al Exportar Lista");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            modo = 1;
            if (dataGridView1.SelectedRows.Count > 0 &&
                     dataGridView1.SelectedRows[0].Index !=
                        dataGridView1.Rows.Count - 1)
            {
                //dataGridView1.Rows.RemoveAt(
                idpasable = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                label4.Text = idpasable;
                Form14 formu14 = new Form14(idpasable, modo);
                formu14.Show();
                modo = 0;
            }
            else
            {
                MessageBox.Show("Selecciona un registro");
            }
            
        }

        

    }
}
