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
using static AccesoABaseDeDatos.AccesoADatos;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    public class ServiciosDeValidacionDAO : IServiciosDeValidacionDAO
    {
        public List<string> CargarCorreosDeUsuarios()
        {
            DataTable tablaDeCorreos;
            try
            {
                tablaDeCorreos = EjecutarSelect("SELECT CorreoElectronico FROM Alumnos UNION SELECT CorreoElectronico FROM DocentesAcademicos UNION SELECT CorreoElectronico FROM Directores");
            }
            catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
            {
                throw new AccesoADatosException("Error al Cargar Correos De Usuarios ServiciosDeValidacionDAO", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al Cargar Correos De Usuarios ServiciosDeValidacionDAO", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
            }

            List<string> correosElectronicos;
            try
            {
                correosElectronicos = ConvertirDataTableAListaDeCadenasDeCorreo(tablaDeCorreos);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a lista cadenas de correo electronico", e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return correosElectronicos;
        }

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
				numeroDeOcurrencias = EjecutarOperacionEscalar("SELECT (SELECT COUNT(*) FROM Alumnos WHERE CorreoElectronico = @CorreoElectronico) + (SELECT COUNT(*) FROM Directores WHERE CorreoElectronico = @CorreoElectronico) + (SELECT COUNT(*) FROM DocentesAcademicos WHERE CorreoElectronico = @CorreoElectronico)", parametroCorreoElectronico);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al Cargar Correos De Usuarios ServiciosDeValidacionDAO", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al Cargar Correos De Usuarios ServiciosDeValidacionDAO", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
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
				numeroDeOcurrencias = EjecutarOperacionEscalar("SELECT COUNT(*) FROM Alumnos WHERE Matricula = @Matricula", parametroMatricula);
			}
			catch (SqlException e) when (e.Number == (int)CodigoDeErrorDeSqlException.ConexionABaseDeDatosFallida)
			{
				throw new AccesoADatosException("Error al Cargar Correos De Usuarios ServiciosDeValidacionDAO", e, TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida);
			}
			catch (SqlException e)
			{
				throw new AccesoADatosException("Error al Cargar Correos De Usuarios ServiciosDeValidacionDAO", e, TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos);
			}

			return numeroDeOcurrencias;
		}


		private List<string> ConvertirDataTableAListaDeCadenasDeCorreo(DataTable tablaDeCorreos)
        {
            List<string> correosElectronicos = new List<string>();
            foreach (DataRow fila in tablaDeCorreos.Rows)
            {
                correosElectronicos.Add(fila["CorreoElectronico"].ToString());
            }
            return correosElectronicos;
        }
    }
}
