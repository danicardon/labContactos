using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agendaContacto;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace agendaContacto
{
    internal class ConexionDb
    {
        class Conexion
        {
            private string CadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Alumno\source\repos\agendaContacto\agendaContacto\dbAgenda\DbAgenda1.accdb";

            // Crea un objeto para manejar la conexión a la base de datos.
            OleDbConnection conexionDb;

            // Inicializa el objeto de conexión (conexion) 
            // usando la cadena de conexión (CadenaConexion) y lo retorna.
            public OleDbConnection EstablecerConexion()
            {
                // Creamos una instancia de la conexión con la cadena de conexión correcta.
                this.conexionDb = new OleDbConnection(this.CadenaConexion);
                return this.conexionDb;
            }



        }
    }
}
