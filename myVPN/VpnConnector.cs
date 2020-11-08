using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace myVPN
{
    public partial class VpnConnector : Form
    {
        VPN vpnConnection = new VPN();

        public VpnConnector()
        {
            InitializeComponent();
        }

        private void ClickButtonConnect(object sender, EventArgs e)
        {
            vpnConnection.Connect(txtHost.Text,txtUsername.Text,txtPassword.Text);
           
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
                        
        }

        private void ClickButtonDisconnect(object sender, EventArgs e)
        {
            vpnConnection.Disconnect();

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }
    }
}