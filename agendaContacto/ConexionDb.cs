using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agendaContacto;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace agendaContacto
{
    internal class ConexionDb
    {
         //creamos objeto para conectarnos con la bd
            private OleDbConnection conexion = new OleDbConnection();
            //para enviar las ordenes a la bd 
            private OleDbCommand comando = new OleDbCommand();
            //nos sirve para adaptar los datos que estan mal en la bd   
            private OleDbDataAdapter adaptador = new OleDbDataAdapter();
            private string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\\Users\\mauro\\source\\repos\\agendaContactos\\agendaContacto\\agendaContacto\\dbAgenda\\AgendaDb11.accdb";
            private string Tabla = "Contactos";


        //Conexion y Prueba de conexion 
            public void conexiones()
            {
                //recibe la cadena de conexion
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                // Asocia el comando SQL a la conexión y define el tipo de comando (SQL)
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;

            }
            public void ProbarConexion()
            {
                try
                {
                    // Configurar la cadena de conexión
                    conexion.ConnectionString = cadenaConexion;
                    conexion.Open();
                    MessageBox.Show("Conexión a la base de datos exitosa.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión si está abierta
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }

        //Armado del TreeView
            public void MostrarTree(TreeView treeView)
            {
                try
                {
                    conexiones(); // Llamamos al la funcion conexiones para conectarnos 

                    // Consulta para obtener los contactos
                    string query = "SELECT Nombre, Apellido, Telefono, Correo, Categoria FROM Contactos";
                    comando.CommandText = query;
                    OleDbDataReader reader = comando.ExecuteReader();


                    // Limpiar el TreeView antes de llenarlo
                    treeView.Nodes.Clear();

                    // Llenar el TreeView
                    while (reader.Read())
                    {
                        string categoria = reader["Categoria"].ToString();
                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();

                        TreeNode categoriaNode = treeView.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == categoria);

                        if (categoriaNode == null)
                        {
                            // Crear nuevo nodo de categoría si no existe
                            categoriaNode = new TreeNode(categoria);
                            treeView.Nodes.Add(categoriaNode);
                        }

                        // Agregar el contacto bajo su categoría
                        TreeNode contactoNode = new TreeNode($"{nombre} {apellido}");
                        categoriaNode.Nodes.Add(contactoNode);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar contactos: " + ex.Message);
                }
                finally
                {
                    if (conexion != null && conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }

        //Agrega nuevo contacto, envia informacion para la categorizacion del contacto
            public void Agregar(Contactos contacto, TreeView treeView)
            {
                try
                {
                    conexiones();

                    //Se edita lp que es la query 
                    string query = "INSERT INTO Contactos (Nombre, Apellido, Telefono, Correo, Categoria) VALUES (@Nombre, @Apellido, @Telefono, @Correo, @Categoria)";

                    comando.CommandText = query;
                    // Asignar valores a los parámetros
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", contacto.Apellido);
                    comando.Parameters.AddWithValue("@Telefono", contacto.Telefono);
                    comando.Parameters.AddWithValue("@Correo", contacto.Correo);
                    comando.Parameters.AddWithValue("@Categoria", contacto.Categoria);

                    // Ejecuta el comando (INSERT INTO) para insertar los datos en la base de datos
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Contacto agregado correctamente.");

                    AgregarContactoAlTreeView(contacto, treeView);

                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR EN BD " + e.ToString());
                }
                finally
                {
                    conexion.Close();
                }
            }

            //Agrega el contacto en la categoria determinada 
            //Toma dos parámetros: un objeto Contactos que contiene la información del contacto, y
            //un objeto TreeView donde se va a agregar el contacto.
            private void AgregarContactoAlTreeView(Contactos contacto, TreeView treeView)
            {
                TreeNode categoriaNode = null;

                foreach (TreeNode nodo in treeView.Nodes)
                {
                    if (nodo.Text == contacto.Categoria)
                    {
                        categoriaNode = nodo;
                        break; // Salimos del bucle si encontramos la categoría
                    }
                }

                if (categoriaNode != null)
                {
                    // Crear un nuevo nodo para el contacto
                    TreeNode contactoNode = new TreeNode($"{contacto.Nombre} {contacto.Apellido}");

                    // Agregar el nodo del contacto como hijo del nodo de categoría
                    categoriaNode.Nodes.Add(contactoNode);
                }
                else
                {
                    MessageBox.Show("La categoría no existe en el TreeView.");
                }
            }

     
            public void Eliminar(Contactos contacto)
            {
                try
                {
                    conexiones();
                    comando.CommandText = "DELETE FROM Contactos where Nombre = ?";

                    comando.Parameters.AddWithValue("?", contacto.Nombre);
                    // Ejecuta el comando (INSERT INTO) para insertar los datos en la base de datos
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Eliminado Correctamente.");

                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR EN BD " + e.ToString());
                }
                finally
                {
                    conexion.Close();
                }
            }


        //public void Modificar(Stock stock)
        //{
        //    try
        //    {
        //        conexiones();

        //        //Query para modificar 
        //        comando.CommandText = "UPDATE Productos SET Nombre = ?, Descripcion = ?, Precio = ?, Stock = ?, Categoria = ? WHERE Codigo = ?";

        //        //Se actualizan los valores de los campos
        //        comando.Parameters.AddWithValue("?", stock.Nombre);
        //        comando.Parameters.AddWithValue("?", stock.Descripcion);
        //        comando.Parameters.AddWithValue("?", stock.Precio);
        //        comando.Parameters.AddWithValue("?", stock.Cantidad);
        //        comando.Parameters.AddWithValue("?", stock.Categoria);
        //        comando.Parameters.AddWithValue("?", stock.Id); // Código del producto que se va a modificar

        //        // Ejecuta el comando (Update INTO) para insertar los datos en la base de datos
        //        comando.ExecuteNonQuery();

        //        MessageBox.Show("Modificado correctamente");

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("ERROR EN BD " + e.ToString());
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}


    }
    }

