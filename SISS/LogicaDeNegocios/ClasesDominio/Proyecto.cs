﻿using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;
using LogicaDeNegocios.ObjetosAdministrador;
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
			return Cupo - proyectoDAO.ContarAlumnosAsignadosAProyecto(this.IDProyecto);
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

        public bool Guardar()
        {
            bool resultadoDeCreacion = false;
            if (ValidarProyecto())
            {
                ProyectoDAO proyectoDAO = new ProyectoDAO();
                proyectoDAO.GuardarProyecto(this);
                resultadoDeCreacion = true;
            }
            return resultadoDeCreacion;
        }

		public Encargado CargarEncargado()
		{
			EncargadoDAO encargadoDAO = new EncargadoDAO();
			Encargado encargado = new Encargado();
			encargado = encargadoDAO.CargarIDPorIDProyecto(IDProyecto);
			encargado = encargadoDAO.CargarEncargadoPorID(encargado.IDEncargado);
			return encargado;
		}

		private bool ValidarProyecto ()
        {
            bool resultadoDeValidacion = false;
            if (ValidarCadena(Nombre)
                && Encargado.IDEncargado > 0
                && ValidarCadena(DescripcionGeneral)
                && ValidarCadena(ObjetivoGeneral)
                && Cupo > 0)
            {
                resultadoDeValidacion = true;
            }
            return resultadoDeValidacion;
        }

		public void DarDeBaja()
		{
			this.Estado = EstadoProyecto.Inactivo;
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
