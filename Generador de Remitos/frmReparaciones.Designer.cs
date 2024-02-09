namespace Generador_de_Remitos
{
    partial class frmReparaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReparaciones));
            this.dgvReparaciones = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGarantia = new System.Windows.Forms.Button();
            this.btnRemito = new System.Windows.Forms.Button();
            this.OrdenService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApellidoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TelefonoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalidadCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DomicilioCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarcaEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModeloEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumSerieEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MotivoFallaEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObservacionesEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccesoriosEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReparacionAEfectuar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorReparacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Confirmado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reparado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscarOrdenService = new System.Windows.Forms.Button();
            this.txtBuscarOrdenService = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReparaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReparaciones
            // 
            this.dgvReparaciones.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvReparaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReparaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrdenService,
            this.FechaIngreso,
            this.NombreCliente,
            this.ApellidoCliente,
            this.TelefonoCliente,
            this.LocalidadCliente,
            this.DomicilioCliente,
            this.MarcaEquipo,
            this.ModeloEquipo,
            this.NumSerieEquipo,
            this.MotivoFallaEquipo,
            this.ObservacionesEquipo,
            this.AccesoriosEquipo,
            this.ReparacionAEfectuar,
            this.ValorReparacion,
            this.Confirmado,
            this.Reparado,
            this.FechaEntrega});
            this.dgvReparaciones.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvReparaciones.Location = new System.Drawing.Point(9, 48);
            this.dgvReparaciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvReparaciones.Name = "dgvReparaciones";
            this.dgvReparaciones.RowHeadersWidth = 51;
            this.dgvReparaciones.RowTemplate.Height = 24;
            this.dgvReparaciones.Size = new System.Drawing.Size(988, 520);
            this.dgvReparaciones.TabIndex = 0;
            this.dgvReparaciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReparaciones_CellClick);
            this.dgvReparaciones.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReparaciones_CellFormatting);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(803, 572);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(188, 32);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(611, 572);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(188, 32);
            this.btnModificar.TabIndex = 3;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGarantia
            // 
            this.btnGarantia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGarantia.Location = new System.Drawing.Point(201, 572);
            this.btnGarantia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGarantia.Name = "btnGarantia";
            this.btnGarantia.Size = new System.Drawing.Size(188, 32);
            this.btnGarantia.TabIndex = 2;
            this.btnGarantia.Text = "Crear Garantia";
            this.btnGarantia.UseVisualStyleBackColor = true;
            this.btnGarantia.Click += new System.EventHandler(this.btnGarantia_Click);
            // 
            // btnRemito
            // 
            this.btnRemito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemito.Location = new System.Drawing.Point(9, 572);
            this.btnRemito.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemito.Name = "btnRemito";
            this.btnRemito.Size = new System.Drawing.Size(188, 32);
            this.btnRemito.TabIndex = 1;
            this.btnRemito.Text = "Crear Remito";
            this.btnRemito.UseVisualStyleBackColor = true;
            this.btnRemito.Click += new System.EventHandler(this.btnRemito_Click);
            // 
            // OrdenService
            // 
            this.OrdenService.HeaderText = "Orden de Service";
            this.OrdenService.MinimumWidth = 6;
            this.OrdenService.Name = "OrdenService";
            this.OrdenService.ReadOnly = true;
            this.OrdenService.Width = 125;
            // 
            // FechaIngreso
            // 
            this.FechaIngreso.HeaderText = "Fecha de Ingreso";
            this.FechaIngreso.MinimumWidth = 6;
            this.FechaIngreso.Name = "FechaIngreso";
            this.FechaIngreso.ReadOnly = true;
            this.FechaIngreso.Width = 125;
            // 
            // NombreCliente
            // 
            this.NombreCliente.HeaderText = "Nombre";
            this.NombreCliente.MinimumWidth = 6;
            this.NombreCliente.Name = "NombreCliente";
            this.NombreCliente.ReadOnly = true;
            this.NombreCliente.Width = 125;
            // 
            // ApellidoCliente
            // 
            this.ApellidoCliente.HeaderText = "Apellido";
            this.ApellidoCliente.MinimumWidth = 6;
            this.ApellidoCliente.Name = "ApellidoCliente";
            this.ApellidoCliente.ReadOnly = true;
            this.ApellidoCliente.Width = 125;
            // 
            // TelefonoCliente
            // 
            this.TelefonoCliente.HeaderText = "Telefono";
            this.TelefonoCliente.MinimumWidth = 6;
            this.TelefonoCliente.Name = "TelefonoCliente";
            this.TelefonoCliente.ReadOnly = true;
            this.TelefonoCliente.Width = 125;
            // 
            // LocalidadCliente
            // 
            this.LocalidadCliente.HeaderText = "Localidad";
            this.LocalidadCliente.MinimumWidth = 6;
            this.LocalidadCliente.Name = "LocalidadCliente";
            this.LocalidadCliente.ReadOnly = true;
            this.LocalidadCliente.Width = 125;
            // 
            // DomicilioCliente
            // 
            this.DomicilioCliente.HeaderText = "Domicilio";
            this.DomicilioCliente.MinimumWidth = 6;
            this.DomicilioCliente.Name = "DomicilioCliente";
            this.DomicilioCliente.ReadOnly = true;
            this.DomicilioCliente.Width = 125;
            // 
            // MarcaEquipo
            // 
            this.MarcaEquipo.HeaderText = "Marca";
            this.MarcaEquipo.MinimumWidth = 6;
            this.MarcaEquipo.Name = "MarcaEquipo";
            this.MarcaEquipo.ReadOnly = true;
            this.MarcaEquipo.Width = 125;
            // 
            // ModeloEquipo
            // 
            this.ModeloEquipo.HeaderText = "Modelo";
            this.ModeloEquipo.MinimumWidth = 6;
            this.ModeloEquipo.Name = "ModeloEquipo";
            this.ModeloEquipo.ReadOnly = true;
            this.ModeloEquipo.Width = 125;
            // 
            // NumSerieEquipo
            // 
            this.NumSerieEquipo.HeaderText = "Numero de Serie";
            this.NumSerieEquipo.MinimumWidth = 6;
            this.NumSerieEquipo.Name = "NumSerieEquipo";
            this.NumSerieEquipo.ReadOnly = true;
            this.NumSerieEquipo.Width = 125;
            // 
            // MotivoFallaEquipo
            // 
            this.MotivoFallaEquipo.HeaderText = "Motivo de falla";
            this.MotivoFallaEquipo.MinimumWidth = 6;
            this.MotivoFallaEquipo.Name = "MotivoFallaEquipo";
            this.MotivoFallaEquipo.ReadOnly = true;
            this.MotivoFallaEquipo.Width = 125;
            // 
            // ObservacionesEquipo
            // 
            this.ObservacionesEquipo.HeaderText = "Observaciones";
            this.ObservacionesEquipo.MinimumWidth = 6;
            this.ObservacionesEquipo.Name = "ObservacionesEquipo";
            this.ObservacionesEquipo.ReadOnly = true;
            this.ObservacionesEquipo.Width = 125;
            // 
            // AccesoriosEquipo
            // 
            this.AccesoriosEquipo.HeaderText = "Accesorios";
            this.AccesoriosEquipo.MinimumWidth = 6;
            this.AccesoriosEquipo.Name = "AccesoriosEquipo";
            this.AccesoriosEquipo.ReadOnly = true;
            this.AccesoriosEquipo.Width = 125;
            // 
            // ReparacionAEfectuar
            // 
            this.ReparacionAEfectuar.HeaderText = "Reparacion A Efectuar";
            this.ReparacionAEfectuar.MinimumWidth = 6;
            this.ReparacionAEfectuar.Name = "ReparacionAEfectuar";
            this.ReparacionAEfectuar.ReadOnly = true;
            this.ReparacionAEfectuar.Width = 125;
            // 
            // ValorReparacion
            // 
            this.ValorReparacion.HeaderText = "Valor Reparacion";
            this.ValorReparacion.MinimumWidth = 6;
            this.ValorReparacion.Name = "ValorReparacion";
            this.ValorReparacion.ReadOnly = true;
            this.ValorReparacion.Width = 125;
            // 
            // Confirmado
            // 
            this.Confirmado.HeaderText = "Confirmado";
            this.Confirmado.MinimumWidth = 6;
            this.Confirmado.Name = "Confirmado";
            this.Confirmado.ReadOnly = true;
            this.Confirmado.Width = 125;
            // 
            // Reparado
            // 
            this.Reparado.HeaderText = "Reparado";
            this.Reparado.MinimumWidth = 6;
            this.Reparado.Name = "Reparado";
            this.Reparado.ReadOnly = true;
            this.Reparado.Width = 125;
            // 
            // FechaEntrega
            // 
            this.FechaEntrega.HeaderText = "FechaEntrega";
            this.FechaEntrega.MinimumWidth = 6;
            this.FechaEntrega.Name = "FechaEntrega";
            this.FechaEntrega.ReadOnly = true;
            this.FechaEntrega.Width = 125;
            // 
            // btnBuscarOrdenService
            // 
            this.btnBuscarOrdenService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarOrdenService.Location = new System.Drawing.Point(898, 11);
            this.btnBuscarOrdenService.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscarOrdenService.Name = "btnBuscarOrdenService";
            this.btnBuscarOrdenService.Size = new System.Drawing.Size(99, 31);
            this.btnBuscarOrdenService.TabIndex = 5;
            this.btnBuscarOrdenService.Text = "Buscar";
            this.btnBuscarOrdenService.UseVisualStyleBackColor = true;
            this.btnBuscarOrdenService.Click += new System.EventHandler(this.btnBuscarOrdenService_Click);
            // 
            // txtBuscarOrdenService
            // 
            this.txtBuscarOrdenService.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarOrdenService.Location = new System.Drawing.Point(699, 11);
            this.txtBuscarOrdenService.Name = "txtBuscarOrdenService";
            this.txtBuscarOrdenService.Size = new System.Drawing.Size(194, 31);
            this.txtBuscarOrdenService.TabIndex = 1;
            this.txtBuscarOrdenService.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmReparaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1000, 615);
            this.Controls.Add(this.txtBuscarOrdenService);
            this.Controls.Add(this.btnBuscarOrdenService);
            this.Controls.Add(this.btnRemito);
            this.Controls.Add(this.btnGarantia);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvReparaciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReparaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reparaciones";
            this.Load += new System.EventHandler(this.frmReparaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReparaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReparaciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnGarantia;
        private System.Windows.Forms.Button btnRemito;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdenService;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaIngreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApellidoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn TelefonoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalidadCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DomicilioCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarcaEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModeloEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumSerieEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MotivoFallaEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObservacionesEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccesoriosEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReparacionAEfectuar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorReparacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Confirmado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reparado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaEntrega;
        private System.Windows.Forms.Button btnBuscarOrdenService;
        private System.Windows.Forms.TextBox txtBuscarOrdenService;
    }
}