using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpliteToBox
{
    public abstract class SpliteBoxManage2<INPUT, OUTPUT>
    {
        public Func<INPUT, OUTPUT> PipelineSteps { get; protected set; }
        public OUTPUT Process(INPUT input)
        {
            return PipelineSteps(input);
        }
    }
    public static class PipelineStepExtensions
    {
        public static OUTPUT Step<INPUT, OUTPUT>(this INPUT input, IPipelineStep<INPUT, OUTPUT> step)
        {
            return step.Process(input);
        }
    }
    public  class TrivalPipeline : SpliteBoxManage2<IEnumerable<SkuInfo>, IEnumerable<BoxInfo>>
    {
        public TrivalPipeline()
        {
            //PipelineSteps = input => input.Step(new DoubleToIntStep())
            //                              .Step(new IntToStringStep());
        }
    }
    public interface IPipelineStep<INPUT, OUTPUT>
    {
        OUTPUT Process(INPUT input);
    }
    public class DoubleToIntStep : IPipelineStep<double, int>
    {
        public int Process(double input)
        {
            return Convert.ToInt32(input);
        }
    }
    public class IntToStringStep : IPipelineStep<int, string>
    {
        public string Process(int input)
        {
            return input.ToString();
        }
    }
}
