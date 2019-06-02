using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Notifications
{
    public class NotifyMgr
    {
        private static List<SmallNotification> _notifies = new List<SmallNotification>();
        private static int _notifiesShowing;

        public static void Notify(string title, string message, NotifyType notifyType, Form host)
        {
            _notifiesShowing++;
            var notify = new SmallNotification(notifyType)
            {
                Title = title,
                Text = message,
                Top = _notifiesShowing * 10,
                Left = host.Width
            };
            host.Controls.Add(notify);
            notify.BringToFront();
            notify.OnCompeleted = () =>
            {
                host.Controls.Remove(notify);
                _notifiesShowing--;
            };
            notify.Start();
        }
    }
}
