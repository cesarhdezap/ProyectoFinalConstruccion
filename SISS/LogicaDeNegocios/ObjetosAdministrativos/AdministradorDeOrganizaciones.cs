using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;


namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeOrganizaciones
    {
        public List<Organizacion> Organizaciones { get; set; }

        public void CargarOrganizaciones()
        {
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarOrganizacionesTodas();
		}

        public void CargarOrganizacionesConNombre()
        {
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarIDYNombreDeOrganizaciones();
        }
    }
}
