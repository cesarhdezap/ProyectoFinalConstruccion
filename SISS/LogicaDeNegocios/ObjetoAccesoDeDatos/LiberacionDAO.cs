using System;
using System.Collections.Generic;
using System.Data;
using LogicaDeNegocios.Interfaces;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class LiberacionDAO : Interfaces.ILiberacionDAO
	{
        private Liberacion ConvertirDataTableALiberacion (DataTable tablaLiberacion)
        {
            //TODO
			throw new NotImplementedException();
        }

        public void GuardarLiberacion(Liberacion liberacion)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Liberacion CargarLiberacionPorID(int IDLiberacion)
        {
            throw new NotImplementedException();
        }

        public Liberacion CargarIDPorIDAsignacion(int IDAsignacion)
        {
            throw new NotImplementedException();
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}
