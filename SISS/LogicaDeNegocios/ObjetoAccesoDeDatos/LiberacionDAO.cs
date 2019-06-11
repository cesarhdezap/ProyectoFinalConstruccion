using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class LiberacionDAO : ILiberacionDAO
	{
        private Liberacion ConvertirDataTableALiberacion (DataTable tablaDeLiberacion)
        {
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            Liberacion liberacion = new Liberacion();
            foreach (DataRow fila in tablaDeLiberacion.Rows)
            {
                liberacion.IDLiberacion = (int)fila["IDLiberacion"];
                liberacion.Fecha = DateTime.Parse(fila["Fecha"].ToString());
            }
            return liberacion;
        }

        public void GuardarLiberacion(Liberacion liberacion)
        {
            SqlParameter[] parametrosDeLiberacion = InicializarParametrosDeSql(liberacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Liberaciones(Fecha, IDDocumento) VALUES(@Fecha, @IDDocumento)", parametrosDeLiberacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al guardar Liberacion: " + liberacion.ToString(), e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al guardar Liberacion: " + liberacion.ToString(), e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Liberacion: " + liberacion.ToString() + "no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

        public Liberacion CargarLiberacionPorID(int IDLiberacion)
        {
            DataTable tablaDeLiberacion = new DataTable();
            SqlParameter[] parametroIDLiberacion = new SqlParameter[1];
            parametroIDLiberacion[0] = new SqlParameter
            {
                ParameterName = "@IDLiberacion",
                Value = IDLiberacion
            };

            try
            {
                tablaDeLiberacion = AccesoADatos.EjecutarSelect("SELECT * FROM Liberaciones WHERE IDLiberacion = @Liberacion", parametroIDLiberacion);
            }
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
                throw new AccesoADatosException("Error al cargar Liberacion con IDLiberacion: " + IDLiberacion, e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al cargar Liberacion con IDLiberacion: " + IDLiberacion, e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}
			Liberacion liberacion = new Liberacion();
            try
            {
                liberacion = ConvertirDataTableALiberacion(tablaDeLiberacion);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Liberacion en cargar Liberacion con IDLiberacion: " + IDLiberacion, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return liberacion;
        }

        private SqlParameter[] InicializarParametrosDeSql(Liberacion liberacion)
        {
            SqlParameter[] parametrosDeLiberacion = new SqlParameter[3];

            for (int i = 0; i < parametrosDeLiberacion.Length; i++)
            {
                parametrosDeLiberacion[i] = new SqlParameter();
            }

            parametrosDeLiberacion[0].ParameterName = "@IDLiberacion";
            parametrosDeLiberacion[0].Value = liberacion.IDLiberacion;
            parametrosDeLiberacion[1].ParameterName = "@Fecha";
            parametrosDeLiberacion[1].Value = liberacion.Fecha.ToString();
            parametrosDeLiberacion[2].ParameterName = "@IDDocumento";
            parametrosDeLiberacion[2].Value = liberacion.CartaDeLiberacion.IDDocumento;

            return parametrosDeLiberacion;
        }

        public int ObtenerUltimoIDInsertado()
        {
            int ultimoIDInsertado = 0;
            try
            {
                ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar("SELECT IDENT_CURRENT('Liberaciones')");
            }
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
            {
                throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en ReporteMensualDAO", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al obtener Ultimo ID Insertado en ReporteMensualDAO", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
            }
            return ultimoIDInsertado;
        }
    }
}
