using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpliteToBox
{
    class Program
    {
        static void Main(string[] args)
        {
            //SpliteToBox.NIKEWMSSpliteBox nIKEWMSSpliteBox = new NIKEWMSSpliteBox();

            //List<SkuInfo> skuInfos = new List<SkuInfo>();
            //skuInfos.Add(new SkuInfo()
            //{
            //    Qty = 5,
            //    SKUType = new SKUType()
            //    {
            //        Code = "SKU001",
            //        Lenght = 60,
            //        Width = 46,
            //        Height = 46
            //    }
            //});
            //var res = nIKEWMSSpliteBox.CalcSplietBox(skuInfos);
            //Console.WriteLine(res.Count());



            //RuleColl ruleColl = new RuleColl();
            //ruleColl.TestName = "T1";

            //RuleColl ruleColl2 = new RuleColl();
            //ruleColl2.TestName = "T2";
            //ruleColl.Add(ruleColl2) ;

            //RuleColl ruleColl3 = new RuleColl();
            //ruleColl3.TestName = "T2-1";

            //RuleColl ruleColl4 = new RuleColl();
            //ruleColl4.TestName = "T2-1-1";
            //ruleColl3.Add(ruleColl4);
            //ruleColl2.Add(ruleColl3);
            //ruleColl.Run();
            //RunTest();

            double input = 1024.1024;
            // 需要安装 Microsoft.Extensions.DependencyInjection
            var services = new ServiceCollection();
            services.AddTransient<TrivalPipeline>();
            var provider = services.BuildServiceProvider();
            var trival = provider.GetService<TrivalPipeline>();
            string result = trival.Process(input);
            Console.WriteLine(result);
            Console.Read();


        }
        static Task RunTest()
        {
           return Task.Run(() => { Console.WriteLine(111); });
        }
    }
}
