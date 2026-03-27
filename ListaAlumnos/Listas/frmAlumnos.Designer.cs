namespace ListaAlumnos.Listas
{
    partial class frmAlumnos
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            btnGuardar = new Button();
            btnBuscar = new Button();
            cmbBuscar = new ComboBox();
            txtNControl = new TextBox();
            dtpBuscar = new DateTimePicker();
            lblGrupo = new Label();
            lblNControl = new Label();
            lblFecha = new Label();
            dgvAlumnos = new DataGridView();
            ctmEliminar = new ContextMenuStrip(components);
            eliminarAlumnoToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
            ctmEliminar.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnGuardar);
            splitContainer1.Panel1.Controls.Add(btnBuscar);
            splitContainer1.Panel1.Controls.Add(cmbBuscar);
            splitContainer1.Panel1.Controls.Add(txtNControl);
            splitContainer1.Panel1.Controls.Add(dtpBuscar);
            splitContainer1.Panel1.Controls.Add(lblGrupo);
            splitContainer1.Panel1.Controls.Add(lblNControl);
            splitContainer1.Panel1.Controls.Add(lblFecha);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvAlumnos);
            splitContainer1.Size = new Size(801, 409);
            splitContainer1.SplitterDistance = 135;
            splitContainer1.TabIndex = 6;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(375, 97);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(272, 97);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 12;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += button1_Click;
            // 
            // cmbBuscar
            // 
            cmbBuscar.FormattingEnabled = true;
            cmbBuscar.Location = new Point(546, 59);
            cmbBuscar.Name = "cmbBuscar";
            cmbBuscar.Size = new Size(121, 23);
            cmbBuscar.TabIndex = 11;
            cmbBuscar.SelectedIndexChanged += cmbBuscar_SelectedIndexChanged;
            // 
            // txtNControl
            // 
            txtNControl.Location = new Point(288, 59);
            txtNControl.Name = "txtNControl";
            txtNControl.Size = new Size(192, 23);
            txtNControl.TabIndex = 10;
            // 
            // dtpBuscar
            // 
            dtpBuscar.Location = new Point(52, 59);
            dtpBuscar.Name = "dtpBuscar";
            dtpBuscar.Size = new Size(200, 23);
            dtpBuscar.TabIndex = 9;
            dtpBuscar.ValueChanged += dtpBuscar_ValueChanged;
            // 
            // lblGrupo
            // 
            lblGrupo.AutoSize = true;
            lblGrupo.Location = new Point(587, 28);
            lblGrupo.Name = "lblGrupo";
            lblGrupo.Size = new Size(40, 15);
            lblGrupo.TabIndex = 8;
            lblGrupo.Text = "Grupo";
            // 
            // lblNControl
            // 
            lblNControl.AutoSize = true;
            lblNControl.Location = new Point(322, 28);
            lblNControl.Name = "lblNControl";
            lblNControl.Size = new Size(108, 15);
            lblNControl.TabIndex = 7;
            lblNControl.Text = "Numero de control";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(117, 28);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(38, 15);
            lblFecha.TabIndex = 6;
            lblFecha.Text = "Fecha";
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlumnos.ContextMenuStrip = ctmEliminar;
            dgvAlumnos.Location = new Point(0, 3);
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.Size = new Size(801, 264);
            dgvAlumnos.TabIndex = 0;
            dgvAlumnos.CellContentDoubleClick += dgvAlumnos_CellContentDoubleClick;
            dgvAlumnos.CellValueChanged += dgvAlumnos_CellValueChanged;
            dgvAlumnos.CurrentCellDirtyStateChanged += dgvAlumnos_CurrentCellDirtyStateChanged;
            // 
            // ctmEliminar
            // 
            ctmEliminar.Items.AddRange(new ToolStripItem[] { eliminarAlumnoToolStripMenuItem });
            ctmEliminar.Name = "ctmEliminar";
            ctmEliminar.Size = new Size(181, 48);
            // 
            // eliminarAlumnoToolStripMenuItem
            // 
            eliminarAlumnoToolStripMenuItem.Name = "eliminarAlumnoToolStripMenuItem";
            eliminarAlumnoToolStripMenuItem.Size = new Size(180, 22);
            eliminarAlumnoToolStripMenuItem.Text = "Eliminar alumno";
            eliminarAlumnoToolStripMenuItem.Click += eliminarAlumnoToolStripMenuItem_Click;
            // 
            // frmAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 409);
            Controls.Add(splitContainer1);
            KeyPreview = true;
            Name = "frmAlumnos";
            Text = "Asistencia";
            Load += frmBuscar_Load;
            KeyDown += frmAlumnos_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
            ctmEliminar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btnBuscar;
        private ComboBox cmbBuscar;
        private TextBox txtNControl;
        private DateTimePicker dtpBuscar;
        private Label lblGrupo;
        private Label lblNControl;
        private Label lblFecha;
        private DataGridView dgvAlumnos;
        private ContextMenuStrip ctmEliminar;
        private ToolStripMenuItem eliminarAlumnoToolStripMenuItem;
        private Button btnGuardar;
    }
}