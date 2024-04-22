using System;
using System.Collections.Generic;
using System.Text;

namespace SpliteToBox
{
    /// <summary>
    /// 箱规模型
    /// </summary>
    public class BoxType
    {
        public int Index { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 箱类型 Base -原箱 Cala--计算箱
        /// </summary>
        public string ModelType { get; set; } = "Cala";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 长
        /// </summary>
        public decimal Lenght { get; set; }
        /// <summary>ganjue 
        /// 宽
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public decimal Height { get; set; }
        public string Str1 { get; set; }

        public string Str5 { get; set; }

        public string Str6 { get; set; }

        /// <summary>
        /// 实际最长边
        /// </summary>
        public decimal ActualLength
        {
            get
            {
                return (Lenght > Width ? Lenght : Width) > Height ? (Lenght > Width ? Lenght : Width) : Height;
            }
        }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volumn
        {
            get { return this.Lenght * this.Width * this.Height; }
        }

        /// <summary>
        /// 体积可用百分比 
        /// </summary>

        public decimal VolumnPercentage { get; set; } = 1;
        /// <summary>
        /// 可用体积
        /// </summary>
        public decimal CanUsedVolumn
        {
            get
            {
                return this.Volumn * this.VolumnPercentage;
            }
        }

        public decimal Weight { get; set; }
        /// <summary>
        /// 重量可用百分比 
        /// </summary>
        public decimal WeightPercentage { get; set; } = 1;
        /// <summary>
        /// 可用重量
        /// </summary>
        public decimal CanUsedWeight { get; set; }

    }


}
