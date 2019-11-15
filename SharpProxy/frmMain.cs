using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;
using System.IO;

namespace SharpProxy
{
    public partial class frmMain : Form
    {
        private const int MinPort = 1;
        private const int MaxPort = 65535;

        // c:\programData
        private static readonly string CommonDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SharpProxy");
        private static readonly string ConfigInfoPath = Path.Combine(CommonDataPath, "config.txt");

        private ProxyThread _proxyThreadListener;

        public frmMain()
        {
            InitializeComponent();
            Text += " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            var ips = GetLocalIPs().OrderBy(x => x);
            if (ips.Any())
            {
                cmbIPAddress.Items.Clear();
                foreach (string ip in ips)
                {
                    cmbIPAddress.Items.Add(ip);
                }

                cmbIPAddress.Text = cmbIPAddress.Items[0].ToString();
            }

            int port = 5000;
            while (!CheckPortAvailability(port))
            {
                port++;
            }

            txtExternalPort.Text = port.ToString();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            txtInternalPort.Focus();

            //Try to load config
            try
            {
                using (StreamReader sr = new StreamReader(ConfigInfoPath))
                {
                    var values = sr.ReadToEnd().Split('\n').Select(x => x.Trim()).ToArray();

                    if (string.IsNullOrWhiteSpace(values[0]))
                    {
                        txtInternalPort.Text = @"52073";
                    }
                    else
                    {
                        txtInternalPort.Text = values[0];
                    }
                    chkRewriteHostHeaders.Checked = bool.Parse(values[1]);
                }
            }
            catch(Exception ex)
            {
                txtLog.Text = ex.Message;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _proxyThreadListener?.Stop();

            //Try to save config
            try
            {
                if (!Directory.Exists(CommonDataPath))
                {
                    Directory.CreateDirectory(CommonDataPath);
                }

                using (StreamWriter sw = new StreamWriter(ConfigInfoPath))
                {
                    sw.WriteLine(txtInternalPort.Text);
                    sw.WriteLine(chkRewriteHostHeaders.Checked);
                }
            }
            catch(Exception ex)
            {
                txtLog.Text = ex.Message;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Validation
            int.TryParse(txtExternalPort.Text, out var externalPort);
            int.TryParse(txtInternalPort.Text, out var internalPort);

            if (!CheckPortRange(externalPort) || !CheckPortRange(internalPort) || externalPort == internalPort)
            {
                string msg = $"Ports must be between {MinPort} - {MaxPort} and must not be the same.";
                ShowError(msg);
                txtLog.Text = msg;
                return;
            }

            if (!CheckPortAvailability(externalPort))
            {
                ShowError("Port " + externalPort + " is not available, please select a different port.");
                return;
            }

            _proxyThreadListener = new ProxyThread(externalPort, internalPort, chkRewriteHostHeaders.Checked);

            ToggleButtons();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _proxyThreadListener.Stop();

            ToggleButtons();
        }

        private void ShowError(string msg)
        {
            txtLog.Text = msg;

            MessageBox.Show(msg, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool CheckPortRange(int port)
        {
            if (port < MinPort || port > MaxPort)
            {
                return false;
            }

            return true;
        }

        private IList<string> GetLocalIPs()
        {
            //Try to find our internal IP address...
            string myHost = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostEntry(myHost).AddressList;
            IList<string> myIPs = new List<string>();
            string fallbackIp = "";

            for (int i = 0; i < addresses.Length; i++)
            {
                //Is this a valid IPv4 address?
                if (addresses[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    string thisAddress = addresses[i].ToString();
                    //Loopback is not our preference...
                    if (thisAddress == "127.0.0.1")
                    {
                        continue;
                    }

                    //169.x.x.x addresses are self-assigned "private network" IP by Windows
                    if (thisAddress.StartsWith("169"))
                    {
                        fallbackIp = thisAddress;
                        continue;
                    }

                    myIPs.Add(thisAddress);
                }
            }

            if (myIPs.Count == 0 && !string.IsNullOrEmpty(fallbackIp))
            {
                myIPs.Add(fallbackIp);
            }

            return myIPs;
        }

        private void ToggleButtons()
        {
            btnStop.Enabled = !btnStop.Enabled;
            btnStart.Enabled = !btnStart.Enabled;

            txtExternalPort.Enabled = !txtExternalPort.Enabled;
            txtInternalPort.Enabled = !txtInternalPort.Enabled;

            chkRewriteHostHeaders.Enabled = !chkRewriteHostHeaders.Enabled;
        }

        private bool CheckPortAvailability(int port)
        {
            //http://stackoverflow.com/questions/570098/in-c-how-to-check-if-a-tcp-port-is-available

            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    return false;
                }
            }

            try
            {
                TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Any, port));
                listener.Start();
                listener.Stop();
            }
            catch (Exception ex)
            {
                txtLog.Text = ex.Message;

                return false;
            }

            return true;
        }

        private void txtPorts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnStart_Click(null, null);
            }
        }
    }
}
