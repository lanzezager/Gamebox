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
    public partial class Form11 : Form
    {
        public Form11(int modo, String idpasa)
        {
            InitializeComponent();
            this.mode = modo;
            this.idpaso = idpasa;
        }



        //Iniciar Caracteristicas de la BD 
        private string StrConexion;
        private NpgsqlConnection con;
        static NpgsqlDataAdapter Adaptador,Adaptador1;
        static DataTable Fuente,Fuente1;
        private DataSet DataSet = new DataSet();
        private DataSet DataSet1 = new DataSet();

        //Declaracion de Variables
        String sql,id,nombre,imagen,imagen1,imagen2,caratula,idioma,genero,tipo,preciolista,preciopublico,existencias,descripcion,id1, ruta, destino,imgext,campovacio,extension, idpaso;
        String nom, pic, pic1, pic2, idiom, gen, tip, exis, desc, preciop, preciol, idprod, campoupdate, campomod;
        int idcont=0,imagecont=0, mode, cargaimagen=0;
        bool canConvert = true;
        decimal resultado = 0;

        //Conexion a la BD
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
             
        public void Consultar()
        {
            Adaptador = new NpgsqlDataAdapter(sql, con);
            DataSet.Reset();
            Fuente = new DataTable();
            Adaptador.Fill(DataSet);
            //Fuente = DataSet.Tables[0];
        }

        public void Consultar1()
        {
            Adaptador1 = new NpgsqlDataAdapter(sql, con);
            DataSet1.Reset();
            Fuente1 = new DataTable();
            Adaptador1.Fill(DataSet1);
            Fuente1 = DataSet1.Tables[0];
        }

        public void llenagrid()
        {
            //Recoger Datos del Grid
          
                nombre = dataGridView1.Rows[0].Cells[0].Value.ToString();
                imagen1 = dataGridView1.Rows[0].Cells[1].Value.ToString();
                idioma = dataGridView1.Rows[0].Cells[2].Value.ToString();
                genero = dataGridView1.Rows[0].Cells[3].Value.ToString();
                tipo = dataGridView1.Rows[0].Cells[4].Value.ToString();
                existencias= dataGridView1.Rows[0].Cells[5].Value.ToString();
                descripcion = dataGridView1.Rows[0].Cells[6].Value.ToString();
                preciopublico = dataGridView1.Rows[0].Cells[7].Value.ToString();                
                preciolista = dataGridView1.Rows[0].Cells[8].Value.ToString();
                id1 = dataGridView1.Rows[0].Cells[9].Value.ToString();
                                      
        }

        public void llenagrid1()
        {
            nom = dataGridView1.Rows[0].Cells[0].Value.ToString();
            pic = dataGridView1.Rows[0].Cells[1].Value.ToString();
            idiom = dataGridView1.Rows[0].Cells[2].Value.ToString();
            gen = dataGridView1.Rows[0].Cells[3].Value.ToString();
            tip = dataGridView1.Rows[0].Cells[4].Value.ToString();
            exis = dataGridView1.Rows[0].Cells[5].Value.ToString();
            desc = dataGridView1.Rows[0].Cells[6].Value.ToString();
            preciop = dataGridView1.Rows[0].Cells[7].Value.ToString();
            preciol = dataGridView1.Rows[0].Cells[8].Value.ToString();
            idprod = dataGridView1.Rows[0].Cells[9].Value.ToString();

            textBox1.Text = nom;
            textBox3.Text = exis;
            textBox4.Text = desc;
            textBox5.Text = preciop;
            textBox6.Text = preciol;            
            comboBox3.Text = tip;
            label14.Text = idprod;
            imagen2 = @"Covers\" + pic;
            pictureBox1.ImageLocation = imagen2;
            extension = pic.Substring(pic.LastIndexOf("."));
            pic1 = pic.Substring(0, pic.Length - 4);
            textBox2.Text = pic1;

            switch (gen)
            {
                case "Action RPG": gen = "Accion RPG";
                    break;

                case "Beat-Em-Up": gen = "Beat-'Em-Up";
                    break;

                case "Varios": gen = "Otros";
                    break;
                default:
                    break;
            }

           /* if (gen == "Action RPG")
            {
                gen = "Accion RPG";
            }
            else
            {

                if (gen.Equals("Beat-Em-Up"))
                {
                    gen = "Beat-'Em-Up";
                }
                else
                {

                    if (genero.Equals("Varios"))
                    {
                        gen = "Otros";
                    }
                }
            }*/

            if (idiom.Equals("Multi"))
            {
                idiom = "Multi-idioma";
            }

            comboBox1.Text = idiom;
            comboBox2.Text = gen;   

        }

        public void Insertar()
        {
            nombre = textBox1.Text;
            caratula = textBox2.Text;
            idioma = comboBox1.Text;
            genero = comboBox2.Text;
            tipo = comboBox3.Text;
            existencias = textBox3.Text;
            descripcion = textBox4.Text;
            preciopublico = textBox5.Text;
            preciolista = textBox6.Text;

            campovacio = "";
            imagecont = 0;
            idcont = caratula.Length;
            imgext = "";

            if (nombre.Equals(""))
            {
                campovacio = "Nombre del Producto\n";
            }

            if (caratula.Equals(""))
            {
                campovacio += "Carátula\n";
            }

            if (idioma.Equals(""))
            {
                campovacio += "Idioma\n";
            }

            if (genero.Equals(""))
            {
                campovacio += "Género\n";
            }

            if (tipo.Equals(""))
            {
                campovacio += "Tipo\n";
            }

            if (existencias.Equals(""))
            {
                campovacio += "Existencias\n";
            }
            else
            {
                //verificar existencias
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(existencias, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El valor de las Existencias que ingreso no es válido");
                    textBox3.Text = "";
                    existencias = "";
                    campovacio += "Existencias\n";
                }

            }

            if (descripcion.Equals(""))
            {
                campovacio += "Descripción\n";
            }

            if (preciopublico.Equals(""))
            {
                campovacio += "Precio al Público\n";
            }
            else
            {
                //verificar precio publico
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(preciopublico, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El Precio al público que ingreso no es válido");
                    textBox5.Text = "";
                    preciopublico = "";
                    campovacio += "Precio al Público\n";
                }
            }

            if (preciolista.Equals(""))
            {
                campovacio += "Precio de Lista\n";
            }
            else
            {
                //verificar precio publico
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(preciolista, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El Precio de lista que ingreso no es válido");
                    textBox6.Text = "";
                    preciolista = "";
                    campovacio += "Precio de Lista\n";
                }
            }

            if (campovacio != "")
            {
                MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n" + campovacio);
            }
            else
            {
                if (idcont > 25)
                {
                    MessageBox.Show("El nombre de la Carátula no debe Exceder los 25 Carateres\npor favor recortelo o cambielo por otro");
                    idcont = 0;

                }
                else
                {
                    imgext = caratula;
                    imgext += extension;
                    sql = "SELECT * FROM \"Productos\" WHERE \"Caratula\" ='" + imgext + "'";
                    Consultar1();
                    dataGridView1.DataSource = Fuente1;
                    if (Fuente1.IsInitialized == false)
                    {
                        llenagrid();
                    }
                    imagecont = dataGridView1.RowCount;

                    if (imagecont > 1)
                    {
                        MessageBox.Show("El nombre de la carátula ya se encuentra en uso\npor favor elija uno distinto");
                    }
                    else
                    {
                        imagecont = 0;
                        sql = "SELECT * FROM \"Productos\" WHERE \"Nombre\" ='" + nombre + "'";
                        Consultar1();
                        dataGridView1.DataSource = Fuente1;
                        if (Fuente1.IsInitialized == false)
                        {
                            llenagrid();
                        }
                        imagecont = dataGridView1.RowCount;

                        if (imagecont > 1)
                        {
                            MessageBox.Show("El Nombre del Producto ya se encuentra en uso\npor favor elija uno distinto");
                        }
                        else
                        {
                            if (idioma.Equals("Multi-idioma"))
                            {
                                idioma = "Multi";
                            }

                            if (genero.Equals("Accion RPG"))
                            {
                                genero = "Action RPG";
                            }

                            if (genero.Equals("Beat-'Em-Up"))
                            {
                                genero = "Beat-Em-Up";
                            }

                            if (genero.Equals("Otros"))
                            {
                                genero = "Varios";
                            }



                            //Correccion Imagen
                            caratula += extension;

                            try
                            {
                                //Crear Nuevo Registro
                                sql = "INSERT INTO \"Productos\" (\"Nombre\", \"Caratula\", \"Idioma\", \"Genero\", \"Tipo\",\"Existencias\",\"Descripcion\",\"Precio_publico\",\"Precio_lista\") VALUES ('" + nombre + "','" + caratula + "','" + idioma + "','" + genero + "','" + tipo + "','" + existencias + "','" + descripcion + "','" + preciopublico + "','" + preciolista + "')";
                                Consultar();
                                con.Close();
                                conectar();
                                //Copiar Imagen
                                ruta = @"Covers";

                                if (!System.IO.Directory.Exists(ruta))
                                {
                                    System.IO.Directory.CreateDirectory(ruta);
                                }

                                destino = System.IO.Path.Combine(ruta, caratula);
                                System.IO.File.Copy(imagen, destino, true);
                                MessageBox.Show("Proceso Terminado con Éxito");

                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox6.Text = "";
                                comboBox1.Text = "";
                                comboBox2.Text = "";
                                comboBox3.Text = "";
                                pictureBox1.ImageLocation = "";

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ha ocurrido un error en el proceso:\n\n" + ex);
                            }
                        }

                    }

                }

            }
        }

        public void modificar()
        {
            nombre = textBox1.Text;
            caratula = textBox2.Text;
            idioma = comboBox1.Text;
            genero = comboBox2.Text;
            tipo = comboBox3.Text;
            existencias = textBox3.Text;
            descripcion = textBox4.Text;
            preciopublico = textBox5.Text;
            preciolista = textBox6.Text;

            campovacio = "";
            imagecont = 0;
            idcont = caratula.Length;
            imgext = "";

            if (nombre.Equals(""))
            {
                campovacio = "Nombre del Producto\n";
            }

            if (caratula.Equals(""))
            {
                campovacio += "Carátula\n";
            }

            if (idioma.Equals(""))
            {
                campovacio += "Idioma\n";
            }

            if (genero.Equals(""))
            {
                campovacio += "Género\n";
            }

            if (tipo.Equals(""))
            {
                campovacio += "Tipo\n";
            }

            if (existencias.Equals(""))
            {
                campovacio += "Existencias\n";
            }
            else
            {
                //verificar existencias
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(existencias, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El valor de las Existencias que ingreso no es válido");
                    textBox3.Text = "";
                    existencias = "";
                    campovacio += "Existencias\n";
                }

            }

            if (descripcion.Equals(""))
            {
                campovacio += "Descripción\n";
            }

            if (preciopublico.Equals(""))
            {
                campovacio += "Precio al Público\n";
            }
            else
            {
                //verificar precio publico
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(preciopublico, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El Precio al público que ingreso no es válido");
                    textBox5.Text = "";
                    preciopublico = "";
                    campovacio += "Precio al Público\n";
                }
            }

            if (preciolista.Equals(""))
            {
                campovacio += "Precio de Lista\n";
            }
            else
            {
                //verificar precio publico
                canConvert = true;
                resultado = 0;
                canConvert = decimal.TryParse(preciolista, out resultado);
                if (canConvert == false)
                {
                    MessageBox.Show("El Precio de lista que ingreso no es válido");
                    textBox6.Text = "";
                    preciolista = "";
                    campovacio += "Precio de Lista\n";
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
                    campoupdate = "\"Nombre\" ='" + nombre + "',";
                    campomod = "Nombre del Producto: " + nom + " por " + nombre + "\n";
                }

                if (caratula != pic1)
                {
                    campomod += "Carátula: " + pic1 + " por " + caratula + "\n";                                      
                    //Correccion Imagen
                    caratula += extension;
                    campoupdate += "\"Caratula\" ='" + caratula + "',";
                }

                if (idioma != idiom)
                {
                    campoupdate += "\"Idioma\" ='" + idioma + "',";
                    campomod += "Idioma: " + idiom + " por " + idioma + "\n";
                }

                if (genero != gen)
                {
                    campoupdate += "\"Genero\" ='" + genero + "',";
                    campomod += "Genero: " + gen + " por " + genero + "\n";
                }

                if (tipo != tip)
                {
                    campoupdate += "\"Tipo\" ='" + tipo + "',";
                    campomod += "Tipo: " + tip + " por " + tipo + "\n";
                }

                if (existencias != exis)
                {
                    campoupdate += "\"Existencias\" ='" + existencias + "',";
                    campomod += "Existencias: " + exis + " por " + existencias + "\n";
                }

                if (descripcion != desc)
                {
                    campoupdate += "\"Descripcion\" ='" + descripcion + "',";
                    campomod += "Descripcion: " + desc + " por " + descripcion + "\n";
                }

                if (preciopublico != preciop)
                {
                    campoupdate += "\"Precio_publico\" ='" + preciopublico + "',";
                    campomod += "Precio al público: " + preciop + " por " + preciopublico + "\n";
                }

                if (preciolista != preciol)
                {
                    campoupdate += "\"Precio_lista\" ='" + preciolista + "',";
                    campomod += "Precio de lista: " + preciol + " por " + preciolista + "\n";
                }

                if ((campoupdate != "") && (campoupdate != null))
                {
                    // MessageBox.Show("Ningún Campo Puede quedar Vacio. \n\nLos Siguientes Campos están Vacíos:\n\n" + campoupdate);

                    if (idcont > 25)
                    {
                        MessageBox.Show("El nombre de la Carátula no debe Exceder los 25 Carateres\npor favor recortelo o cambielo por otro");
                        idcont = 0;

                    }
                    else
                    {
                        imgext = caratula;
                        //imgext += extension;
                        sql = "SELECT * FROM \"Productos\" WHERE \"Caratula\" ='" + imgext + "'";
                        Consultar1();
                        dataGridView1.DataSource = Fuente1;
                        if (Fuente1.IsInitialized == false)
                        {
                            llenagrid();
                        }

                        if (caratula != pic1)
                        {
                            imagecont = dataGridView1.RowCount;
                        }
                        else
                        {
                            imagecont = 1;
                        }

                        if (imagecont > 1)
                        {
                            MessageBox.Show("El nombre de la carátula ya se encuentra en uso\npor favor elija uno distinto");
                        }
                        else
                        {
                            imagecont = 0;
                            sql = "SELECT * FROM \"Productos\" WHERE \"Nombre\" ='" + nombre + "'";
                            Consultar1();
                            dataGridView1.DataSource = Fuente1;
                            if (Fuente1.IsInitialized == false)
                            {
                                llenagrid();
                            }
                            if (nombre != nom)
                            {
                                imagecont = dataGridView1.RowCount;
                            }
                            else
                            {
                                imagecont = 1;
                            }

                            if (imagecont > 1)
                            {
                                MessageBox.Show("El Nombre del Producto ya se encuentra en uso\npor favor elija uno distinto");
                            }
                            else
                            {
                                if (idioma.Equals("Multi-idioma"))
                                {
                                    idioma = "Multi";
                                }

                                if (genero.Equals("Accion RPG"))
                                {
                                    genero = "Action RPG";
                                }

                                if (genero.Equals("Beat-'Em-Up"))
                                {
                                    genero = "Beat-Em-Up";
                                }

                                if (genero.Equals("Otros"))
                                {
                                    genero = "Varios";
                                }


                                DialogResult dialogResult = MessageBox.Show("Los Siguientes Campos van a ser Modificados:\n\n" + campomod +"\n¿Estas Seguro de Querer Continuar?\n", "Confirmacion", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.No)
                                {
                                    /*   textBox1.Text = nombre;
                                       textBox3.Text = direccion;
                                       textBox5.Text = id1;
                                       textBox6.Text = usuario;
                                       textBox7.Text = "••••••";
                                       comboBox1.Text = puesto;
                                       textBox9.Text = sueldo;*/

                                    cargaimagen = 0;
                                    MessageBox.Show("No se Hizo Ninguna Modificacion");
                                }
                                if (dialogResult == DialogResult.Yes)
                                {
                                        try
                                        {

                                            //Modificar Registro Existente
                                            campoupdate = campoupdate.Substring(0, campoupdate.Length - 1);
                                            sql = "UPDATE \"Productos\" SET " + campoupdate + " WHERE \"Id_producto\" =" + idprod;
                                            Consultar();
                                            con.Close();
                                            conectar();

                                            if (cargaimagen == 1)
                                            {
                                                //Copiar Imagen
                                                ruta = @"Covers";

                                                if (!System.IO.Directory.Exists(ruta))
                                                {
                                                    System.IO.Directory.CreateDirectory(ruta);
                                                }

                                                destino = System.IO.Path.Combine(ruta, caratula);
                                                System.IO.File.Copy(imagen, destino, true);
                                                
                                                System.IO.File.Delete(imagen2);
                                            }
                                            MessageBox.Show("Proceso Terminado con Éxito");

                                            textBox1.Text = "";
                                            textBox2.Text = "";
                                            textBox3.Text = "";
                                            textBox4.Text = "";
                                            textBox5.Text = "";
                                            textBox6.Text = "";
                                            comboBox1.Text = "";
                                            comboBox2.Text = "";
                                            comboBox3.Text = "";
                                            pictureBox1.ImageLocation = "";
                                            cargaimagen = 0;

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Ha ocurrido un error en el proceso:\n\n" + ex);
                                        }
                                    

                                }

                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se cambio nada");
                }
            }
        }   

        private void Form11_Load (object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            conectar();
            label12.Visible = false;
            label14.Visible = false;
            button4.Visible = false;

            if (mode == 2)
            {
                label12.Visible = true;
                label14.Visible = true;
                this.Text = "Media Box: Administracion: Modificar Producto";
                sql = "SELECT * FROM \"Productos\" WHERE \"Id_producto\" ='" + idpaso + "'";
                Consultar1();
                dataGridView1.DataSource = Fuente1;
                llenagrid1();
            }

            if (mode == 3)
            {
                label12.Visible = true;
                label14.Visible = true;

                //deshabilitar botones
                button4.Visible = true;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;

                //deshabilitar textos
                textBox1.ReadOnly= true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;

                this.Text = "Media Box: Administracion: Detalle de Producto";
                sql = "SELECT * FROM \"Productos\" WHERE \"Id_producto\" ='" + idpaso + "'";
                Consultar1();
                dataGridView1.DataSource = Fuente1;
                llenagrid1();

            }

        }

        //Abrir Imagen
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Archivos de Imagen(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Mostrar Imagen
                    imagen = openFileDialog1.FileName;
                    pictureBox1.ImageLocation = imagen;
                    imagen1 = openFileDialog1.SafeFileName;

                    if (mode == 2)
                    {
                        pic2 = imagen1;
                        cargaimagen = 1;
                    }

                    //Habilitar Edicion
                    textBox2.ReadOnly = false;
                    textBox2.Text = imagen1.Substring(0, imagen1.Length -4);
                    extension = imagen1.Substring(imagen1.LastIndexOf("."));

                    //Obtener nombre Imagen y longitud del nombre
                    
                    idcont = imagen1.Length - 4;
                    //cont = idcont.ToString();
                  
                    /*
                  //Copiar Imagen
                    ruta = @"Covers";
                    
                    if (!System.IO.Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }

                    destino = System.IO.Path.Combine(ruta, imagen1);
                    System.IO.File.Copy(imagen,destino,true);
                     */
                }

            /*    do
                {
                    id = Convert.ToString(idcont);
                    label1.Text = id;
                    sql = "SELECT * FROM JuegosPSP WHERE id =" + id + "";
                    Consultar();
                    dataGridView1.DataSource = Fuente;
                    llenagrid();
                     
                    imagen = imagen1 + ".jpg";
                   
                    sql = "UPDATE JuegosPSP SET imagen= '"+imagen+"' WHERE id ="+id;
                    Consultar();
                    label2.Text = imagen;
                   // idcont = Convert.ToInt32(id);
                    idcont = idcont + 1;

                } while (nombre2!=null);
                con.Close();
                conectar();
                MessageBox.Show("Proceso Concluido Exitosamente");*/

            }
            catch (Exception ex) {
                MessageBox.Show("ERROR: \n" + ex, "Error al Procesar");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        //Boton Aceptar
        private void button2_Click(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                Insertar();
            }

            if (mode == 2)
            {
                modificar();
            }
         }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

          }
        }
    

