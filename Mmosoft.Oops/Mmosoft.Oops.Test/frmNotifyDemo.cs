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
            // Notification's content is short, the context has been guessed by the user:
            // *Order purchased* when user click Purchase order
            // So this notification should be open in 2 seconds then close automatically
            NotifyMgr.Notify(
                this,
                "Order status", 
                "Your order has been purchased!", 
                NotifyType.Information);
        }

        private void btnWarning_Click(object sender, EventArgs e)
        {
            // This notification's content is maybe guessed by the user (or maybe not)
            // So the notification should keeping display until the user manually click to close it.
            NotifyMgr.Notify(
                this,
                "Incorrect password", 
                "Your password is incorrect. Please check again!",
                NotifyType.Warning, NotifyOut.Manually);
        }

        private void btnDanger_Click(object sender, EventArgs e)
        {
            // This notification's content can not be guessed by the user
            // So the notification should keeping display until the user manually click to close it.
            NotifyMgr.Notify(this,
                "System error", 
                "The main database has been corrupted!", 
                NotifyType.Danger, NotifyOut.Manually);
        }
    }
}
