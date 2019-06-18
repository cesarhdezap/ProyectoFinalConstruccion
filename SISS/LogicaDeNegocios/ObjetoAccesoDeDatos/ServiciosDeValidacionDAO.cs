using AccesoABaseDeDatos;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    public class ServiciosDeValidacionDAO : IServiciosDeValidacionDAO
    {

		public int ContarOcurrenciasDeCorreo(string correo)
		{
			int numeroDeOcurrencias = 0;

			SqlParameter[] parametroCorreoElectronico = new SqlParameter[1];
			parametroCorreoElectronico[0] = new SqlParameter
			{
				ParameterName = "@CorreoElectronico",
				Value = correo
			};

			try
			{
				numeroDeOcurrencias = AccesoADatos.EjecutarOperacionEscalar(QuerysDeServiciosDeValidacion.CONTAR_OCURRENCIAS_DE_CORREO, parametroCorreoElectronico);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, correo);
			}

			return numeroDeOcurrencias;
		}

		public int ContarOcurrenciasDeMatricula(string matricula)
		{
			int numeroDeOcurrencias = 0;

			SqlParameter[] parametroMatricula = new SqlParameter[1];
			parametroMatricula[0] = new SqlParameter
			{
				ParameterName = "@Matricula",
				Value = matricula
			};

			try
			{
				numeroDeOcurrencias = AccesoADatos.EjecutarOperacionEscalar(QuerysDeServiciosDeValidacion.CONTAR_OCURRENCIAS_DE_MATRICULA, parametroMatricula);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, matricula);
			}

			return numeroDeOcurrencias;
		}
    }
}
