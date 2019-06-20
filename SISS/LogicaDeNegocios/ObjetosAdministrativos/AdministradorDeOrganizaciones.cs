using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    /// <summary>
    /// Clase administradora de Organizaciones en una lista del objeto.
    /// Contiene métodos para cargar todas las Organizaciones y
    /// cargar Organizaciones con solo nombre.
    /// </summary>
	public class AdministradorDeOrganizaciones
    {
        public List<Organizacion> Organizaciones { get; set; }

        /// <summary>
        /// Carga todas las Organizaciones de la base de datos a la lista <see cref="Organizaciones"/>.
        /// </summary>
        public void CargarOrganizaciones()
        {
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarOrganizacionesTodas();
		}

        /// <summary>
        /// Carga todas las Organizaciones con solo <see cref="Organizacion.Nombre"/> en la lista
        /// del objeto <see cref="Organizaciones"/>.
        /// </summary>
        public void CargarOrganizacionesConNombre()
        {
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarIDYNombreDeOrganizaciones();
        }
    }
}
