using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Generador_de_Remitos.Utils;

namespace Generador_de_Remitos
{
    public class ConsultasSQL
    {
        private ConexionBD conexion;

        public ConsultasSQL(string dbConexion)
        {
            this.conexion = new ConexionBD(dbConexion);
        }


        public void CargarCliente(string ordenService, string nombre, string apellido, string telefono, string localidad, string domicilio)
        {
            try
            {
                string storedProcedure = "P_Cargar_Cliente";

                string pOrdenService = "@OrdenService";
                string pNombre = "@Nombre";
                string pApellido = "@Apellido";
                string pTelefono = "@Telefono";
                string pLocalidad = "@Localidad";
                string pDomicilio = "@Domicilio";

                SqlParameter[] parametros = new SqlParameter[] { ordenService.ToString().ToSqlParameter(pOrdenService),
                                                                    nombre.ToString().ToSqlParameter(pNombre),
                                                                    apellido.ToString().ToSqlParameter(pApellido),
                                                                    telefono.ToString().ToSqlParameter(pTelefono),
                                                                    localidad.ToString().ToSqlParameter(pLocalidad),
                                                                    domicilio.ToString().ToSqlParameter(pDomicilio) };

                this.conexion.EjecutarStoredProcedure(storedProcedure, parametros);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void CargarEquipo(string ordenService, string tipo, string marca, string modelo, string accesorios, string motivo, string observaciones)
        {
            try
            {
                string storedProcedure = "P_Cargar_Equipo";

                string pOrdenService = "@OrdenService";
                string pTipo = "@Tipo";
                string pMarca = "@Marca";
                string pModelo = "@Modelo";
                string pAccesorios = "@Accesorios";
                string pMotivo = "@Motivo";
                string pObservaciones = "@Observaciones";

                SqlParameter[] parametros = new SqlParameter[] { ordenService.ToString().ToSqlParameter(pOrdenService),
                                                                 tipo.ToString().ToSqlParameter(pTipo),
                                                                 marca.ToString().ToSqlParameter(pMarca),
                                                                 modelo.ToString().ToSqlParameter(pModelo),
                                                                 accesorios.ToString().ToSqlParameter(pAccesorios),
                                                                 motivo.ToString().ToSqlParameter(pMotivo),
                                                                 observaciones.ToString().ToSqlParameter(pObservaciones) };

                this.conexion.EjecutarStoredProcedure(storedProcedure, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerarReparacion()
        {
            try
            {
                string storedProcedure = "P_Crear_Reparaciones";

                SqlParameter[] parametros = new SqlParameter[] { };

                this.conexion.EjecutarStoredProcedure(storedProcedure, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarCliente(string buscarPor, string datoABuscar)
        {
            try
            {
                string storedProcedure = "P_Buscar_Cliente";

                string pBuscarPor = "@BuscarPor";
                string pDatoABuscar = "@Dato";

                SqlParameter[] parametros = new SqlParameter[] { buscarPor.ToString().ToSqlParameter(pBuscarPor),
                                                             datoABuscar.ToString().ToSqlParameter(pDatoABuscar) };

                return this.conexion.DataTableFromSP(storedProcedure, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerReparaciones()
        {
            try
            {
                string view = "V_Obtener_Reparaciones";

                return conexion.DataTableFromView(view);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerUltimaOrdenService()
        {
            try
            {
                string view = "V_Obtener_Ultima_OrdenService";

                return conexion.DataTableFromView(view);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarReparacion(string ordenService, string reparacionAEfectuar, int valorReparacion, string confirmaReparacion, string equipoReparado, string equipoEntregado)
        {
            try
            {
                string storedProcedure = "P_Actualizar_Reparaciones";

                string pOrdenService = "@OrdenService";
                string pReparacionAEfectuar = "@ReparacionAEfectuar";
                string pValorReparacion = "@ValorReparacion";
                string pConfirmaReparacion = "@Confirmado";
                string pEquipoReparado = "@Reparado";
                string pEquipoEntregado = "@FechaEntrega"; 

                SqlParameter[] parametros = new SqlParameter[] { ordenService.ToString().ToSqlParameter(pOrdenService),
                                                             reparacionAEfectuar.ToString().ToSqlParameter(pReparacionAEfectuar) ,
                                                             valorReparacion.ToString().ToSqlParameter(pValorReparacion) ,
                                                             confirmaReparacion.ToString().ToSqlParameter(pConfirmaReparacion) ,
                                                             equipoReparado.ToString().ToSqlParameter(pEquipoReparado) ,
                                                             equipoEntregado.ToString().ToSqlParameter(pEquipoEntregado) };

                this.conexion.EjecutarStoredProcedure(storedProcedure, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
