using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PutBoxLibrary
{
    public class BoxInfo
    {
        public decimal UsedVolumn
        {
            get
            {
                return SkuInfos.Sum(m => m.SKUType.Volumn * m.Qty);
            }
        }
        public decimal UsedWeight { get; set; }
        public BoxType BoxType { get; set; }
        public List<SkuInfo> SkuInfos { get; set; }

        public void AddSKUType(SkuInfo sKU, bool IsReallyQty = false)
        {
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
