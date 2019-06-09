using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;

namespace LogicaDeNegocios.ObjetosAdministrador
{
	public class AdministradorDeOrganizaciones
    {
        private List<Organizacion> Organizaciones;

        public bool CrearOrganizacion(Organizacion organizacion)
        {
            CargarOrganizaciones();
            bool resultadoDeCreacion = false;
            if (!Organizaciones.Exists(e => e.CorreoElectronico == organizacion.CorreoElectronico))
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

    }
}
