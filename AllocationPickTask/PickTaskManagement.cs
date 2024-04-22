
using AllocationPickTask.Common;
using AllocationPickTask.Entity;
using AllocationPickTask.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AllocationPickTask
{
    public class PickTaskManagement : BaseAccessor
    {
        public void CreatePickTask()
        {

            //判断是不是已经下发过拣货任务
            //获取订单信息
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select * from WMS_Order where id in (select OID from  WMS_PackingBoxFlag where Status=5)");
            sb.Append(@"select  WMS_OrderDetail.[ID]
             ,WMS_OrderDetail.[OID]
             ,WMS_OrderDetail.[OrderNumber]
             ,WMS_OrderDetail.[ExternOrderNumber]
             ,WMS_OrderDetail.[POID]
             ,WMS_OrderDetail.[PODID]
             ,WMS_OrderDetail.[CustomerID]
             ,WMS_OrderDetail.[CustomerName]
             ,WMS_OrderDetail.[LineNumber]
             ,WMS_OrderDetail.[SKU]
             ,WMS_OrderDetail.[UPC]
             ,WMS_OrderDetail.[GoodsName]
             ,WMS_OrderDetail.[GoodsType]
             ,WMS_OrderDetail.[Lot]
             ,WMS_OrderDetail.[BoxNumber]
             ,WMS_OrderDetail.[BatchNumber]
             ,WMS_OrderDetail.[Unit]
             ,WMS_OrderDetail.[Specifications]
             ,WMS_OrderDetail.[Warehouse]
             ,WMS_OrderDetail.[Area]
             ,WMS_OrderDetail.[Location]
             ,wms_calcboxinfo.Qty Qty
             ,WMS_OrderDetail.[Picker]
             ,WMS_OrderDetail.[PickTime]
             ,WMS_OrderDetail.[Confirmer]
             ,WMS_OrderDetail.[ConfirmeTime]
             ,WMS_OrderDetail.[Creator]
             ,WMS_OrderDetail.[CreateTime]
             ,WMS_OrderDetail.[Updator]
             ,WMS_OrderDetail.[UpdateTime]
             ,WMS_OrderDetail.[Remark]
             ,WMS_OrderDetail.[str1]
             ,WMS_OrderDetail.[str2]
             ,WMS_OrderDetail.[str3]
             ,WMS_OrderDetail.[str4]
             ,WMS_OrderDetail.[str5]
             ,WMS_OrderDetail.[str6]
             ,WMS_OrderDetail.[str7]
             ,WMS_OrderDetail.[str8]
             ,WMS_OrderDetail.[str9]
             ,WMS_OrderDetail.[str10]
             ,WMS_OrderDetail.[str11]
             ,WMS_OrderDetail.[str12]
             ,WMS_OrderDetail.[str13]
             ,WMS_OrderDetail.[str14]
             ,WMS_OrderDetail.[str15]
             ,WMS_OrderDetail.[str16]
             ,WMS_OrderDetail.[str17]
             ,WMS_OrderDetail.[str18]
             ,WMS_OrderDetail.[str19]
             ,WMS_OrderDetail.[str20]
             ,WMS_OrderDetail.[DateTime1]
             ,WMS_OrderDetail.[DateTime2]
             ,WMS_OrderDetail.[DateTime3]
             ,WMS_OrderDetail.[DateTime4]
             ,WMS_OrderDetail.[DateTime5]
             ,WMS_OrderDetail.[Int1]
             ,WMS_OrderDetail.[Int2]
             ,WMS_OrderDetail.[Int3]
             ,WMS_OrderDetail.[Int4]
             ,WMS_OrderDetail.[Int5] 
            ,wms_calcboxinfo.PackageNumber
            ,wms_calcboxinfo.BoxCode
            ,wms_calcboxinfo.ModelType
            ,wms_calcboxinfo.BoxName
            ,wms_calcboxinfo.BoxName
            ,wms_calcboxinfo.Qty BoxQty
            ,wms_calcboxinfo.UsedWeight
            ,wms_calcboxinfo.UsedVolumn
            ,wms_calcboxinfo.BoxLength
            ,wms_calcboxinfo.BoxWidth
            ,wms_calcboxinfo.BoxHeight
            from  wms_calcboxinfo left join
	        WMS_OrderDetail on wms_calcboxinfo.SKUSeq=WMS_OrderDetail.ID
	        where wms_calcboxinfo.oid in (select OID from  WMS_PackingBoxFlag where Status=5)");

            DataSet ds = this.ExecuteDataSetBySqlString(sb.ToString());
            var OrderCollection = ds.Tables[0].ConvertToEntityCollection<OrderInfo>();
            var OrderDetailCollection = ds.Tables[1].ConvertToEntityCollection<OrderDetailInfo>();

            //未获取到数据，返回
            if (OrderCollection.Count() == 0)
            {
                return;
            }
            //获取订单中的产品信息
            //var GetProductResult = new OrderManagementService().GetOrderProductDetail(Ids);
            //#region 将产品信息冗余到订单中进行排序
            //GetOrderResult.Result.OrderDetailCollection.AsParallel().ForAll((a) =>
            //{
            //    a.Size = GetProductResult.Result.Where(p => p.SKU == a.SKU).First().Str9;
            //    a.Article = GetProductResult.Result.Where(p => p.SKU == a.SKU).First().Str10;
            //    a.BU = GetProductResult.Result.Where(p => p.SKU == a.SKU).First().Str11;
            //});
            //#endregion
            //声明主订单容器
            List<OrderInfo> orderInfos = new List<OrderInfo>();
            //声明明细订单容器
            List<OrderDetailInfo> orderDetailInfos = new List<OrderDetailInfo>();
            //拆分的逻辑
            var result = OrderDetailCollection.GroupBy(a => new { a.OrderNumber, a.ExternOrderNumber, a.Article, a.Size, a.BU });
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderInfo, PickTask>()
                   .ForMember(a => a.OID, opt => opt.MapFrom(c => c.ID))
                   //添加创建人为当前用户
                   .ForMember(a => a.Creator, opt => opt.MapFrom(c => "User"))
                   .ForMember(a => a.CreateTime, opt => opt.MapFrom(c => DateTime.Now))
                   //忽略修改时间
                   .ForMember(a => a.UpdateTime, opt => opt.Ignore());

                cfg.CreateMap<OrderDetailInfo, PickTaskDetail>()
                     .ForMember(a => a.OID, opt => opt.MapFrom(c => c.OID))
                     .ForMember(a => a.ODID, opt => opt.MapFrom(c => c.ID))
                     .ForMember(a => a.str2, opt => opt.MapFrom(c => c.str2))
                     .ForMember(a => a.str3, opt => opt.MapFrom(c => c.PackageNumber))
                     .ForMember(a => a.IsFCL, opt => opt.MapFrom(c => c.ModelType == "Base" ? 1 : 0))
                     //.ForMember(a => a.BoxNo, opt => opt.MapFrom(c => c.BoxCode))
                     .ForMember(a => a.BoxName, opt => opt.MapFrom(c => c.BoxName))
                     .ForMember(a => a.BoxType, opt => opt.MapFrom(c => c.BoxCode))
                     .ForMember(a => a.NetWeight, opt => opt.MapFrom(c => c.UsedWeight))
                     .ForMember(a => a.Length, opt => opt.MapFrom(c => c.BoxLength))
                     .ForMember(a => a.Width, opt => opt.MapFrom(c => c.BoxWidth))
                     .ForMember(a => a.Height, opt => opt.MapFrom(c => c.BoxHeight))
                     .ForMember(a => a.Volumn, opt => opt.MapFrom(c => c.UsedVolumn))

                     .ForMember(a => a.PickTaskNumber, opt => opt.MapFrom(c => c.PickingTaskNo))
                     //添加创建人为当前用户
                     .ForMember(a => a.Creator, opt => opt.MapFrom(c => "User"))
                     .ForMember(a => a.CreateTime, opt => opt.MapFrom(c => DateTime.Now))
                     //忽略修改时间
                     .ForMember(a => a.UpdateTime, opt => opt.Ignore());
            });
            Mapper mapper = new Mapper(config);
            //新逻辑:一个箱号，一个小任务，任务主信息为订单，明细信息为小任务号，按照一箱一个任务（使用同一个大任务号，方便任务拆分，还是在同一个订单里面）；


            //将订单容器中的数据，导入到拣货任务容器
            //声明拣货任务容器
            List<PickTask> pickTask = new List<PickTask>();
            List<PickTaskDetail> pickTaskDetails = new List<PickTaskDetail>();
            //先处理明细数据，然后根据明细数据数量生成主数据数量

            OrderCollection.ToList().ForEach(item =>
            {
                var order = OrderCollection.Where(c => c.OrderNumber == item.OrderNumber);
                var pickdata = mapper.DefaultContext.Mapper.Map<PickTask>(order.First());
                pickTask.Add(pickdata);
                orderDetailInfos = OrderDetailCollection.Where(c => c.OrderNumber == item.OrderNumber).ToList();
                orderDetailInfos.ForEach(a =>
                {
                    a.PickingTaskNo = a.OrderNumber;

                });
                pickTaskDetails.AddRange(mapper.DefaultContext.Mapper.Map<List<PickTaskDetail>>(orderDetailInfos));

            });
            pickTaskDetails.GroupBy(b => b.str3).Each((index, a) =>
            {
                var PickTaskNumber = SnowFlakeHelper.GetSnowInstance().NextId().ToString();
                a.ToList().ForEach(c =>
                {
                    c.BoxLineNo = (index + 1).ToString();
                    c.PickTaskNumber = PickTaskNumber;
                    if (c.IsFCL == 0)
                    {
                        c.BoxNo = c.str3;
                    }
                });

            });

            var response = CreatePickTask(pickTask, pickTaskDetails);

        }
        /// <summary>
        /// 获取出库单中明细的产品信息
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public string CreatePickTask(List<PickTask> pickTask, List<PickTaskDetail> pickTaskDetails)
        {
            string message = "";
            using (SqlConnection conn = new SqlConnection(BaseAccessor._dataBase.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_WMS_CreatePickTask", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pickTask", pickTask.Select(p => new WMSPickTaskToDb(p)));
                    cmd.Parameters[0].SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.AddWithValue("@pickTaskDetails", pickTaskDetails.Select(p => new WMSPickTaskDetailToDb(p)));
                    cmd.Parameters[1].SqlDbType = SqlDbType.Structured;
                    //cmd.Parameters.AddWithValue("@message", message);
                    //cmd.Parameters[2].SqlDbType = SqlDbType.NVarChar;
                    //cmd.Parameters[2].Direction = ParameterDirection.Output;
                    //cmd.Parameters[2].Size = 500;
                    cmd.CommandTimeout = 300;
                    conn.Open();

                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    //message = sda.SelecteCommand.Parameters["@message"].Value.ToString();
                    conn.Close();
                    return "成功";
                }
                catch (Exception ex)
                {
                    return message + "(" + ex.Message + ")";
                }

            }

        }

    }
}
