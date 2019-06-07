using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    public class DirectorDAO : IDirectorDAO
    {
        public string CargarIDPorCorreo(string correoElectronico)
        {
            string IDDirector = string.Empty;
            DataTable tablaDeMatricula = new DataTable();
            SqlParameter[] parametroCorreo = new SqlParameter[1];
            parametroCorreo[0] = new SqlParameter
            {
                ParameterName = "@CorreoElectronico",
                Value = correoElectronico
            };

            try
            {
                tablaDeMatricula = AccesoADatos.EjecutarSelect("SELECT IDPersonal FROM Directores WHERE CorreoElectronico = @CorreoElecronico", parametroCorreo);
            }
            catch (SqlException e)
            {
                Console.WriteLine("No se encontro la matricula del alumno con correo: {0}", correoElectronico);
            }
            return IDDirector;
        }
    }
}
