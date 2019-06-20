using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos relacionados a la autenticación de usuarios en la base de datos.
	/// Contiene metodos para cargar datos de autenticación.
	/// </summary>
	public class AutenticacionDAO : IAutenticacionDAO
    {
		/// <summary>
		/// Carga la contraseña de un usuario dado su correo electrónico.
		/// </summary>
		/// <param name="correoElectronico">El correo electronico relacionado a la constraseña a cargar.</param>
		/// <returns>La constraseña relacionada al correo electrónico dado.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public string CargarContraseñaPorCorreo(string correoElectronico)
        {
            SqlParameter[] parametroCorreoElectronico = new SqlParameter[1];

            parametroCorreoElectronico[0] = new SqlParameter
            {
                ParameterName = "@CorreoElectronico",
                Value = correoElectronico
            };

            DataTable tablaDeContraseña = new DataTable();

            try
            {
                tablaDeContraseña= AccesoADatos.EjecutarSelect(QuerysDeAutenticacion.CARGAR_CONTRASEÑA_POR_CORREO, parametroCorreoElectronico);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, correoElectronico);
			}

			string contraseña;

            try
            {
                contraseña = ConvertirDataTableACadena(tablaDeContraseña);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a cadena", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return contraseña;
        }

		/// <summary>
		/// Carga una lista con todos los correos electrónicos de todos los usuarios.
		/// </summary>
		/// <returns>Lista con todos los correos electrónicos de los usuarios.</returns>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public List<string> CargarCorreosDeUsuarios()
        {
            DataTable tablaDeCorreos = new DataTable();

            try
            {
                tablaDeCorreos = AccesoADatos.EjecutarSelect(QuerysDeAutenticacion.CARGAR_CORREOS_DE_USUARIOS);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e);
			}

			List<string> listaDeCorreos;

            try
            {
                listaDeCorreos = ConvertirDataTableAListaDeCadenas(tablaDeCorreos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir cargar correos de usuarios", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return listaDeCorreos;
        }

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="List{string}"/> de <see cref="string"/>.
		/// </summary>
		/// <param name="tablaDeCorreos">La <see cref="DataTable"/> que contiene datos de tipo <see cref="string"/></param>
		/// <returns>Una <see cref="List{string}"/> de <see cref="string"/> contenidas en la <see cref="DataTable	"/></returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private List<string> ConvertirDataTableAListaDeCadenas(DataTable tablaDeCorreos)
        {
            List<string> listaDeCorreos = new List<string>();

            foreach (DataRow fila in tablaDeCorreos.Rows)
            {
                listaDeCorreos.Add(fila["CorreoElectronico"].ToString());
            }

            return listaDeCorreos;
        }

		/// <summary>
		/// Convierte una <see cref="DataTable"/> a una <see cref="string"/>.
		/// </summary>
		/// <param name="tablaDeContraseña">La <see cref="DataTable"/> que contiene la <see cref="string"/>.</param>
		/// <returns>Una <see cref="string"/> contenida en la <see cref="DataTable"/>.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private string ConvertirDataTableACadena(DataTable tablaDeContraseña)
        {
            DataRow filaDeConstraseña = tablaDeContraseña.Select()[0];
            string cadenaDeContraseña = (string)filaDeConstraseña["Contraseña"];
            return cadenaDeContraseña;
        }
    }
}
