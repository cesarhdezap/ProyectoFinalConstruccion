using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
	public class Proyecto
	{
		public int IDProyecto { get; set; }
		public string Nombre { get; set; }
        public EstadoProyecto Estado { get; set; }
		public string DescripcionGeneral { get; set; }
		public string ObjetivoGeneral { get; set; }
		public int Cupo { get; set; }
		public List<Asignacion> Asignaciones { get; set; }
        public Encargado Encargado { get; set; }

		public int ObtenerDisponibilidad()
		{
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			return Cupo - proyectoDAO.ContarAlumnosAsignadosAProyecto(IDProyecto);
		}

        public void AsignarAlumno(Alumno alumno, Solicitud solicitud = null)
        {
            Asignacion asignacion = new Asignacion
            {
                EstadoAsignacion = EstadoAsignacion.Activo,
                FechaDeInicio = DateTime.Now,
                HorasCubiertas = 0,
                Alumno = alumno,
                Proyecto = this,
                Solicitud = solicitud
            };

            AsignacionDAO asignacionDAO = new AsignacionDAO();
            asignacionDAO.GuardarAsignacion(asignacion);
        }

        public void Guardar()
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            proyectoDAO.GuardarProyecto(this);
        }

		public Encargado CargarEncargado()
		{
			EncargadoDAO encargadoDAO = new EncargadoDAO();
			Encargado encargado = new Encargado();
			encargado = encargadoDAO.CargarIDPorIDProyecto(IDProyecto);
			encargado = encargadoDAO.CargarEncargadoPorID(encargado.IDEncargado);
			return encargado;
		}

		public bool Validar()
        {
            bool resultadoDeValidacion = false;

            if (ValidarCadena(Nombre)
                && ValidarCadena(DescripcionGeneral)
                && ValidarCadena(ObjetivoGeneral)
                && Cupo > VALOR_ENTERO_MINIMO_PERMITIDO)
            {
                resultadoDeValidacion = true;
            }
            return resultadoDeValidacion;
        }

		public void DarDeBaja()
		{
            Estado = EstadoProyecto.Inactivo;
			ActualizarRegistroDeProyecto();
		}

		private void ActualizarRegistroDeProyecto()
		{
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			proyectoDAO.ActualizarProyectoPorID(IDProyecto, this);
		}
	}

    public enum EstadoProyecto
    {
        Activo,
        Inactivo
    }
}
