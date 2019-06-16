using LogicaDeNegocios.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AutenticacionDAO : IAutenticacionDAO
    {
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

        private List<string> ConvertirDataTableAListaDeCadenas(DataTable tablaDeCorreos)
        {
            List<string> listaDeCorreos = new List<string>();
            foreach (DataRow fila in tablaDeCorreos.Rows)
            {
                listaDeCorreos.Add(fila["CorreoElectronico"].ToString());
            }
            return listaDeCorreos;
        }

        private string ConvertirDataTableACadena(DataTable tablaDeContraseña)
        {
            DataRow filaDeConstraseña = tablaDeContraseña.Select()[0];
            string cadenaDeContraseña = (string)filaDeConstraseña["Contraseña"];
            return cadenaDeContraseña;
        }
    }
}
