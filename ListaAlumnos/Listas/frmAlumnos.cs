using ListaAlumnos.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(codigoBuffer))
                {
                    marcarAsistenciaDirecta(codigoBuffer.Trim());
                    codigoBuffer = ""; // Limpiamos después de procesar
                    return true; // ESTO detiene el "clic" automático en los botones
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string fecha = dtpBuscar.Value.ToString("yyyy-MM-dd");
            string nc = txtNControl.Text.Trim();

            DataSet ds = datos.ejecutar(
                $"SELECT e.nControl, e.nombre, e.apPaterno, e.apMaterno, e.grupo, " +
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

        // Si cambiamos de grupo o de fecha, refrescamos la lista automáticamente
        private void dtpBuscar_ValueChanged(object sender, EventArgs e)
        {
            cargarEstudiantes();
            // Sacamos el foco del combo para que el escáner no mueva las opciones
            this.ActiveControl = null;
        }

        // Este método trae a todos los alumnos del grupo seleccionado
        void cargarEstudiantes()
        {
            // Usamos LEFT JOIN para que aparezcan TODOS los alumnos del grupo,
            // aunque todavía no tengan asistencia marcada en la tabla de 'asistencia'
            DataSet ds = datos.ejecutar(
                $"SELECT e.idAlumno, e.nControl, e.nombre, e.apPaterno, e.apMaterno, e.grupo, " +
                $"IFNULL(a.asistio,0) AS asistio " +
                $"FROM AlumnosLista e " +
                $"LEFT JOIN asistencia a ON e.nControl = a.nControl " +
                $"AND a.fecha = '{dtpBuscar.Value:yyyy-MM-dd}' " +
                $"WHERE e.grupo = '{cmbBuscar.Text}'");

            dgvAlumnos.DataSource = ds.Tables[0];
            // Si la columna 'asistio' existe, la convertimos en CheckBox para que sea visual
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

        // Si cambiamos de grupo o de fecha, refrescamos la lista automáticamente
        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarEstudiantes();
            // Sacamos el foco del combo para que el escáner no mueva las opciones
            this.ActiveControl = null;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Variable para ir guardando los números que entran del escáner
        string codigoBuffer = "";

        // Lógica principal del Escáner de Código de Barras
        void marcarAsistenciaDirecta(string nc)
        {
            string numControl = nc.Trim();
            string fechaHoy = dtpBuscar.Value.ToString("yyyy-MM-dd");
            bool encontradoEnGrupo = false; // Bandera de validación

            foreach (DataGridViewRow fila in dgvAlumnos.Rows)
            {
                // Verificamos si el número de control coincide con alguna fila de la tabla actual
                if (fila.Cells["nControl"].Value?.ToString() == numControl)
                {
                    encontradoEnGrupo = true; // ¡Lo encontramos en el grupo cargado!

                    // Validación anti-error DBNull para cambios manuales
                    bool yaTieneAsistencia = false;
                    if (fila.Cells["asistio"].Value != null && fila.Cells["asistio"].Value != DBNull.Value)
                    {
                        yaTieneAsistencia = Convert.ToBoolean(fila.Cells["asistio"].Value);
                    }

                    if (yaTieneAsistencia)
                    {
                        // Si ya tiene la palomita, no hacemos nada y salimos
                        codigoBuffer = "";
                        return;
                    }

                    // Si está en el grupo y no tiene asistencia, guardamos
                    datos.ejecutarComando($@"
                INSERT INTO asistencia (nControl, fecha, asistio) 
                VALUES ('{numControl}', '{fechaHoy}', 1) 
                ON DUPLICATE KEY UPDATE asistio=1");

                    // Marcamos la palomita visualmente
                    fila.Cells["asistio"].Value = true;
                    dgvAlumnos.CurrentCell = fila.Cells["nControl"];

                    codigoBuffer = "";
                    this.ActiveControl = null;
                    return;
                }
            }

            // SI TERMINA EL CICLO Y NO SE MARCÓ LA BANDERA:
            if (!encontradoEnGrupo)
            {
                MessageBox.Show($"El alumno con No. Control {numControl} NO pertenece al Grupo {cmbBuscar.Text}.",
                                "Grupo Incorrecto",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }

            codigoBuffer = "";
            this.ActiveControl = null;
        }

        private void frmBuscar_Load(object sender, EventArgs e)
        {
            // Llenamos el combo con los grupos disponibles
            cmbBuscar.Items.Add("A");
            cmbBuscar.Items.Add("B");
            cmbBuscar.Items.Add("C");

            cmbBuscar.SelectedIndex = 0;

            // Cargamos la lista en cuanto abre la ventana
            cargarEstudiantes();

            // Estos eventos detectan si alguien marca o desmarca un cuadrito manualmente
            dgvAlumnos.CellValueChanged += dgvAlumnos_CellValueChanged;
            dgvAlumnos.CurrentCellDirtyStateChanged += dgvAlumnos_CurrentCellDirtyStateChanged;
            this.ActiveControl = btnBuscar;
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
        }

        // Estos métodos aseguran que al marcar un CheckBox con el mouse, el programa se entere de inmediato
        private void dgvAlumnos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAlumnos.IsCurrentCellDirty)
            {
                // Esto obliga a la tabla a reconocer el clic de la palomita en el momento
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

        // Abrir Actualizacion del alumno con doble clic
        private void dgvAlumnos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmRegistrar alumno = new frmRegistrar(
                (Convert.ToInt32(dgvAlumnos.CurrentRow.Cells[0].Value)),
                dgvAlumnos.CurrentRow.Cells[1].Value.ToString(),
                dgvAlumnos.CurrentRow.Cells[2].Value.ToString(),
                dgvAlumnos.CurrentRow.Cells[3].Value.ToString(),
                dgvAlumnos.CurrentRow.Cells[4].Value.ToString(),
                dgvAlumnos.CurrentRow.Cells[5].Value.ToString()
            );
            alumno.ShowDialog();
            cargarEstudiantes();
        }

        //Detecta las teclas del escáner(o teclado) sin necesidad de un TextBox enfocado
        private void frmAlumnos_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                string tecla = e.KeyCode.ToString().Replace("D", "").Replace("NumPad", "");
                codigoBuffer += tecla;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Si el buffer tiene algo, es el escáner
                if (!string.IsNullOrEmpty(codigoBuffer))
                {
                    marcarAsistenciaDirecta(codigoBuffer.Trim());

                    codigoBuffer = ""; // Limpiamos
                }
            }
        }

        // Botón para asegurar que todo lo que vemos en pantalla se guarde físicamente
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string fecha = dtpBuscar.Value.ToString("yyyy-MM-dd"); // obtiene la fecha seleccionada

            foreach (DataGridViewRow fila in dgvAlumnos.Rows)
            {
                // si no hay número de control, se salta la fila
                if (fila.Cells["nControl"].Value == null) continue;

                string nc = fila.Cells["nControl"].Value.ToString(); // obtiene el número de control

                // revisa si la celda de asistencia tiene valor
                bool asistio = false;
                if (fila.Cells["asistio"].Value != null && fila.Cells["asistio"].Value != DBNull.Value)
                {
                    asistio = Convert.ToBoolean(fila.Cells["asistio"].Value);
                }

                int valorAsistencia = asistio ? 1 : 0; // convierte a 1 o 0 para la BD

                // guarda la asistencia, si ya existe solo la actualiza
                datos.ejecutarComando($@"
                INSERT INTO asistencia (nControl, fecha, asistio) 
                VALUES ('{nc}', '{fecha}', {valorAsistencia}) 
                ON DUPLICATE KEY UPDATE asistio = {valorAsistencia}");
            }

            MessageBox.Show("Cambios guardados correctamente.", "Sistema");
            this.ActiveControl = null; // quita el foco para seguir usando el escáner
        }
    }
}
