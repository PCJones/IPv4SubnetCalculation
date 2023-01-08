using IPv4SubnetCalculationLibrary;

namespace IPv4SubnetCalculationGUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HandleUserInput();
        }

        private void HandleUserInput()
        {
            var ipAddressRaw = txtIpAddress.Text;
            var subnetMaskRaw = txtSubnetMask.Text;
            IPv4? ipAddress = null;
            IPv4? subnetMask = null;
            if (IPv4.TryCreateIPv4(ipAddressRaw, out ipAddress))
            {
                txtIpAddress.BackColor = Color.Green;
            }
            else
            {
                txtIpAddress.BackColor= Color.Red;
            }


            if (IPv4.TryCreateIPv4(subnetMaskRaw, out subnetMask))
            {
                txtSubnetMask.BackColor = Color.Green;

                if (ipAddress != null)
                {
                    txtNetworkAddress.Text = IPv4.GetNetworkAddress(ipAddress, subnetMask).ToString();
                }

                txtPossibleClients.Text = IPv4.GetPossibleClientsAmount(subnetMask).ToString();
            }
            else
            {
                txtSubnetMask.BackColor = Color.Red;
            }
        }

        private void txtIpAddress_TextChanged(object sender, EventArgs e)
        {
            HandleUserInput();
        }

        private void txtSubnetMask_TextChanged(object sender, EventArgs e)
        {
            HandleUserInput();
        }
    }
}