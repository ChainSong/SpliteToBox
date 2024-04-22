using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllocationPickTask;
using SpliteToBox;
using SpliteToBox.Common;

namespace NikeSpliteBox
{
    class Program
    {
        static void Main(string[] args)
        { 


            NIKEWMSSpliteBox nIKEWMSSpliteBox = new NIKEWMSSpliteBox();
            var infos = nIKEWMSSpliteBox.GetSKUInfos();
            foreach (var item in infos)
            {
                try
                {
                    var boxinfos = nIKEWMSSpliteBox.GetCalcInfos<WMS_OrderDetail>(item.Item2);
                    if (boxinfos != null && boxinfos.Any())
                        nIKEWMSSpliteBox.SaveBoxInfo(item.Item1, boxinfos);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
            new PickTaskManagement().CreatePickTask();

            //Console.ReadLine();
        }
    }
}
