using System;
using System.Collections.Generic;
using System.Data;


namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AsignacionDAO : Interfaces.IAsignacionDAO
	{
		public void ActualizarAsignacionPorID(int IDasignacion, Asignacion asignacion)
		{
			throw new NotImplementedException();
		}

		public Asignacion CargarAsignacionPorID(int IDasignacion)
		{
			throw new NotImplementedException();
		}

		public List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula)
		{
			return null;
		}

		public DataTable ConvertirAsignacionADataTable(Asignacion asignacion)
		{
			throw new NotImplementedException();
		}

		public int GuardarAsignacion(Asignacion asignacion)
		{
			throw new NotImplementedException();
		}
	}
}
