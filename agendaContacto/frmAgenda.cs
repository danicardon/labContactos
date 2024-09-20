using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace agendaContacto
{
    public partial class frmAgenda : Form
    {
        public frmAgenda()
        {
            InitializeComponent();
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            // Crear una instancia de la clase Conexion
            ConexionDb db = new ConexionDb();

            // Intentar establecer la conexión
            try
            {
                // Llamar al método correcto que devuelve la conexión
                var dbConnection = db.e;
                dbConnection.Open();

                // Si la conexión se establece correctamente, muestra un mensaje
                MessageBox.Show("Conexión exitosa a la base de datos.", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cerrar la conexión después de la verificación
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                // Si ocurre un error, muestra un mensaje de error
                MessageBox.Show("Error al establecer la conexión: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
