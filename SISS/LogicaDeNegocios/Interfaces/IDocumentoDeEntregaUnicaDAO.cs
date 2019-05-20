using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocumentoDeEntregaUnicaDAO
	{
		void ActualizarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntragaUnica, DocumentoDeEntregaUnica documentoDeEntregaUnica);
        DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica);
        List<DocumentoDeEntregaUnica> CargarIDsPorIDAsignacion(int IDAsignacion);
		List<DocumentoDeEntregaUnica> CargarIDsPorMatriculaAlumno(string matricula);
        DataTable ConvertirAsignacionADataTable(Asignacion asignacion);
        Asignacion ConvertirDataTableAAsignacion(DataTable dataTable);
        List<Asignacion> ConvertirDataTableAListaDeAsignaciones(DataTable dataTable);
        int GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica);	
	}
}
