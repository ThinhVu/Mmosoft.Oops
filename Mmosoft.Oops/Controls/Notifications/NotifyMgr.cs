using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Notifications
{
    public class NotifyMgr
    {
        private static List<Notification> _notifies = new List<Notification>();

        public static void Notify(Form host, string title, string message, NotifyType notifyType, NotifyOut outType = NotifyOut.Automatically, int initialWidth = 300)
        {
            var notify = new Notification(notifyType, outType, initialWidth)
            {
                Title = title,
                Text = message,
                Top = 10,
                Left = host.Width
            };
            host.Controls.Add(notify);
            notify.BringToFront();
            notify.OnCompeleted = () =>
            {
                host.Controls.Remove(notify);
            };
            notify.Start();
        }
    }
}
