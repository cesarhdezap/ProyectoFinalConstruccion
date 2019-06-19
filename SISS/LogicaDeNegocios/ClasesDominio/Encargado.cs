using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
	public class Encargado : Persona
	{
		public int IDEncargado { get; set; }
		public string Puesto { get; set; }
		public List<Proyecto> Proyectos { get; set; }
        public Organizacion Organizacion { get; set; }

		public void AñadirProyecto(Proyecto proyecto)
		{
            proyecto.Encargado = this;
            Proyectos.Add(proyecto);
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            proyectoDAO.GuardarProyecto(proyecto);
        }

        public void Guardar()
        {
			EncargadoDAO encargadoDAO = new EncargadoDAO();
			encargadoDAO.GuardarEncargado(this);
        }

        public bool Validar()
        {
            bool resultadoDeValidacion = false;
            if (ValidarNombre(Nombre)
                && ValidarCorreoElectronico(CorreoElectronico)
                && ValidarTelefono(Telefono)
                && ValidarCadena(Puesto))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

		public Organizacion CargarOrganizacion()
		{
			OrganizacionDAO organizacionDAO = new OrganizacionDAO();
			Organizacion organizacion = new Organizacion();
			organizacion = organizacionDAO.CargarIDPorIDEncargado(IDEncargado);
			organizacion = organizacionDAO.CargarOrganizacionPorID(organizacion.IDOrganizacion);
			return organizacion;
		}
	}
}
