using AccesoABaseDeDatos;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;

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

        public Director CargarDirectorPorIDPersonal(int IDPersonal)
        {
            if (IDPersonal <= 0)
            {
                throw new AccesoADatosException("Error al Cargar Director Por IDpersonal: " + IDPersonal + ". IDpersonal no es valido.");
            }
            DataTable tablaDeDirector = new DataTable();
            SqlParameter[] parametroIDPersonal = new SqlParameter[1];
            parametroIDPersonal[0] = new SqlParameter()
            {
                ParameterName = "@IDpersonal",
                Value = IDPersonal
            };
            try
            {
                tablaDeDirector = AccesoADatos.EjecutarSelect("SELECT * FROM Directores WHERE IDPersonal = @IDPersonal", parametroIDPersonal);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Director por IDPersonal: " + IDPersonal, e);
            }
            Director director = new Director();
            try
            {
                director = ConvertirDataTableADirector(tablaDeDirector);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Director en cargar Director por IDPersonal: " + IDPersonal, e);
            }
            return director;
        }

        private Director ConvertirDataTableADirector(DataTable tablaDirector)
        {
            Director director = new Director();
            foreach (DataRow fila in tablaDirector.Rows)
            {
                director.Nombre = fila["Nombre"].ToString();
                director.CorreoElectronico = fila["CorreoElectronico"].ToString();
            }
            return director;
        }
    }
}
