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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

      

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static DataTable DataTabla;
        private DataSet DataSet = new DataSet();

        //Variables
        
        String sql,id, nombre, password,puesto,pass,nom;
        int fila = 0, intento = 0, permiso = 0;

        //Inician Funciones de Conexion de la BD
        private void conectar()
        {
            StrConexion = @"Server=127.0.0.1;Port=5432;" + "User id=mediabox; password=mediabox; Database=MediaContent; Encoding=UTF8;";

            //Objeto conexion
            con = new NpgsqlConnection(StrConexion);

            //con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MediaContent.accdb";

            try
            {
                con.Open();
                pictureBox1.Visible = true;
                //MessageBox.Show("Inicio Correcto");
            }

            catch (Exception es)
            {
                MessageBox.Show("Error de Inicio \nLa Aplicacion se cerrará"+es.ToString());
                this.Close();
            }
        }

       
        //Conexion a la BD     
        public void Consultar()
        {
            Adaptador = new NpgsqlDataAdapter(sql, con);
            DataSet.Reset();
            DataTabla = new DataTable();
            Adaptador.Fill(DataSet);
            DataTabla = DataSet.Tables[0];

        }
       
        //Llena Grid
        public void Consultas()
        {
            //Recoger Datos del Grid

            nombre = dataGridView1.Rows[fila].Cells[0].Value.ToString();
            password = dataGridView1.Rows[fila].Cells[1].Value.ToString();
            puesto = dataGridView1.Rows[fila].Cells[2].Value.ToString();
            id = dataGridView1.Rows[fila].Cells[6].Value.ToString();
             
        }

        //Acceso Invitado
        private void button2_Click(object sender, EventArgs e)
        {
            
            permiso = 0;
            Form1 formu1 = new Form1(permiso, "Invitado", "","");
            this.Hide();
            formu1.ShowDialog();
            this.Close();
        }

        //Acceso Usuario/Gerente
        public void acceso()
        {
            nom = textBox1.Text;
            pass = textBox2.Text;
            try
            {
                if (nom != "" && pass != "")
                {
                    sql = "SELECT * FROM \"Empleados\" WHERE \"Usuario\" = '" + nom + "'";

                    Consultar();
                    dataGridView1.DataSource = DataTabla;
                    Consultas();

                    if (pass == password)
                    {

                        if ((puesto == "Gerente") || (puesto == "Encargado Sucursal"))
                        {
                            permiso = 2;
                            Form1 formu1 = new Form1(permiso, nombre, password, id);
                            MessageBox.Show("Acceso Concedido");
                            this.Hide();
                            formu1.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            permiso = 1;
                            Form1 formu1 = new Form1(permiso, nombre, password,id);
                            MessageBox.Show("Acceso Concedido");
                            this.Hide();
                            formu1.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        intento++;
                        MessageBox.Show("Contraseña Incorrecta \n    Intento " + intento + "/3");
                        // textBox1.Text = "";
                        textBox2.Text = "";
                        password = "";
                        nombre = "";
                    }

                    if (intento >= 3)
                    {
                        MessageBox.Show("ERROR: Ha Excedido el Limite de Intentos Permitidos");
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        button1.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: No debe quedar ningún campo vacío");
                }
            }
            catch (Exception ex)
            {
                if (id == null)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    password = "";
                    nombre = "";
                    intento++;
                    MessageBox.Show("Usuario no encontrado \n      Intento " + intento + "/3");
                }
                else
                {
                    MessageBox.Show("Error de Login:\n\n" + ex);
                }

                if (intento >= 3)
                {
                    MessageBox.Show("ERROR: Ha Alcanzado el Limite Máximo de Intentos Permitidos");
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    button1.Enabled = false;
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            acceso();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            pictureBox1.Visible = false;
            conectar();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                acceso();
                //  MessageBox.Show("El enter Si chacha");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}
