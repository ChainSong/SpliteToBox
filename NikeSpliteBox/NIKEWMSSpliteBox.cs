using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SpliteToBox;
using System.Linq;
using SpliteToBox.Common;

namespace NikeSpliteBox
{
    public class NIKEWMSSpliteBox : SpliteBoxManage
    {
        public event EventHandler<SavedEventArgs> OnSaved;
        List<BoxInfo<WMS_OrderDetail>> boxInfos = new List<BoxInfo<WMS_OrderDetail>>();
        private List<BoxType> boxTypes = new List<BoxType>();
        public override IEnumerable<BoxType> GetBoxInfo()
        {
            var dt = SqlHelper.GetDateTable(" select * from wms_config where type='BoxSize' and str5='NIKEB2C' and str7='1' and status=1");
            foreach (DataRow item in dt.Rows)
            {
                boxTypes.Add(new BoxType()
                {
                    Code = item["Code"].ToString(),
                    Lenght = Convert.ToDecimal(item["Str1"].ToString()),
                    Width = Convert.ToDecimal(item["Str2"].ToString()),
                    Height = Convert.ToDecimal(item["Str3"].ToString()),
                    Weight = Convert.ToDecimal(item["Str4"].ToString()),
                    Str6 = item["Str6"].ToString(),
                    Index = 0,
                    Name = item["Name"].ToString(),
                    VolumnPercentage = Convert.ToDecimal(item["Str8"].ToString()),

                });
            }
            return boxTypes;
        }

        public List<(DataRow, List<WMS_OrderDetail>)> GetSKUInfos()
        {
            DataTable dt = SqlHelper.GetDateTable("select * from WMS_PackingBoxFlag where status=1");

            var list = SqlHelper.GetDateTable(@"select [ID]
             ,[OID]
             ,[OrderNumber]
             ,[ExternOrderNumber]
             ,[POID]
             ,[PODID]
             ,[CustomerID]
             ,[CustomerName]
             ,[LineNumber]
             ,[SKU]
             ,[UPC]
             ,[GoodsName]
             ,[GoodsType]
             ,[Lot]
             ,[BoxNumber]
             ,[BatchNumber]
             ,[Unit]
             ,[Specifications]
             ,[Warehouse]
             ,[Area]
             ,[Location]
             ,[Qty]
             ,[Picker]
             ,[PickTime]
             ,[Confirmer]
             ,[ConfirmeTime]
             ,[Creator]
             ,[CreateTime]
             ,[Updator]
             ,[UpdateTime]
             ,[Remark]
             ,[str1]
             ,[str2]
             ,[str3]
             ,[str4]
             ,[str5]
             ,(select top 1  WMS_ArticleDetail.Division from WMS_ArticleDetail left join WMS_Product on  WMS_ArticleDetail.ArticleNo=WMS_Product.Str10) as [str6]
             ,[str7]
             ,[str8]
             ,(select top 1 ordertype from wms_order where id =oid) str9
             ,( select top 1 PartyName1 from SDC_Order_Sys  left join 
 SDC_OrderParty_Sys  on SDC_Order_Sys.FulfillmentRequestNumber=SDC_OrderParty_Sys.FK_Order_FulfillmentRequestNumber
 where  PartyTypeCode='SHIP_TO' and PLCode=wms_orderdetail.ExternOrderNumber) str10
             ,(select top 1 Instruction3Text from SDC_OrderItemInstruction_Sys
left join SDC_Order_Sys on SDC_OrderItemInstruction_Sys.FK_Order_FulfillmentRequestNumber=SDC_Order_Sys.FulfillmentRequestNumber
where Instruction3Text='SK' and PLCode=wms_orderdetail.ExternOrderNumber ) str11
             ,(select top 1 str10 from wms_product p where p.storerId=wms_orderdetail.customerid and p.sku=wms_orderdetail.sku ) as str12
             ,[str13]
             ,[str14]
             ,[str15]
             ,[str16]
             ,[str17]
             ,[str18]
             ,[str19]
             ,[str20]
             ,[DateTime1]
             ,[DateTime2]
             ,[DateTime3]
             ,[DateTime4]
             ,[DateTime5]
             ,[Int1]
             ,[Int2]
             ,[Int3]
             ,[Int4]
             ,[Int5] from wms_orderdetail where oid in (select oid from WMS_PackingBoxFlag where status=1 ) ");//.DataTableToList<WMS_OrderDetail>();
            var res = new List<(DataRow, List<WMS_OrderDetail>)>();
            foreach (DataRow item in dt.Rows)
            {
                long oid = Convert.ToInt64(item["OID"].ToString());

                var odlist = list.Select(" oid =" + oid);
                if (odlist.Any())
                {
                    try
                    {
                        var listdt = TransOderDetailList(odlist);
                        res.Add((item, listdt));
                    }
                    catch (Exception asd)
                    {
                        UpdateCalcBoxError(oid, "获取sku基础信息失败");
                    }
                }
            }
            return res;
        }
        private DataTable GetSKUBaseInfo(List<string> skus)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in skus)
            {
                sb.Append("'" + item + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            var skustr = sb.ToString();
            var list = SqlHelper.GetDateTable($"select a.sku,a.StandardVolume,a.StandardWeight,b.Division  from wms_product a left join WMS_ArticleDetail b on a.str10=b.ArticleNo where a.StorerID=109 and a.sku  in ({skustr}) ");//.DataTableToList<WMS_OrderDetail>();
            return list;
        }
        private List<WMS_OrderDetail> TransOderDetailList(DataRow[] dataRows)
        {
            if (dataRows.Length <= 0)
                return null;
            List<WMS_OrderDetail> res = new List<WMS_OrderDetail>();
            List<string> skus = dataRows.AsEnumerable().Select(m => m.Field<string>("SKU")).Distinct().ToList();
            var skubaseinfo = GetSKUBaseInfo(skus);
            foreach (DataRow item in dataRows)
            {
                WMS_OrderDetail wMS_OrderDetail = new WMS_OrderDetail();
                wMS_OrderDetail.OID = Convert.ToInt64(item["OID"].ToString());
                wMS_OrderDetail.SeqNo = Convert.ToInt64(item["ID"].ToString());
                wMS_OrderDetail.Qty = Convert.ToDecimal(item["Qty"].ToString());
                wMS_OrderDetail.str2 = item["Str2"].ToString();
                wMS_OrderDetail.str6 = item["str6"].ToString();
                wMS_OrderDetail.str9 = item["str9"].ToString();
                wMS_OrderDetail.str12 = item["str12"].ToString();
                wMS_OrderDetail.Location = item["Location"].ToString();
                wMS_OrderDetail.SKUType = new SKUType()
                {
                    Code = item["SKU"].ToString(),
                    Articleno = item["str12"].ToString(),
                    Lenght = skubaseinfo.Select($" sku ='{item["SKU"].ToString()}'")[0]["StandardVolume"].ToString().GetSKULength(),
                    Width = skubaseinfo.Select($" sku ='{item["SKU"].ToString()}'")[0]["StandardVolume"].ToString().GetSKUWidth(),
                    Height = skubaseinfo.Select($" sku ='{item["SKU"].ToString()}'")[0]["StandardVolume"].ToString().GetSKUHeight(),
                    NetWeight = Convert.ToDecimal(skubaseinfo.Select($" sku ='{item["SKU"].ToString()}'")[0]["StandardWeight"].ToString()),
                    GroupType = skubaseinfo.Select($" sku ='{item["SKU"].ToString()}'")[0]["Division"].ToString(),

                };
                res.Add(wMS_OrderDetail);
            }
            return res;
        }

        public List<BoxInfo<WMS_OrderDetail>> GetCalcInfos<T>(List<WMS_OrderDetail> wMS_OrderDetails) where T : SkuInfo
        {
            boxInfos.Clear();
            //原箱
            var basecartons = wMS_OrderDetails.Where(m => !string.IsNullOrEmpty(m.str2)).ToList();
            if (basecartons.Any())
            {
                var basenumbers = basecartons.Select(m => m.str2).Distinct().ToList();
                var Listbaseboxinfo = new List<BoxInfo<WMS_OrderDetail>>();
                foreach (var item in basenumbers)
                {
                    BoxInfo<WMS_OrderDetail> boxInfo = new BoxInfo<WMS_OrderDetail>();
                    boxInfo.BoxNumber = SnowFlakeHelper.GetSnowInstance().NextId().ToString();
                    boxInfo.BoxType = new BoxType();
                    boxInfo.BoxType.ModelType = "Base";
                    boxInfo.BoxType.Str1 = item;

                    //boxInfo.BoxType.Str1 = SnowFlakeHelper.GetSnowInstance().NextId().ToString();

                    boxInfo.SkuInfos = wMS_OrderDetails.Where(m => m.str2 == item).ToList();
                    var BoxInfo = DerivationBoxInfo(boxInfo.SkuInfos.First().SKUType.GroupType, boxInfo.SkuInfos.Sum(a => a.Qty));

                    boxInfo.BoxType.Lenght = BoxInfo.Item1;
                    boxInfo.BoxType.Width = BoxInfo.Item2;
                    boxInfo.BoxType.Height = BoxInfo.Item3;
                    //boxInfo.BoxType.Volumn = BoxInfo.Item4;

                    boxInfos.Add(boxInfo);
                }
            }
            var calcskuinfos = wMS_OrderDetails.Where(m => string.IsNullOrEmpty(m.str2)).ToList();
            try
            {
                if (calcskuinfos.Any())
                {
                    if (calcskuinfos.First().str9 == "OB-经销商")
                    {
                        calcskuinfos.ForEach((m) => { m.SKUType.IsSingle = false; });
                    }
                    else if (
                         calcskuinfos.First().str9 == "OB-大陆仓" ||
                         calcskuinfos.First().str9 == "OB-港台仓" ||
                         calcskuinfos.First().str9 == "OB-DH"
                       )
                    {
                        if (calcskuinfos.First().str10 == "HK Main DC(SZ QHW)" ||
                            calcskuinfos.First().str10 == "HK Return Warehouse(HK)" ||
                            calcskuinfos.First().str10 == "Nike Taiwan" ||
                            calcskuinfos.First().str10 == "Nike China CLC" ||
                            calcskuinfos.First().str10 == "Nike China CLC 3rd building" ||
                            calcskuinfos.First().str10 == "Nike China MN"
                            //|| calcskuinfos.First().str10 == "Phoenix Center B2B(Ph.C.B2B)"
                            )
                        {
                            calcskuinfos.ForEach((m) => { m.SKUType.IsSingle = false; });
                        }
                    }
                    //DN中带有SK VAS code(Customer pack code)时，此SKU装箱时不支持混SKU装箱
                    //2023-07-17 新逻辑 由原来的不可以混箱，改为可以混箱子
                    //if (!string.IsNullOrEmpty(calcskuinfos.First().str11) && calcskuinfos.First().str11 == "SK")
                    //{
                    //    calcskuinfos.ForEach((m) => { m.SKUType.IsSingle = true; });
                    //}
                    var calcboxinfos = base.CalcSplietBox<WMS_OrderDetail>(calcskuinfos).ToList();
                    boxInfos.AddRange(calcboxinfos);
                }
            }
            catch (Exception ex)
            {
                UpdateCalcBoxError(wMS_OrderDetails.First().OID, "算箱失败," + ex.Message);
                boxInfos.Clear();
            }
            //需计算箱
            return boxInfos;
        }
        /// <summary>
        /// 推导长宽高
        /// </summary>
        /// <returns></returns>
        private (decimal, decimal, decimal, decimal) DerivationBoxInfo(string Division, decimal Qty)
        {
            //鞋子12双/箱
            if (Division == "20" && Qty == 12)
            {
                return (66, 48, 31, (66 * 48 * 31));
            }
            //鞋子6双/箱
            if (Division == "20" && Qty == 6)
            {
                return (53, 41, 38, (53 * 41 * 38));
            }
            //鞋子非6/12双/箱
            if (Division == "20")
            {
                return (60, 43, 33, (60 * 43 * 33));
            }
            //衣服/配件：
            return (58, 40, 25, (58 * 40 * 25));

        }
        public void SaveBoxInfo(DataRow order, List<BoxInfo<WMS_OrderDetail>> boxinfos)
        {
            var savelist = new List<WMS_CalcBoxinfo>();
            foreach (var box in boxinfos)
            {
                foreach (var sku in box.SkuInfos)
                {
                    WMS_CalcBoxinfo wMS_CalcBoxinfo = new WMS_CalcBoxinfo();

                    wMS_CalcBoxinfo.OrderNumber = order["OrderNumber"].ToString();
                    wMS_CalcBoxinfo.ExternOrderNumber = order["ExternOrderNumber"].ToString();
                    wMS_CalcBoxinfo.OID = Convert.ToInt64(order["OID"].ToString());
                    wMS_CalcBoxinfo.PackageNumber = box.BoxNumber;
                    wMS_CalcBoxinfo.BoxCode = box.BoxType.Code;
                    wMS_CalcBoxinfo.ModelType = box.BoxType.ModelType;
                    wMS_CalcBoxinfo.BoxName = box.BoxType.Name;
                    wMS_CalcBoxinfo.SKUSeq = sku.SeqNo;
                    wMS_CalcBoxinfo.SKU = sku.SKUType.Code;
                    wMS_CalcBoxinfo.Qty = sku.Qty;
                    wMS_CalcBoxinfo.Location = sku.Location;
                    wMS_CalcBoxinfo.UsedVolumn = box.UsedVolumn;
                    wMS_CalcBoxinfo.UsedWeight = box.UsedWeight;

                    wMS_CalcBoxinfo.BoxLength = box.BoxType.Lenght;
                    wMS_CalcBoxinfo.BoxWidth = box.BoxType.Width;
                    wMS_CalcBoxinfo.BoxHeight = box.BoxType.Height;
                    wMS_CalcBoxinfo.Str1 = box.BoxType.Str1;

                    savelist.Add(wMS_CalcBoxinfo);
                    //wMS_CalcBoxinfo.Str1
                    //wMS_CalcBoxinfo.Str2
                    //wMS_CalcBoxinfo.Str3

                }
            }
            string sqlupdate = $"Update WMS_PackingBoxFlag set status=5 where oid ={Convert.ToInt64(order["OID"].ToString())}";
            SqlHelper.InsertList("WMS_CalcBoxinfo", savelist, sqlupdate);
            if (OnSaved != null)
                OnSaved.Invoke(null, new SavedEventArgs(savelist));
        }

        public void UpdateCalcBoxError(long oid, string errorinfo)
        {
            string sqlupdate = $"Update WMS_PackingBoxFlag set status=3,str2='{errorinfo}' where oid ={oid}";
            SqlHelper.ExecSql(sqlupdate);
        }
    }
}
