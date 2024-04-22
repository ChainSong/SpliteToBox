using System;
using System.Collections.Generic;
using System.Text;

namespace SpliteToBox
{
    /// <summary>
    /// 物品模型
    /// </summary>
    public class SKUType
    {

        public string Code { get; set; }
        /// <summary>
        /// 长
        /// </summary>
        public decimal Lenght { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public decimal Height { get; set; }

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
        public decimal Volumn
        {
            get
            {
                return this.Lenght * this.Width * this.Height;
            }
        }

        /// <summary>
        /// 分组类型1
        /// </summary>

        public string GroupType { get; set; }

        /// <summary>
        /// 分组类型2
        /// </summary>
        public string GroupType2 { get; set; }
        /// <summary>
        /// 净重
        /// </summary>

        public decimal NetWeight { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 是否单独装箱
        /// </summary>
        public bool IsSingle { get; set; } = true;

        public string Articleno { get; set; }


    }
}
