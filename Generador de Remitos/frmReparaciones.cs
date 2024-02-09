using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Generador_de_Remitos
{
    public partial class frmReparaciones : Form
    {
        private DataGridViewRow filaSeleccionada;

        public frmReparaciones()
        {
            InitializeComponent();
        }

        #region Eventos

        private void frmReparaciones_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;

                this.InicializarFormulario();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReparaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Consulta se hizo clic en una celda de la fila (no en el encabezado)
            if (e.RowIndex >= 0)
            {
                // Guardar la fila seleccionada
                filaSeleccionada = dgvReparaciones.Rows[e.RowIndex];

                btnModificar.Enabled = true;
                btnGarantia.Enabled = true;
                btnRemito.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnGarantia.Enabled = false;
                btnRemito.Enabled = false;
            }
        }

        private void dgvReparaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvReparaciones.Columns[e.ColumnIndex].Name == "OrdenService")
            {
                string reparado = "";

                DataGridViewCell cell = this.dgvReparaciones.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (this.dgvReparaciones.Rows[e.RowIndex].Cells["Reparado"].Value != null)
                {
                    reparado = this.dgvReparaciones.Rows[e.RowIndex].Cells["Reparado"].Value.ToString();
                }

                reparado = reparado.ToLower();

                if (reparado == "no reparado")
                {
                    cell.Style.BackColor = Color.OrangeRed;
                }
                else if (reparado == "reparado")
                {
                    cell.Style.BackColor = Color.LightGreen;
                }
            }
        }

        #endregion

        #region Eventos Botones

        private void btnBuscarOrdenService_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarOrdenService.Text != string.Empty)
                {
                    dgvReparaciones.ClearSelection();

                    foreach (DataGridViewRow fila in dgvReparaciones.Rows)
                    {
                        if (fila != null && fila.Cells["OrdenService"].Value != null)
                        {
                            if (fila.Cells["OrdenService"].Value.ToString() == txtBuscarOrdenService.Text)
                            {
                                fila.Cells["OrdenService"].Selected = true;
                                break;
                            }
                        }
                    }

                    if (dgvReparaciones.SelectedCells.Count == 0)
                    {
                        throw new Exception($"No hay ninguna Orden de Service con el numero: {txtBuscarOrdenService.Text}");
                    }
                }
                else
                {
                    throw new Exception("No hay ninguna Orden de Service para buscar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (filaSeleccionada.Cells["FechaEntrega"].Value.ToString() != "")
                {
                    throw new Exception("No se puede modificar un equipo entregado");
                }

                frmModificarReparacion modificarReparacion = new frmModificarReparacion(filaSeleccionada);

                modificarReparacion.ShowDialog();

                this.InicializarFormulario();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGarantia_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tabla = new DataTable();

                if (filaSeleccionada.Cells["Reparado"].Value.ToString() == "No Reparado")
                {
                    throw new Exception("No se puede crear la garantia de un equipo No Reparado");
                }

                tabla = this.crearTablaGarantia();
                
                //El parametro false indica que se trata de una Garantia y no un Remito
                if (MessageBoxExcel(tabla, false))
                {
                    //El parametro false indica que se trata de una Garantia y no un Remito
                    ExcelManager excelManager = new ExcelManager(false);

                    excelManager.GenerarExcel(tabla);

                    // Ruta del archivo que se quiere abrir
                    string pathCompleto = Utils.PathExcel + Utils.NombreExcelGarantia;

                    // Abre el archivo con la aplicación predeterminada
                    Process.Start(pathCompleto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemito_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();

            tabla = this.crearTablaRemito();

            //El parametro true indica que se trata de un Remito y no una Garantia
            if (MessageBoxExcel(tabla, true))
            {
                //El parametro true indica que se trata de un Remito y no una Garantia
                ExcelManager excelManager = new ExcelManager(true);

                excelManager.GenerarExcel(tabla);

                // Ruta del archivo que se quiere abrir
                string pathCompleto = Utils.PathExcel + Utils.NombreExcelRemito;

                // Abre el archivo con la aplicación predeterminada
                Process.Start(pathCompleto);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region Metodos

        private bool MessageBoxExcel(DataTable tabla, bool esRemito)
        {
            string nombre;
            string mensajeAviso;
            DialogResult result;

            nombre = tabla.Rows[0]["NombreYApellido"].ToString();

            mensajeAviso = "Se creara el archivo para el cliente " + nombre;
            
            if (esRemito)
            {
                result = MessageBox.Show(mensajeAviso, "Confirmar creacion de Remito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show(mensajeAviso, "Confirmar creacion de Garantia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.No)
            {
                return false;
            }

            return true;
        }

        private DataTable crearTablaRemito()
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = dataTable.NewRow();

            // Agregar las 11 columnas
            dataTable.Columns.Add("OrdenService", typeof(string));
            dataTable.Columns.Add("NombreYApellido", typeof(string));
            dataTable.Columns.Add("Telefono", typeof(string));
            dataTable.Columns.Add("Localidad", typeof(string));
            dataTable.Columns.Add("Domicilio", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("Marca", typeof(string));
            dataTable.Columns.Add("Modelo", typeof(string));
            dataTable.Columns.Add("Accesorios", typeof(string));
            dataTable.Columns.Add("MotivoFalla", typeof(string));
            dataTable.Columns.Add("Observaciones", typeof(string));


            // Agregar una fila con los valores correspondientes a cada columna
            string nombre = filaSeleccionada.Cells["NombreCliente"].Value.ToString().Trim();
            string apellido = filaSeleccionada.Cells["ApellidoCliente"].Value.ToString().Trim();

            dataRow["OrdenService"] = filaSeleccionada.Cells["OrdenService"].Value.ToString();
            dataRow["NombreYApellido"] = nombre + " " + apellido;
            dataRow["Telefono"] = filaSeleccionada.Cells["TelefonoCliente"].Value.ToString();
            dataRow["Localidad"] = filaSeleccionada.Cells["LocalidadCliente"].Value.ToString();
            dataRow["Domicilio"] = filaSeleccionada.Cells["DomicilioCliente"].Value.ToString();
            dataRow["Tipo"] = filaSeleccionada.Cells["TipoEquipo"].Value.ToString();
            dataRow["Marca"] = filaSeleccionada.Cells["MarcaEquipo"].Value.ToString();
            dataRow["Modelo"] = filaSeleccionada.Cells["ModeloEquipo"].Value.ToString();
            dataRow["Accesorios"] = filaSeleccionada.Cells["AccesoriosEquipo"].Value.ToString();
            dataRow["MotivoFalla"] = filaSeleccionada.Cells["MotivoFallaEquipo"].Value.ToString();
            dataRow["Observaciones"] = filaSeleccionada.Cells["ObservacionesEquipo"].Value.ToString();

            dataTable.Rows.Add(dataRow);

            return dataTable;
        }

        private DataTable crearTablaGarantia()
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = dataTable.NewRow();

            // Agregar las 11 columnas
            dataTable.Columns.Add("OrdenService", typeof(string));
            dataTable.Columns.Add("NombreYApellido", typeof(string));
            dataTable.Columns.Add("Telefono", typeof(string));
            dataTable.Columns.Add("Localidad", typeof(string));
            dataTable.Columns.Add("Domicilio", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("Marca", typeof(string));
            dataTable.Columns.Add("Modelo", typeof(string));
            dataTable.Columns.Add("ReparacionAEfectuar", typeof(string));
            dataTable.Columns.Add("ValorReparacion", typeof(string));


            // Agregar una fila con los valores correspondientes a cada columna
            string nombre = filaSeleccionada.Cells["NombreCliente"].Value.ToString().Trim();
            string apellido = filaSeleccionada.Cells["ApellidoCliente"].Value.ToString().Trim();

            dataRow["OrdenService"] = filaSeleccionada.Cells["OrdenService"].Value.ToString();
            dataRow["NombreYApellido"] = nombre + " " + apellido;
            dataRow["Telefono"] = filaSeleccionada.Cells["TelefonoCliente"].Value.ToString();
            dataRow["Localidad"] = filaSeleccionada.Cells["LocalidadCliente"].Value.ToString();
            dataRow["Domicilio"] = filaSeleccionada.Cells["DomicilioCliente"].Value.ToString();
            dataRow["Tipo"] = filaSeleccionada.Cells["TipoEquipo"].Value.ToString();
            dataRow["Marca"] = filaSeleccionada.Cells["MarcaEquipo"].Value.ToString();
            dataRow["Modelo"] = filaSeleccionada.Cells["ModeloEquipo"].Value.ToString();
            dataRow["ReparacionAEfectuar"] = filaSeleccionada.Cells["ReparacionAEfectuar"].Value.ToString();
            dataRow["ValorReparacion"] = "$" + filaSeleccionada.Cells["ValorReparacion"].Value.ToString();

            dataTable.Rows.Add(dataRow);

            return dataTable;
        }

        private void InicializarFormulario()
        {
            try
            {
                DataTable reparaciones = new DataTable();
                ConsultasSQL consulta = new ConsultasSQL(Utils.NombreBD);

                dgvReparaciones.Rows.Clear();

                reparaciones = consulta.ObtenerReparaciones();

                if (dgvReparaciones.Columns.Count != reparaciones.Columns.Count)
                {
                    throw new Exception("La cantidad de columnas no son las mismas, revisar codigo o base de datos");
                }

                foreach (DataRow dato in reparaciones.Rows)
                {
                    dgvReparaciones.Rows.Add(dato.ItemArray);
                }

                dgvReparaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //Especifico que tenga el signo $ adelante y el valor sin decimales
                DataGridViewColumn columnaValor = dgvReparaciones.Columns["ValorReparacion"];
                columnaValor.DefaultCellStyle.Format = "C0";

                //Ordena desde la ultima Orden de service a la primera
                dgvReparaciones.Sort(dgvReparaciones.Columns["OrdenService"], ListSortDirection.Descending);

                dgvReparaciones.ClearSelection();

                btnModificar.Enabled = false;
                btnGarantia.Enabled = false;
                btnRemito.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
