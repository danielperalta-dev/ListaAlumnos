using ListaAlumnos.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListaAlumnos.Listas
{
    public partial class frmRegistrar : Form
    {
        DatosA datos = new DatosA();
        public frmRegistrar()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool resultado = datos.ejecutarComando(
               $"INSERT INTO AlumnosLista (nControl,nombre,apPaterno,apMaterno,grupo) " +
               $"VALUES ('{txtNControl.Text}','{txtNombre.Text}','{txtPaterno.Text}','{txtMaterno.Text}','{cmbGrupo.Text}')");

            if (resultado)
            {
                MessageBox.Show("Alumno agregado correctamente");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al agregar Alumno");
            }
        }

        private void frmAsistencia_Load(object sender, EventArgs e)
        {
            cmbGrupo.Items.Add("A");
            cmbGrupo.Items.Add("B");
            cmbGrupo.Items.Add("C");

            cmbGrupo.SelectedIndex = 0;
        }
    }
}
