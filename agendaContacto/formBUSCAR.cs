using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace agendaContacto
{
    public partial class formBUSCAR : Form
    {
        public formBUSCAR()
        {
            InitializeComponent();
      

            btnNombre.CheckedChanged += BuscarPor;
            btnTelefono.CheckedChanged += BuscarPor;
            btnCorreo.CheckedChanged += BuscarPor;  

        }
        private void BuscarPor(object sender, EventArgs e)
        {
            if (btnNombre.Checked)
            {txtNombre.Enabled = true;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;

            } else if (btnTelefono.Checked)
            {
                txtNombre.Enabled = false;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = false;

            }else if (btnCorreo.Checked)
             {
                 txtNombre.Enabled = false;
                 txtTelefono.Enabled = false;
                 txtCorreo.Enabled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Instancia para la conexion
            conexionDB conexion = new conexionDB();
            // DataTable para almacenar datos de la BD 
            DataTable resultadoBusqueda = new DataTable();

           

            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                string nombre = txtNombre.Text;
                resultadoBusqueda = conexion.BuscarPorNombre(nombre); // Busca por nombre
              
            }

            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && int.TryParse(txtTelefono.Text, out int telefono))
            {
                resultadoBusqueda = conexion.BuscarPorTelefono(telefono); // Busca por teléfono
            }


            if (!string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                string correo = txtCorreo.Text;
                resultadoBusqueda = conexion.BuscarPorCorreo(correo);
                
            }
                // Mostrar los resultados en el DataGridView
             dgvContacto.DataSource = resultadoBusqueda;
            
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

