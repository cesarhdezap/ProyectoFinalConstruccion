using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocumentoDeEntregaUnicaDAO
	{
		int GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica);
		DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica);
		List<DocumentoDeEntregaUnica> CargarIDsPorMatriculaAlumno(string matricula);
		void ActualizarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntragaUnica, DocumentoDeEntregaUnica documentoDeEntregaUnica);
	}
}
