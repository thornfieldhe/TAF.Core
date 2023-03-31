using System.Reflection;

namespace Taf.Core
{
    public interface IBuildStep
    {
        int Sequence
        {
            get;
        }

        int Times
        {
            get;
        }

        MethodInfo Handler
        {
            get; set;
        }
    }
}