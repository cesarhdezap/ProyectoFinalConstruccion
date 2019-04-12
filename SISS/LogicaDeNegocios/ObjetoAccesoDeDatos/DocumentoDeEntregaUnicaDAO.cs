using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocumentoDeEntregaUnicaDAO : Interfaces.IDocumentoDeEntregaUnicaDAO
	{
		public void ActualizarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica, DocumentoDeEntregaUnica documentoDeEntregaUnica)
		{
			throw new NotImplementedException();
		}

		public DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica)
		{
			throw new NotImplementedException();
		}

		public List<DocumentoDeEntregaUnica> CargarIDsPorMatriculaAlumno(string matricula)
		{
			throw new NotImplementedException();
		}

		public DataTable ConvertirDocumentoDeEntregaUnicaADataTable(DocumentoDeEntregaUnica documentoDeEntregaUnica)
		{
			throw new NotImplementedException();
		}

		public int GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica)
		{
			throw new NotImplementedException();
		}
	}
}
