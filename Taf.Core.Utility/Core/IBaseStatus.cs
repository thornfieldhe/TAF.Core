namespace Taf
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseStatus
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsClean
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool IsDirty
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool IsDelete
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool IsNew
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        void MarkClean();

        /// <summary>
        /// 
        /// </summary>
        void MarkDirty();

        /// <summary>
        /// 
        /// </summary>
        void MarkNew();

        /// <summary>
        /// 
        /// </summary>
        void MarkDelete();
    }
}
