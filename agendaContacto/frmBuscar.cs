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
    public partial class frmBuscar : Form
    {
        public frmBuscar()
        {
            InitializeComponent();

            btnNombre.CheckedChanged += BuscarPor;
            btnTelefono.CheckedChanged += BuscarPor;
            btnCorreo.CheckedChanged += BuscarPor;

        }
        private void BuscarPor(object sender, EventArgs e)
        {
            if (btnNombre.Checked)
            {
                txtNombre.Enabled = true;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
           
            }else if (btnTelefono.Checked)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnNombre.Checked)
            {
                
            }
        }
    }
}

