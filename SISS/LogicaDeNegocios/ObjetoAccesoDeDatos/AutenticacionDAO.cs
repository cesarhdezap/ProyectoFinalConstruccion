using LogicaDeNegocios.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using LogicaDeNegocios.Excepciones;

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
                tablaDeContraseña= AccesoADatos.EjecutarSelect("SELECT Contraseña FROM (SELECT CorreoElectronico,Contraseña FROM Alumnos UNION SELECT CorreoElectronico, Contraseña FROM DocentesAcademicos UNION SELECT CorreoElectronico, Contraseña From Directores) AS U WHERE CorreoElectronico = @CorreoElectronico", parametroCorreoElectronico);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException(e.Message,e);
            }
            string contraseña = ConvertirDataTableACadena(tablaDeContraseña);

            return contraseña;
        }


        public List<string> CargarCorreosDeUsuarios()
        {
            DataTable tablaDeCorreos = new DataTable();
            try
            {
                tablaDeCorreos = AccesoADatos.EjecutarSelect("SELECT CorreoElectronico FROM Alumnos UNION SELECT CorreoElectronico FROM DocentesAcademicos UNION SELECT CorreoElectronico FROM Directores");
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException(e.Message, e);
            }
            
            List<string> listaDeCorreos = ConvertirDataTableAListaDeCadenas(tablaDeCorreos);

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
