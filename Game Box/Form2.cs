using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Npgsql;

namespace Game_Box
{
    public partial class Form2 : Form
    {     
       public Form2(DataTable d, String ide)
        {           
            InitializeComponent();
            this.data = d;
            this.idem = ide;            
        }

       //Iniciar Caracteristicas de la BD
       private string StrConexion;
       private NpgsqlConnection con;
       static NpgsqlDataAdapter Adaptador, Adaptador1, Adaptador2, Adaptador3;
       static DataTable Fuente;
       static DataTable Fuente1;
       static DataTable Fuente2;
       static DataTable Fuente3;
       static DataTable data1;
       private DataSet DataSet = new DataSet();
       private DataSet DataSet1 = new DataSet();
       private DataSet DataSet2 = new DataSet();
       private DataSet DataSet3 = new DataSet();
     


        //Variables
        
        String idpro,nombre,tipo,cantidad,precio,sql,sql1,sql2,sql3,id,nombreem,direccion,saldo,idc,desc,idem,existencia,preciof, existenf,fecha,aux,aux1,aux2;
        String idprod1, nombre1, tipo1, precio1,exis1;
        DataTable data;

        decimal total,prec=0;
        int cont,cont2=0,existent=0,tot=0,idd=0,idde=0,cant=0,cant1=0,contfil=0,contfil1=0,contfil2=0,contfil3=0;
        //DataGridView data;

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
           // Fuente1 = DataSet1.Tables[0];
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
         //   Fuente2 = DataSet2.Tables[0];
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
            //Fuente3 = DataSet3.Tables[0];
        }

        public void llenagrid()
        {
            nombreem = dataGridView2.Rows[0].Cells[0].Value.ToString();
            direccion = dataGridView2.Rows[0].Cells[1].Value.ToString();
            saldo = dataGridView2.Rows[0].Cells[2].Value.ToString();
            idc = dataGridView2.Rows[0].Cells[3].Value.ToString();

            label4.Text = nombreem;
        }
            
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;

            conectar();
            panel1.Visible = false;
                  
            dataGridView1.DataSource = data;
            //arreglarcant();
           

          
           // cont = dataGridView1.RowCount;
           // label4.Text = Convert.ToString(cont);
            
            for (int i = 0; i < cont - 1; i++)
            {
                precio = dataGridView1.Rows[i].Cells[4].Value.ToString();
                prec = Convert.ToDecimal(precio);

                total = prec + total;

                preciof = Convert.ToString(total);

                cont2 = cont2 + 1;
                label2.Text = Convert.ToString(total);
            } 

            //label2.Text = precio;
            
            dataGridView1.Columns[0].Width = 100;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Columns[5].Visible = false;
                                   
            //campos();
          
           


        }

        //eliminar duplicados y ajustar cantidad
        public void arreglarcant()
        {
            try
                
            {
                contfil = dataGridView3.RowCount;
                foreach (DataGridViewRow RowGrid in this.dataGridView3.Rows)
                {

                    if (RowGrid.Cells[0].Value.Equals(null)) { }
                    else
                    {
                        aux = RowGrid.Cells[0].Value.ToString();

                        label2.Text = aux;

                        foreach (DataGridViewRow RowGrid1 in this.dataGridView3.Rows)
                        {
                            if (RowGrid1.Cells[0].Value.Equals(null)) { }
                            else
                            {
                                label3.Text = aux1;
                                aux1 = RowGrid1.Cells[0].Value.ToString();
                                if (aux.Equals(aux1))
                                {
                                    cant = cant + 1;
                                }

                                contfil3 = contfil3 + 1;

                                if (contfil == contfil1)
                                {
                                    break;
                                }
                            }
                        }

                        foreach (DataGridViewRow RowGrid2 in this.dataGridView3.Rows)
                        {
                            if (RowGrid.Cells[0].Value.Equals(null)) { }
                            else
                            {
                                aux2 = RowGrid2.Cells[0].Value.ToString();

                                if (aux.Equals(aux2))
                                {
                                    cant1 = cant1 + 1;
                                    if (cant1 == 1)
                                    {
                                        idprod1 = RowGrid.Cells[0].Value.ToString();
                                        nombre1 = RowGrid.Cells[1].Value.ToString();
                                        tipo1 = RowGrid.Cells[2].Value.ToString();
                                        precio1 = RowGrid.Cells[4].Value.ToString();
                                        exis1 = RowGrid.Cells[5].Value.ToString();

                                        dataGridView1.Rows.Add(idprod1, nombre1, tipo1, cant, precio1, exis1);
                                    }

                                }

                                contfil2 = contfil2 + 1;

                                if (contfil == contfil2)
                                {
                                    break;
                                }
                            }

                        }

                        cant = 0;
                        cant1 = 0;
                        contfil1 = contfil+1;
                        
                        if (contfil == contfil1)
                        {
                            break;
                        }
                    }
                }
            }catch(Exception ex1){
                MessageBox.Show("Ha ocurrido un error en el proceso:\n\n" + ex1);

            }
        }

        public void abrir()
        {

            /*
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[0].Name = "Nombre del Producto";
            dataGridView1.Columns[1].Name = "Tipo";
            dataGridView1.Columns[2].Name = "Cantidad";
            dataGridView1.Columns[3].Name = "Precio";
            
            try{
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivo Lista (*.LIST)|*.LIST";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog1.FileName);
                while ((linea = sr.ReadLine()) != null)
                {
                    dato = linea.Split(';');
                    nombre = dato[0];
                    tipojuego = dato[1];
                    tamano = dato[2];
                    numdvd = dato[3];
                    dataGridView1.Rows.Add(nombre, tipojuego, tamano, numdvd);
                }
                    MessageBox.Show("Archivo Importado con Éxito");
                sr.Close();
                }else{
                        MessageBox.Show("Importación Cancelada");
                     }
            }catch{
                    MessageBox.Show("Error al Importar Archivo");
                   }
        */
      
        }

        public void cliente()
        {
            if (textBox1.Text=="")
            {
            }else{

                try
                {
                    sql = "SELECT * FROM \"Clientes\" WHERE \"Id_clientes\" ='" + textBox1.Text + "'";
                    Consultar();
                    dataGridView2.DataSource = Fuente;
                    llenagrid();
                    MessageBox.Show("Cliente encontrado");
                    panel1.Visible = false;

                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERROR" + ex, "Error al Mostrar Datos");
                }
            }
        }

        public void campos()
        {
            dataGridView1.Columns[0].Name = "Nombre del Producto";
            dataGridView1.Columns[1].Name = "Tipo";
            dataGridView1.Columns[2].Name = "Cantidad";
            dataGridView1.Columns[3].Name = "Precio";
        }

        public void venta()
        {
           cont= dataGridView1.RowCount;
            prec = 0;
            cont2 = 0;
            
            for (int j = 0; j < cont - 1; j++)
            {
                idpro = dataGridView1.Rows[j].Cells[0].Value.ToString();
                nombre = dataGridView1.Rows[j].Cells[1].Value.ToString();
                tipo = dataGridView1.Rows[j].Cells[2].Value.ToString();
                cantidad = dataGridView1.Rows[j].Cells[3].Value.ToString();
                precio = dataGridView1.Rows[j].Cells[4].Value.ToString();
                existencia = dataGridView1.Rows[j].Cells[5].Value.ToString();

                
                existent = Convert.ToInt32(existencia);

                
                if (cont2 == 0)
                {
                    desc = idpro;
                }

                desc += "," + idpro;
                cont2 = 1;

                existenf = Convert.ToString(existent - 1);

                try
                {
                    sql2 = "UPDATE \"Productos\" SET \"Existencias\" ='" +existenf+ "' WHERE \"Id_producto\" =" + idpro;
                    Consultar2();
                    con.Close();
                    conectar();
                }
                catch (Exception ep)
                {
                    MessageBox.Show("ERROR" + ep, "Error al Mostrar Datos");
                }

                
              }

            try
            {
                tot = Convert.ToInt32(total);
                idd = Convert.ToInt32(idc);
                idde = Convert.ToInt32(idem);
                label1.Text = existencia;
                label2.Text = preciof;
                label3.Text = idc;
                label4.Text = idem;
                fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                sql3 = "INSERT INTO \"Ventas\" (\"Descripcion\", \"Total_venta\", \"Id_empleado\", \"Id_cliente\", \"Fecha_venta\") VALUES ('" + desc + "','" + preciof + "','" + idem + "','" + idc + "','" + fecha + "')";
                
                Consultar3();
                con.Close();
                conectar();
                dataGridView1.DataSource = data1;
                label4.Text = "-";
                MessageBox.Show("Proceso concluido exitosamente");
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR" + e, "Error al Mostrar Datos");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            //abrir();            
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label4.Text.Equals("-"))
            {
                MessageBox.Show("Elige un cliente primero"); 
            }
            else
            {
                venta();
            }
            
            /* if (dataGridView1.SelectedRows.Count > 0 &&
           dataGridView1.SelectedRows[0].Index !=
              dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows.RemoveAt(
                    dataGridView1.SelectedRows[0].Index);
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
            /*SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivo de Texto(*.TXT)|*.TXT|Archivo Lista (*.LIST)|*.LIST";
            saveFileDialog1.Title = "Guardar Lista";
            saveFileDialog1.ShowDialog();
           try{

               if ((saveFileDialog1.FileName != "") && (saveFileDialog1.ShowDialog() == DialogResult.OK))
               {

                   ruta = saveFileDialog1.FileName;
                   extension = ruta.Substring(ruta.LastIndexOf('.') + 1).ToLower();
                   if (extension == "txt")
                   {
                       TextWriter sw = new StreamWriter(ruta);
                       int rowcount = dataGridView1.Rows.Count;
                       for (int i = 0; i < rowcount - 1; i++)
                       {
                           sw.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString() + "\t" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "\t" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\t" + dataGridView1.Rows[i].Cells[3].Value.ToString());
                       }
                       sw.Close(); //Don't Forget Close the TextWriter Object(sw)


                       MessageBox.Show("Lista Guardada con Éxito");
                   }

                   if (extension == "list")
                   {
                       TextWriter sw = new StreamWriter(ruta);
                       int rowcount = dataGridView1.Rows.Count;
                       for (int i = 0; i < rowcount - 1; i++)
                       {
                           sw.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[1].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[2].Value.ToString() + ";" + dataGridView1.Rows[i].Cells[3].Value.ToString());
                       }
                       sw.Close(); //Don't Forget Close the TextWriter Object(sw)
                       MessageBox.Show("Lista Guardada con Éxito");
                   }
               } MessageBox.Show("Exportación Cancelada");
     
           }catch{
                 MessageBox.Show("Error al Exportar Lista"); 
                 }

                */
                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cliente();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void b(object sender, KeyPressEventArgs e)
        {
            cliente();
        }
    }
}
