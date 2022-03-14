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
    public partial class Form12 : Form
    {
        public Form12(String idp, String pass, int modo)
        {
            InitializeComponent();
            this.id = idp;
            this.password = pass;
            this.mode = modo;
        
        }

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static NpgsqlDataAdapter Adaptador1;
        static DataTable Fuente,Fuente1;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();

        //Inicializar Variables
        String id, sql, password, contrasena, info, nombre, imagen, idioma, genero, tipo, existencias, descripcion, preciopublico, preciolista, id1, direccion, saldo, idc;
        int mode;

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
            //Fuente = DataSet.Tables[0];
        }

        public void llenagrid()
        {
            nombre = dataGridView1.Rows[0].Cells[0].Value.ToString();
            imagen = dataGridView1.Rows[0].Cells[1].Value.ToString();
            idioma = dataGridView1.Rows[0].Cells[2].Value.ToString();
            genero = dataGridView1.Rows[0].Cells[3].Value.ToString();
            tipo = dataGridView1.Rows[0].Cells[4].Value.ToString();
            existencias = dataGridView1.Rows[0].Cells[5].Value.ToString();
            descripcion = dataGridView1.Rows[0].Cells[6].Value.ToString();
            preciopublico = dataGridView1.Rows[0].Cells[7].Value.ToString();
            preciolista = dataGridView1.Rows[0].Cells[8].Value.ToString();
            id1 = dataGridView1.Rows[0].Cells[9].Value.ToString();

            info = id1 + " | " + nombre + " | " + idioma + " | " + genero + " | " + tipo + " | " + preciopublico;

            label2.Text = info;
        }

        public void llenagrid1()
        {
            nombre = dataGridView1.Rows[0].Cells[0].Value.ToString();
            direccion = dataGridView1.Rows[0].Cells[1].Value.ToString();
            saldo = dataGridView1.Rows[0].Cells[2].Value.ToString();
            idc = dataGridView1.Rows[0].Cells[3].Value.ToString();

            info = idc + " | " + nombre + " | " + direccion + " | " + saldo;

            label2.Text = info;
            

        }
        
        private void Form12_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Width = 532;
            this.Height = 180;
            conectar();

            try
            {
                if (mode == 0)
                {
                    sql = "SELECT * FROM \"Productos\" WHERE \"Id_producto\" ='" + id + "'";
                    Consultar();
                    dataGridView1.DataSource = Fuente;
                    llenagrid();
                }

                if (mode == 1)
                {
                    sql = "SELECT * FROM \"Clientes\" WHERE \"Id_clientes\" ='" + id + "'";
                    Consultar();
                    dataGridView1.DataSource = Fuente;
                    llenagrid1();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex, "Error al Mostrar Datos");
            }

            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Height = 310;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            info = "";
            con.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contrasena = textBox1.Text;
            if (contrasena.Equals(""))
            {
                MessageBox.Show("Ingresa la contraseña", "ERROR");
            }
            else
            {
                if (contrasena.Equals(password))
                {
                    try
                    {
                      switch (mode)
                        {
                          case 0: sql = "DELETE  FROM \"Productos\" WHERE \"Id_producto\" =" + id + "";
                                break;

                          case 1: sql = "DELETE  FROM \"Clientes\" WHERE \"Id_clientes\" =" + id + "";
                                break;
                        }

                       

                        Consultar1();
                        MessageBox.Show("Proceso Concluido Exitosamente");
                        info = "";
                        textBox1.Text = "";
                        con.Close();
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: \n" + ex, "Error al Procesar");
                    }
                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta", "ERROR");
                    textBox1.Text = "";
                }
            }
        }
    }
}
