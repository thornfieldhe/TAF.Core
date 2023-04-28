using System.Reflection;

namespace Taf.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBuildStep
    {
        /// <summary>
        /// 
        /// </summary>
        int Sequence
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        int Times
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        MethodInfo Handler
        {
            get; set;
        }
    }
}