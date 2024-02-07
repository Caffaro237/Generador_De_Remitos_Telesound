using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generador_de_Remitos
{
    public partial class frmBuscarCliente : Form
    {
        private string nombre;
        private string apellido;
        private string telefono;
        private string localidad;
        private string domicilio;
        
        #region Propiedades

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
            }
        }

        public string Apellido
        {
            get
            {
                return this.apellido;
            }
            set
            {
                this.apellido = value;
            }
        }

        public string Telefono
        {
            get
            {
                return this.telefono;
            }
            set
            {
                this.telefono = value;
            }
        }

        public string Localidad
        {
            get
            {
                return this.localidad;
            }
            set
            {
                this.localidad = value;
            }
        }

        public string Domicilio
        {
            get
            {
                return this.domicilio;
            }
            set
            {
                this.domicilio = value;
            }
        }

        #endregion

        public frmBuscarCliente()
        {
            InitializeComponent();
        }

        #region Eventos

        private void BuscarCliente_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;

                foreach (Utils.BuscarPor tipo in Enum.GetValues(typeof(Utils.BuscarPor)))
                {
                    cmbBuscarPor.Items.Add(tipo.ToString().Replace('_', ' '));
                }

                cmbBuscarPor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDatoABuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; //Evita que se reproduzca el sonido de advertencia de Windows
                e.SuppressKeyPress = true; //Evita que se propague el evento de teclado

                this.RealizarBusqueda();
            }
        }

        #endregion

        #region Evento Botones

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.RealizarBusqueda();
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

        private void RealizarBusqueda()
        {
            try
            {
                DataTable cliente = new DataTable();
                ConsultasSQL consulta = new ConsultasSQL(Utils.NombreBD);
                string buscarPor = "";

                this.FormatearTelefono();

                if (cmbBuscarPor.Text != String.Empty ||
                        txtDatoABuscar.Text != String.Empty)
                {
                    buscarPor = cmbBuscarPor.Text;

                    if (cmbBuscarPor.Text == "Orden De Service")
                    {
                        buscarPor = "OrdenService";
                    }

                    cliente = consulta.BuscarCliente(buscarPor, txtDatoABuscar.Text);
                }

                if (cliente.Rows.Count > 0)
                {
                    this.Nombre = cliente.Rows[0].ItemArray.GetValue(0).ToString();
                    this.Apellido = cliente.Rows[0].ItemArray.GetValue(1).ToString();
                    this.Telefono = cliente.Rows[0].ItemArray.GetValue(2).ToString();
                    this.Localidad = cliente.Rows[0].ItemArray.GetValue(3).ToString();
                    this.Domicilio = cliente.Rows[0].ItemArray.GetValue(4).ToString();

                    this.Dispose();
                }
                else
                {
                    MessageBox.Show($"No se encontro ningun cliente con {cmbBuscarPor.Text}: {txtDatoABuscar.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string FormatearTelefono()
        {
            try
            {
                string numeroFormateado = txtDatoABuscar.Text;

                if (txtDatoABuscar.Text.Length == 10 || !txtDatoABuscar.Text.Contains("-")) // Verificar si tiene 10 dígitos o si contiene - en el string
                {
                    if (txtDatoABuscar.Text.StartsWith("11"))
                    {
                        numeroFormateado = txtDatoABuscar.Text.Substring(0, 2) + "-" + txtDatoABuscar.Text.Substring(2, 4) + "-" + txtDatoABuscar.Text.Substring(6, 4);
                    }
                }

                txtDatoABuscar.Text = numeroFormateado;

                return numeroFormateado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
