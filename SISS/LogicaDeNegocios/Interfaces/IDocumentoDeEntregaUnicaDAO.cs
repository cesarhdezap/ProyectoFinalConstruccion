using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocumentoDeEntregaUnicaDAO
	{
        DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica);
        List<DocumentoDeEntregaUnica> CargarIDsPorIDAsignacion(int IDAsignacion);
        void GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica);
        int ObtenerUltimoIDInsertado();
    }
}
