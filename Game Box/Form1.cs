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
    public partial class Form1 : Form
    {
        public Form1(int permiso, String user, String pass, String ide)
        {
            InitializeComponent();
            this.permi = permiso;
            this.usuario = user;
            this.password = pass;
            this.idem = ide;            
        }      
      
        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador;
        static DataTable Fuente;
        private DataSet DataSet = new DataSet();
       // private DataSet miDataSet = new DataSet();

       //Inicializar Variables
       String nombre,nombre2,sql,idioma,genero,tipo,descripcion,numdvd,imagen,imagen1,tamano,id1,clave,consultatipo,usuario,password,preciolista,idem;
       
       int fila = 0, totalfilas = 0, tipoconsulta = 0,permi,cont;
      // float tamano1;
        // DataTable de ventas 
       DataTable dt = new DataTable();       

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
                pictureBox2.Visible = true;
                //MessageBox.Show("Inicio Correcto");
            }
            
            catch (Exception es)
            {
                MessageBox.Show("Error de Inicio \nLa Aplicacion se cerrará \n"+es.ToString());
                this.Close();
            }
       }

    
       //Conexion a la BD     
       public void Consultar( )
       {
           Adaptador = new NpgsqlDataAdapter(sql, con);
           DataSet.Reset();
           Fuente = new DataTable();
           Adaptador.Fill(DataSet);
           Fuente = DataSet.Tables[0];
       }
       //Llena Grid
       public void Consultas()
       {
           //Recoger Datos del Grid
           nombre2 = dataGridView1.Rows[fila].Cells[0].Value.ToString();
           imagen1 = dataGridView1.Rows[fila].Cells[1].Value.ToString();
           idioma = dataGridView1.Rows[fila].Cells[2].Value.ToString();
           genero = dataGridView1.Rows[fila].Cells[3].Value.ToString();
           tipo = dataGridView1.Rows[fila].Cells[4].Value.ToString();
           tamano = dataGridView1.Rows[fila].Cells[5].Value.ToString(); //Este es existencias
           descripcion = dataGridView1.Rows[fila].Cells[6].Value.ToString();
           numdvd = dataGridView1.Rows[fila].Cells[7].Value.ToString(); //Este es precio público     
           preciolista = dataGridView1.Rows[fila].Cells[8].Value.ToString();
           id1 = dataGridView1.Rows[fila].Cells[9].Value.ToString();

           //Asignar Datos a Variables
           label9.Text = (fila+1) + "/" + (totalfilas-1);
           textBox1.Text = id1;
           label1.Text = nombre2;
           textBox3.Text = idioma;
           textBox4.Text = genero;
           textBox5.Text = tipo;
           textBox6.Text = tamano;
           textBox7.Text = descripcion;
           textBox8.Text = numdvd;
           //textBox9.Text = preciolista;
           imagen = @"Covers\" + imagen1;
           //Bitmap picture = new Bitmap(imagen);
           //pictureBox1.Image= (Image) picture;
           pictureBox1.ImageLocation = imagen;
       }
       //Boton Avanzar
       public void boton_avanzar()
       {
           try
           {
               if ((textBox1.Text != "") || (textBox2.Text != ""))
               {
                   if (fila < (totalfilas - 2))
                   {
                       fila = fila + 1;

                   }
                   else
                   {
                       MessageBox.Show("Ultimo Registro");
                   }
                   Consultas();
               }
               else
               {
                   /*   try
                      {
                          OpenFileDialog abrir = new OpenFileDialog();

                          Bitmap bmImagen;
                          //Utilizamos un filtro para las extensiones de las imagenes que deseamos buscar
                           abrir.Filter = "jpeg (*.jpg,*.jpeg)|*.jpg;*.jpeg|gif (*.gif)|*.gif|bitmap   (*.bmp)|*.bmp";
                          //se realiza la llamada al examinador de windows           
                          if (abrir.ShowDialog() == DialogResult.OK)
                          {
                              //capturamos el nombre y extension del archivo en sNombre
                              string sNombre = abrir.FileName;
                              //Convertimos la Imagen en un Bitmap
                              bmImagen = new Bitmap(sNombre);
                              //cargamos la imagen en el picturebox
                              pictureBox1.Image = bmImagen;
                              label1.Text = sNombre;
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                      }*/
               }
           }
           catch
           {
               if (id1 == null)
               {
                   MessageBox.Show("Ingresa una Busqueda Primero");
               }
           }
       }
        
       private void button3_MouseClick(object sender, MouseEventArgs e)
       {
           boton_avanzar();     
       }  
       //Boton Regresar
       public void boton_regresar()
       {
           try
           {
               if ((textBox1.Text != "") || (textBox2.Text != ""))
               {
                   if (fila > 0)
                   {
                       fila = fila - 1;

                   }
                   else
                   {
                       MessageBox.Show("Primer Registro");
                       fila = 0;
                   }

                   Consultas();
               }
           }
           catch
           {
               if (id1 == null)
               {
                   MessageBox.Show("Ingresa una Busqueda Primero");
               }
           }
       }

       private void button2_Click(object sender, EventArgs e)
        {
         boton_regresar();
        }
       //Proceso de Consultar
       public void Consulta()
       {
          // id = textBox1.Text;
           nombre = textBox2.Text;
           fila = 0;
           totalfilas = 0;
          /* if (textBox1.Text != "")
           {
               idnum = Convert.ToInt32(textBox1.Text);
           }*/
           try
           {

               if ((nombre == ""))
               {
                   MessageBox.Show("Introduce un ID o un Valor para Comenzar");

               }
               else
               {
                 
                       if ((nombre != ""))//Consulta por Nombre
                       {
                         
                           sql = "SELECT * FROM \"Productos\" WHERE \"" + consultatipo + "\" LIKE '%" + textBox2.Text + "%' ORDER BY \""+consultatipo+"\"";
                           Consultar();
                           dataGridView1.DataSource = Fuente;
                           totalfilas = dataGridView1.RowCount;
                           Consultas();
                           MessageBox.Show(totalfilas - 1 + " Registros Encontrados");
                       }
                      
                   }
           }
           
           catch
           {

               if (nombre2 == null)
               {
                   MessageBox.Show("Busqueda Sin Resultados");
                   textBox1.Text = "";
                   textBox2.Text = "";
               }
               else
               {
                   MessageBox.Show("Error de Busqueda\nIntente Otra vez");
               }
           }
       }

       public void Consulta_idioma()
       {
           try
           {

              
            sql = "SELECT * FROM \"Productos\" WHERE \"Idioma\" LIKE '%" + textBox9.Text + "%'";
            Consultar();
            dataGridView1.DataSource = Fuente;
            totalfilas = dataGridView1.RowCount;
            Consultas();
            MessageBox.Show(totalfilas - 1 + " Registros Encontrados");
          }catch
               {

                   if (nombre2 == null)
                   {
                       MessageBox.Show("Busqueda Sin Resultados");
                       textBox1.Text = "";
                       textBox2.Text = "";
                   }else{
                           MessageBox.Show("Error de Busqueda\nIntente Otra vez");
                        }
                }
       }

       public void Consulta_genero()
       {
           try
           {


               sql = "SELECT * FROM \"Productos\" WHERE \"Genero\" LIKE '%" + textBox9.Text + "%'";
               Consultar();
               dataGridView1.DataSource = Fuente;
               totalfilas = dataGridView1.RowCount;
               Consultas();
               MessageBox.Show(totalfilas - 1 + " Registros Encontrados");
           }
           catch
           {

               if (nombre2 == null)
               {
                   MessageBox.Show("Busqueda Sin Resultados");
                   textBox1.Text = "";
                   textBox2.Text = "";
               }
               else
               {
                   MessageBox.Show("Error de Busqueda\nIntente Otra vez");
               }
           }
       }

       public void Consulta_tipo()
       {
           try
           {

               sql = "SELECT * FROM \"Productos\" WHERE \"Tipo\" = '" + textBox9.Text + "'";
               Consultar();
               dataGridView1.DataSource = Fuente;
               totalfilas = dataGridView1.RowCount;
               Consultas();
               MessageBox.Show(totalfilas - 1 + " Registros Encontrados");
           }
           catch
           {

               if (nombre2 == null)
               {
                   MessageBox.Show("Busqueda Sin Resultados");
                   textBox1.Text = "";
                   textBox2.Text = "";
               }
               else
               {
                   MessageBox.Show("Error de Busqueda\nIntente Otra vez");
               }
           }
       }

       public void Consulta_tamano()
       {
           try
           {
               sql = "SELECT * FROM \"Productos\" WHERE \"Existencias\"" + textBox9.Text + " ORDER BY Existencias ASC";
               Consultar();
               dataGridView1.DataSource = Fuente;
               totalfilas = dataGridView1.RowCount;
               Consultas();
               MessageBox.Show(totalfilas - 1 + " Registros Encontrados");
           }
           catch
           {

               if (nombre2 == null)
               {
                   MessageBox.Show("Busqueda Sin Resultados");
                   textBox1.Text = "";
                   textBox2.Text = "";
               }
               else
               {
                   MessageBox.Show("Error de Busqueda\nIntente Otra vez");
               }
           }
       }

       public void Consulta_tamano1()
       {
           
           nombre = textBox2.Text;
             try
             {

                 if ((nombre == ""))
                 {
                     MessageBox.Show("Introduce un ID o un Valor para Comenzar");

                 }
                 else
                 {

                     if ((nombre != ""))//Consulta por Tamaño
                     {

                         sql = "SELECT * FROM \"Productos\" WHERE \"Existencias\" " + textBox2.Text + "";
                         Consultar();
                         dataGridView1.DataSource = Fuente;
                         totalfilas = dataGridView1.RowCount;
                         Consultas();
                         MessageBox.Show(totalfilas - 1 + " Registros Encontrados");
                     }

                 }
             }

             catch
             {

                 if (nombre2 == null)
                 {
                     MessageBox.Show("Busqueda Sin Resultados");
                     textBox1.Text = "";
                     textBox2.Text = "";
                 }
                 else
                 {
                     MessageBox.Show("Error de Busqueda\nIntente Otra vez");
                 }
             }
       }

       public void boton_buscar()
       {
           if (tipoconsulta == 0)
           {
               consultatipo = "Nombre";
               Consulta();
           }

           if (tipoconsulta == 1)
           {
             // consultatipo = "TamanoGB";
               Consulta_tamano1();
           }
           
           if (tipoconsulta == 2)
           {
               consultatipo = "Descripcion";
               Consulta();
           }


       }
                   
       private void button1_Click(object sender, EventArgs e)
       {
           boton_buscar();
       }

       private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

       private void nombreToolStripMenuItem_Click(object sender, EventArgs e)
       {
           tipoconsulta = 0;
           label8.Text = "Nombre";
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
          
       }
                               
       private void descripcionToolStripMenuItem_Click(object sender, EventArgs e)
       {
           tipoconsulta = 2;
           label8.Text = "Descripcion";
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
       }
               
       private void textBox2_KeyPress(object sender, KeyPressEventArgs keyPress)
       {
           if (keyPress.KeyChar == (char)(Keys.Enter))
           {
               boton_buscar();
             //  MessageBox.Show("El enter Si chacha");
           }

          /*if (keyPress.KeyChar == (char)(Keys.Right))
           {
               boton_avanzar();
               MessageBox.Show("La Flecha Derecha Si chacha :D");
           }

           if (keyPress.KeyChar == (char)(Keys.Left))
           {
               boton_regresar();
               MessageBox.Show("La Flecha Izquierda Si chacha :D");
           }*/
       }

       private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
       {
           boton_regresar();
        // MessageBox.Show("La Flecha Izquierda Si chacha :D");
       }

       private void siguienteToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           boton_avanzar();
       //    MessageBox.Show("La Flecha Derecha Si chacha :D");
       }
                
       private void multiplesIdiomasToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Multi";
           Consulta_idioma();
       }

       private void soloEspañolToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Español";
           Consulta_idioma();
       }

       private void soloInglésToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Ingles";
           Consulta_idioma();
       }

       private void japonesToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Japones";
           Consulta_idioma();
       }

       private void accionToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Accion";
           Consulta_genero();
       }

       private void actionRPGToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Action RPG";
           Consulta_genero();
       }

       private void aventuraAccionToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Aventura/Accion";
           Consulta_genero();
       }

       private void beatEmUpToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Beat-Em-Up";
           Consulta_genero();
       }

       private void carrerasToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Carreras";
           Consulta_genero();
       }

       private void cartasToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Cartas";
           Consulta_genero();
       }

       private void deportesToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Deportes";
           Consulta_genero();
       }

       private void musicalToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Musical";
           Consulta_genero();
       }

       private void novelaGráficaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Novela Grafica";
           Consulta_genero();
       }

       private void peleaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Pelea";
           Consulta_genero();
       }

       private void puzzleToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Puzzle";
           Consulta_genero();
       }

       private void rPGClásicoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "RPG Clasico";
           Consulta_genero();
       }

       private void rPGMusicalToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "RPG Musical";
           Consulta_genero();
       }

       private void rPGTácticoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "RPG Tactico";
           Consulta_genero();
       }

       private void shooterToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Shooter";
           Consulta_genero();
       }
        
       private void simulacionToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Simulacion";
           Consulta_genero();
       }

       private void terrorAccionToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Terror/Accion";
           Consulta_genero();
       }

       private void variosToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Varios";
           Consulta_genero();
       }

       private void juegoPSPToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Juego PSP";
           Consulta_tipo();
       }

       private void juegoPSPPSXToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Juego PSP/PSX";
           Consulta_tipo();
       }

       private void juegoPSPPSNToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Juego PSP/PSN";
           Consulta_tipo();
       }

       private void miniToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Mini";
           Consulta_tipo();
       }

       private void emuladorGBAToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "Emulador PSP/GBA";
           Consulta_tipo();
       }

       private void uMDVideoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "UMD VIDEO";
           Consulta_tipo();
       }

       private void menorA100MBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "< 0.10";
           Consulta_tamano();
       }

       private void menorA250MBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "< 0.25";
           Consulta_tamano();
       }

       private void menorA500MBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "< 0.50";
           Consulta_tamano();
       }

       private void menorA750MBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "< 0.75";
           Consulta_tamano();
       }

       private void menorA1GBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "< 1.00";
           Consulta_tamano();
       }

       private void menorA125GBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = "< 1.25";
           Consulta_tamano();
       }

       private void mayorA126GBToolStripMenuItem_Click(object sender, EventArgs e)
       {
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           textBox9.Text = ">= 1.25";
           Consulta_tamano();
       }

       private void personalizadaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           tipoconsulta = 1;
           label8.Text = "Tamaño (GB)";
           textBox1.Text = "";
           textBox2.Text = "";
           fila = 0;
           totalfilas = 0;
           MessageBox.Show("Introduzca consultas con operadores lógicos.\nTome en Cuenta que los valores son en Fracciones Decimales de GigaBytes.\nEjemplo:\n \"= .75\" Para Busquedas de Valor Exacto\n \">.98\" Para Busquedas Simples\n\">.98 & TamanoGB <=1.50\" Para Busquedas de Rangos\n\">.98 ORDER BY TamanoGB ASC\" Para Ordenar los Resultados de Forma Ascendente\n\">.98 ORDER BY TamanoGB DESC\" Para Ordenar los Resultados de Forma Descendente\nOmita el Caracter '\"' (Comillas).");

       
       
       }

       private void accederALaBaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
       {
           panel4.Visible = true;
           textBox10.Focus();
       }

       private void label12_Click(object sender, EventArgs e)
       {
           panel4.Visible = false;
           textBox10.Text = "";
       }
       //Acceso al Modo Gerente
       public void accesogerente()
       {
           clave = textBox10.Text;
           try
           {
               if (clave == password)
               {
                   MessageBox.Show("Acceso Concedido");
                   panel4.Visible = false;
                   // System.Diagnostics.Process.Start(@"MediaContent.accdb");
                   textBox10.Text = "";
                   Form5 formu5 = new Form5(usuario,password);
                   formu5.Show();

                  

               }
               else
               {
                   if (textBox10.Text == "")
                       MessageBox.Show("Ingresa La Contraseña");

                   if (textBox10.Text != "")
                       MessageBox.Show("Contraseña Incorrecta");
               }
           }
           catch
           {
               MessageBox.Show("Ingresa La Contraseña");
           }
       }

       private void button4_Click(object sender, EventArgs e)
       {
           accesogerente();
       }

       public void funcion_ver() {

         /*  ayudaver = ayudaver + 1;
           if (ayudaver == 1) { ver = 1; }
           ver = ver + 1;

           if (ver == 1)
           {
              // dataGridView2.Visible = false;
               Form2 formu = new Form2(dt);
               formu.Close();
                ver = 1;

           }
           if (ver == 2)
           {
              // dataGridView2.Visible = true;*/
               Form2 formu = new Form2(dt,idem);
               formu.Show();
                //ver = 0;

           //}
       
       
       
       }

       public void anadir()
       {
           try
           {
               int coinci = 0;

               for (int i = 0; i < dt.Rows.Count;i++)
               {
                   if(dt.Rows[i][0].ToString()==id1){
                       dt.Rows[i][3] = (Convert.ToInt32(dt.Rows[i][3].ToString())) + 1;
                       coinci++;
                   }
               }

               if (coinci == 0)
               {
                   dt.Rows.Add(id1, nombre2, tipo, 1, numdvd, tamano);
               }
               
               toolTip6.Active = true;
               toolTip6.Show("Producto Añadido\nal Carrito", label1,185,25,3000);
             


                               
           }
           catch(Exception es)
           {
               MessageBox.Show("Hubo un Error con la Lista"+es);
           }
           
       }

       public void quitar()
       {
          /* Form2 formi = new Form2(dt);
           if (formi.dataGridView1.SelectedRows.Count > 0 &&
            formi.dataGridView1.SelectedRows[0].Index !=
               formi.dataGridView1.Rows.Count - 1)
           {
               formi.dataGridView1.Rows.RemoveAt(
                   formi.dataGridView1.SelectedRows[0].Index);
           }*/
       }

       private void button7_Click(object sender, EventArgs e)
       {
           funcion_ver();

       }

       private void button5_Click(object sender, EventArgs e)
       {
           anadir();
       }

       private void button6_Click(object sender, EventArgs e)
       {
           quitar();
       }

       private void añadirElementoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           anadir();
       }

       private void quitarElementoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           quitar();
       }

       private void verListaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           funcion_ver();
       }

       private void importarListaToolStripMenuItem_Click(object sender, EventArgs e)
       {
          // Form2 abrirformu = new Form2(dt);
         //  abrirformu.Show();
          // abrirformu.abrir();
       }

       private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           Form4 formu4 = new Form4();
           formu4.Show();
       }

       private void verLaAyudaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           Form3 ayuda = new Form3();
           ayuda.Show();
       }

       private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
       {

       }

       private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
       {

       }

       private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (e.KeyChar == (char)(Keys.Enter))
           {
               accesogerente();
               //  MessageBox.Show("El enter Si chacha");
           }
       }

       private void timer1_Tick(object sender, EventArgs e)
       {
           label13.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
       }
     
       private void Form1_Load(object sender, EventArgs e)
       {

           this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
           this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
           textBox2.Select();
           textBox2.Focus();
           /*textBox3.Enabled = false;
           textBox4.Enabled = false;
           textBox5.Enabled = false;
           textBox6.Enabled = false;
           textBox7.Enabled = false;*/
          // textBox8.Visible = false;
          // textBox9.Visible = false;
           //label10.Visible = false;
           label11.Text = ("Para Acceder al Panel de Administracion \nNecesita Autentificarse como Gerente");
           panel3.Visible = false;
           panel4.Visible = false;
           pictureBox2.Visible = false;
           groupBox2.Enabled = false;

           //tooltips
           toolTip1.SetToolTip(this.button1, "Buscar en la Base de Datos");
           toolTip1.SetToolTip(this.button2, "Resultado Anterior");
           toolTip1.SetToolTip(this.button3, "Resultado Siguiente");
           toolTip1.SetToolTip(this.button5, "Añadir Producto al Carrito");
           toolTip1.SetToolTip(this.button7, "Ver Carrito");
           toolTip6.IsBalloon = true;
          // toolTip6.SetToolTip(this.pictureBox1, "Producto Añadido");
           toolTip6.UseAnimation = true;
           toolTip6.UseFading = true;
           toolTip6.Active = false;

           //Hora
           timer1.Start();
           
           //Invitado
           if (permi == 0)
           {
               groupBox2.Enabled = false;
               this.Text = "Media Box: "+usuario;

           }

           //Usuario Registrado
           if (permi == 1)
           {
               groupBox2.Enabled = true;
               accederALaBaseDeDatosToolStripMenuItem.Visible = true;
               this.Text = "Media Box: "+usuario;
               
           }

           //Gerente
           if (permi == 2)
           {
               groupBox2.Enabled = true;
               accederALaBaseDeDatosToolStripMenuItem.Visible = true;
               accederALaBaseDeDatosToolStripMenuItem.Enabled = true;
               this.Text = "Media Box: "+usuario;
           }


           //Configuracion de Inicio del Grid 2
           /* dataGridView2.ColumnCount = 4;
            dataGridView2.Columns[0].Name = "Nombre Juego";
            dataGridView2.Columns[1].Name = "Tipo de Juego";
            dataGridView2.Columns[2].Name = "Tamaño (GB)";
            dataGridView2.Columns[3].Name = "N° DVD";*/
           dt.Columns.Add("Id del Producto");
           dt.Columns.Add("Nombre del Producto");
           dt.Columns.Add("Tipo");
           dt.Columns.Add("Cantidad");
           dt.Columns.Add("Precio");
           dt.Columns.Add("Existencias");
           conectar();
       }

      

       }

    }

