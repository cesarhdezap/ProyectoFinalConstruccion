using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoABaseDeDatos
{
    public enum CodigoDeErrorDeSqlException
    {
		ConexionAServidorFallida = -1,
		TiempoDeEsperaExpirado = -2,
		ServidorNoRespondio = 2,
		ColumnaInvalida = 207,
		ObjetoInvalido = 208,
		ServidorNoEncontrado = 53,
        InsercionFallidaPorLlavePrimariaDuplicada = 2627
    }
}
