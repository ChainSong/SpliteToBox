using System;
using System.Collections.Generic;
using System.Text;

namespace SpliteToBox
{
    public class NIKEWMSSpliteBox : SpliteBoxManage
    {
        List<BoxInfo<WMS_OrderDetail>> boxInfos = new List<BoxInfo<WMS_OrderDetail>>();
        private List<BoxType> boxTypes=new List<BoxType>();
        public override IEnumerable<BoxType> GetBoxInfo()
        {
            boxTypes.Add(new BoxType() {
             Code="A001",
              Lenght=100,
               Width=100,
               Height=80,
                Index=0,
                 Name="Box1",
                  VolumnPercentage=0.9M,
                   
            });
            boxTypes.Add(new BoxType()
            {
                Code = "A002",
                Lenght = 90,
                Width = 90,
                Height = 70,
                Index = 1,
                Name = "Box2",
                VolumnPercentage = 0.9M,

            });
            return boxTypes;
        }

        public List<WMS_OrderDetail> GetSKUInfos()
        {
            return new List<WMS_OrderDetail>();
        }

        public List<T> GetCalcInfos<T>(List<WMS_OrderDetail> wMS_OrderDetails) where T:SkuInfo
        {
            //原箱
            boxInfos.Add(null);
            //需计算箱
            return new List<T>();
        }

        public void ClacBox(List<WMS_OrderDetail> skuInfos)
        {
           var boxinfos=  base.CalcSplietBox(skuInfos);
        }
    }
}
