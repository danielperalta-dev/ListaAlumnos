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

    public partial class frmAlumnos : Form
    {
        DatosA datos = new DatosA();
        public frmAlumnos()
        {
            InitializeComponent();
        }

        private void txtGrupo_Click(object sender, EventArgs e)
        {

        }
        void cargarAsistenciaFecha()
        {
            string fecha = dtpBuscar.Value.ToString("yyyy-MM-dd");

            DataSet ds = datos.ejecutar(
                $"SELECT e.nControl, e.nombre, e.apPaterno, e.grupo, " +
                $"IFNULL(a.asistio,0) AS asistio " +
                $"FROM AlumnosLista e " +
                $"LEFT JOIN asistencia a ON e.nControl = a.nControl " +
                $"AND a.fecha = '{fecha}'");

            if (ds != null && ds.Tables.Count > 0)
            {
                dgvAlumnos.DataSource = ds.Tables[0];
                if (dgvAlumnos.Columns.Contains("asistio"))
                {
                    int index = dgvAlumnos.Columns["asistio"].Index;
                    dgvAlumnos.Columns.Remove("asistio");

                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.Name = "asistio";
                    chk.HeaderText = "asistio";
                    chk.DataPropertyName = "asistio";

                    dgvAlumnos.Columns.Insert(index, chk);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fecha = dtpBuscar.Value.ToString("yyyy-MM-dd");
            string nc = txtNControl.Text.Trim();

            DataSet ds = datos.ejecutar(
                $"SELECT e.nControl, e.nombre, e.apPaterno, e.grupo, " +
                $"IFNULL(a.asistio,0) AS asistio " +
                $"FROM AlumnosLista e " +
                $"LEFT JOIN asistencia a ON e.nControl = a.nControl " +
                $"AND a.fecha = '{fecha}' " +
                $"WHERE e.nControl = '{nc}'");

            if (ds != null && ds.Tables.Count > 0)
            {
                dgvAlumnos.DataSource = ds.Tables[0];
                dgvAlumnos.Columns["asistio"].CellTemplate = new DataGridViewCheckBoxCell();
            }
        }

        private void dtpBuscar_ValueChanged(object sender, EventArgs e)
        {
            cargarAsistenciaFecha();
        }

        void cargarEstudiantes()
        {
            DataSet ds = datos.ejecutar(
                $"SELECT e.nControl, e.nombre, e.apPaterno, e.grupo, " +
                $"IFNULL(a.asistio,0) AS asistio " +
                $"FROM AlumnosLista e " +
                $"LEFT JOIN asistencia a ON e.nControl = a.nControl " +
                $"AND a.fecha = '{dtpBuscar.Value:yyyy-MM-dd}' " +
                $"WHERE e.grupo = '{cmbBuscar.Text}'");

            dgvAlumnos.DataSource = ds.Tables[0];
            if (dgvAlumnos.Columns.Contains("asistio"))
            {
                int index = dgvAlumnos.Columns["asistio"].Index;
                dgvAlumnos.Columns.Remove("asistio");

                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.Name = "asistio";
                chk.HeaderText = "asistio";
                chk.DataPropertyName = "asistio";

                dgvAlumnos.Columns.Insert(index, chk);
            }
        }
        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarEstudiantes();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmBuscar_Load(object sender, EventArgs e)
        {
            cmbBuscar.Items.Add("A");
            cmbBuscar.Items.Add("B");
            cmbBuscar.Items.Add("C");

            cmbBuscar.SelectedIndex = 0;

            cargarEstudiantes();
            dgvAlumnos.CellValueChanged += dgvAlumnos_CellValueChanged;
            dgvAlumnos.CurrentCellDirtyStateChanged += dgvAlumnos_CurrentCellDirtyStateChanged;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nc = dgvAlumnos.CurrentRow.Cells["nControl"].Value.ToString();
            string fecha = dtpBuscar.Value.ToString("yyyy-MM-dd");

            if (MessageBox.Show(
                "Deseas eliminar la asistencia del alumno: " + nc,
                "Sistema",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool f = datos.ejecutarComando(
                    $"DELETE FROM asistencia WHERE nControl='{nc}' AND fecha='{fecha}'");

                if (f == true)
                {
                    MessageBox.Show("Asistencia eliminada con éxito", "Sistema");
                    cargarEstudiantes();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la asistencia", "Sistema");
                }
            }
        }

        private void dgvAlumnos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvAlumnos.Columns[e.ColumnIndex].Name == "asistio")
            {
                string nc = dgvAlumnos.Rows[e.RowIndex].Cells["nControl"].Value.ToString();
                string fecha = dtpBuscar.Value.ToString("yyyy-MM-dd");

                bool asistio;
                int valor;

                object v = dgvAlumnos.Rows[e.RowIndex].Cells["asistio"].Value;

                if (v == null || v == DBNull.Value)
                {
                    asistio = false;
                }
                else
                {
                    asistio = Convert.ToBoolean(v);
                }

                if (asistio == true)
                {
                    valor = 1;
                }
                else
                {
                    valor = 0;
                }

                DataSet ds = datos.ejecutar(
                    $"SELECT * FROM asistencia WHERE nControl='{nc}' AND fecha='{fecha}'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    datos.ejecutarComando(
                        $"UPDATE asistencia SET asistio={valor} WHERE nControl='{nc}' AND fecha='{fecha}'");
                }
                else
                {
                    datos.ejecutarComando(
                        $"INSERT INTO asistencia (nControl,fecha,asistio) VALUES ('{nc}','{fecha}',{valor})");
                }
            }
        }

        private void dgvAlumnos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAlumnos.IsCurrentCellDirty)
            {
                dgvAlumnos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void eliminarAlumnoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Obtenemos el número de control y nombre para la confirmación
            string nc = dgvAlumnos.CurrentRow.Cells["nControl"].Value.ToString();
            string nombre = dgvAlumnos.CurrentRow.Cells["nombre"].Value.ToString();

            // Confirmación de seguridad
            DialogResult result = MessageBox.Show(
                $"¿ESTÁS SEGURO? Esta acción eliminará permanentemente al alumno {nombre} ({nc}) " +
                "y TODOS sus registros de asistencia históricos.",
                "Advertencia de Eliminación Total",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Eliminamos primero de la tabla de asistencias (por integridad referencial)
                    datos.ejecutarComando($"DELETE FROM asistencia WHERE nControl = '{nc}'");

                    // Eliminamos de la tabla principal de alumnos
                    bool exito = datos.ejecutarComando($"DELETE FROM AlumnosLista WHERE nControl = '{nc}'");

                    if (exito)
                    {
                        MessageBox.Show("El alumno y su historial han sido eliminados correctamente.", "Sistema");
                        cargarEstudiantes(); // Refrescamos el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro del alumno.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Error Crítico");
                }
            }
        }
    }
}
