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
        int id = 0;
        DatosA datos = new DatosA();
        bool updating = false;
        public frmRegistrar()
        {
            InitializeComponent();
        }

        public frmRegistrar(int id, string nc, string nombre, string ap, string am, string grupo)
        {
            InitializeComponent();
            this.id = id;
            txtNControl.Text = nc;
            txtNombre.Text = nombre;
            txtPaterno.Text = ap;
            txtMaterno.Text = am;
            cmbGrupo.Text = grupo;
            updating = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (updating == false)
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
            else
            {
                {
                    bool resultado = datos.ejecutarComando(
                       $"UPDATE AlumnosLista SET nombre='{txtNombre.Text}',apPaterno='{txtPaterno.Text}',apMaterno='{txtMaterno.Text}',grupo='{cmbGrupo.Text}' " +
                       $"WHERE idAlumno='{id}'");
                    if (resultado)
                    {
                        MessageBox.Show("Alumno actualizado correctamente");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar Alumno");
                    }
                }
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
