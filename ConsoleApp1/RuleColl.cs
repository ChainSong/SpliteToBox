using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpliteToBox
{
    public class RuleColl
    {
        List<RuleColl> RuleColls;
        public RuleColl()
        {
            RuleColls = new List<RuleColl>();
        }
        public List<RuleColl> GetRules()
        {
            return RuleColls;
        }
        public void Add(RuleColl ruleColl)
        {
            RuleColls.Add(ruleColl);
        }
        public string TestName { get; set; }
        public void Run()
        {
            if (RuleColls.Any())
            {
                foreach (var item in RuleColls)
                {
                    item.Run();
                }
            }
            Console.WriteLine(TestName);
        }
    }
}
