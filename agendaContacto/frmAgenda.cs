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
    public partial class frmAgenda : Form
    {
        private int currentContactoId;

        public frmAgenda()
        {
            InitializeComponent();
            
            ConexionDb objAgenda = new ConexionDb();

            // Agregar opciones al ComboBox
            cmbCategoria.Items.Add("Familia");
            cmbCategoria.Items.Add("Amigos");
            cmbCategoria.Items.Add("Trabajo");

            // (Opcional) Establecer una opción predeterminada
            cmbCategoria.SelectedIndex = -1; // Arranca Vacio



            treeViewContactos.Nodes.Add("Familia");
            treeViewContactos.Nodes.Add("Trabajo");
            treeViewContactos.Nodes.Add("Amigos");

            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);


        }
        //Guardamos datos para crear nuevos contactos
        private Contactos guardarDatos()
        {
            Contactos ContactoNuevo = new Contactos();

        
            ContactoNuevo.Nombre = txtNombre.Text;
            ContactoNuevo.Apellido = txtApellido.Text;
            ContactoNuevo.Telefono = int.Parse(txtTelefono.Text);
            ContactoNuevo.Correo = txtCorreo.Text;
            



            // Ver si hay una opción seleccionada en el CmbCategoria
            if (cmbCategoria.SelectedItem != null)
            {
                // Obtener el texto de la opción seleccionada y asignarlo a Categoria
                ContactoNuevo.Categoria = cmbCategoria.SelectedItem.ToString();
            }
            else
            {
                // Manejo de error si no se selecciona una categoría
                MessageBox.Show("Seleccionar una categoria.");
                return null; // O manejarlo como necesites
            }

            return ContactoNuevo;
 

        }
        
        
        //Verificamos si anda mediante el listado de la bd
        private void btnListar_Click(object sender, EventArgs e)
        {
            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);
        }
        private void NoSeRepita()
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contactos nuevoContacto = guardarDatos();
            if (nuevoContacto != null) // Asegúrate de que no sea nulo
            {
                ConexionDb contactoNuevo = new ConexionDb();
                contactoNuevo.Agregar(nuevoContacto, treeViewContactos); // Pasa el TreeView
                
            }

            Limpiar();
            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ConexionDb editContacto = new ConexionDb();

            editContacto.Modificar(guardarDatos());

            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar eliminarForm = new Eliminar(); // Usa el nombre correcto
            eliminarForm.Show(); // Muestra el formulario
                
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        //Actualizar 
        private void telefonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar buscarForm = new frmBuscar(); // Usa el nombre correcto
            buscarForm.Show(); // Muestra el formulario
        }

        private void seleccionar(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Ver si el nodo tiene un objeto Contactos en el Tag (creado en mostartree)
            if (e.Node.Tag is Contactos contactoSeleccionado)
            {
                // Mostrar los datos del contacto en los TextBox
                txtNombre.Text = contactoSeleccionado.Nombre;
                txtApellido.Text = contactoSeleccionado.Apellido;
                txtTelefono.Text = contactoSeleccionado.Telefono.ToString();
                txtCorreo.Text = contactoSeleccionado.Correo;

                //Para msotar el cmb Categoria
                cmbCategoria.SelectedItem = contactoSeleccionado.Categoria;

                currentContactoId = contactoSeleccionado.Id;
            }

            Limpiar();
           
        }

        private void Limpiar()
        {
            //txtApellido.Clear();
            //txtCorreo.Clear();
            //txtNombre.Clear();
            //txtTelefono.Clear();
            //cmbCategoria.SelectedItem = 0;
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {

        }
     
    }
}
