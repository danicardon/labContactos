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


        //Grabamos los datos
        //ABRIR, GRABAR Y CERRAR
        public void Grabar(string nombre, string apellido, int telefono,string correo, string categoria)
        {

            //Escribimos nombre del archivo y decir si existe o no 
            //Abrimos el archivo 
            StreamWriter AD = new StreamWriter(NombreArchivo , true);

            AD.Write(nombre);
            AD.Write(";"); //separador de campos para diferenciar nombre de apellido 
            AD.Write(apellido);
            AD.Write(";");
            AD.Write(telefono);
            AD.Write(";");
            AD.Write(correo);
            AD.Write(";");
             
            //WriteLine da el enter para ir al otro registro
            AD.WriteLine(categoria);


           

            AD.Close();
            //bORRAMOS PARA NO OCUPAR ESPACIO
            AD.Dispose();


        }

    }
}
