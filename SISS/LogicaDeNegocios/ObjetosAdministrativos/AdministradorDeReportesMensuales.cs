using System;
using System.Collections.Generic;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    public class AdministradorDeReportesMensuales
    {
        public List<ReporteMensual> ReportesMensuales { get; set; }
        public void CargarReportesMensualesPorMatricula(string matricula)
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Asignacion asignacion = new Asignacion();
            asignacion = asignacionDAO.CargarIDsPorMatriculaDeAlumno(matricula).ElementAt(0);
            this.ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion(asignacion.IDAsignacion);
            for (int i = 0; i<ReportesMensuales.Count; i++)
            {
                ReportesMensuales[i] = reporteMensualDAO.CargarReporteMensualPorID(ReportesMensuales.ElementAt(i).IDDocumento);
            }
        }
    }
}
