using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccesoABaseDeDatos;
using System.Data;
using System.Data.SqlClient;

namespace Pruebas
{
    /// <summary>
    /// Descripción resumida de PruebasDeAccesoADatos
    /// </summary>
    [TestClass]
    public class PruebasDeAccesoADatos
    {
        public PruebasDeAccesoADatos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [DataTestMethod]
        [DataRow("INSERT INTO Alumnos VALUES ('s17012947', 'Cesar Hernandez', 'cesarhdez@gmail.com', '2282437357', 'LIS', '1', 'passwordsegura')")]

        public void ProbarEjecutarInsertInto_ConsultaCorrecta_RegresaNumeroDeFilasAfectadas(string input)
        {
            int numeroDeFilasAfectadas = 0;
            const int UNA_FILA_AFECTADA = 1;
            numeroDeFilasAfectadas = AccesoADatos.EjecutarInsertInto(input);

            Assert.AreEqual(UNA_FILA_AFECTADA, numeroDeFilasAfectadas);
        }


        [DataTestMethod]
        [DataRow("SELECT Matricula FROM Alumnos WHERE Matricula = @Matricula", "s17012947")]
        public void ProbarEjecutarSelect_AlumnoDatosCorrectos_RegresaDatatable(string consulta, string matricula)
        {
            DataTable tablaAlumnosActual;
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@Matricula";
            parametros[0].Value = matricula;
            
            string matriculaActual = String.Empty;


            tablaAlumnosActual = AccesoADatos.EjecutarSelect(consulta, parametros);

            matriculaActual = tablaAlumnosActual.Rows[0][0].ToString();

            Assert.AreEqual(matricula, matriculaActual);
        }

        [DataTestMethod]
        [DataRow("SELECT * FROM Alumnos",7)]
        public void ProbarEjecutarSelect_AlumnosTodos_RegresaDatatable(string consulta, int numeroDeFilasEsperado)
        {
            DataTable tablaAlumnosActual;
            tablaAlumnosActual = AccesoADatos.EjecutarSelect(consulta);
            int numeroDeFilas = tablaAlumnosActual.Rows.Count;
            Assert.AreEqual(numeroDeFilasEsperado, numeroDeFilas);
        }
    }
}
