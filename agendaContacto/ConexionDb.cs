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
using System.Diagnostics.Contracts;

namespace agendaContacto
{
    internal class conexionDB
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();
        private string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\\Users\\dcardon\\source\\repos\\LabContactos\\agendaContacto\\Database\\contactosDB.accdb";
        private string Tabla = "Contactos";

        public void conexiones()
        {
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
        }

        public void ProbarConexion()
        {
            try
            {
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
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public void MostrarTree(TreeView treeView)
        {
            try
            {
                conexiones();
                string query = "SELECT Nombre, Apellido, Telefono, Correo, Categoria FROM Contactos";
                comando.CommandText = query;
                OleDbDataReader reader = comando.ExecuteReader();
                treeView.Nodes.Clear();
                while (reader.Read())
                {
                    string categoria = reader["Categoria"].ToString();
                    string nombre = reader["Nombre"].ToString();
                    string apellido = reader["Apellido"].ToString();
                    string correo = reader["Correo"].ToString();
                    string telefono = reader["Telefono"].ToString();
                    TreeNode categoriaNode = treeView.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == categoria);
                    if (categoriaNode == null)
                    {
                        categoriaNode = new TreeNode(categoria);
                        treeView.Nodes.Add(categoriaNode);
                    }
                    var contacto = new Contactos
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        Telefono = int.TryParse(telefono, out int telefonoInt) ? telefonoInt : 0,
                        Correo = correo,
                        Categoria = categoria,
                    };
                    TreeNode contactoNode = new TreeNode($"{nombre} {apellido}");
                    contactoNode.Tag = contacto;
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

        public void Agregar(Contactos contacto, TreeView treeView)
        {
            try
            {
                conexiones();
                string query = "INSERT INTO Contactos (Nombre, Apellido, Telefono, Correo, Categoria) VALUES (@Nombre, @Apellido, @Telefono, @Correo, @Categoria)";
                comando.CommandText = query;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                comando.Parameters.AddWithValue("@Apellido", contacto.Apellido);
                comando.Parameters.AddWithValue("@Telefono", contacto.Telefono);
                comando.Parameters.AddWithValue("@Correo", contacto.Correo);
                comando.Parameters.AddWithValue("@Categoria", contacto.Categoria);
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

        private void AgregarContactoAlTreeView(Contactos contacto, TreeView treeView)
        {
            TreeNode categoriaNode = null;
            foreach (TreeNode nodo in treeView.Nodes)
            {
                if (nodo.Text == contacto.Categoria)
                {
                    categoriaNode = nodo;
                    break;
                }
            }
            if (categoriaNode != null)
            {
                TreeNode contactoNode = new TreeNode($"{contacto.Nombre} {contacto.Apellido}");
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

        public DataTable BuscarPorNombre(string nombre)
        {
            conexiones();
            string query = "SELECT * FROM Contactos WHERE Nombre = @Nombre";
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Nombre", nombre);
            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataTable resultados = new DataTable();
            adaptador.Fill(resultados);
            conexion.Close();
            return resultados;
        }

        public DataTable BuscarPorCorreo(string correo)
        {
            conexiones();
            string query = "SELECT * FROM Contactos WHERE Correo = @Correo";
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Correo", correo);
            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataTable resultados = new DataTable();
            adaptador.Fill(resultados);
            conexion.Close();
            return resultados;
        }

        public DataTable BuscarPorTelefono(int telefono)
        {
            conexiones();
            string query = "SELECT * FROM Contactos WHERE Telefono = @Telefono";
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Telefono", telefono);
            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataTable resultados = new DataTable();
            adaptador.Fill(resultados);
            conexion.Close();
            return resultados;
        }

        public void Modificar(Contactos contactoNuevo)
        {
            try
            {
                conexiones();
                comando.CommandText = "UPDATE Contactos SET Nombre = ?, Apellido  = ?, Telefono = ?, Correo = ?, Categoria = ? WHERE Telefono = ?";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("?", contactoNuevo.Nombre);
                comando.Parameters.AddWithValue("?", contactoNuevo.Apellido);
                comando.Parameters.AddWithValue("?", contactoNuevo.Telefono);
                comando.Parameters.AddWithValue("?", contactoNuevo.Correo);
                comando.Parameters.AddWithValue("?", contactoNuevo.Categoria);
                comando.Parameters.AddWithValue("?", contactoNuevo.Telefono);
                comando.ExecuteNonQuery();
                MessageBox.Show("Modificado correctamente");
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
    }
}
