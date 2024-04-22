namespace SpliteToBox
{
    public class WMS_OrderDetail :SkuInfo
    {
        public long OID { get; set; }
        public string str2 { get; set; }

        public string str6 { get; set; }

        public string str9 { get; set; }//订单类型
        public string str10 { get; set; }//ship to name
        public string str11 { get; set; }//SK VAS code(

        //article
        public string str12 { get; set; }

        public string Location { get; set; }
    }
}