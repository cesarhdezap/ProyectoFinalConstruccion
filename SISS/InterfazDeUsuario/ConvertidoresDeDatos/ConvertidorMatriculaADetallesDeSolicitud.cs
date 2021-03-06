﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario
{
	class ConvertidorMatriculaADetallesDeSolicitud : IValueConverter

	{
		public object Convert(object matricula, Type targetType, object parameter, CultureInfo culture)
		{
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			List<Proyecto> proyectosSolicitados = new List<Proyecto>();
			SolicitudDAO solicitudDAO = new SolicitudDAO();
			Solicitud solicitud = new Solicitud();
			string cadenaResultado = "Proyectos solicitados: " + System.Environment.NewLine;

			try
			{
				solicitud = solicitudDAO.CargarIDPorMatricula((string)matricula);

				if (solicitud != null)
				{
					proyectosSolicitados = proyectoDAO.CargarIDsPorIDSolicitud(solicitud.IDSolicitud);

					for (int i = 0; i < proyectosSolicitados.Count; i++)
					{
						proyectosSolicitados[i] = proyectoDAO.CargarProyectoPorID(proyectosSolicitados[i].IDProyecto);
					}

					foreach (Proyecto proyecto in proyectosSolicitados)
					{
						cadenaResultado = cadenaResultado + "- Nombre: " + proyecto.Nombre + System.Environment.NewLine
														  + "- Descripción general: " + proyecto.DescripcionGeneral + System.Environment.NewLine
														  + "- Cupo: " + proyecto.ObtenerDisponibilidad() + System.Environment.NewLine
														  + System.Environment.NewLine;
					}
				}
				else
				{
					cadenaResultado = "El alumno no solicito proyectos.";
				}
			}
			catch (AccesoADatosException e)
			{
				MostrarMessageBoxDeExcepcion(e);
			}
			return cadenaResultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
