using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myVPN
{
    public class VPN
    {
        #region initialize private variable
        private string FolderPath = string.Concat(Directory.GetCurrentDirectory(), "\\VPN");
        private string batFileName = "VpnConnection.bat";
        private string pbkFileName = "VpnConnection.pbk";
        private Process newProcess;
        #endregion

        #region initialize public variable
        public string host;
        public string username;
        public string password;
        #endregion

        public VPN()
        {

        }
        #region define public functions
        public void Connect(string host, string username, string password)
        {
            this.CreateVpnDirectory();
            this.SettingVpnConnectionPbkFile(host);
            this.SettingVpnConnectionBatFile(username, password, "CONNECT");
            this.StartProcess();
        }

        public void Disconnect()
        {
            this.SettingVpnConnectionBatFile(null,null,"DISCONNECT");
            this.StartProcess();
        }

        #endregion

        #region Vpn connection directory creation
        private void CreateVpnDirectory()
        {
            if (!Directory.Exists(this.FolderPath))
                Directory.CreateDirectory(this.FolderPath);
        }
        #endregion

        #region writing file pbk
        private void SettingVpnConnectionPbkFile(string serverAddress)
        {
            var sb = new StringBuilder();
            try
            {
                sb.AppendLine("[VPN]");
                sb.AppendLine("MEDIA=rastapi");
                sb.AppendLine("Port=VPN2-0");
                sb.AppendLine("Device=WAN Miniport (IKEv2)");
                sb.AppendLine("DEVICE=vpn");
                sb.AppendLine("PhoneNumber=" + serverAddress);
            }
            catch
            {
                throw new Exception("Impossible to prepare pbk file.");
            }
            
            try
            {
                File.WriteAllText(this.FolderPath + $"\\{this.pbkFileName}", sb.ToString());
            }
            catch
            {
                throw new Exception("Impossible to write pbk file.");
            }
        }
        #endregion

        #region writing file bat
        private void SettingVpnConnectionBatFile(string username,string password,string operationType)
        {
            var sb = new StringBuilder();
            if (operationType == "CONNECT")
                sb.AppendLine("rasdial \"VPN\" " + username + " " + password + " /phonebook:\"" + this.FolderPath + $"\\{this.pbkFileName}\"");
            else if (operationType == "DISCONNECT")
                sb.AppendLine("rasdial /d");
            else
                throw new Exception("Impossible to prepare bat file.");

            try
            {
                File.WriteAllText(this.FolderPath + $"\\{this.batFileName}", sb.ToString());
            }
            catch
            {
                throw new Exception("Impossible to write bat file.");
            }
        }
        #endregion

        #region define function starting the Connection/Disconnection process
        private void StartProcess()
        {
            try
            {
                this.newProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = this.FolderPath + $"\\{this.batFileName}",
                        WindowStyle = ProcessWindowStyle.Normal
                    }
                };

            }
            catch
            {
                throw new Exception("Impossible to initialize start process.");
            }

            try
            {
                newProcess.Start();
                newProcess.WaitForExit();
            }
            catch
            {
                throw new Exception("Impossible to start a new process.");
            }
           
        }
        #endregion
    }
}
