﻿using AccesoABaseDeDatos;
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
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    public class DirectorDAO : IDirectorDAO
    {
        public string CargarIDPorCorreo(string correoElectronico)
        {
            DataTable tablaDeID = new DataTable();
            SqlParameter[] parametroCorreo = new SqlParameter[1];
            parametroCorreo[0] = new SqlParameter
            {
                ParameterName = "@CorreoElectronico",
                Value = correoElectronico
            };

            try
            {
                tablaDeID = AccesoADatos.EjecutarSelect(QuerysDeDirector.CARGAR_ID_POR_CORREO, parametroCorreo);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, correoElectronico);
			}

			string IDDirector = string.Empty;
            try
            {
                IDDirector = ConvertirDataTableADirectorConSoloID(tablaDeID).IDPersonal.ToString();
            }
            catch (FormatException e)
            {
                IDDirector = string.Empty;
                throw new AccesoADatosException("Error al convertir datatable a Director en cargar ID por CorreoElectronico: " + correoElectronico, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }


            return IDDirector;
        }

        private Director ConvertirDataTableADirectorConSoloID(DataTable tablaDeID)
        {
            Director director = new Director();
            foreach (DataRow fila in tablaDeID.Rows)
            {
                director.IDPersonal = (int)fila["IDPersonal"];
            }
            return director;
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
				tablaDeDirector = AccesoADatos.EjecutarSelect(QuerysDeDirector.CARGAR_DIRECTOR_POR_ID, parametroIDPersonal);
			}
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDPersonal);
			}

			Director director = new Director();
            try
            {
                director = ConvertirDataTableADirector(tablaDeDirector);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Director en cargar Director por IDPersonal: " + IDPersonal, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
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
