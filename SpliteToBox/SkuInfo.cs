using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SpliteToBox
{
    public class SkuInfo
    {
        public long SeqNo { get; set; } = 0;
        public SKUType SKUType { get; set; }
        public decimal Qty { get; set; }

        /// <summary>
        /// actical 总数
        /// </summary>
        public decimal Index1 { get; set; } = 0;

        /// <summary>
        /// actical+sku 总数
        /// </summary>
        public decimal Index2 { get; set; } = 0;
        public decimal Index3 { get; set; } = 0;
        public decimal Index4 { get; set; } = 0;
        public decimal Index5 { get; set; } = 0;
    }
}
