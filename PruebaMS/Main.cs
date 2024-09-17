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
using System.Management;
using MaterialSkin.Controls;
using System.Printing;

namespace PruebaMS
{
    public partial class Main : MaterialSkin.Controls.MaterialForm
    {
        public Main()
        {
            InitializeComponent();
            ListConnectedScanners();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ListConnectedScanners()
        {
            try
            {
                // Crear un objeto de DeviceManager para manejar los dispositivos WIA
                DeviceManager deviceManager = new DeviceManager();

                // Limpiar el ListBox antes de agregar elementos
                listBox1.Items.Clear();

                // Iterar a través de los dispositivos conectados
                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    DeviceInfo deviceInfo = deviceManager.DeviceInfos[i];

                    // Verificar si el dispositivo es un escáner
                    if (deviceInfo.Type == WiaDeviceType.ScannerDeviceType)
                    {
                        // Agregar el nombre del escáner al ListBox
                        listBox1.Items.Add(deviceInfo.Properties["Name"].get_Value());
                    }
                }

                // Verificar si no se encontraron escáneres
                if (listBox1.Items.Count == 0)
                {
                    listBox1.Items.Add("No hay impresoras o escáners conectados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar escáneres: " + ex.Message);
            }
        }

        private void buttonCable_Click(object sender, EventArgs e)
        {
            try
            {
                ManagementClass printers = new ManagementClass("Win32_SerialPort");
                ManagementObjectCollection portCollection = printers.GetInstances();

                foreach (ManagementObject port in portCollection)
                {
                    string nombrePuerto = port["Name"].ToString();
                    string estadoPuerto = port["Status"].ToString();
                    string velocidad = port["Speed"].ToString();
                    string protocolo = port["Protocol"].ToString();

                    // Filtrar solo puertos serie asociados a impresoras
                    if (port["Manufacturer"].ToString().ToLower().Contains("hp") ||
                        port["Manufacturer"].ToString().ToLower().Contains("epson") ||
                        port["Manufacturer"].ToString().ToLower().Contains("canon"))
                    {
                        listBox2.Items.Add($"{nombrePuerto} - Velocidad: {velocidad} bps, Protocolo: {protocolo}, Estado: {estadoPuerto}");
                    }
                }

                // Verificar si no se encontraron impresoras locales
                if (listBox2.Items.Count == 0)
                {
                    listBox2.Items.Add("No hay impresoras locales conectadas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar impresoras: " + ex.Message);
            }
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            try
            {
                ManagementClass printers = new ManagementClass("Win32_Printer");
                ManagementObjectCollection printerCollection = printers.GetInstances();

                foreach (ManagementObject printer in printerCollection)
                {
                    string nombreImpresora = printer["Name"].ToString();
                    string estadoImpresora = printer["Status"].ToString();

                    // Filtrar solo impresoras conectadas por red
                    if (printer["Network" as object] != null && (bool)printer["Network"])
                    {
                        listBox3.Items.Add($"{nombreImpresora} ({estadoImpresora})");
                    }
                }

                // Verificar si no se encontraron impresoras de red
                if (listBox3.Items.Count == 0)
                {
                    listBox3.Items.Add("No hay impresoras de red conectadas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar impresoras de red: " + ex.Message);
            }
        }
    }
}
    