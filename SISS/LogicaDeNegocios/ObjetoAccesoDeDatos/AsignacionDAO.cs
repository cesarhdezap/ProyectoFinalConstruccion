using System;
using System.Collections.Generic;
using System.Data;


namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AsignacionDAO : Interfaces.IAsignacionDAO
	{
		public void ActualizarAsignacionPorID(int IDasignacion, Asignacion asignacion)
		{
			//TODO
			throw new NotImplementedException();
		}

		public Asignacion CargarAsignacionPorID(int IDasignacion)
		{
			//TODO
			throw new NotImplementedException();
		}

		public List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula)
		{
			//TODO
			throw new NotImplementedException();
		}

		public DataTable ConvertirAsignacionADataTable(Asignacion asignacion)
		{
			//TODO
			throw new NotImplementedException();
		}

		public int GuardarAsignacion(Asignacion asignacion)
		{
			//TODO
			throw new NotImplementedException();
		}
	}
}
