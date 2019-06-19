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
        /// <summary>
        /// Cuenta las veces que el correo electrónico dado aparece en la base de datos.
        /// </summary>
        /// <param name="correo">Correo electrónico para contar ocurrencias.</param>
        /// <returns>Numero de veces que el correo electrónico dado aparece en la base de datos.</returns>
        /// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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

        /// <summary>
        /// Cuenta las veces que la matrícula dada aparece en la base de datos.
        /// </summary>
        /// <param name="correo">Matrícula para contar ocurrencias.</param>
        /// <returns>Numero de veces que la Matrícula dada aparece en la base de datos.</returns>
        /// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
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
