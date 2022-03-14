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
    public partial class Form10 : Form
    {
        public Form10(String idp,String pass,String user)
        {
            InitializeComponent();
            this.id = idp;
            this.pass = pass;
            this.usuar = user;
        }

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador, Adaptador1;
        static DataTable Fuente, Fuente1;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();
        

        String id,sql,id1,usuario,contraseña,puesto,sueldo,nombre,direccion,info,pass,usuar;
       

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
           // Fuente = DataSet.Tables[0];
        }
               
        public void llenagrid()
        {
            usuario = dataGridView1.Rows[0].Cells[0].Value.ToString();
            contraseña = dataGridView1.Rows[0].Cells[1].Value.ToString();
            puesto = dataGridView1.Rows[0].Cells[2].Value.ToString();
            sueldo = dataGridView1.Rows[0].Cells[3].Value.ToString();
            nombre = dataGridView1.Rows[0].Cells[4].Value.ToString();
            direccion = dataGridView1.Rows[0].Cells[5].Value.ToString();
            id1 = dataGridView1.Rows[0].Cells[6].Value.ToString();

            info=id1+" | "+usuario+" | "+puesto+" | "+sueldo+" | "+nombre+" | "+direccion;

            label1.Text=info;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            conectar();

            try
            {
                sql = "SELECT * FROM \"Empleados\" WHERE \"Id_empleado\" =" + id + "";
                Consultar();
                dataGridView1.DataSource = Fuente;
                llenagrid();

                if (usuario.Equals(usuar))
                {
                    button3.Enabled = false;
                    MessageBox.Show("No Puedes Eliminar tu propio perfil");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Mostrar Datos", "ERROR" + ex);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            info = "";
            con.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == pass)
            {

                try
                {
                    sql = "DELETE  FROM \"Empleados\" WHERE \"Id_empleado\" =" + id + "";
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
                MessageBox.Show("Contraseña Incorrecta","ERROR");
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            label1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox1.Text = "";
        }
    }
}
