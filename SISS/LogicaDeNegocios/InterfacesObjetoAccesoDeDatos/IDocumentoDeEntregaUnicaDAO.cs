﻿using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocumentoDeEntregaUnicaDAO
	{
        DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDAsignacion);
        List<DocumentoDeEntregaUnica> CargarIDsPorIDAsignacion(int IDAsignacion);
        void GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica, int IDAsignacion);
		int ObtenerUltimoIDInsertado();
	}
}
