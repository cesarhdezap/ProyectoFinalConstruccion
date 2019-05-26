using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class LiberacionDAO : Interfaces.ILiberacionDAO
	{
        public Liberacion CargarLiberacionPorIDAsignacion(int IDasignacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        private Liberacion ConvertirDataTableALiberacion (DataTable tablaLiberacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        private List<Liberacion> ConvertirDataTableAListaDeLiberaciones(DataTable listaLiberacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        private DataTable ConvertirLiberacionADataTable(Liberacion liberacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        public int GuardarLiberacion(Liberacion liberacion)
        {
            //TODO
			throw new NotImplementedException();
        }


	}
}
