using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
    public class Organizacion
	{
		public int IDOrganizacion { get; set; }
		public string CorreoElectronico { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Nombre { get; set; }
		public List<Encargado> Encargados { get; set; }

        public void AñadirEncargado(Encargado encargado)
		{
            encargado.Organizacion = this;
            Encargados.Add(encargado);
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            encargadoDAO.GuardarEncargado(encargado);
        }

		public void Guardar()
		{
			OrganizacionDAO organizacionDAO = new OrganizacionDAO();
			organizacionDAO.GuardarOrganizacion(this);
		}

        public bool Validar()
        {
            bool resultadoDeValidacion = false;
            if (ValidarCadena(Nombre)
                && ValidarCadena(Direccion)
                && ValidarTelefono(Telefono)
                && ValidarCorreoElectronico(CorreoElectronico))
            {
                resultadoDeValidacion = true;
            }
            return resultadoDeValidacion;
        }
    }
}

	
