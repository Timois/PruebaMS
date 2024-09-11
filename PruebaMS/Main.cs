using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
namespace PruebaMS
{
    public partial class Main : MaterialSkin.Controls.MaterialForm
    {
        public List<MaterialSkin.Controls.MaterialButton> devicesButton;
        public Main()
        {
            this.devicesButton = new List<MaterialSkin.Controls.MaterialButton>();
            getPrinters();
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void getPrinters()
        {
            try
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
                // Crear un objeto Device Manager para gestionar dispositivos WIA
                DeviceManager deviceManager = new DeviceManager();
                Device device = null;
                var i = 0;

                // Encontrar un dispositivo WIA
                foreach (DeviceInfo deviceInfo in deviceManager.DeviceInfos)
                {
                    var ap = new MaterialSkin.Controls.MaterialButton();
                    ap.Text = deviceInfo.Properties["Name"].get_Value().ToString();
                    ap.Location = new System.Drawing.Point(251, 122 + i * 150);
                    ap.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton1.Icon")));
                    devicesButton.Add(ap);
                    this.Controls.Add(ap);
                    i++;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {

        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var button1 = new MaterialSkin.Controls.MaterialButton();
            this.Hide();

            // Configurar el botón
            button1.Text = "Nuevo Botón";
            button1.Location = new Point(100, 100); // Ubicación del botón
            button1.Size = new Size(100, 40); // Tamaño del botón

            // Añadir el botón al formulario
            this.Controls.Add(button1);
            button1.Show();

            this.Show(); // Mostrar el formulario nuevamente si lo deseas
        }
    }
}
