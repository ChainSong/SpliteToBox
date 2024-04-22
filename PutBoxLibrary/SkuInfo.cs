using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PutBoxLibrary
{
    public class SkuInfo
    {
        public long SeqNo { get; set; } = 0;
        public SKUType SKUType { get; set; }
        public int Qty { get; set; }
    }
}
