using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using agendaContacto;

namespace agendaContacto
{
    public partial class Eliminar : Form
    {
        public Eliminar()
        {
            InitializeComponent();
        }

        private Contactos eliminarDatos()
        {
            Contactos contactoNuevo = new Contactos();

            string nombre = txtEliminar.Text;

            contactoNuevo.Nombre = nombre;


            return contactoNuevo;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ConexionDb contacto = new ConexionDb();
            contacto.Eliminar(eliminarDatos());

            txtEliminar.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
      
    }
}
