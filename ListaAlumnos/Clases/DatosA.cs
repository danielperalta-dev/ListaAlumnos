using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace ListaAlumnos.Clases
{
    internal class DatosA
    {
        string cadenaConexion = "server=192.168.64.1;port=3308;user=luis;pwd=Daniel123+;Database=daniel";
        MySqlConnection conexion;

        private void Conectar()
        {
            try
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }


        private void Desconectar()
        {
            try
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public DataSet ejecutar(string comando)
        {
            try
            {
                Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter(comando, conexion);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Desconectar();
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Desconectar();
                return null;
            }
        }

        public bool ejecutarComando(String comando)
        {
            try
            {
                Conectar();
                MySqlCommand cmd = new MySqlCommand(comando, conexion);
                cmd.ExecuteNonQuery();
                Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Desconectar();
                return false;
            }
        }
    }
}
