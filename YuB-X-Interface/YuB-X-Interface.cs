using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace YuB_X_Interface
{
    public partial class YuB_X_Interface : Form
    {
        public YuB_X_Interface()
        {
            InitializeComponent();
        }

        private void YuB_X_Interface_Load(object sender, EventArgs e)
        {

        }

        private void Execute_Click(object sender, EventArgs e)
        {
            try
            {
                using (TcpClient Client = new TcpClient("127.0.0.1", 6969))
                using (NetworkStream Stream = Client.GetStream())
                {
                    byte[] ScriptBytes = Encoding.UTF8.GetBytes(ScriptEditor.Text);
                    int ScriptLength = ScriptBytes.Length;

                    byte[] LengthBytes = BitConverter.GetBytes(ScriptLength);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(LengthBytes);

                    Stream.Write(LengthBytes, 0, 4);
                    Stream.Write(ScriptBytes, 0, ScriptBytes.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Execute script: " + ex.Message, "YuB-X-Interface", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Inject_Click(object sender, EventArgs e)
        {
            string InjectorPath = System.IO.Path.Combine(Application.StartupPath, "YuB-X-Injector.exe");

            if (!System.IO.File.Exists(InjectorPath))
            {
                using (var WebClient = new System.Net.WebClient())
                {
                    WebClient.DownloadFile("https://github.com/YuB-W/Roblox_Injector/releases/download/yubxv2_injector/injector.exe", InjectorPath);
                }
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(InjectorPath) { UseShellExecute = true } );
        }
    }
}
