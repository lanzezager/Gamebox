using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;


namespace Game_Box
{
    public partial class Form13 : Form
    {
        public Form13(String idpasa, int modo)
        {
            InitializeComponent();
            this.id = idpasa;
            this.mode = modo;
        }

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static NpgsqlDataAdapter Adaptador1;
        static DataTable Fuente, Fuente1;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();

        //Declaracion de Variables
        String sql, id, idc, nombre, direccion, saldo, nom, dir, sal, campovacio, campoupdate, campomod;
        int mode,cont=0;
        decimal resultado = 0;
        bool canConvert;

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
                //MessageBox.Show("Inicio Correcto");
            }

            catch (Exception)
            {
                MessageBox.Show("Error de Inicio \nLa Aplicacion se cerrará");
                this.Close();
            }
        }

        //Conexion a la BD     
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
            Adaptador1 = new NpgsqlDataAdapter(sql, con);
            DataSet1.Reset();
            Fuente1 = new DataTable();
            Adaptador1.Fill(DataSet1);
            //Fuente1 = DataSet1.Tables[0];
        }

        public void llenagrid()
        {
            nombre = dataGridView1.Rows[0].Cells[0].Value.ToString();
            direccion = dataGridView1.Rows[0].Cells[1].Value.ToString();
            saldo = dataGridView1.Rows[0].Cells[2].Value.ToString();
            idc = dataGridView1.Rows[0].Cells[3].Value.ToString();                                                
        }

        public void llenagrid1()
        {
            nombre = dataGridView1.Rows[0].Cells[0].Value.ToString();
            direccion = dataGridView1.Rows[0].Cells[1].Value.ToString();
            saldo = dataGridView1.Rows[0].Cells[2].Value.ToString();
            idc = dataGridView1.Rows[0].Cells[3].Value.ToString();

            textBox1.Text = nombre;
            textBox2.Text = direccion;
            textBox3.Text = saldo;
            label5.Text = idc;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void insertar() 
        {
            nom = textBox1.Text;
            dir = textBox2.Text;
            sal = textBox3.Text;

            campovacio = "";

            if (nom.Equals(""))
            {
                campovacio = "Nombre\n";
            }

            if (dir.Equals(""))
            {
                campovacio += "Dirección\n";
            }
            if (sal.Equals(""))
            {
                campovacio += "Saldo\n";
            }
            else
            {
                //verificar saldo
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(sal, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El Saldo que ingreso no es válido");
                    textBox3.Text = "";
                    sal = "";
                    campovacio += "Saldo\n";
                }
            }

            if ((campovacio != ""))
            {
                MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n" + campovacio);
            }
            else
            {
                    sql = "SELECT * FROM \"Clientes\" WHERE \"Nombre\" ='" + nom + "'";
                    Consultar();
                    dataGridView1.DataSource = Fuente;

                    if (Fuente.IsInitialized == false)
                    {
                        llenagrid();
                    }
                   
                    cont = dataGridView1.RowCount;                        

                    if (cont > 1)
                    {
                        MessageBox.Show("Ya Existe un Cliente con ese nombre\npor favor no duplique clientes");
                    }
                    else
                    {
                        sql = "INSERT INTO \"Clientes\" (\"Nombre\", \"Direccion\", \"Saldo\") VALUES ('" + nom + "','" + dir + "','" + sal + "')";
                        Consultar1();
                        con.Close();
                        conectar();
                        MessageBox.Show("Proceso concluido exitosamente");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        
                    }

                
              }

            }
        
        public void modificar()
        {
            nom = textBox1.Text;
            dir = textBox2.Text;
            sal = textBox3.Text;

            campovacio = "";

            if (nom.Equals(""))
            {
                campovacio = "Nombre\n";
            }

            if (dir.Equals(""))
            {
                campovacio += "Dirección\n";
            }
            if (sal.Equals(""))
            {
                campovacio += "Saldo\n";
            }
            else
            {
                //verificar saldo
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(sal, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El Saldo que ingreso no es válido");
                    textBox3.Text = "";
                    sal = "";
                    campovacio += "Saldo\n";
                }
            }

            if ((campovacio != ""))
            {
                MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n" + campovacio);
            }
            else
            {
                if (nombre != nom)
                {
                    campoupdate = "\"Nombre\" ='" + nom + "',";
                    campomod = "Nombre: " + nombre + " por " + nom + "\n";
                }

                if (direccion != dir)
                {
                    campoupdate += "\"Direccion\" ='" + dir + "',";
                    campomod += "Direccion: " + direccion + " por " + dir + "\n";
                }

                if (saldo != sal)
                {
                    campoupdate += "\"Saldo\" ='" + sal + "',";
                    campomod += "Saldo: " + saldo + " por " + sal + "\n";
                }

                if ((campoupdate != "") && (campoupdate != null))
                {
                    sql = "SELECT * FROM \"Clientes\" WHERE \"Nombre\" ='" + nom + "'";
                    Consultar();
                    dataGridView1.DataSource = Fuente;

                    if (Fuente.IsInitialized == false)
                    {
                        llenagrid();
                    }

                    if (nombre != nom)
                    {
                        cont = dataGridView1.RowCount;
                    }
                    else
                    {
                        cont = 1;
                    }

                    if (cont > 1)
                    {
                        MessageBox.Show("Ya Existe un Cliente con ese nombre\npor favor no duplique clientes");
                    }
                    else
                    {
                        //Modificar Registro Existente
                        campoupdate = campoupdate.Substring(0, campoupdate.Length - 1);
                        sql = "UPDATE \"Clientes\" SET " + campoupdate + " WHERE \"Id_clientes\" =" + id; ;
                        Consultar1();
                        con.Close();
                        conectar();
                        MessageBox.Show("Proceso concluido exitosamente");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                }

            }
        }
        
        private void Form13_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            conectar();

            if (mode == 1)
            {               
                try
                {
                    label4.Visible = true;
                    label5.Visible = true;
                    sql = "SELECT * FROM \"Clientes\" WHERE \"Id_clientes\" ='" + id + "'";
                    Consultar();
                    dataGridView1.DataSource = Fuente;
                    llenagrid1();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex, "Error al Mostrar Datos");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (mode)
            {
                case 0: insertar();
                    break;
                case 1: modificar();
                    break;

                default:
                    break;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
    }
}
