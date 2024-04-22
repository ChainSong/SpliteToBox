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
    public class WMSPickTaskDetailToDb : SqlDataRecord
    {
        public WMSPickTaskDetailToDb(PickTaskDetail wmsInfo)
            : base(s_metadata)
        {

            SetSqlInt64(0, wmsInfo.ID);
            SetSqlInt64(1, wmsInfo.PTID);
            SetSqlInt64(2, wmsInfo.OID);
            SetSqlInt64(3, wmsInfo.ODID);
            SetSqlString(4, wmsInfo.OrderNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(5, wmsInfo.PickTaskNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(6, wmsInfo.ExternOrderNumber ?? SqlTypes.SqlString.Null);
            SetSqlInt64(7, wmsInfo.POID);
            SetSqlInt64(8, wmsInfo.PODID ?? SqlTypes.SqlInt64.Null);
            SetSqlInt64(9, wmsInfo.CustomerID ?? SqlTypes.SqlInt64.Null);
            SetSqlString(10, wmsInfo.CustomerName ?? SqlTypes.SqlString.Null);
            SetSqlString(11, wmsInfo.LineNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(12, wmsInfo.SKU ?? SqlTypes.SqlString.Null);
            SetSqlString(13, wmsInfo.UPC ?? SqlTypes.SqlString.Null);
            SetSqlString(14, wmsInfo.GoodsName ?? SqlTypes.SqlString.Null);
            SetSqlString(15, wmsInfo.GoodsType ?? SqlTypes.SqlString.Null);
            SetSqlString(16, wmsInfo.Lot ?? SqlTypes.SqlString.Null);
            SetSqlString(17, wmsInfo.BoxNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(18, wmsInfo.BatchNumber ?? SqlTypes.SqlString.Null);
            SetSqlString(19, wmsInfo.Unit ?? SqlTypes.SqlString.Null);
            SetSqlString(20, wmsInfo.Specifications ?? SqlTypes.SqlString.Null);
            SetSqlString(21, wmsInfo.Warehouse ?? SqlTypes.SqlString.Null);
            SetSqlString(22, wmsInfo.Area ?? SqlTypes.SqlString.Null);
            SetSqlString(23, wmsInfo.Location ?? SqlTypes.SqlString.Null);
            SetSqlDecimal(24, wmsInfo.Qty ?? SqlTypes.SqlDecimal.Null);
            SetSqlString(25, wmsInfo.Picker ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(26, wmsInfo.PickTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlString(27, wmsInfo.Confirmer ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(28, wmsInfo.ConfirmeTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlInt32(29, wmsInfo.Status);
            SetSqlString(30, wmsInfo.BoxType ?? SqlTypes.SqlString.Null);
            SetSqlString(31, wmsInfo.BoxNo ?? SqlTypes.SqlString.Null);

            SetSqlString(32, wmsInfo.BoxLineNo ?? SqlTypes.SqlString.Null);
            SetSqlInt32(33, wmsInfo.IsFCL);
            SetSqlString(34, wmsInfo.Creator ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(35, wmsInfo.CreateTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlString(36, wmsInfo.Updator ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(37, wmsInfo.UpdateTime ?? SqlTypes.SqlDateTime.Null);
            SetSqlString(38, wmsInfo.Remark ?? SqlTypes.SqlString.Null);
            SetSqlString(39, wmsInfo.str1 ?? SqlTypes.SqlString.Null);
            SetSqlString(40, wmsInfo.str2 ?? SqlTypes.SqlString.Null);
            SetSqlString(41, wmsInfo.str3 ?? SqlTypes.SqlString.Null);
            SetSqlString(42, wmsInfo.str4 ?? SqlTypes.SqlString.Null);
            SetSqlString(43, wmsInfo.str5 ?? SqlTypes.SqlString.Null);
            SetSqlString(44, wmsInfo.str6 ?? SqlTypes.SqlString.Null);
            SetSqlString(45, wmsInfo.str7 ?? SqlTypes.SqlString.Null);
            SetSqlString(46, wmsInfo.str8 ?? SqlTypes.SqlString.Null);
            SetSqlString(47, wmsInfo.str9 ?? SqlTypes.SqlString.Null);
            SetSqlString(48, wmsInfo.str10 ?? SqlTypes.SqlString.Null);
            SetSqlString(49, wmsInfo.str11 ?? SqlTypes.SqlString.Null);
            SetSqlString(50, wmsInfo.str12 ?? SqlTypes.SqlString.Null);
            SetSqlString(51, wmsInfo.str13 ?? SqlTypes.SqlString.Null);
            SetSqlString(52, wmsInfo.str14 ?? SqlTypes.SqlString.Null);
            SetSqlString(53, wmsInfo.str15 ?? SqlTypes.SqlString.Null);
            SetSqlString(54, wmsInfo.str16 ?? SqlTypes.SqlString.Null);
            SetSqlString(55, wmsInfo.str17 ?? SqlTypes.SqlString.Null);
            SetSqlString(56, wmsInfo.str18 ?? SqlTypes.SqlString.Null);
            SetSqlString(57, wmsInfo.str19 ?? SqlTypes.SqlString.Null);
            SetSqlString(58, wmsInfo.str20 ?? SqlTypes.SqlString.Null);
            SetSqlDateTime(59, wmsInfo.DateTime1 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(60, wmsInfo.DateTime2 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(61, wmsInfo.DateTime3 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(62, wmsInfo.DateTime4 ?? SqlTypes.SqlDateTime.Null);
            SetSqlDateTime(63, wmsInfo.DateTime5 ?? SqlTypes.SqlDateTime.Null);
            SetSqlInt32(64, wmsInfo.Int1 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(65, wmsInfo.Int2 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(66, wmsInfo.Int3 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(67, wmsInfo.Int4 ?? SqlTypes.SqlInt32.Null);
            SetSqlInt32(68, wmsInfo.Int5 ?? SqlTypes.SqlInt32.Null);
            SetSqlDecimal(69, wmsInfo.PickQty);
            SetSqlString(70, wmsInfo.BoxName ?? SqlTypes.SqlString.Null);
            SetSqlString(71, wmsInfo.NetWeight ?? SqlTypes.SqlString.Null);
            SetSqlString(72, wmsInfo.Length ?? SqlTypes.SqlString.Null);
            SetSqlString(73, wmsInfo.Width ?? SqlTypes.SqlString.Null);
            SetSqlString(74, wmsInfo.Height ?? SqlTypes.SqlString.Null);
            SetSqlString(75, wmsInfo.Volumn ?? SqlTypes.SqlString.Null);






        }

        private static readonly SqlMetaData[] s_metadata =
        {
            new SqlMetaData("ID", SqlDbType.BigInt),
            new SqlMetaData("PTID", SqlDbType.BigInt),
            new SqlMetaData("OID", SqlDbType.BigInt),
            new SqlMetaData("ODID", SqlDbType.BigInt),
            new SqlMetaData("OrderNumber", SqlDbType.VarChar,50),
            new SqlMetaData("PickTaskNumber",SqlDbType.VarChar,50),
            new SqlMetaData("ExternOrderNumber", SqlDbType.VarChar,50),
            new SqlMetaData("POID", SqlDbType.BigInt),
            new SqlMetaData("PODID", SqlDbType.BigInt),
            new SqlMetaData("CustomerID", SqlDbType.BigInt),
            new SqlMetaData("CustomerName", SqlDbType.VarChar,50),
            new SqlMetaData("LineNumber",SqlDbType.VarChar,50),
            new SqlMetaData("SKU", SqlDbType.VarChar,50),
            new SqlMetaData("UPC", SqlDbType.VarChar,100),
            new SqlMetaData("GoodsName", SqlDbType.VarChar,50),
            new SqlMetaData("GoodsType", SqlDbType.VarChar,50),
            new SqlMetaData("Lot", SqlDbType.VarChar,50),
            new SqlMetaData("BoxNumber", SqlDbType.VarChar,100),
            new SqlMetaData("BatchNumber",SqlDbType.VarChar,50),
            new SqlMetaData("Unit", SqlDbType.VarChar,100),
            new SqlMetaData("Specifications", SqlDbType.VarChar,100),
            new SqlMetaData("Warehouse", SqlDbType.VarChar,50),
            new SqlMetaData("Area", SqlDbType.VarChar,50),
            new SqlMetaData("Location",SqlDbType.VarChar,50),
            new SqlMetaData("Qty", SqlDbType.Decimal),
            new SqlMetaData("Picker", SqlDbType.VarChar,50),
            new SqlMetaData("PickTime", SqlDbType.DateTime),
            new SqlMetaData("Confirmer", SqlDbType.VarChar,50),
            new SqlMetaData("ConfirmeTime", SqlDbType.DateTime),
            new SqlMetaData("Status", SqlDbType.Int),
            new SqlMetaData("BoxType", SqlDbType.VarChar,50),
            new SqlMetaData("BoxNo", SqlDbType.VarChar,50),
            new SqlMetaData("BoxLineNo", SqlDbType.VarChar,50),
            new SqlMetaData("IsFCL", SqlDbType.Int),
            new SqlMetaData("Creator", SqlDbType.VarChar,50),
            new SqlMetaData("CreateTime", SqlDbType.DateTime),
            new SqlMetaData("Updator", SqlDbType.VarChar,50),
            new SqlMetaData("UpdateTime", SqlDbType.DateTime),
            new SqlMetaData("Remark", SqlDbType.VarChar,500),
             new SqlMetaData("str1", SqlDbType.VarChar,50),
             new SqlMetaData("str2", SqlDbType.VarChar,50),
             new SqlMetaData("str3", SqlDbType.VarChar,50),
             new SqlMetaData("str4", SqlDbType.VarChar,50),
             new SqlMetaData("str5", SqlDbType.VarChar,50),
             new SqlMetaData("str6", SqlDbType.VarChar,50),
             new SqlMetaData("str7", SqlDbType.VarChar,50),
             new SqlMetaData("str8", SqlDbType.VarChar,50),
             new SqlMetaData("str9", SqlDbType.VarChar,50),
            new SqlMetaData("str10", SqlDbType.VarChar,50),
            new SqlMetaData("str11", SqlDbType.VarChar,50),
            new SqlMetaData("str12", SqlDbType.VarChar,50),
            new SqlMetaData("str13", SqlDbType.VarChar,50),
            new SqlMetaData("str14", SqlDbType.VarChar,50),
            new SqlMetaData("str15", SqlDbType.VarChar,50),
            new SqlMetaData("str16", SqlDbType.VarChar,200),
            new SqlMetaData("str17", SqlDbType.VarChar,200),
            new SqlMetaData("str18", SqlDbType.VarChar,200),
            new SqlMetaData("str19", SqlDbType.VarChar,200),
            new SqlMetaData("str20", SqlDbType.VarChar,500),
            new SqlMetaData("DateTime1", SqlDbType.DateTime),
            new SqlMetaData("DateTime2", SqlDbType.DateTime),
            new SqlMetaData("DateTime3", SqlDbType.DateTime),
            new SqlMetaData("DateTime4", SqlDbType.DateTime),
            new SqlMetaData("DateTime5", SqlDbType.DateTime),
            new SqlMetaData("Int1", SqlDbType.Int),
            new SqlMetaData("Int2", SqlDbType.Int),
            new SqlMetaData("Int3", SqlDbType.Int),
            new SqlMetaData("Int4", SqlDbType.Int),
            new SqlMetaData("Int5", SqlDbType.Int),
            new SqlMetaData("PickQty", SqlDbType.Decimal),
            new SqlMetaData("BoxName", SqlDbType.VarChar,50),
            new SqlMetaData("NetWeight", SqlDbType.VarChar,50),
            new SqlMetaData("Length", SqlDbType.VarChar,50),
            new SqlMetaData("Width ", SqlDbType.VarChar,50),
            new SqlMetaData("Height", SqlDbType.VarChar,50),
            new SqlMetaData("Volumn", SqlDbType.VarChar,50),

        };
    }
}
