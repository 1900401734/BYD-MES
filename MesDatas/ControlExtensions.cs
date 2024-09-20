using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas
{
    public static class ControlExtensions
    {
        public static Task InvokeAsync(this Control control, Action action)
        {
            return Task.Factory.FromAsync(control.BeginInvoke(action), control.EndInvoke);
        }
    }
}
