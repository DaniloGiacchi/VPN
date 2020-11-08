using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace myVPN
{
    public partial class Form1 : Form
    {
        VPN vpnConnection = new VPN();
        public Form1()
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

       /* private void GET()
        {
            WebRequest request = WebRequest.Create("https://freevpn.me/accounts/");
            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                
            }

            // Close the response.
            response.Close();
        }*/
    }
}