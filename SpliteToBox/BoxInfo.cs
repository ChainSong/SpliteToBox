using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpliteToBox
{
    public class BoxInfo<T> where T: SkuInfo
    {
        public string BoxNumber { get; set; }
        public decimal UsedVolumn
        {
            get
            {
                return SkuInfos.Sum(m => m.SKUType.Volumn * m.Qty);
            }
        }
        public decimal UsedWeight
        {
            get
            {
               return this.BoxType.Weight + this.SkuInfos.Sum(m => m.SKUType.NetWeight * m.Qty);
            }
        }
        public BoxType BoxType { get; set; }
        public List<T> SkuInfos { get; set; }

        public void AddSKUType(T sKUa, bool IsReallyQty = false)
        {
            var sKU = sKUa.DeepClone();
            if (!IsReallyQty)
                sKU.Qty = 1;
            if (this.SkuInfos.Exists(m => m.SKUType.Code == sKU.SKUType.Code && m.SeqNo== sKU.SeqNo))
                SkuInfos.First(m => m.SKUType.Code == sKU.SKUType.Code && m.SeqNo == sKU.SeqNo).Qty += 1;
            else
            {
                this.SkuInfos.Add(sKU);
            }
        }
    }
}
