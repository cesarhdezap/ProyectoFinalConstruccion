using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos Alumno en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos Alumno.
	/// </summary>
	class LiberacionDAO : ILiberacionDAO
	{
		/// <summary>
		/// Convierte una DataTable a una Liberacion.
		/// </summary>
		/// <param name="tablaDeLiberacion">La DataTable que contiene datos de la Liberacion<./param>
		/// <returns>La Liberacion contenido en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
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

		/// <summary>
		/// Guarda una Liberacion en la base de datos.
		/// </summary>
		/// <param name="liberacion">La Liberacion a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarLiberacion(Liberacion liberacion)
        {
            SqlParameter[] parametrosDeLiberacion = InicializarParametrosDeSql(liberacion);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeLiberacion.GUARDAR_LIBERACION, parametrosDeLiberacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, liberacion);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Liberacion: " + liberacion.ToString() + "no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Carga a la Liberacion con la ID dada.
		/// </summary>
		/// <param name="IDLiberacion">La ID de la Liberacion a cargar.</param>
		/// <returns>La Liberacion con la ID dada.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
                tablaDeLiberacion = AccesoADatos.EjecutarSelect(QuerysDeLiberacion.CARGAR_LIBERACION_POR_ID, parametroIDLiberacion);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDLiberacion);
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

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en una Liberacion.
		/// </summary>
		/// <param name="liberacion">La Liberacion para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos de la Liberacion.</returns>
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

		/// <summary>
		/// Obtiene el ultimo ID insertado en la tabla de Liberacion en la base de datos.
		/// </summary>
		/// <returns>El ultimo ID insertado en la tabla de Liberacion</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiró una excepción. </exception>
		/// <exception cref="InvalidCastException">Tira esta excepción si la base de datos no regresa un valor entero.</exception>
		public int ObtenerUltimoIDInsertado()
		{
			int ultimoIDInsertado = 0;
			try
			{
				ultimoIDInsertado = AccesoADatos.EjecutarOperacionEscalar(QuerysDeLiberacion.OBTENER_ULTIMO_ID_INSERTADO);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}
			return ultimoIDInsertado;
		}
    }
}
