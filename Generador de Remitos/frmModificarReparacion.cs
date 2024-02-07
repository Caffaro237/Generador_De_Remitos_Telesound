using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Generador_de_Remitos
{
    public partial class frmModificarReparacion : Form
    {
        private DataGridViewRow fila;

        #region Constructores

        public frmModificarReparacion()
        {
            InitializeComponent();
        }

        public frmModificarReparacion(DataGridViewRow filaSeleccionada)
        {
            InitializeComponent();

            fila = filaSeleccionada;
        }

        #endregion

        #region Eventos

        private void frmModificarReparacion_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;

                lblOrdenService.Text = fila.Cells["OrdenService"].Value.ToString();
                rtbReparacionAEfectuar.Text = fila.Cells["ReparacionAEfectuar"].Value.ToString();
                txtValorReparacion.Text = fila.Cells["ValorReparacion"].Value.ToString();

                if (fila.Cells["Confirmado"].Value.ToString() == "" || fila.Cells["Confirmado"].Value.ToString() == "NO")
                {
                    rbNoConfirma.Checked = true;
                }
                else
                {
                    rbSiConfirma.Checked = true;
                }

                if (fila.Cells["Reparado"].Value.ToString() == "" || fila.Cells["Reparado"].Value.ToString() == "No Reparado")
                {
                    rbNoReparado.Checked = true;
                }
                else
                {
                    rbSiReparado.Checked = true;
                }

                if (fila.Cells["FechaEntrega"].Value.ToString() == "")
                {
                    cbEquipoEntregado.Checked = false;
                }
                else
                {
                    cbEquipoEntregado.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtValorReparacion_Click(object sender, EventArgs e)
        {
            txtValorReparacion.SelectAll();
        }

        private void txtValorReparacion_Enter(object sender, EventArgs e)
        {
            txtValorReparacion.SelectAll();
        }

        #endregion

        #region Eventos Botones

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActualizarReparacion() == DialogResult.Yes)
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region Metodos

        private DialogResult ActualizarReparacion()
        {
            try
            {
                DialogResult result;

                int valorReparacion = 0;
                string confirmaReparacion = "";
                string equipoReparado = "";
                string equipoEntregado = "";
                string mensajeAviso = "";

                ConsultasSQL consulta = new ConsultasSQL(Utils.NombreBD);

                int.TryParse(txtValorReparacion.Text, out valorReparacion);

                if (rbSiConfirma.Checked == true)
                {
                    confirmaReparacion = "OK";
                }
                else
                {
                    confirmaReparacion = "NO";
                }

                if (rbSiReparado.Checked == true)
                {
                    equipoReparado = "Reparado";
                }
                else
                {
                    equipoReparado = "No Reparado";
                }

                if (cbEquipoEntregado.Checked == true)
                {
                    equipoEntregado = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                mensajeAviso = $"Se va a modificar la orden {lblOrdenService.Text} con los siguientes datos:\n";
                mensajeAviso += $"Reparacion a efectuar: {rtbReparacionAEfectuar.Text}\n";
                mensajeAviso += $"Valor de la reparacion: ${valorReparacion.ToString()}\n";
                mensajeAviso += $"Confirmacion de reparacion: {confirmaReparacion}\n";
                mensajeAviso += $"Equipo reparado: {equipoReparado}\n";
                mensajeAviso += $"Fecha de entrega: {equipoEntregado}\n";

                result = MessageBox.Show(mensajeAviso, "Confirmar cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    consulta.ActualizarReparacion(lblOrdenService.Text, rtbReparacionAEfectuar.Text, valorReparacion, confirmaReparacion, equipoReparado, equipoEntregado);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
