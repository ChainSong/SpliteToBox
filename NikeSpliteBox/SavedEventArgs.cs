using SpliteToBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikeSpliteBox
{
    public class SavedEventArgs : EventArgs
    {
        public List<WMS_CalcBoxinfo> CalcBoxinfo { get;protected set; }
        public SavedEventArgs(List<WMS_CalcBoxinfo> wMS_CalcBoxinfos)
        {
            CalcBoxinfo = wMS_CalcBoxinfos;
        }

    }
}
