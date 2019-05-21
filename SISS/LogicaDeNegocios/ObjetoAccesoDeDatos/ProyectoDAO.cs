using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class ProyectoDAO : Interfaces.IProyectoDAO
	{
        public void ActualizarProyectoPorID(int IDproyecto, Proyecto proyecto)
		{
			//TODO
			throw new NotImplementedException();
		}

        List<Proyecto> CargarIDsPorIDEncargado(int IDencargado){
			//TODO
			throw new NotImplementedException();
		}

        public Proyecto CargarProyectoPorID(int IDproyecto){
			//TODO
			throw new NotImplementedException();
		}

        public List<Proyecto> CargarProyectosPorEstado(EstadoProyecto estado){
			//TODO
			throw new NotImplementedException();
		}

        public List<Proyecto> CargarProyectosTodos(){
			//TODO
			throw new NotImplementedException();
		}

        private List<Proyecto> ConvertirDataTableAListaDeProyectos (DataTable dataTable){
			//TODO
			throw new NotImplementedException();
		}

        private Proyecto ConvertirDataTableAProyecto (DataTable dataTable){
			//TODO
			throw new NotImplementedException();
		}

        private DataTable ConvertirProyectoADataTable (Proyecto proyecto){
			//TODO
			throw new NotImplementedException();
		}

        public int GuardarProyecto(Proyecto proyecto){
			//TODO
			throw new NotImplementedException();
		}


	}
}
