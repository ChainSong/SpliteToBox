﻿using AllocationPickTask.Common;

using System;
using System.Collections.Generic;
using System.Text;

namespace AllocationPickTask.Model
{
    [Serializable]
    public class OrderDetailInfo
    {
        [EntityPropertyExtension("ExpressNumber", "ExpressNumber")]
        public string ExpressNumber { get; set; }

        [EntityPropertyExtension("RowID", "RowID")]
        public long RowID { get; set; }

        [EntityPropertyExtension("WaveNumber", "WaveNumber")]
        public string WaveNumber { get; set; }

        [EntityPropertyExtension("WaveTime", "WaveTime")]
        public DateTime? WaveTime { get; set; }

        [EntityPropertyExtension("Unit", "Unit")]
        public string Unit { get; set; }

        [EntityPropertyExtension("Specifications", "Specifications")]
        public string Specifications { get; set; }

        [EntityPropertyExtension("ID", "ID")]
        public long ID { get; set; }

        [EntityPropertyExtension("OID", "OID")]
        public long OID { get; set; }

        [EntityPropertyExtension("POID", "POID")]
        public long POID { get; set; }

        [EntityPropertyExtension("OrderNumber", "OrderNumber")]
        public string OrderNumber { get; set; }

        [EntityPropertyExtension("ExternOrderNumber", "ExternOrderNumber")]
        public string ExternOrderNumber { get; set; }


        [EntityPropertyExtension("PODID", "PODID")]
        public long? PODID { get; set; }

        [EntityPropertyExtension("CustomerID", "CustomerID")]
        public long? CustomerID { get; set; }

        [EntityPropertyExtension("CustomerName", "CustomerName")]
        public string CustomerName { get; set; }

        [EntityPropertyExtension("LineNumber", "LineNumber")]
        public string LineNumber { get; set; }

        [EntityPropertyExtension("SKU", "SKU")]
        public string SKU { get; set; }
        [EntityPropertyExtension("ManufacturerSKU", "ManufacturerSKU")]
        public string ManufacturerSKU { get; set; }
        [EntityPropertyExtension("Article", "Article")]
        public string Article { get; set; }

        [EntityPropertyExtension("Size", "Size")]
        public string Size { get; set; }

        [EntityPropertyExtension("BU", "BU")]
        public string BU { get; set; }

        [EntityPropertyExtension("UPC", "UPC")]
        public string UPC { get; set; }

        [EntityPropertyExtension("BoxNumber", "BoxNumber")]
        public string BoxNumber { get; set; }

        [EntityPropertyExtension("BatchNumber", "BatchNumber")]
        public string BatchNumber { get; set; }

        [EntityPropertyExtension("GoodsName", "GoodsName")]
        public string GoodsName { get; set; }

        [EntityPropertyExtension("GoodsType", "GoodsType")]
        public string GoodsType { get; set; }

        [EntityPropertyExtension("Lot", "Lot")]
        public string Lot { get; set; }

        [EntityPropertyExtension("Warehouse", "Warehouse")]
        public string Warehouse { get; set; }

        [EntityPropertyExtension("Area", "Area")]
        public string Area { get; set; }

        [EntityPropertyExtension("Location", "Location")]
        public string Location { get; set; }

        [EntityPropertyExtension("Qty", "Qty")]
        public decimal? Qty { get; set; }

        [EntityPropertyExtension("Picker", "Picker")]
        public string Picker { get; set; }

        [EntityPropertyExtension("PickTime", "PickTime")]
        public DateTime? PickTime { get; set; }

        [EntityPropertyExtension("Confirmer", "Confirmer")]
        public string Confirmer { get; set; }

        [EntityPropertyExtension("ConfirmeTime", "ConfirmeTime")]
        public DateTime? ConfirmeTime { get; set; }

        [EntityPropertyExtension("Creator", "Creator")]
        public string Creator { get; set; }

        [EntityPropertyExtension("CreateTime", "CreateTime")]
        public DateTime? CreateTime { get; set; }

        [EntityPropertyExtension("Updator", "Updator")]
        public string Updator { get; set; }

        [EntityPropertyExtension("UpdateTime", "UpdateTime")]
        public DateTime? UpdateTime { get; set; }

        [EntityPropertyExtension("Remark", "Remark")]
        public string Remark { get; set; }

        //Zone
        [EntityPropertyExtension("Zone", "Zone")]
        public string Zone { get; set; }

        [EntityPropertyExtension("str1", "str1")]
        public string str1 { get; set; }

        [EntityPropertyExtension("str2", "str2")]
        public string str2 { get; set; }

        [EntityPropertyExtension("str3", "str3")]
        public string str3 { get; set; }

        [EntityPropertyExtension("str4", "str4")]
        public string str4 { get; set; }

        [EntityPropertyExtension("str5", "str5")]
        public string str5 { get; set; }

        [EntityPropertyExtension("str6", "str6")]
        public string str6 { get; set; }

        [EntityPropertyExtension("str7", "str7")]
        public string str7 { get; set; }

        [EntityPropertyExtension("str8", "str8")]
        public string str8 { get; set; }

        [EntityPropertyExtension("str9", "str9")]
        public string str9 { get; set; }

        [EntityPropertyExtension("str10", "str10")]
        public string str10 { get; set; }

        [EntityPropertyExtension("str11", "str11")]
        public string str11 { get; set; }

        [EntityPropertyExtension("str12", "str12")]
        public string str12 { get; set; }

        [EntityPropertyExtension("str13", "str13")]
        public string str13 { get; set; }

        [EntityPropertyExtension("str14", "str14")]
        public string str14 { get; set; }

        [EntityPropertyExtension("str15", "str15")]
        public string str15 { get; set; }

        [EntityPropertyExtension("str16", "str16")]
        public string str16 { get; set; }

        [EntityPropertyExtension("str17", "str17")]
        public string str17 { get; set; }

        [EntityPropertyExtension("str18", "str18")]
        public string str18 { get; set; }

        [EntityPropertyExtension("str19", "str19")]
        public string str19 { get; set; }

        [EntityPropertyExtension("str20", "str20")]
        public string str20 { get; set; }

        [EntityPropertyExtension("Gender", "Gender")]
        public string Gender { get; set; }

        [EntityPropertyExtension("Price", "Price")]
        public string Price { get; set; }

        [EntityPropertyExtension("SafeLock", "SafeLock")]
        public string SafeLock { get; set; }

        [EntityPropertyExtension("Hanger", "Hanger")]
        public string Hanger { get; set; }

        [EntityPropertyExtension("DateTime1", "DateTime1")]
        public DateTime? DateTime1 { get; set; }

        [EntityPropertyExtension("DateTime2", "DateTime2")]
        public DateTime? DateTime2 { get; set; }

        [EntityPropertyExtension("DateTime3", "DateTime3")]
        public DateTime? DateTime3 { get; set; }

        [EntityPropertyExtension("DateTime4", "DateTime4")]
        public DateTime? DateTime4 { get; set; }

        [EntityPropertyExtension("DateTime5", "DateTime5")]
        public DateTime? DateTime5 { get; set; }

        [EntityPropertyExtension("Int1", "Int1")]
        public int? Int1 { get; set; }

        [EntityPropertyExtension("Int2", "Int2")]
        public int? Int2 { get; set; }

        [EntityPropertyExtension("Int3", "Int3")]
        public int? Int3 { get; set; }

        [EntityPropertyExtension("Int4", "Int4")]
        public int? Int4 { get; set; }

        [EntityPropertyExtension("Int5", "Int5")]
        public int? Int5 { get; set; }

        [EntityPropertyExtension("PStr11", "PStr11")]
        public string PStr11 { get; set; }
        [EntityPropertyExtension("PStr12", "PStr12")]
        public string PStr12 { get; set; }
        [EntityPropertyExtension("PGrade", "PGrade")]
        public string PGrade { get; set; }
        [EntityPropertyExtension("PInt2", "PInt2")]
        public int? PInt2 { get; set; }

        //MSRP字段
        [EntityPropertyExtension("MSRP", "MSRP")]
        public string MSRP { get; set; }

        /// <summary>
        /// 退货仓专用 PLNO
        /// </summary>
        [EntityPropertyExtension("PLNO", "PLNO")]
        public string PLNO { get; set; }

        [EntityPropertyExtension("OutboundNo", "OutboundNo")]
        public string OutboundNo { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        [EntityPropertyExtension("PL", "PL")]
        public string PL { get; set; }

        [EntityPropertyExtension("PickingTaskNo", "PickingTaskNo")]
        public string PickingTaskNo { get; set; }

        //产品信息冗余
        [EntityPropertyExtension("Color", "Color")]
        public string Color { get; set; }

        [EntityPropertyExtension("PackageNumber", "PackageNumber")]
        public string PackageNumber { get; set; }


        [EntityPropertyExtension("BoxCode", "BoxCode")]
        public string BoxCode { get; set; }

        [EntityPropertyExtension("ModelType", "ModelType")]
        public string ModelType { get; set; }

        [EntityPropertyExtension("BoxName", "BoxName")]
        public string BoxName { get; set; }
        [EntityPropertyExtension("BoxName", "BoxName")]
        public string BoxQty { get; set; }

        [EntityPropertyExtension("UsedVolumn", "UsedVolumn")]
        public string UsedVolumn { get; set; }

        [EntityPropertyExtension("UsedWeight", "UsedWeight")]
        public string UsedWeight { get; set; }

        [EntityPropertyExtension("BoxLength", "BoxLength")]
        public string BoxLength { get; set; }

        [EntityPropertyExtension("BoxWidth", "BoxWidth")]
        public string BoxWidth { get; set; }

        [EntityPropertyExtension("BoxHeight", "BoxHeight")]
        public string BoxHeight { get; set; }





    }
}