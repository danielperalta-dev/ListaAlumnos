namespace ListaAlumnos.Listas
{
    partial class frmAsistencia
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
            txtNombre = new TextBox();
            txtPaterno = new TextBox();
            txtNControl = new TextBox();
            txtMaterno = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbGrupo = new ComboBox();
            label5 = new Label();
            btnAgregar = new Button();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(55, 58);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(174, 23);
            txtNombre.TabIndex = 0;
            // 
            // txtPaterno
            // 
            txtPaterno.Location = new Point(55, 126);
            txtPaterno.Name = "txtPaterno";
            txtPaterno.Size = new Size(174, 23);
            txtPaterno.TabIndex = 1;
            // 
            // txtNControl
            // 
            txtNControl.Location = new Point(55, 254);
            txtNControl.Name = "txtNControl";
            txtNControl.Size = new Size(174, 23);
            txtNControl.TabIndex = 2;
            // 
            // txtMaterno
            // 
            txtMaterno.Location = new Point(55, 186);
            txtMaterno.Name = "txtMaterno";
            txtMaterno.Size = new Size(174, 23);
            txtMaterno.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 31);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 4;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 108);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 5;
            label2.Text = "Apellido paterno";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(65, 168);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 6;
            label3.Text = "Apellido materno";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(65, 236);
            label4.Name = "label4";
            label4.Size = new Size(108, 15);
            label4.TabIndex = 7;
            label4.Text = "Numero de control";
            // 
            // cmbGrupo
            // 
            cmbGrupo.FormattingEnabled = true;
            cmbGrupo.Location = new Point(55, 315);
            cmbGrupo.Name = "cmbGrupo";
            cmbGrupo.Size = new Size(121, 23);
            cmbGrupo.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(96, 297);
            label5.Name = "label5";
            label5.Size = new Size(40, 15);
            label5.TabIndex = 9;
            label5.Text = "Grupo";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(278, 168);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // frmAsistencia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(396, 372);
            Controls.Add(btnAgregar);
            Controls.Add(label5);
            Controls.Add(cmbGrupo);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMaterno);
            Controls.Add(txtNControl);
            Controls.Add(txtPaterno);
            Controls.Add(txtNombre);
            Name = "frmAsistencia";
            Text = "Registrar";
            Load += frmAsistencia_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtPaterno;
        private TextBox txtNControl;
        private TextBox txtMaterno;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox cmbGrupo;
        private Label label5;
        private Button btnAgregar;
    }
}