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
    public partial class Form9 : Form
    {
        public Form9(String idp, String usua)
        {
            InitializeComponent();
            this.id = idp;
            this.usuar = usua;
        }

       
        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static DataTable Fuente;
        private DataSet DataSet = new DataSet();

        //Declarar Variables
        String id, sql, id1, usuario,user, contraseña, pass, pass2, puesto, puest, sueldo, sueld, nombre, nom,direccion, direc,campoupdate,campovacio,campomod,usuar;
        int cambiocont=0, cambiobien=0;

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
            Adaptador = new NpgsqlDataAdapter(sql, con);
            DataSet.Reset();
            Fuente = new DataTable();
            Adaptador.Fill(DataSet);
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
          

            textBox1.Text = nombre;
            textBox3.Text = direccion;
            textBox5.Text = id1;
            textBox6.Text = usuario;
            textBox9.Text = "••••••";
            comboBox1.Text = puesto;
            textBox2.Text = sueldo;
          
        }

        public void actualizar()
        {
            nom = textBox1.Text;
            direc = textBox3.Text;            
            user = textBox6.Text;
            pass = textBox7.Text;//contraseña nueva
            pass2 = textBox9.Text;//contraseña anterior
            puest = comboBox1.Text;
            sueld = textBox2.Text;

            campoupdate = "";
            campovacio = "";


            if (nom.Equals(""))
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

            if (pass2.Equals(""))
            {                    
                campovacio += "Contraseña\n";
                cambiocont = 1;
            }

            if (puest.Equals(""))
            {
                campovacio += "Puesto\n";
            }

            if (sueld.Equals(""))
            {
                campovacio += "Sueldo\n";
            }

            if (campovacio != "")
            {
                MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n"+ campovacio, "Mensaje de Error:");
            }
            else
            {
                
                if (nombre != nom)
                {
                    campoupdate = "\"Nombre\" ='" + nom + "',";
                    campomod = "Nombre: "+nombre+ " por "+nom+"\n";
                }
                               
                if (direc != direccion)
                {
                    campoupdate += "\"Direccion\" ='" + direc + "',";
                    campomod += "Direccionn: " + direccion + " por " + direc + "\n";
                }

                if (user != usuario)
                {
                    campoupdate += "\"Usuario\" ='" + user + "',";
                    campomod += "Usuario: " + usuario + " por " + user + "\n";
                }

                if (cambiocont == 1)
                {
                    MessageBox.Show("Decidiste Cambiar la contraseña\nAsi que Ingresa la Contraseña Anterior para verificar \nsi eres el usuario del perfil");
                    if (pass2.Equals(contraseña))
                    {
                        if (pass.Equals(""))
                        {
                            MessageBox.Show("Decidiste Cambiar la contraseña\nAsi que Ingresa la Nueva Contraseña");
                        }
                        else
                        {
                            campoupdate += "\"Contraseña\" ='" + pass + "',";
                            campomod += "Contraseña \n";
                            cambiocont = 2;
                            //campomod += "Contraseña: " + contraseña + " por " + pass + "\n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("La contraseña anterior es Incorrecta");
                        textBox9.Text = "";
                    }
                }

                if (puesto != puest)
                {
                    campoupdate += "\"Puesto\" ='" + puest + "',";
                    campomod += "Puesto: " + puesto + " por " + puest + "\n";
                }

                if (sueldo != sueld)
                {
                    campoupdate += "\"Salario\" ='" + sueld + "',";
                    campomod += "Puesto: " + sueldo + " por " + sueld + "\n";
                }

                if ((campoupdate != "") && (cambiocont == 2 || cambiocont == 0))
                {
                    // MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n" + campoupdate);
                    DialogResult dialogResult = MessageBox.Show("Los Siguientes Campos van a ser Modificados:\n" + campomod + "\n¿Estas Seguro de Querer Continuar?\n", "Confirmacion", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.No)
                    {
                        textBox1.Text = nombre;
                        textBox3.Text = direccion;
                        textBox5.Text = id1;
                        textBox6.Text = usuario;
                        textBox7.Text = "••••••";
                        comboBox1.Text = puesto;
                        textBox9.Text = sueldo;
                        cambiocont = 0;

                        MessageBox.Show("No se Hizo Ninguna Modificacion");
                    }
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            campoupdate = campoupdate.Substring(0, campoupdate.Length - 1);
                            label10.Text = campoupdate;
                            sql = "UPDATE \"Empleados\" SET " + campoupdate + " WHERE \"Id_empleado\" =" + id1;
                            Consultar1();
                            con.Close();
                            conectar();
                            MessageBox.Show("Proceso Terminado con Éxito");
                            //  exito = 1;
                            /*   if (exito == 1)
                               {
                                   llenagrid();
                               }*/

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("El Proceso no Concluyo Adecuandamente\nRevise si Introdujo Correctamente los Datos\n" + ex, "ERROR");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("No se Hizo nada");
                }

            }
            
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            conectar();
            //pictureBox1.Tag = "Para Cambiar la Contraseña es\n necesario que ingrese la contraseña anterior\n Y colocar una nueva contraseña ";
            //toolTip1.Tag = "Para Cambiar la Contraseña es\n necesario que ingrese la contraseña anterior\n Y colocar una nueva contraseña\n CLICK ";
            toolTip1.SetToolTip(pictureBox1, "Para Cambiar la Contraseña es necesario\nque ingrese la contraseña anterior\ny colocar una nueva contraseña.");
            try
            {
                sql = "SELECT * FROM \"Empleados\" WHERE \"Id_empleado\" =" + id + "";
                Consultar();
                dataGridView1.DataSource = Fuente;
                llenagrid();
                if(usuar.Equals(usuario)){

                    textBox1.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    //textBox7.ReadOnly = true;
                    //textBox9.ReadOnly = true;
                    comboBox1.Enabled = false;
                    textBox2.ReadOnly = true;
                    MessageBox.Show("De tu perfil Solo puedes modificar tu contraseña\nSi requieres otra modificacion a tu perfil\nNotifica a otro Gerente o Encargado de Sucursal");
                 //   MessageBox.Show("No puedes Modificar tu propio usuario\nSi requieres una modificacion a tu perfil\nNotifica a otro Gerente o Encargado de Sucursal");
                  //  con.Close();
                  //  this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Mostrar Datos","ERROR" + ex);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox7.PasswordChar = '\0';
            }
            else
            {
                textBox7.PasswordChar = '•';
            }
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

            con.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          //  toolTip1.Active= true;
        }
    }
}
