
using SpliteToBox.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpliteToBox
{
    public abstract class SpliteBoxManage
    {

        private IEnumerable<BoxType> _boxlistAll;
        private IEnumerable<BoxType> _boxlist;

        public SpliteBoxManage()
        {
            //_boxlist = GetBoxInfo();
            //_boxlist = _boxlist.OrderByDescending(m => m.CanUsedVolumn);
            _boxlistAll = GetBoxInfo();
            _boxlistAll = _boxlistAll.OrderByDescending(m => m.CanUsedVolumn);
        }
        public abstract IEnumerable<BoxType> GetBoxInfo();

        public void RefreshBoxList(IEnumerable<BoxType> boxTypes)
        {
            _boxlist = boxTypes;
        }

        /// <summary>
        /// 开始计算分箱
        /// </summary>
        /// <param name="sKUInfos"></param>
        /// <returns></returns>
        public IEnumerable<BoxInfo<T>> CalcSplietBox<T>(IEnumerable<T> sKUInfos) where T : SkuInfo
        {
            var allboxinfos = new List<BoxInfo<T>>();
            var groups = GroupGoods(sKUInfos);

            //循环分组装箱
            foreach (var item in groups)
            {
                //先拿出独立包装商品
                var singelList = item.Where(m => m.SKUType.IsSingle)?.ToList().DeepClone();
                if (singelList != null && singelList.Any())
                {
                    allboxinfos.AddRange(PackageBox<T>(singelList, true));
                }
                var nosingelList = sKUInfos.Where(m => !m.SKUType.IsSingle)?.ToList().DeepClone();
                //分组装箱
                if (nosingelList != null && nosingelList.Any())
                {

                    allboxinfos.AddRange(PackageBox<T>(nosingelList));
                }
            }

            return allboxinfos;
        }

        /// <summary>
        /// 按照设定逻辑分组数据（取决于混装逻辑）
        /// </summary>
        /// <param name="sKUInfos">商品集合</param>
        /// <returns>分组类型和对应商品集合</returns>
        protected virtual IEnumerable<IGrouping<string, T>> GroupGoods<T>(IEnumerable<T> sKUInfos) where T : SkuInfo
        {
            return sKUInfos.GroupBy(m => m.SKUType.GroupType + m.SKUType.GroupType2).ToList();
        }

        /// <summary>
        /// 循环装箱
        /// </summary>
        /// <param name="sKUInfos">需要装箱的明细数据</param>
        /// <param name="IsSingel">是否单独装箱</param>
        /// <returns>返回箱规及对应明细的箱集合</returns>
        private IEnumerable<BoxInfo<T>> PackageBox<T>(IEnumerable<T> sKUInfos, bool IsSingel = false) where T : SkuInfo
        {
            var dcode = sKUInfos.First().SKUType.GroupType;
            _boxlist = _boxlistAll.Where(m => string.IsNullOrEmpty(m.Str6) || m.Str6.Contains(dcode) && m.ActualLength >= sKUInfos.First().SKUType.ActualLength).OrderByDescending(n => n.CanUsedVolumn).ToList().DeepClone();
            List<BoxInfo<T>> boxInfos = new List<BoxInfo<T>>();
            List<T> goodsList = sKUInfos.OrderByDescending(m=>m.SKUType.Code).ThenByDescending(m => m.SKUType.Lenght).ToList();
            //if (goodsList.First().SKUType.GroupType == "20")
            if ((!IsSingel) && (_boxlist.Last().CanUsedVolumn < goodsList.Sum(m => m.SKUType.Volumn) || goodsList.First().SKUType.GroupType != "20" || (_boxlist.Last().Lenght * _boxlist.Last().Width >= goodsList.Sum(x => x.SKUType.Width * x.SKUType.Height * x.Qty))) && false)
            {
                var BoxInfo = new BoxInfo<T>()
                {
                    BoxType = _boxlist.First(),
                    SkuInfos = goodsList,
                    BoxNumber = SnowFlakeHelper.GetSnowInstance().NextId().ToString()
                    //Guid.NewGuid().ToString("N")
                    //UsedVolumn = sKUInfos.Sum(m => m.SKUType.Volumn)
                };
                boxInfos.Add(BoxInfo);
                return boxInfos;
            }
            else
            {
                if (IsSingel)
                {
                    var BoxInfo = CreateBox<T>(sKUInfos.First());
                    //string tempcode = string.Empty;
                    for (int x = 0; x < goodsList.Count; x++)
                    {
                        var item = goodsList[x];
                        for (int i = 0; i < item.Qty; i++)
                        {
                            if (CheckTrues(BoxInfo, item.SKUType))
                            {
                                BoxInfo.AddSKUType(item);
                            }
                            else//当前商品装不下
                            {
                                BoxInfo = ChangeBoxType(BoxInfo, item);
                                boxInfos.Add(BoxInfo.DeepClone());

                                BoxInfo = CreateBox<T>(item);
                                BoxInfo.AddSKUType(item);
                            }
                        }
                        if (((x + 1 >= goodsList.Count) ? true : (item.SKUType.Code != goodsList[x + 1].SKUType.Code)))
                        {
                            //每个sku重新换箱
                            BoxInfo = ChangeBoxType(BoxInfo, item);
                            boxInfos.Add(BoxInfo.DeepClone());
                            BoxInfo = CreateBox<T>(item);
                        }
                        
                    }
                    if (BoxInfo.SkuInfos != null && BoxInfo.SkuInfos.Any())
                    {
                        BoxInfo = ChangeBoxType(BoxInfo, sKUInfos.First());
                        boxInfos.Add(BoxInfo.DeepClone());
                    }
                }
                else
                {
                    RollingCalcBox(boxInfos, goodsList);
                }
            }
            return boxInfos;
        }
        /// <summary>
        /// 用于临时存储sku最大箱规
        /// </summary>
        Dictionary<string, decimal> skuboxtypedic = new Dictionary<string, decimal>();
        /// <summary>
        /// 混装订单滚动装箱（未完成）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="boxInfos"></param>
        /// <param name="sKUInfos"></param>
        private void RollingCalcBox<T>(List<BoxInfo<T>> boxInfos, List<T> sKUInfos) where T : SkuInfo
        {
            skuboxtypedic.Clear();
            var lastboxs = new List<T>();
            List<T> goodsList = sKUInfos.OrderByDescending(m => m.SKUType.Code).ThenByDescending(m => m.SKUType.Lenght).ToList();

            var BoxInfo = CreateBox<T>(sKUInfos.First());
            //string tempcode = string.Empty;
            for (int x = 0; x < goodsList.Count; x++)
            { 
                var item = goodsList[x];
              
                for (int i = 0; i < item.Qty; i++)
                { 
                    if (CheckTrues(BoxInfo, item.SKUType))
                    {
                        BoxInfo.AddSKUType(item);
                    }
                    else//当前商品装不下
                    {
                        if (BoxInfo.SkuInfos.Count <= 0)//这里有个特殊情况 第一次就放不下 考虑遍历所有箱，因为箱子有的体积小，但是高长
                        {
                            var changetry = false;
                             var _boxlist = _boxlistAll.Where(m => (string.IsNullOrEmpty(m.Str6) || m.Str6.Contains(item.SKUType.GroupType)) && m.ActualLength>=item.SKUType.ActualLength).OrderByDescending(n => n.CanUsedVolumn).ToList().DeepClone();
                            var maxheight = _boxlist.Max(m => m.Height);
                            foreach (var bitem in _boxlist)
                            {
                                BoxInfo.BoxType = bitem;
                                if (CheckTrues(BoxInfo, item.SKUType))
                                {
                                    changetry = true;
                                    BoxInfo.AddSKUType(item);
                                    break;
                                }
                            }
                            if (changetry)
                                continue;
                            throw new Exception("SKU:"+ item.SKUType.Code + " 没有符合的箱型");
                        }
                        if (!skuboxtypedic.ContainsKey(item.SKUType.Code))//添加过不能再加，避免获取到的最大数量有误
                            skuboxtypedic.Add(item.SKUType.Code, BoxInfo.SkuInfos.Where(m => m.SKUType.Code == item.SKUType.Code).Sum(m => m.Qty));
                        BoxInfo = ChangeBoxType(BoxInfo, item);
                        boxInfos.Add(BoxInfo.DeepClone());

                        BoxInfo = CreateBox<T>(item);
                        if (CheckTrues(BoxInfo, item.SKUType))
                        {
                            BoxInfo.AddSKUType(item);
                        }
                        else
                        {
                            if (BoxInfo.SkuInfos.Count <= 0)//这里有个特殊情况 第一次就放不下 考虑遍历所有箱，因为箱子有的体积小，但是高长
                            {
                                var changetry = false;
                                var _boxlist = _boxlistAll.Where(m => (string.IsNullOrEmpty(m.Str6) || m.Str6.Contains(item.SKUType.GroupType)) && m.ActualLength >= item.SKUType.ActualLength).OrderByDescending(n => n.CanUsedVolumn).ToList().DeepClone();
                                var maxheight = _boxlist.Max(m => m.Height);
                                foreach (var bitem in _boxlist)
                                {
                                    BoxInfo.BoxType = bitem;
                                    if (CheckTrues(BoxInfo, item.SKUType))
                                    {
                                        changetry = true;
                                        BoxInfo.AddSKUType(item);
                                        break;
                                    }
                                }
                                if (changetry)
                                    continue;
                                throw new Exception("SKU:" + item.SKUType.Code + " 没有符合的箱型");
                            }
                        }
                    } 
                }
                if (((x + 1 >= goodsList.Count) ? true : (item.SKUType.Code != goodsList[x + 1].SKUType.Code)))
                {
                    //每个sku重新换箱
                   
                    if (skuboxtypedic.ContainsKey(BoxInfo.SkuInfos.First().SKUType.Code) && BoxInfo.SkuInfos.First().Qty < skuboxtypedic[BoxInfo.SkuInfos.First().SKUType.Code]) //保存的箱规比较 如果小于，则为零头
                    {
                        lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                        BoxInfo = CreateBox<T>(item);
                    }
                    else//主动计算一下箱规，这种刚好一箱情况挺多的 ，本来想偷懒的
                    {
                        //testdata
                        var vboxinfo = BoxInfo.DeepClone();
                        if(CheckTrues(vboxinfo, vboxinfo.SkuInfos.First().SKUType))
                        {
                            lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                            BoxInfo = CreateBox<T>(item);
                        }
                    }
                    if (BoxInfo.SkuInfos != null && BoxInfo.SkuInfos.Any())
                    {
                        BoxInfo = ChangeBoxType(BoxInfo, item); 
                        boxInfos.Add(BoxInfo.DeepClone());
                        BoxInfo = CreateBox<T>(item);
                    } 

                    //if (boxInfos.Count <= 0 ||( BoxInfo.SkuInfos.Sum(m => m.Qty) < boxInfos[boxInfos.Count - 1].SkuInfos.Sum(m => m.Qty) && BoxInfo.SkuInfos.First().SKUType.Code == boxInfos[boxInfos.Count - 1].SkuInfos.First().SKUType.Code))
                    //{
                    //    lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                    //}
                    ////else if(BoxInfo.SkuInfos.First().SKUType.Code != boxInfos[boxInfos.Count - 1].SkuInfos.First().SKUType.Code )
                    //else
                    //{
                    //    if(CheckTrues(BoxInfo, BoxInfo.SkuInfos.Last().SKUType))
                    //    //if ((BoxInfo.BoxType.CanUsedVolumn - BoxInfo.UsedVolumn) > BoxInfo.SkuInfos.First().SKUType.Volumn)
                    //    {
                    //        lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                    //    }
                    //    else
                    //    {
                    //        boxInfos.Add(BoxInfo.DeepClone());
                    //    }
                    //}

                    //boxInfos.Add(BoxInfo.DeepClone());

                }
               
            }
            //if (BoxInfo.SkuInfos != null && BoxInfo.SkuInfos.Any())
            //{
            //    BoxInfo = ChangeBoxType(BoxInfo, sKUInfos.First());
            //    //if (BoxInfo.SkuInfos.Sum(m => m.Qty) < boxInfos[boxInfos.Count - 1].SkuInfos.Sum(m => m.Qty) && BoxInfo.SkuInfos.First().SKUType.Code == boxInfos[boxInfos.Count - 1].SkuInfos.First().SKUType.Code)
            //    //{
            //    //    lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
            //    //}
            //    //else
            //    //{
            //    //    boxInfos.Add(BoxInfo.DeepClone());
            //    //}
            //    boxInfos.Add(BoxInfo.DeepClone());
            //}
            //剩下的根据article完全混装
            if (lastboxs.Any())
            {
                lastboxs.ForEach(m =>
                {
                    m.Index1 = lastboxs.Where(x => x.SKUType.Articleno == m.SKUType.Articleno).Sum(n=>n.Qty);
                    m.Index2 = lastboxs.Where(x => x.SKUType.Articleno == m.SKUType.Articleno && x.SKUType.Code == m.SKUType.Code).Sum(n => n.Qty);
                });
                goodsList = lastboxs.OrderByDescending(m =>  m.Index1).OrderByDescending(m => m.SKUType.Articleno).ThenByDescending(m => m.Index2).ThenByDescending(m => m.SKUType.Code).ThenByDescending(m => m.SKUType.Lenght).ToList();

                BoxInfo = CreateBox<T>(sKUInfos.First());
                //string tempcode = string.Empty;
                for (int x = 0; x < goodsList.Count; x++)
                {
                    var item = goodsList[x];
                    for (int i = 0; i < item.Qty; i++)
                    {
                        if (CheckTruesBig(BoxInfo, item.SKUType))
                        {
                            BoxInfo.AddSKUType(item);
                        }
                        else//当前商品装不下
                        {
                            if (BoxInfo.SkuInfos.Count <= 0)//这里有个特殊情况 第一次就放不下 考虑遍历所有箱，因为箱子有的体积小，但是高长
                            {
                                throw new Exception("SKU:" + item.SKUType.Code + " 没有符合的箱型");
                            }

                            BoxInfo = ChangeBoxType(BoxInfo, item);
                            boxInfos.Add(BoxInfo.DeepClone());

                            BoxInfo = CreateBox<T>(item);
                            if (CheckTruesBig(BoxInfo, item.SKUType))
                            {
                                BoxInfo.AddSKUType(item);
                            } 
                            else
                            {
                                if (BoxInfo.SkuInfos.Count <= 0)//这里有个特殊情况 第一次就放不下 考虑遍历所有箱，因为箱子有的体积小，但是高长
                                {
                                    throw new Exception("SKU:" + item.SKUType.Code + " 没有符合的箱型");
                                }
                            }
                        }
                    }
                    //if (((x + 1 >= goodsList.Count) ? true : (item.SKUType.GroupType2 != goodsList[x + 1].SKUType.GroupType2)))
                    //{
                    //    //每个sku重新换箱
                    //    BoxInfo = ChangeBoxType(BoxInfo, item);

                    //    boxInfos.Add(BoxInfo.DeepClone());

                    //    BoxInfo = CreateBox<T>(item);
                    //}
                }
                if (BoxInfo.SkuInfos != null && BoxInfo.SkuInfos.Any())
                {
                    BoxInfo = ChangeBoxType(BoxInfo, sKUInfos.First());

                    boxInfos.Add(BoxInfo.DeepClone());

                }
            }

        }

        /// <summary>
        /// 混装订单滚动装箱（未完成）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="boxInfos"></param>
        /// <param name="sKUInfos"></param>
        private void RollingCalcBoxCopy<T>(List<BoxInfo<T>> boxInfos, List<T> sKUInfos) where T : SkuInfo
        {
            skuboxtypedic.Clear();
            var lastboxs = new List<T>();
            List<T> goodsList = sKUInfos.OrderByDescending(m => m.SKUType.Code).ThenByDescending(m => m.SKUType.Lenght).ToList();

            var BoxInfo = CreateBox<T>(sKUInfos.First());
            //string tempcode = string.Empty;
            for (int x = 0; x < goodsList.Count; x++)
            {
               
                var item = goodsList[x];
                for (int i = 0; i < item.Qty; i++)
                {
                    try
                    {
                        if (CheckTrues(BoxInfo, item.SKUType))
                        {
                            BoxInfo.AddSKUType(item);
                        }
                        else//当前商品装不下
                        {
                            if (!skuboxtypedic.ContainsKey(item.SKUType.Code))//添加过不能再加，避免获取到的最大数量有误
                                skuboxtypedic.Add(item.SKUType.Code, BoxInfo.SkuInfos.Where(m => m.SKUType.Code == item.SKUType.Code).Sum(m => m.Qty));
                            BoxInfo = ChangeBoxType(BoxInfo, item);
                            boxInfos.Add(BoxInfo.DeepClone());

                            BoxInfo = CreateBox<T>(item);
                            BoxInfo.AddSKUType(item);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (((x + 1 >= goodsList.Count) ? true : (item.SKUType.Code != goodsList[x + 1].SKUType.Code)))
                {

                    try { 
                    //每个sku重新换箱
                    BoxInfo = ChangeBoxType(BoxInfo, item);
                    if (boxInfos.Count <= 0 || (BoxInfo.SkuInfos.Sum(m => m.Qty) < boxInfos[boxInfos.Count - 1].SkuInfos.Sum(m => m.Qty) && BoxInfo.SkuInfos.First().SKUType.Code == boxInfos[boxInfos.Count - 1].SkuInfos.First().SKUType.Code))
                    {
                        lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                    }
                    //else if(BoxInfo.SkuInfos.First().SKUType.Code != boxInfos[boxInfos.Count - 1].SkuInfos.First().SKUType.Code )
                    else
                    {
                        if (CheckTrues(BoxInfo, BoxInfo.SkuInfos.Last().SKUType))
                        //if ((BoxInfo.BoxType.CanUsedVolumn - BoxInfo.UsedVolumn) > BoxInfo.SkuInfos.First().SKUType.Volumn)
                        {
                            lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                        }
                        else
                        {
                            if (!skuboxtypedic.ContainsKey(item.SKUType.Code))//添加过不能再加，避免获取到的最大数量有误
                                skuboxtypedic.Add(item.SKUType.Code, BoxInfo.SkuInfos.Sum(m => m.Qty));

                            boxInfos.Add(BoxInfo.DeepClone());
                        }
                    }

                    //boxInfos.Add(BoxInfo.DeepClone());
                    BoxInfo = CreateBox<T>(item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            if (BoxInfo.SkuInfos != null && BoxInfo.SkuInfos.Any())
            {
                BoxInfo = ChangeBoxType(BoxInfo, sKUInfos.First());
                //if (BoxInfo.SkuInfos.Sum(m => m.Qty) < boxInfos[boxInfos.Count - 1].SkuInfos.Sum(m => m.Qty) && BoxInfo.SkuInfos.First().SKUType.Code == boxInfos[boxInfos.Count - 1].SkuInfos.First().SKUType.Code)
                //{
                //    lastboxs.AddRange(BoxInfo.SkuInfos.DeepClone());
                //}
                //else
                //{
                //    boxInfos.Add(BoxInfo.DeepClone());
                //}
                boxInfos.Add(BoxInfo.DeepClone());
            }
            //剩下的根据article完全混装
            if (lastboxs.Any())
            {
                goodsList = lastboxs.OrderByDescending(m => m.SKUType.Code).ThenByDescending(m => m.SKUType.Articleno).ThenByDescending(m => m.SKUType.Lenght).ThenByDescending(m => m.Qty).ToList();

                BoxInfo = CreateBox<T>(sKUInfos.First());
                //string tempcode = string.Empty;
                for (int x = 0; x < goodsList.Count; x++)
                {
                    var item = goodsList[x];
                    for (int i = 0; i < item.Qty; i++)
                    {
                        if (CheckTrues(BoxInfo, item.SKUType))
                        {
                            BoxInfo.AddSKUType(item);
                        }
                        else//当前商品装不下
                        {
                            BoxInfo = ChangeBoxType(BoxInfo, item);
                            boxInfos.Add(BoxInfo.DeepClone());

                            BoxInfo = CreateBox<T>(item);
                            BoxInfo.AddSKUType(item);
                        }
                    }
                    //if (((x + 1 >= goodsList.Count) ? true : (item.SKUType.GroupType2 != goodsList[x + 1].SKUType.GroupType2)))
                    //{
                    //    //每个sku重新换箱
                    //    BoxInfo = ChangeBoxType(BoxInfo, item);

                    //    boxInfos.Add(BoxInfo.DeepClone());

                    //    BoxInfo = CreateBox<T>(item);
                    //}
                }
                if (BoxInfo.SkuInfos != null && BoxInfo.SkuInfos.Any())
                {
                    BoxInfo = ChangeBoxType(BoxInfo, sKUInfos.First());

                    boxInfos.Add(BoxInfo.DeepClone());

                }
            }

        }

        /// <summary>
        /// 缩箱 按照最小箱规格装箱
        /// </summary>
        /// <param name="boxInfo"></param>
        /// <returns></returns>
        private BoxInfo<T> ChangeBoxType<T>(BoxInfo<T> boxInfo,SkuInfo skuInfo) where T : SkuInfo
        {

            var dcode = skuInfo.SKUType.GroupType;
            _boxlist = _boxlistAll.Where(m => string.IsNullOrEmpty(m.Str6) || m.Str6.Contains(dcode) && m.ActualLength >= skuInfo.SKUType.ActualLength).OrderByDescending(n => n.CanUsedVolumn).ToList().DeepClone();
            var NextBoxList = _boxlist.Where(m=>m.ActualLength> boxInfo.SkuInfos.Max(n=>n.SKUType.ActualLength)).Where(m => boxInfo.UsedVolumn  < m.CanUsedVolumn);
            if (NextBoxList!=null && NextBoxList.Any())
            {
                //var boxitem = NextBoxList.OrderBy(m => m.CanUsedVolumn).First();
                //boxInfo.BoxType = boxitem;
                var checkboxinfo = boxInfo.DeepClone();
                checkboxinfo.SkuInfos.Last().Qty -= 1;
                foreach (var boxitem in NextBoxList)
                {
                    checkboxinfo.BoxType = boxitem;
                    if (!CheckTrues(checkboxinfo, skuInfo.SKUType))
                        break;
                    boxInfo.BoxType = boxitem;
                }
            }
            return boxInfo;
        }

        /// <summary>
        /// 创建空箱
        /// </summary>
        /// <returns></returns>
        private BoxInfo<T> CreateBox<T>(SkuInfo item) where T:SkuInfo
        {
            var dcode = item.SKUType.GroupType;
            _boxlist = _boxlistAll.Where(m => (string.IsNullOrEmpty(m.Str6) || m.Str6.Contains(dcode)) ).OrderByDescending(n=>n.CanUsedVolumn).ToList().DeepClone();
            return new BoxInfo<T>()
            {
                BoxType = _boxlist.First(),
                SkuInfos = new List<T>(),
                BoxNumber =   SnowFlakeHelper.GetSnowInstance().NextId().ToString()
            //Guid.NewGuid().ToString("N")
        };
        }

        /// <summary>
        /// 按照最大规格的校验 提供升箱型功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="boxInfo"></param>
        /// <param name="sKU"></param>
        /// <returns></returns>
        private bool CheckTruesBig<T>(BoxInfo<T> boxInfo, SKUType item) where T : SkuInfo//是否还能继续装箱
        {
            if (!CheckTrues(boxInfo, item))
            {
                var changetry = false;
                var _boxlist = _boxlistAll.Where(m => (string.IsNullOrEmpty(m.Str6) || m.Str6.Contains(item.GroupType)) && m.ActualLength >= item.ActualLength).OrderByDescending(n => n.CanUsedVolumn).ToList().DeepClone();
                var maxheight = _boxlist.Max(m => m.Height);
                foreach (var bitem in _boxlist)
                {
                    boxInfo.BoxType = bitem;
                    if (CheckTrues(boxInfo, item))
                    {
                        changetry = true;
                        break;
                    }
                }
                return changetry;
            }
            return true;
        }
        /// <summary>
        /// 检验该商品是否能继续放入
        /// </summary>
        /// <param name="boxInfo">箱信息</param>
        /// <param name="sKU">sku</param>
        /// <returns></returns>
        private bool CheckTrues<T>(BoxInfo<T> boxInfo, SKUType sKU) where T : SkuInfo//是否还能继续装箱
        {
            if (sKU.ActualLength > boxInfo.BoxType.ActualLength)
                throw new Exception("商品单边大于箱规长度，无法装箱");
            var currentvol = boxInfo.UsedVolumn;
            if (sKU != null)
                currentvol += sKU.Volumn;
            if (currentvol > boxInfo.BoxType.CanUsedVolumn)
                return false;
            //这里需要校验体积满足的时候 是否真的能放下商品
            if (sKU.GroupType == "20")
            {
                if (!CheckPoss(boxInfo, sKU))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 校验摆放规则
        /// </summary>
        /// <param name="boxInfo"></param>
        /// <param name="sKU"></param>
        /// <returns></returns>
        private bool CheckPoss<T>(BoxInfo<T> boxInfo, SKUType sKU) where T:SkuInfo
        {
            var maxheight = boxInfo.SkuInfos.Count > 0 ? boxInfo.SkuInfos.Max(m => m.SKUType.Lenght) : 0;
            maxheight = Math.Max(maxheight, sKU.Lenght);
            if (boxInfo.BoxType.Height < maxheight)
                return false;
            //先按照简单的鞋子摆放 只摆放一层 先校验基本面积
            var maxwh= boxInfo.SkuInfos.Count > 0 ? boxInfo.SkuInfos.Max(m => m.SKUType.Width* m.SKUType.Height) : 0;
            maxheight = Math.Max(maxwh, sKU.Width* sKU.Height);
            var curentranle = boxInfo.SkuInfos.Sum(x => maxheight * x.Qty);
            if (sKU != null)
                curentranle += sKU.Width * sKU.Height;
            if (boxInfo.BoxType.Lenght * boxInfo.BoxType.Width < curentranle)
                return false;
            //再校验是否能摆放下
            if (!TestingPackage((boxInfo.BoxType.Lenght, boxInfo.BoxType.Width, boxInfo.BoxType.Height), (sKU.Lenght, sKU.Width, sKU.Height), boxInfo.SkuInfos.Sum(m => m.Qty) + 1))
                return false;
            return true;
        }

        private bool TestingPackage((decimal,decimal ,decimal ) boxsize, (decimal, decimal, decimal) skusize,decimal qty)
        {
            decimal qty1 = Math.Floor(boxsize.Item1 / skusize.Item2) * Math.Floor(boxsize.Item2 / skusize.Item3);
            decimal qty2 = Math.Floor(boxsize.Item2 / skusize.Item2) * Math.Floor(boxsize.Item2 / skusize.Item3);
            if (qty <= Math.Max(qty1, qty2))
                return true;
            return false;
        }
    }
}
