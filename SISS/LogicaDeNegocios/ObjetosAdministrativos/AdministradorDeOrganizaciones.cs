using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;


namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeOrganizaciones
    {
        private List<Organizacion> Organizaciones;

        public bool ValidarExistencia(Organizacion organizacion)
        {
            CargarOrganizaciones();
            bool resultadoDeCreacion = false;
            resultadoDeCreacion = !Organizaciones.Exists(e => e.CorreoElectronico == organizacion.CorreoElectronico);
            return resultadoDeCreacion;
        }

        public void CargarOrganizaciones()
        {
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarOrganizacionesTodas();
		}

        
    }
}
