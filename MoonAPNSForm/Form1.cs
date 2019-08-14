using MoonAPNSForm.src;
using System;
using System.Windows.Forms;

namespace MoonAPNSForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void init() {
            this.Text = String.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
        }

        private void Btn_browser_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            this.textBox_file_p12.Text = file.FileName;//Path.GetFullPath(file.FileName);
        }

        private void Btn_submit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_device_token.Text))
            {
                MessageBox.Show("Please Input Device Token！");
                return;
            }
            if (String.IsNullOrEmpty(textBox_file_p12.Text))
            {
                MessageBox.Show("Please Choice *.p12 File！");
                return;
            }
            if (String.IsNullOrEmpty(textBox_key.Text))
            {
                MessageBox.Show("Please Input p12 password！");
                return;
            }
            if (String.IsNullOrEmpty(textBox_message.Text))
            {
                MessageBox.Show("Please Input Message！");
                return;
            }
            textBox_rs.Text = "Processing...";
            ApplePushMessage applePushMessage = new ApplePushMessage();
            applePushMessage.SetDeviceToken(textBox_device_token.Text);
            applePushMessage.SetP12FileName(textBox_file_p12.Text);
            applePushMessage.SetP12Key(textBox_key.Text);
            applePushMessage.SetSandBox(radioButton_sandbox.Checked);
            applePushMessage.SetMessage(textBox_message.Text);
            applePushMessage.Send();
            textBox_rs.Text = applePushMessage.GetResponse();

            //清除資料
            textBox_file_p12.Text = "";
            textBox_key.Text = "";
        }
    }
}
