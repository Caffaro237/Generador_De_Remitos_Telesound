using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_Remitos
{
    public static class Utils
    {
        public enum BuscarPor
        {
            Orden_De_Service,
            Nombre,
            Telefono
        }

        public enum TipoEquipo
        {
            PC_Escritorio,
            All_in_One,
            Notebook,
            Netbook,
            Celular

        }

        public enum Marca
        {
            DELL,
            ASUS,
            Samsung,
            Lenovo,
            Positivo_BGH,
            Toshiba,
            HP,
            Compaq,
            Acer,
            LG,
            Philips,
            Noblex,
            Top_House,
            Goldstar,
            EXO
        }

        public enum Accesorios
        {
            Sin_accesorios,
            Con_cargador,
            Con_mouse,
            Con_teclado,
            Con_mouse_con_teclado,
            Con_cable,
            Con_caja,
            Con_caja_con_cargador,
            Con_remoto,
            Con_remoto_con_cable,
            En_bolsa,
            En_bolsa_con_cargador

        }
        

        public static string PathExcel
        {
            get
            {
                try
                {

                    string path;

                    if (ConfigurationManager.AppSettings["KeyCarpetaExcel"] == null)
                    {
                        throw new Exception();
                    }

                    path = Directory.GetCurrentDirectory() + @"\";
                    path += ConfigurationManager.AppSettings["KeyCarpetaExcel"] + @"\";

                    return path;
                }
                catch (Exception)
                {
                    throw new Exception("No se encontro el valor para Path de la carpeta del Excel");
                }
            }
        }

        public static string NombreExcelRemito
        {
            get
            {
                if (ConfigurationManager.AppSettings["KeyNombreExcelRemito"] == null)
                {
                    throw new Exception("No se encontro el valor para el Nombre del Excel para el Remito");
                }

                return ConfigurationManager.AppSettings["KeyNombreExcelRemito"];
            }
        }

        public static string NombreExcelGarantia
        {
            get
            {
                if (ConfigurationManager.AppSettings["KeyNombreExcelGarantia"] == null)
                {
                    throw new Exception("No se encontro el valor para el Nombre del Excel para la Garantia");
                }

                return ConfigurationManager.AppSettings["KeyNombreExcelGarantia"];
            }
        }

        public static int ConnectionTimeout
        {
            get
            {
                try
                {
                    string connectionTimeout = ConfigurationManager.ConnectionStrings["ConnectionTimeout"].ToString();
                    int timeout = int.Parse(connectionTimeout);
                    return timeout;
                }
                catch (Exception)
                {
                    throw new Exception("Error al buscar el valor de ConnectionTimeout en AppConfig");
                }
            }
        }

        public static string NombreBD
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["NombreBD"].ToString() == null)
                {
                    throw new Exception("No se encontro el valor para NombreBD");
                }

                return ConfigurationManager.ConnectionStrings["NombreBD"].ToString();
            }
        }

        public static string UsuarioBD
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["Usuario"].ToString() == null)
                {
                    throw new Exception("No se encontro el valor para UsuarioBD");
                }

                return ConfigurationManager.ConnectionStrings["Usuario"].ToString();
            }
        }

        public static string PasswordBD
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["Password"].ToString() == null)
                {
                    throw new Exception("No se encontro el valor para PasswordBD");
                }

                return ConfigurationManager.ConnectionStrings["Password"].ToString();
            }
        }

    }
}
