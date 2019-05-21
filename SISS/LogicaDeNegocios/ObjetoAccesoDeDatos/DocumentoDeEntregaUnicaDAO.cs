using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class DocumentoDeEntregaUnicaDAO : Interfaces.IDocumentoDeEntregaUnicaDAO
	{
        public void ActualizarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntragaUnica, DocumentoDeEntregaUnica documentoDeEntregaUnica)
        {
            
            //TODO
			throw new NotImplementedException();
        }

        public DocumentoDeEntregaUnica CargarDocumentoDeEntregaUnicaPorID(int IDdocumentoDeEntregaUnica)
        {
            
            
            //TODO
			throw new NotImplementedException();
        }

        public List<DocumentoDeEntregaUnica> CargarIDsPorIDAsignacion(int IDAsignacion)
        {
            
            
            //TODO
			throw new NotImplementedException();
        }

		public List<DocumentoDeEntregaUnica> CargarIDsPorMatriculaAlumno(string matricula)
        {
            
            
            //TODO
			throw new NotImplementedException();
        }

        private DataTable ConvertirAsignacionADataTable(Asignacion asignacion)
        {
            
            
            //TODO
			throw new NotImplementedException();
        }

        private Asignacion ConvertirDataTableAAsignacion(DataTable dataTable)
        {
            
            
            //TODO
			throw new NotImplementedException();
        }
        
        private List<Asignacion> ConvertirDataTableAListaDeAsignaciones(DataTable dataTable)
        {
            
            
            //TODO
			throw new NotImplementedException();
        }

        private DataTable  ConvertirDocumentoDeEntregaUnicaADataTable(DocumentoDeEntregaUnica documentoDeEntregaUnica)
        {
            
            //TODO
			throw new NotImplementedException();
        }

        public int GuardarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica)
        {
            
            //TODO
			throw new NotImplementedException();
        }
	
	}
}

