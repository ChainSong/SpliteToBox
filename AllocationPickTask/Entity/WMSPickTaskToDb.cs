using AllocationPickTask.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlTypes = global::System.Data.SqlTypes;
namespace AllocationPickTask.Entity
{
    public class WMSPickTaskToDb : SqlDataRecord
    {
        public WMSPickTaskToDb(PickTask wmsInfo)
            : base(s_metadata)
        {

            SetSqlInt64(0, wmsInfo.ID);
            SetSqlInt64(1, wmsInfo.OID);
            SetSqlString(2, wmsInfo.WaveNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(3, wmsInfo.OrderNumber ?? SqlTypes.SqlString.Null);
            SetSqlInt64(4, wmsInfo.POID);
            SetSqlString(5, wmsInfo.PreOrderNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(6, wmsInfo.PickTaskNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(7, wmsInfo.ExternOrderNumber ?? SqlTypes.SqlString.Null);
            SetSqlInt64(8, wmsInfo.CustomerID);
            SetSqlString(9, wmsInfo.CustomerName ?? SqlTypes.SqlString.Null);
            SetSqlString(10, wmsInfo.Warehouse ?? SqlTypes.SqlString.Null);
            SetSqlString(11, wmsInfo.OrderType ?? SqlTypes.SqlString.Null);
            SetSqlInt32(12, wmsInfo.Status ?? SqlTypes.SqlInt32.Null);
            SetSqlDateTime(13, wmsInfo.OrderTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlString(14, wmsInfo.Province ?? SqlTypes.SqlString.Null);
            SetSqlString(15, wmsInfo.City ?? SqlTypes.SqlString.Null);
            SetSqlString(16, wmsInfo.District ?? SqlTypes.SqlString.Null);
            SetSqlString(17, wmsInfo.Address ?? SqlTypes.SqlString.Null);
            SetSqlString(18, wmsInfo.Consignee ?? SqlTypes.SqlString.Null);
            SetSqlString(19, wmsInfo.Contact ?? SqlTypes.SqlString.Null);
            SetSqlInt32(20, wmsInfo.IsMerged ?? SqlTypes.SqlInt32.Null);
            SetSqlString(21, wmsInfo.MergeNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(22, wmsInfo.ExpressCompany ?? SqlTypes.SqlString.Null);
            SetSqlString(23, wmsInfo.ExpressNumber ?? SqlTypes.SqlString.Null);
            SetSqlInt32(24, wmsInfo.ExpressStatus ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(25, wmsInfo.PickPrintCount ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(26, wmsInfo.ExpressPrintCount ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(27, wmsInfo.PodPrintCount ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(28, wmsInfo.OtherPrintCount ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(29, wmsInfo.DetailCount ?? SqlTypes.SqlInt32.Null);
            SetSqlString(30, wmsInfo.Creator ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(31, wmsInfo.CreateTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlString(32, wmsInfo.Updator ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(33, wmsInfo.UpdateTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(34, wmsInfo.CompleteDate ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(35, wmsInfo.WaveTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlString(36, wmsInfo.Remark ?? SqlTypes.SqlString.Null);
            SetSqlString(37, wmsInfo.str1 ?? SqlTypes.SqlString.Null);
            SetSqlString(38, wmsInfo.str2 ?? SqlTypes.SqlString.Null);
            SetSqlString(39, wmsInfo.str3 ?? SqlTypes.SqlString.Null);
            SetSqlString(40, wmsInfo.str4 ?? SqlTypes.SqlString.Null);
            SetSqlString(41, wmsInfo.str5 ?? SqlTypes.SqlString.Null);
            SetSqlString(42, wmsInfo.str6 ?? SqlTypes.SqlString.Null);
            SetSqlString(43, wmsInfo.str7 ?? SqlTypes.SqlString.Null);
            SetSqlString(44, wmsInfo.str8 ?? SqlTypes.SqlString.Null);
            SetSqlString(45, wmsInfo.str9 ?? SqlTypes.SqlString.Null);
            SetSqlString(46, wmsInfo.str10 ?? SqlTypes.SqlString.Null);
            SetSqlString(47, wmsInfo.str11 ?? SqlTypes.SqlString.Null);
            SetSqlString(48, wmsInfo.str12 ?? SqlTypes.SqlString.Null);
            SetSqlString(49, wmsInfo.str13 ?? SqlTypes.SqlString.Null);
            SetSqlString(50, wmsInfo.str14 ?? SqlTypes.SqlString.Null);
            SetSqlString(51, wmsInfo.str15 ?? SqlTypes.SqlString.Null);
            SetSqlString(52, wmsInfo.str16 ?? SqlTypes.SqlString.Null);
            SetSqlString(53, wmsInfo.str17 ?? SqlTypes.SqlString.Null);
            SetSqlString(54, wmsInfo.str18 ?? SqlTypes.SqlString.Null);
            SetSqlString(55, wmsInfo.str19 ?? SqlTypes.SqlString.Null);
            SetSqlString(56, wmsInfo.str20 ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(57, wmsInfo.DateTime1 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(58, wmsInfo.DateTime2 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(59, wmsInfo.DateTime3 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(60, wmsInfo.DateTime4 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(61, wmsInfo.DateTime5 ?? SqlTypes.SqlDateTime.Null);
            SetSqlInt32(62, wmsInfo.Int1 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(63, wmsInfo.Int2 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(64, wmsInfo.Int3 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(65, wmsInfo.Int4 ?? System.Data.SqlTypes.SqlInt32.Null);
            SetSqlInt32(66, wmsInfo.Int5 ?? SqlTypes.SqlInt32.Null);





        }

        private static readonly SqlMetaData[] s_metadata =
        {
            new SqlMetaData("ID", SqlDbType.BigInt),
            new SqlMetaData("OID", SqlDbType.BigInt),
            new SqlMetaData("WaveNumber", SqlDbType.VarChar,100),
            new SqlMetaData("OrderNumber", SqlDbType.VarChar,50),
            new SqlMetaData("POID", SqlDbType.BigInt),
            new SqlMetaData("PreOrderNumber", SqlDbType.VarChar,50),
            new SqlMetaData("PickTaskNumber", SqlDbType.VarChar,50),
            new SqlMetaData("ExternOrderNumber",  SqlDbType.VarChar,50),
            new SqlMetaData("CustomerID", SqlDbType.BigInt),
            new SqlMetaData("CustomerName",  SqlDbType.VarChar,50),
            new SqlMetaData("Warehouse",  SqlDbType.VarChar,50),
            new SqlMetaData("OrderType", SqlDbType.VarChar,50),
            new SqlMetaData("Status", SqlDbType.Int),
            new SqlMetaData("OrderTime", SqlDbType.DateTime),
            new SqlMetaData("Province",  SqlDbType.VarChar,50),
            new SqlMetaData("City",  SqlDbType.VarChar,50),
            new SqlMetaData("District", SqlDbType.VarChar,50),
            new SqlMetaData("Address", SqlDbType.VarChar,200),
            new SqlMetaData("Consignee",  SqlDbType.VarChar,50),
            new SqlMetaData("Contact",  SqlDbType.VarChar,50),
            new SqlMetaData("IsMerged", SqlDbType.Int),
            new SqlMetaData("MergeNumber",  SqlDbType.VarChar,50),
            new SqlMetaData("ExpressCompany", SqlDbType.VarChar,50),
            new SqlMetaData("ExpressNumber",  SqlDbType.VarChar,50),
            new SqlMetaData("ExpressStatus", SqlDbType.Int),
            new SqlMetaData("PickPrintCount", SqlDbType.Int),
            new SqlMetaData("ExpressPrintCount", SqlDbType.Int),
            new SqlMetaData("PodPrintCount", SqlDbType.Int),
            new SqlMetaData("OtherPrintCount", SqlDbType.Int),
            new SqlMetaData("DetailCount", SqlDbType.Int),
            new SqlMetaData("Creator", SqlDbType.VarChar,50),
            new SqlMetaData("CreateTime", SqlDbType.DateTime),
            new SqlMetaData("Updator", SqlDbType.VarChar,50),
            new SqlMetaData("UpdateTime", SqlDbType.DateTime),
            new SqlMetaData("CompleteDate", SqlDbType.DateTime),
            new SqlMetaData("WaveTime", SqlDbType.DateTime),
            new SqlMetaData("Remark", SqlDbType.VarChar,500),
            new SqlMetaData("str1",SqlDbType.VarChar,50),
            new SqlMetaData("str2",SqlDbType.VarChar,50),
            new SqlMetaData("str3",SqlDbType.VarChar,50),
            new SqlMetaData("str4",SqlDbType.VarChar,50),
            new SqlMetaData("str5",SqlDbType.VarChar,50),
            new SqlMetaData("str6",SqlDbType.VarChar,50),
            new SqlMetaData("str7",SqlDbType.VarChar,50),
            new SqlMetaData("str8",SqlDbType.VarChar,50),
            new SqlMetaData("str9",SqlDbType.VarChar,50),
            new SqlMetaData("str10", SqlDbType.VarChar,50),
            new SqlMetaData("str11", SqlDbType.VarChar,50),
            new SqlMetaData("str12", SqlDbType.VarChar,50),
            new SqlMetaData("str13", SqlDbType.VarChar,50),
            new SqlMetaData("str14", SqlDbType.VarChar,50),
            new SqlMetaData("str15", SqlDbType.VarChar,50),
            new SqlMetaData("str16",SqlDbType.VarChar,200),
            new SqlMetaData("str17",SqlDbType.VarChar,200),
            new SqlMetaData("str18",SqlDbType.VarChar,200),
            new SqlMetaData("str19",SqlDbType.VarChar,200),
            new SqlMetaData("str20",SqlDbType.VarChar,500),
            new SqlMetaData("DateTime1", SqlDbType.DateTime),
            new SqlMetaData("DateTime2", SqlDbType.DateTime),
            new SqlMetaData("DateTime3", SqlDbType.DateTime),
            new SqlMetaData("DateTime4", SqlDbType.DateTime),
            new SqlMetaData("DateTime5", SqlDbType.DateTime),
            new SqlMetaData("Int1", SqlDbType.Int),
            new SqlMetaData("Int2", SqlDbType.Int),
            new SqlMetaData("Int3", SqlDbType.Int),
            new SqlMetaData("Int4", SqlDbType.Int),
            new SqlMetaData("Int5 ", SqlDbType.Int),



        };
    }
}
