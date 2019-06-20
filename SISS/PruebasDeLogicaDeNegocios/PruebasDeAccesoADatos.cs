using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccesoABaseDeDatos;
using System.Data;
using System.Data.SqlClient;

namespace Pruebas
{
	[TestClass]
    public class PruebasDeAccesoADatos
    {
        [DataTestMethod]
        [DataRow("INSERT INTO Alumnos VALUES ('s17012947', 'Cesar Hernandez', 'cesarhdez@gmail.com', '2282437357', 'LIS', '1', 'passwordsegura')")]
        [DataRow("INSERT INTO Directores VALUES ('Juan Jimenez', 'juanj@correo.com', 'juanjpass')")]

        public void ProbarEjecutarInsertInto_ConsultaCorrecta_RegresaNumeroDeFilasAfectadas(string consulta)
        {
            int numeroDeFilasAfectadas = 0;
            const int UNA_FILA_AFECTADA = 1;
            numeroDeFilasAfectadas = AccesoADatos.EjecutarInsertInto(consulta);

            Assert.AreEqual(UNA_FILA_AFECTADA, numeroDeFilasAfectadas);
        }

        [DataTestMethod]
        [DataRow("INSERT INTO Alumnos VALUES ('s17012947', 'Cesar Hernandez', 'cesarhdez@gmail.com', '2282437357', 'LIS', '1', 'passwordsegura')")]
        public void ProbarEjecutarInsertInto_LlaveDuplicada_RegresaSqlException(string consulta)
        {
            bool excepcionTirada = false;
            try
            {
                AccesoADatos.EjecutarInsertInto(consulta);
            }
            catch (SqlException ex) when (ex.Number == (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException("Se esperaba SqlException de error " + (int)CodigoDeErrorDeSqlException.InsercionFallidaPorLlavePrimariaDuplicada);
            }
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
            
            string matriculaActual = string.Empty;


            tablaAlumnosActual = AccesoADatos.EjecutarSelect(consulta, parametros);

            matriculaActual = tablaAlumnosActual.Rows[0][0].ToString();

            Assert.AreEqual(matricula, matriculaActual);
        }

        [TestMethod]
        public void ProbarEjecutarSelect_AlumnosTodos_RegresaDatatable()
        {
            AccesoADatos.EjecutarSelect("SELECT * FROM Alumnos");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ProbarEjecutarSelect_ColumnaInvalida_RegresaSqlException()
        {
            bool excepcionTirada = false;
            try
            {
                AccesoADatos.EjecutarSelect("SELECT Nombres FROM Alumnos");
            }
            catch (SqlException ex) when (ex.Number == (int)CodigoDeErrorDeSqlException.ColumnaInvalida)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException("Se esperaba SqlException de error " + (int)CodigoDeErrorDeSqlException.ColumnaInvalida);
            }
        }

        [TestMethod]
        public void ProbarEjecutarSelect_ObjetoInvalido_RegresaSqlException()
        {
            bool excepcionTirada = false;
            try
            {
                AccesoADatos.EjecutarSelect("SELECT Matricula FROM Alumno");
            }
            catch (SqlException ex) when (ex.Number == (int)CodigoDeErrorDeSqlException.ObjetoInvalido)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException("Se esperaba SqlException de error " + (int)CodigoDeErrorDeSqlException.ObjetoInvalido);
            }
        }

    }
}
