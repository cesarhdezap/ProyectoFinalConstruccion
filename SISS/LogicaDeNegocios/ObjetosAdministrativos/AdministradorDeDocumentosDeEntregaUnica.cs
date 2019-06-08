using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    public class AdministradorDeDocumentosDeEntregaUnica
    {
        public List<DocumentoDeEntregaUnica> DocumentosDeEntregaUnica { get; set; }
        public void CargarDocumentosDeEntregaUnicaPorMatricula(string matricula)
        {
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            Asignacion asignacion = new Asignacion();
            asignacion = asignacionDAO.CargarIDsPorMatriculaDeAlumno(matricula).ElementAt(0);
            this.DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion(asignacion.IDAsignacion);
            for (int i = 0; i < DocumentosDeEntregaUnica.Count; i++)
            {
                DocumentosDeEntregaUnica[i] = documentoDeEntregaUnicaDAO.CargarDocumentoDeEntregaUnicaPorID(DocumentosDeEntregaUnica.ElementAt(i).IDDocumento);
            }
        }
    }
}
