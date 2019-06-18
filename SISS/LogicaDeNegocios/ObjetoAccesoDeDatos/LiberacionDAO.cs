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
	/// Clase de abstraccion para acceso a objetos <see cref="Liberacion"/> en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos <see cref="Liberacion"/>.
	/// </summary>
	class LiberacionDAO : ILiberacionDAO
	{
		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="Liberacion"/> .
		/// </summary>
		/// <param name="tablaDeLiberacion">La <see cref="DataTable"/>  que contiene datos de la <see cref="Liberacion"/>.</param>
		/// <returns>La <see cref="Liberacion"/> contenido en la <see cref="DataTable"/>.</returns>
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
		/// Guarda una <see cref="Liberacion"/> en la base de datos.
		/// </summary>
		/// <param name="liberacion">La <see cref="Liberacion"/> a guardar.</param>
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
		/// Carga a la <see cref="Liberacion"/> con la <see cref="Liberacion.IDLiberacion"/> dada.
		/// </summary>
		/// <param name="IDLiberacion"><see cref="Liberacion.IDLiberacion"/> de la <see cref="Liberacion"/> a cargar.</param>
		/// <returns>La <see cref="Liberacion"/> con la <see cref="Liberacion.IDLiberacion"/> dada.</returns>
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
		/// Inicializa un arreglo de <see cref="SqlParameter"/> basado en una <see cref="Liberacion"/>.
		/// </summary>
		/// <param name="liberacion">La <see cref="Liberacion"/> para inicializar los parametros.</param>
		/// <returns>Un arreglo de <see cref="SqlParameter"/> donde cada posición es uno de los atributos de la <see cref="Liberacion"/>.</returns>
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
		/// Obtiene el ultimo <see cref="Liberacion.IDLiberacion"/> insertado en la tabla de Liberacion en la base de datos.
		/// </summary>
		/// <returns>El ultimo <see cref="Liberacion.IDLiberacion"/> insertado en la tabla de Liberacion</returns>
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
