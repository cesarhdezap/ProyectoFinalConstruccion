using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeOrganizaciones
    {
        private List<Organizacion> Organizaciones;

        public bool CrearOrganizacion(Organizacion organizacion)
        {
            CargarOrganizaciones();
            bool resultadoDeCreacion = false;
            resultadoDeCreacion = !Organizaciones.Exists(e => e.CorreoElectronico == organizacion.CorreoElectronico) && ValidarOrganizacion(organizacion);
            if (resultadoDeCreacion)
            {
                OrganizacionDAO organizacionDAO = new OrganizacionDAO();
                organizacionDAO.GuardarOrganizacion(organizacion);
                resultadoDeCreacion = true;
            }
            return resultadoDeCreacion;
        }

        public void CargarOrganizaciones()
        {
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarOrganizacionesTodas();
		}

        private bool ValidarOrganizacion(Organizacion organizacion)
        {
            bool resultadoDeValidacion = false;
            if (ValidarNombreDeOrganizacion(organizacion.Nombre)
                && ValidarDireccion(organizacion.Direccion)
                && ValidarTelefono(organizacion.Telefono)
                && ValidarCorreoElectronico(organizacion.CorreoElectronico))
            {
                resultadoDeValidacion = true;
            }
            return resultadoDeValidacion;
        }

    }
}
