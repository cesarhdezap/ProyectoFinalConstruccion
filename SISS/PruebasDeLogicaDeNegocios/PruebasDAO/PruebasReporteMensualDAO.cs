using System.Collections.Generic;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasDAO
{
	[TestClass]
    public class PruebasReporteMensualDAO
    {
        [TestMethod]
        public void ProbarCargarIDsPorIDAsignacion()
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            List<ReporteMensual> reportesMensuales;
            reportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion(2);
            Assert.AreEqual(1, reportesMensuales.Count);
        }
    }
}
