using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;

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
			return Cupo - proyectoDAO.ContarOcupanciaDeProyecto(this.IDProyecto);
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

        
		
	}
    public enum EstadoProyecto
    {
        Activo,
        Inactivo
    }
}
