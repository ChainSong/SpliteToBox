using System;
using System.Collections.Generic;
using System.Text;

namespace SpliteToBox
{
    public class WMS_CalcBoxinfo
    {
        public long ID { get; set; }
        public string OrderNumber { get; set; }
        public string ExternOrderNumber { get; set; }
        public long OID { get; set; }
        public string PackageNumber { get; set; }
        public string BoxCode { get; set; }
        public string ModelType { get; set; }
        public string BoxName { get; set; }
        public long SKUSeq { get; set; }
        public string SKU { get; set; }
        public decimal Qty { get; set; }
        public string Location { get; set; }

        public string Str1 { get; set; }
        public string Str2 { get; set; }
        public string Str3 { get; set; }

        public decimal UsedVolumn { get; set; }
        public decimal UsedWeight { get; set; }
        public decimal BoxLength { get; set; }

        public decimal BoxWidth { get; set; }

        public decimal BoxHeight { get; set; }



    }

}
