﻿namespace LogicaDeNegocios.Interfaces
{
	interface ILiberacionDAO
	{
        Liberacion CargarLiberacionPorID(int IDLiberacion);
        void GuardarLiberacion(Liberacion liberacion);
		int ObtenerUltimoIDInsertado();
	}
}
