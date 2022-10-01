using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace T2Grupal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Registro r = new Registro();
        List<Registro> registros = new List<Registro>();
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            double monto = 0;
            monto = int.Parse(txtCantidad.Text) * double.Parse(txtPrecio.Text);
            string persona = "";
            persona += txtNombre.Text + ",";
            persona += txtCodigo.Text + ",";
            persona += txtPrecio.Text + ",";
            persona += txtCantidad.Text + ",";
            persona += monto.ToString();
            StreamWriter writer = File.AppendText("Registro.txt");
            writer.WriteLine(persona);
            writer.Close();
            leer();
            registros.Add(new Registro()
            {
                P2_nombre = txtNombre.Text,
                P2_codigo = txtCodigo.Text,
                P2_precio_unitario = double.Parse(txtPrecio.Text),
                P2_cantidad = int.Parse(txtCantidad.Text)
            });
            dgvRegistro.Rows.Clear();
            foreach (var m in registros)
            {
                dgvRegistro.Rows.Add(m.P2_nombre, m.P2_codigo, m.P2_precio_unitario, m.P2_cantidad, m.monto());
            }
        }
        public void leer()
        {
            StreamReader reader = new StreamReader("Registro.txt");
            dgvRegistro.Rows.Clear();
            string linea = null;
            do
            {
                linea = reader.ReadLine();
                    if (linea != null)
                    {
                    string[] datos = linea.Split(',');
                    dgvRegistro.Rows.Add(datos);
                    }
            } while (linea != null);
            reader.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("Registro.txt");
            writer.Close();

            /*if (!File.Exists("Registro.txt"))
            {
            }
            else
            {
                leer();
            }*/
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("Registro.txt");
            StreamWriter writer = new StreamWriter("Temp.txt");
            string[] datos = new string[5];
            string registro = reader.ReadLine();
            string bus = txtBus.Text;
            int cont = 0;
            while (registro != null)
            {
                datos = registro.Split(',');
                if (!bus.Equals(datos[1]))
                {
                    writer.WriteLine(registro);
                }
                else
                {
                    dgvRegistro.Rows.RemoveAt(cont);
                    registros.RemoveAt(cont);
                }
                registro = reader.ReadLine();
                cont++;
            }
            reader.Close();
            writer.Close();
            File.Delete("Registro.txt");
            File.Move("Temp.txt", "Registro.txt");
            leer();

        }
        List<Registro> listaordenada = new List<Registro>();
        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            listaordenada = registros.OrderBy(medit => medit.P2_nombre).ToList();
            registros = listaordenada;
            dgvRegistro.Rows.Clear();
            foreach (var m in listaordenada)
            {
                Console.WriteLine("Hola");
                Console.WriteLine(m.P2_codigo);
                dgvRegistro.Rows.Add(m.P2_nombre, m.P2_codigo, m.P2_precio_unitario, m.P2_cantidad, m.monto());
            }
            File.Delete("Registro.txt");
            StreamWriter writer = new StreamWriter("Registro.txt");
            foreach(var n in listaordenada)
            {
                double monto = n.P2_cantidad * n.P2_precio_unitario;
                string persona = "";
                persona += n.P2_nombre + ",";
                persona += n.P2_codigo + ",";
                persona += n.P2_precio_unitario + ",";
                persona += n.P2_cantidad + ",";
                persona += monto;

                writer.WriteLine(persona);
            }
            writer.Close();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("Registro.txt");
            string[] datos = new string[5];
            bool flag = false;
            string bus = txtBus.Text;
            string registro = reader.ReadLine();
            while (registro != null && flag == false)
            {
                datos = registro.Split(',');
                if (bus.Equals(datos[1]))
                {
                    MessageBox.Show("Se encontro el medicamento");
                    flag = true;
                }
                else
                {
                    registro = reader.ReadLine();
                }
            }
            if (flag == false)
                MessageBox.Show("No se encontro esa búsqueda", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            reader.Close();
        }
    }
}