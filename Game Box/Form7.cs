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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static DataTable Fuente;
        private DataSet DataSet = new DataSet();
        static NpgsqlDataAdapter Adaptador1;
        static DataTable Fuente1;
        private DataSet DataSet1 = new DataSet();
    

        //Declaracion de Variables
        String sql,sql1,nombre,direc,user,pass,pass1,puesto,sueldo,campovacio,usuario;
        int exito = 0;

        private void conectar()
        {
            StrConexion = @"Server=127.0.0.1;Port=5432;" + "User id=mediabox; password=mediabox; Database=MediaContent; Encoding=UTF8;";

            //Objeto conexion
            con = new NpgsqlConnection(StrConexion);

            //con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MediaContent.accdb";

            try
            {
                con.Open();
               // pictureBox1.Visible = true;
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
          //  Fuente = DataSet.Tables[0];

        }
        public void Consultar1()
        {
            Adaptador1 = new NpgsqlDataAdapter(sql1, con);
            DataSet1.Reset();
            Fuente1 = new DataTable();
            Adaptador1.Fill(DataSet1);
            Fuente1 = DataSet1.Tables[0];

        }

       
        public void insertar()
        {
            nombre = textBox1.Text;
            direc = textBox3.Text;            
            user = textBox5.Text;
            pass = textBox6.Text;
            pass1 = textBox7.Text;
            puesto = comboBox1.Text;
            sueldo = textBox9.Text;
            
            campovacio = "";


            if (nombre.Equals(""))
            {
                campovacio = "Nombre\n";
            }
            
            if (direc.Equals(""))
            {
                campovacio += "Direccion\n";
            }

            if (user.Equals(""))
            {
                campovacio += "Usuario\n";
            }

            if (pass.Equals(""))
            {
                campovacio += "Contraseña\n";
            }

            if (pass1.Equals(""))
            {
                campovacio += "Confirmacion de Contraseña\n";
            }

            if (sueldo.Equals(""))
            {
                campovacio += "Sueldo\n";
            }

            if (puesto.Equals(""))
            {
                campovacio += "Puesto\n";
            }

            if (campovacio != "")
            {
                MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n" + campovacio);
            }
            else
            {


                if (pass != pass1)
                {
                    MessageBox.Show("Error: La Contraseña no coincide con la\nConfirmacion de la Contraseña");
                }
                else
                {
                    sql1 = "SELECT * FROM \"Empleados\" WHERE \"Usuario\" ='" +user+ "'";
                    Consultar1();
                    dataGridView1.DataSource = Fuente1;

                    if((dataGridView1.RowCount - 1) >= 1){

                        MessageBox.Show("El nombre de usuario: "+user+" ya se encuentra en uso \n Por Favor Ingrese uno Diferente");
                        textBox5.Text = "";
                        user = "";
                        sql1 = "";

                    }
                    else{
                            try
                            {
                                sql = "INSERT INTO \"Empleados\" (\"Usuario\", \"Contraseña\", \"Puesto\", \"Salario\", \"Nombre\",\"Direccion\") VALUES ('"+user+"','"+pass+"','"+puesto+"','"+sueldo+"','"+nombre+"','"+direc+"')";
                                Consultar();
                                con.Close();
                                conectar();
                                MessageBox.Show("Proceso Terminado con Éxito");
                                exito = 1;
                                if(exito == 1)
                                {
                                    textBox1.Text = "";                            
                                    textBox3.Text = "";                            
                                    textBox5.Text = "";
                                    textBox6.Text = "";
                                    textBox7.Text = "";
                                    comboBox1.Text = "";
                                    textBox9.Text = "";
                                    sql1 = "";
                                }

                            }
                            catch (Exception ex){
                                MessageBox.Show("ERROR:\nEl Proceso no Concluyo Adecuandamente\nRevise si Introdujo Correctamente los Datos\n\n"+ex
                                    );
                            }
                    }
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";           
            textBox3.Text = "";            
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            textBox9.Text = "";
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;            
            conectar();
            sql=("SELECT * FROM \"Empleados\" ");
            Consultar();

        }
    }
}
