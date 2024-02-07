using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_Remitos
{
    public static class Extensiones
    {
        public static SqlParameter ToSqlParameter(this object valorParametro, string nombreParametro) =>
            new SqlParameter(nombreParametro, valorParametro);
    }
}
