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
using System.Collections;
using agendaContacto;
using System.Diagnostics.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace agendaContacto
{
    public partial class formGENERAL : Form
    {
        private int currentContactoId;

        public formGENERAL()
        {
            InitializeComponent();

            conexionDB objAgenda = new conexionDB();

            cmbCategoria.Items.Add("Familia");
            cmbCategoria.Items.Add("Amigos");
            cmbCategoria.Items.Add("Trabajo");

            cmbCategoria.SelectedIndex = -1;

            treeViewContactos.Nodes.Add("Familia");
            treeViewContactos.Nodes.Add("Trabajo");
            treeViewContactos.Nodes.Add("Amigos");

            conexionDB db = new conexionDB();
            db.MostrarTree(treeViewContactos);
        }

        expContactos exportarContancto = new expContactos();

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string categoria = cmbCategoria.Text;
            int telefono = Convert.ToInt32(txtTelefono.Text);

            exportarContancto.Grabar(txtNombre.Text, txtApellido.Text, telefono, txtCorreo.Text, categoria);
            MessageBox.Show("Datos listos para exportar");

            Limpiar();
        }

        private Contactos guardarDatos()
        {
            Contactos ContactoNuevo = new Contactos();

            ContactoNuevo.Nombre = txtNombre.Text;
            ContactoNuevo.Apellido = txtApellido.Text;
            ContactoNuevo.Telefono = int.Parse(txtTelefono.Text);
            ContactoNuevo.Correo = txtCorreo.Text;

            if (cmbCategoria.SelectedItem != null)
            {
                ContactoNuevo.Categoria = cmbCategoria.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Seleccionar una categoria.");
                return null;
            }

            return ContactoNuevo;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            conexionDB db = new conexionDB();
            db.MostrarTree(treeViewContactos);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contactos nuevoContacto = guardarDatos();
            if (nuevoContacto != null)
            {
                conexionDB contactoNuevo = new conexionDB();
                contactoNuevo.Agregar(nuevoContacto, treeViewContactos);
            }

            Limpiar();
            conexionDB db = new conexionDB();
            db.MostrarTree(treeViewContactos);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            conexionDB editContacto = new conexionDB();

            editContacto.Modificar(guardarDatos());

            conexionDB db = new conexionDB();
            db.MostrarTree(treeViewContactos);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar eliminarForm = new Eliminar();
            eliminarForm.Show();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void telefonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionDB db = new conexionDB();
            db.MostrarTree(treeViewContactos);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            formBUSCAR buscarForm = new formBUSCAR();
            buscarForm.Show();
        }

        private void seleccionar(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Contactos contactoSeleccionado)
            {
                txtNombre.Text = contactoSeleccionado.Nombre;
                txtApellido.Text = contactoSeleccionado.Apellido;
                txtTelefono.Text = contactoSeleccionado.Telefono.ToString();
                txtCorreo.Text = contactoSeleccionado.Correo;

                cmbCategoria.SelectedItem = contactoSeleccionado.Categoria;

                currentContactoId = contactoSeleccionado.Id;
            }

            Limpiar();
        }

        private void Limpiar()
        {
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar eliminarForm = new Eliminar();
            eliminarForm.Show();
        }

        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            formBUSCAR buscarForm = new formBUSCAR();
            buscarForm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeViewContactos_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void controlCajaDetexto()
        {
            if (txtApellido.Text != "" && txtCorreo.Text != " " && txtNombre.Text != "" && txtTelefono.Text != " ")
            {
                btnExportar.Enabled = true;
            }
            else
            {
                btnExportar.Enabled = false;
            }
        }
    }
}
