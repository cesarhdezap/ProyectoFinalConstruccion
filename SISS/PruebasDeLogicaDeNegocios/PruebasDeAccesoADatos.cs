using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccesoABaseDeDatos;
using System.Data;
using System.Data.SqlClient;

namespace PruebasDeLogicaDeNegocios
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
        [DataRow("INSERT INTO Alumnos VALUES ('s17012947', 'Cesar Hernandez','LIS', 'Activo', '2282437357', 'cesarhdez@gmail.com','passwordsegura', '21')")]

        public void ProbarEjecutarInsertIntoConsultaCorrecta_RegresaNumeroDeFilasAfectadas(string input)
        {
            //Organizar
            int numeroDeFilasAfectadas;

            //Actuar
            numeroDeFilasAfectadas = AccesoADatos.EjecutarInsertInto(input);

            //Declarar
            Assert.AreEqual(1, numeroDeFilasAfectadas);
        }
        [DataTestMethod]
        [DataRow("SELECT Matricula FROM Alumno WHERE Matricula = @Matricula", "s17012970")]
        public void ProbarEjecutarSelectConsultaCorrecta_RegresaDatatable (string consulta, string matricula)
        {
            DataTable tablaAlumnosActual;
            string matriculaActual;
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@Matricula";
            parametros[0].Value = matricula;

            //Actuar
            tablaAlumnosActual = AccesoADatos.EjecutarSelect(consulta, parametros);

            DataRow filaAlumno;
            filaAlumno = tablaAlumnosActual.Rows[0];
            matriculaActual = filaAlumno["matricula"].ToString();

            Assert.AreEqual(matricula, matriculaActual);
        }
        
    }
}
