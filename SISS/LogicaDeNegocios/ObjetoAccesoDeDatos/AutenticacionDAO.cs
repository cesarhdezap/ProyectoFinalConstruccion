using LogicaDeNegocios.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using AccesoABaseDeDatos;
using System.Data.SqlClient;


namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	public class AutenticacionDAO : IAutenticacionDAO
    {
        public string CargarContraseñaPorCorreo(string correoElectronico)
        {
            SqlParameter[] parametroCorreoElectronico = new SqlParameter[1];
            parametroCorreoElectronico[1].ParameterName = "@CorreoElectronico";
            parametroCorreoElectronico[1].Value = correoElectronico;
            DataTable tablaDeContraseña = new DataTable();

            try
            {
                tablaDeContraseña= AccesoADatos.EjecutarSelect("Query?", parametroCorreoElectronico);
            } 
            catch (SqlException e)
            {

            }
            string contraseña = ConvertirDataTableACadena(tablaDeContraseña);

            return contraseña;
        }

        public List<string> CargarCorreoDeUsuarios()
        {
			//TODO
			throw new NotImplementedException();
        }

        private string ConvertirDataTableACadena(DataTable tablaDeContraseña)
        {
            DataRow filaDeConstraseña = tablaDeContraseña.Select()[0];
            string cadenaDeContraseña = (string)filaDeConstraseña["Contraseña"];
            return cadenaDeContraseña;
        }
    }
}
