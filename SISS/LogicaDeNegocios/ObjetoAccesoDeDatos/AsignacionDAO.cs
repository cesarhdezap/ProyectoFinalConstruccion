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

        public List<Asignacion> CargarIDsPorMatricula(string matricula)
        {
            //TODO
			throw new NotImplementedException();
        }

        public List<Asignacion> CargarIDsPorMatriculaDeAlumno(string matricula)
		{
			//TODO
			throw new NotImplementedException();
		}

		private DataTable ConvertirAsignacionADataTable(Asignacion asignación)
        {
            //TODO
			throw new NotImplementedException();
        }

        private Asignacion ConvertirDataTableAAsignacion(DataTable tablaAsignaciones)
        {
            
            //TODO
			throw new NotImplementedException();
        }

        private List<Asignacion> ConvertirDataTableAListaDeAsignaciones(DataTable tablaAsignaciones)
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
