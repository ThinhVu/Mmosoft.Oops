using System;
using System.Windows.Forms;

using Mmosoft.Oops.Controls.Notifications;

namespace Mmosoft.Oops.Test
{
    public partial class frmNotifyDemo : Form
    {
        public frmNotifyDemo()
        {
            InitializeComponent();
        }

        private void btnInfor_Click(object sender, EventArgs e)
        {
            NotifyMgr.Notify("Order status", "Your order has been processed!", NotifyType.Information, this);
        }

        private void btnWarning_Click(object sender, EventArgs e)
        {
            NotifyMgr.Notify("Incorrect password", "No batches have been uploaded to the Manifest. Are you sure you would like to Confirm the Manifest?", NotifyType.Warning, this);
        }

        private void btnDanger_Click(object sender, EventArgs e)
        {
            NotifyMgr.Notify("System error", "Database has been corrupted!", NotifyType.Danger, this);
        }
    }
}
