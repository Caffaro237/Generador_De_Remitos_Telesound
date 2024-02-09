using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Generador_de_Remitos
{
    public partial class frmRemito : Form
    {
        private bool offline;
        private readonly string nombrePrograma;
        private readonly string nombreProgramaExe;
        private string pathPrograma;
        private string rutaExe;

        public frmRemito()
        {
            InitializeComponent();
            offline = false;
            nombrePrograma = "LogMeIn Hamachi";
            nombreProgramaExe = "hamachi-2-ui.exe";
            pathPrograma = "";
            rutaExe = "";
        }

        #region Eventos

        private void Remito_Load(object sender, EventArgs e)
        {
            try
            {
                ConexionBD conexion = new ConexionBD(Utils.NombreBD);
                
                if (!conexion.TestConnection())
                {
                    offline = true;
                }
                else
                {
                    offline = false;
                }

                if (offline)
                {
                    this.InicializarFormularioOffline();

                    this.CargarComboBox();
                }
                else
                {
                    this.InicializarFormulario();

                    this.CargarComboBox();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true; // Cancela la acción del evento KeyPress
            }
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Verifica si el texto contiene "-" para determinar la longitud máxima permitida
                int maxLength = txtTelefono.Text.Contains("-") ? 12 : 10;

                // Limita la longitud del texto
                if (txtTelefono.TextLength > maxLength)
                {
                    txtTelefono.Text = txtTelefono.Text.Substring(0, maxLength);
                    txtTelefono.SelectionStart = maxLength;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos Botones

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                this.InicializarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultasSQL consultas = new ConsultasSQL(Utils.NombreBD);

                string camposVacios = CamposVacios();

                if (!string.IsNullOrEmpty(camposVacios))
                {
                    throw new Exception(camposVacios);
                }

                this.FormatearPalabras();

                this.FormatearTelefono();

                consultas.CargarCliente(lblOrdenService.Text,
                                        txtNombre.Text.Replace(" ", ""),
                                        txtApellido.Text.Replace(" ", ""),
                                        txtTelefono.Text,
                                        txtLocalidad.Text,
                                        txtDomicilio.Text);

                consultas.CargarEquipo(lblOrdenService.Text,
                                        txtNumSerie.Text,
                                        cmbMarca.Text,
                                        txtModelo.Text,
                                        cmbAccesorios.Text,
                                        rtbMotivo.Text,
                                        rtbObservaciones.Text);

                consultas.GenerarReparacion();

                btnCargar.Enabled = false;

                MessageBox.Show("Remito cargado con exito", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmBuscarCliente buscarCliente = new frmBuscarCliente();

                buscarCliente.ShowDialog();

                txtNombre.Text = buscarCliente.Nombre;
                txtApellido.Text = buscarCliente.Apellido;
                txtTelefono.Text = buscarCliente.Telefono;
                txtLocalidad.Text = buscarCliente.Localidad;
                txtDomicilio.Text = buscarCliente.Domicilio;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarReparaciones_Click(object sender, EventArgs e)
        {
            try
            {
                frmReparaciones reparaciones = new frmReparaciones();

                reparaciones.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tabla = new DataTable();

                string camposVacios = CamposVacios();

                if (!string.IsNullOrEmpty(camposVacios))
                {
                    throw new Exception(camposVacios);
                }

                this.FormatearPalabras();

                this.FormatearTelefono();

                tabla = this.CrearTabla();

                //El parametro true indica que se trata de un Remito y no una Garantia
                ExcelManager excelManager = new ExcelManager(true);

                excelManager.GenerarExcel(tabla);


                // Ruta del archivo que se quiere abrir
                string pathCompleto = Utils.PathExcel + Utils.NombreExcelRemito;

                // Abre el archivo con la aplicación predeterminada
                Process.Start(pathCompleto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void lblOffline_Click(object sender, EventArgs e)
        {
            OnLoad(EventArgs.Empty);
        }

        #endregion

        #region Metodos


        private static string ObtenerPathPrograma(string nombrePrograma)
        {
            // La ruta en el Registro de Windows donde se almacenan las instalaciones de programas
            string rutaRegistro = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(rutaRegistro))
            {
                if (key != null)
                {
                    foreach (string subkeyName in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkeyName))
                        {
                            // Buscar el programa por su nombre
                            object displayName = subkey.GetValue("DisplayName");

                            if (displayName != null && displayName.ToString() == nombrePrograma)
                            {
                                // Si se encuentra, obtener el valor de la clave "InstallLocation"
                                object installLocation = subkey.GetValue("InstallLocation");

                                if (installLocation != null)
                                {
                                    return installLocation.ToString();
                                }
                            }
                        }
                    }
                }
            }

            // Si no se encuentra el programa o el path, devolver null
            return null;
        }

        private static void EjecutarExe(string rutaExe)
        {
            try
            {
                // Crear un proceso de inicio
                Process proceso = new Process();

                // Configurar las propiedades del proceso
                proceso.StartInfo.FileName = rutaExe;

                // Iniciar el proceso
                proceso.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar el archivo ejecutable: {ex.Message}");
            }
        }

        private DataTable CrearTabla()
        {
            DataTable dataTable = new DataTable();

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
            DataRow dataRow = dataTable.NewRow();
            dataRow["OrdenService"] = lblOrdenService.Text;
            dataRow["NombreYApellido"] = txtNombre.Text + " " + txtApellido.Text;
            dataRow["Telefono"] = txtTelefono.Text;
            dataRow["Localidad"] = txtLocalidad.Text;
            dataRow["Domicilio"] = txtDomicilio.Text;
            dataRow["NumSerie"] = txtNumSerie.Text;
            dataRow["Marca"] = cmbMarca.Text;
            dataRow["Modelo"] = txtModelo.Text;
            dataRow["Accesorios"] = cmbAccesorios.Text;
            dataRow["MotivoFalla"] = rtbMotivo.Text;
            dataRow["Observaciones"] = rtbObservaciones.Text;
            dataTable.Rows.Add(dataRow);

            return dataTable;
        }

        private void FormatearPalabras()
        {
            try
            {
                txtNombre.Text = this.PasarPrimerLetraAMayuscula(txtNombre.Text, true);
                txtApellido.Text = this.PasarPrimerLetraAMayuscula(txtApellido.Text, true);
                txtLocalidad.Text = this.PasarPrimerLetraAMayuscula(txtLocalidad.Text, true);
                txtDomicilio.Text = this.PasarPrimerLetraAMayuscula(txtDomicilio.Text, true);
                rtbMotivo.Text = this.PasarPrimerLetraAMayuscula(rtbMotivo.Text, false);
                rtbObservaciones.Text = this.PasarPrimerLetraAMayuscula(rtbObservaciones.Text, false);

                txtModelo.Text = txtModelo.Text.ToUpper();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string PasarPrimerLetraAMayuscula(string texto, bool todoElTexto)
        {
            try
            {
                if (todoElTexto)
                {
                    texto = texto.ToLower();

                    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(texto);
                }
                else
                {
                    texto = texto.ToLower();

                    return texto.Substring(0, 1).ToUpper() + texto.Substring(1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InicializarFormularioOffline()
        {
            try
            {
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
                txtTelefono.Text = string.Empty;
                txtLocalidad.Text = string.Empty;
                txtDomicilio.Text = string.Empty;
                txtNumSerie.Text = string.Empty;
                cmbMarca.Text = string.Empty;
                txtModelo.Text = string.Empty;
                cmbAccesorios.Text = string.Empty;
                rtbMotivo.Text = string.Empty;
                rtbObservaciones.Text = string.Empty;


                cmbMarca.Enabled = true;
                txtModelo.Enabled = true;


                btnCargar.Enabled = false;
                btnBuscarCliente.Enabled = false;
                btnConsultarReparaciones.Enabled = false;


                lblTituloOrdenService.Hide();
                lblOrdenService.Hide();


                lblOffline.Text = "Presione aqui para\nreintentar conexion";

                MessageBox.Show("No se conecto al servidor\nRevisar Hamachi", "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                lblOffline.Text = "Presione aqui para\nreintentar conexion";

                // Llamada a la función para obtener el path del programa
                pathPrograma = ObtenerPathPrograma(nombrePrograma);

                rutaExe = pathPrograma + nombreProgramaExe;

                if (pathPrograma != null)
                {
                    // Llamada a la función para ejecutar el archivo ejecutable
                    EjecutarExe(rutaExe);
                }
                else
                {
                    throw new Exception($"No se encontro el programa {nombrePrograma}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InicializarFormulario()
        {
            try
            {
                btnCargar.Enabled = true;
                btnBuscarCliente.Enabled = true;
                btnConsultarReparaciones.Enabled = true;

                lblTituloOrdenService.Show();
                lblOrdenService.Show();


                DataTable tabla = new DataTable();
                ConsultasSQL consulta = new ConsultasSQL(Utils.NombreBD);

                tabla = consulta.ObtenerUltimaOrdenService();

                int.TryParse(tabla.Rows[0].ItemArray.GetValue(0).ToString(), out int numeroOrden);

                if(numeroOrden > 0)
                {
                    numeroOrden += 1;
                }
                else
                {
                    numeroOrden = 1000;
                }

                lblOffline.Text = string.Empty;

                lblOrdenService.Text = numeroOrden.ToString();

                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
                txtTelefono.Text = string.Empty;
                txtLocalidad.Text = string.Empty;
                txtDomicilio.Text = string.Empty;
                txtNumSerie.Text = string.Empty;
                cmbMarca.Text = string.Empty;
                txtModelo.Text = string.Empty;
                cmbAccesorios.Text = string.Empty;
                rtbMotivo.Text = string.Empty;
                rtbObservaciones.Text = string.Empty;

                btnCargar.Enabled = true;
                cmbMarca.Enabled = true;
                txtModelo.Enabled = true;
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
                string numeroFormateado = txtTelefono.Text;

                if (txtTelefono.Text.Length == 10 || !txtTelefono.Text.Contains("-")) // Verificar si tiene 10 dígitos o si contiene - en el string
                {
                    if (txtTelefono.Text.StartsWith("11"))
                    {
                        numeroFormateado = txtTelefono.Text.Substring(0, 2) + "-" + txtTelefono.Text.Substring(2, 4) + "-" + txtTelefono.Text.Substring(6, 4);
                    }
                }

                txtTelefono.Text = numeroFormateado;

                return numeroFormateado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboBox()
        {
            try
            {
                foreach (Utils.Marca marca in Enum.GetValues(typeof(Utils.Marca)))
                {
                    cmbMarca.Items.Add(marca.ToString().Replace('_', ' '));
                }

                foreach (Utils.Accesorios accesorios in Enum.GetValues(typeof(Utils.Accesorios)))
                {
                    cmbAccesorios.Items.Add(accesorios.ToString().Replace('_', ' '));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CamposVacios()
        {
            try
            {
                string[] camposRequeridos;
                string camposVacios = "";

                if (cmbMarca.Enabled == true && txtModelo.Enabled == true)
                {
                    camposRequeridos = new string[]
                    {
                        "Nombre", 
                        "Apellido", 
                        "Telefono", 
                        "Localidad", 
                        "Domicilio", 
                        "Tipo", 
                        "Marca",
                        "Modelo",
                        "Accesorios", 
                        "Motivo", 
                        "Observaciones"
                    };
                }
                else
                {
                    camposRequeridos = new string[]
                    {
                        "Nombre",
                        "Apellido",
                        "Telefono",
                        "Localidad",
                        "Domicilio",
                        "Tipo",
                        "Accesorios",
                        "Motivo",
                        "Observaciones"
                    };
                }

                foreach (string campo in camposRequeridos)
                {
                    TextBox textBox = Controls.Find("txt" + campo, true).FirstOrDefault() as TextBox;
                    ComboBox comboBox = Controls.Find("cmb" + campo, true).FirstOrDefault() as ComboBox;
                    RichTextBox richTextBox = Controls.Find("rtb" + campo, true).FirstOrDefault() as RichTextBox;

                    if ((textBox != null && string.IsNullOrEmpty(textBox.Text)) ||
                        (comboBox != null && string.IsNullOrEmpty(comboBox.Text)) ||
                        (richTextBox != null && string.IsNullOrEmpty(richTextBox.Text)))
                    {
                        camposVacios += campo + "\n";
                    }
                }

                // Si hay campos vacíos, muestra el mensaje
                if (!string.IsNullOrEmpty(camposVacios))
                {
                    camposVacios = "Los siguientes campos están vacíos:\n" + camposVacios;
                }

                return camposVacios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
