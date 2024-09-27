using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace agendaContacto
{
    internal class expContactos
    {

        public string NombreArchivo = "Contactos.csv"; 

        public void Grabar(string nombre, string apellido, int telefono,string correo, string categoria)
        {

            StreamWriter AD = new StreamWriter(NombreArchivo , true);

            AD.Write(nombre);
            AD.Write(";");
            AD.Write(apellido);
            AD.Write(";");
            AD.Write(telefono);
            AD.Write(";");
            AD.Write(correo);
            AD.Write(";");

            AD.WriteLine(categoria);

            AD.Close();
            AD.Dispose();


        }

    }
}
