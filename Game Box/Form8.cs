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
    public partial class Form8 : Form
    {
        public Form8(String idpaso)
        {
            InitializeComponent();
            this.id = idpaso;
            
        }

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static NpgsqlDataAdapter Adaptador1;        
        static DataTable Fuente,Fuente1;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();
        

        //Declarar Variables
        String id,sql,id1,usuario,contraseña,puesto,sueldo,nombre,direccion,totalven,sql1;
        int totalventas = 0;
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
            Adaptador1 = new NpgsqlDataAdapter(sql1, con);
            DataSet1.Reset();
            Fuente1 = new DataTable();
            Adaptador1.Fill(DataSet1);
            Fuente1 = DataSet1.Tables[0];
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
            

            label2.Text = nombre;
            label6.Text = direccion;
            label10.Text = id1;
            label12.Text = usuario;
            label16.Text = puesto;
            label18.Text = sueldo;
            

        }                       

        private void Form8_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            conectar();
            try
            {
                //Consulta
                sql = "SELECT * FROM \"Empleados\" WHERE \"Id_empleado\" =" + id + "";
                Consultar();
                dataGridView1.DataSource = Fuente;
                llenagrid();

                //Checar Ventas totales
                sql1 = "SELECT \"Id_empleado\" FROM \"Ventas\" WHERE \"Id_empleado\"="+id+"";
                Consultar1();
                dataGridView2.DataSource = Fuente1;
                totalventas = dataGridView2.RowCount;
                totalventas = totalventas - 1;
                totalven = totalventas.ToString();
                label20.Text = totalven;

               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al Mostrar Datos"+ ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label14.Text = contraseña;
            }
            else
            {
                label14.Text = "••••••••";
            }
        }
    }
}
