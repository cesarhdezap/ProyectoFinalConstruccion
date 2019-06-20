using System;
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
        public void ProbarCargarIDsPorIDAsignacion_SinExcepciones_RegresaListaDeReportesMensuales()
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            reporteMensualDAO.CargarIDsPorIDAsignacion(2);
            Assert.IsTrue(true);
        }
    }
}
