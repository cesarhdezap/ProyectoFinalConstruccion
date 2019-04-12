using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicaDeNegocios.Interfaces
{
	interface IDocumentoDeEntregaUnicaDAO
	{
		int GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica);
		DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica);
		void ActualizarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntragaUnica, DocumentoDeEntregaUnica documentoDeEntregaUnica);
		DataTable ConvertirDocumentoDeEntregaUnicaADataTable(DocumentoDeEntregaUnica documentoDeEntregaUnica);
	}
}
