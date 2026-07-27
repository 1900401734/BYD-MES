using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas.Services
{
    public static class SplitContainerExtensions
    {
        public static void SetPercent(this SplitContainer split, double percent)
        {
            if (!split.IsHandleCreated || split.Width <= 0 || split.Height <= 0) return;

            int totalSize = (split.Orientation == Orientation.Vertical) ?
                           split.Width - split.SplitterWidth :
                           split.Height - split.SplitterWidth;

            int distance = (int)(totalSize * percent);
            distance = Math.Max(split.Panel1MinSize,
                               Math.Min(distance, totalSize - split.Panel2MinSize));

            split.SplitterDistance = distance;
        }
    }
}
