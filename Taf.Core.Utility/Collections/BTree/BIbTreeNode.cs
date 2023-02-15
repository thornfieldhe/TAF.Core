namespace Taf.Core.Utility
{
    public  class BIbTreeNode<T> : IBTreeNode<T>
    {
        public BTreeNodeColor Colour { get; set; }
        public IBTreeNode<T> Left { get; set; }
        public IBTreeNode<T> Right { get; set; }
        public IBTreeNode<T> Parent { get; set; }
        public T Data { get; set; }

        /// <summary>
        /// 初始化一个节点。所有初始化的节点默认都是红色
        /// </summary>
        /// <param name="data"></param>
        public BIbTreeNode(T data) : this(data, BTreeNodeColor.Red)
        {
            
        }

        /// <summary>
        /// 初始化一个节点。所有初始化的节点默认都是红色
        /// </summary>
        public BIbTreeNode() : this(default(T), BTreeNodeColor.Red)
        {
        }

        /// <summary>
        ///  初始化一个节点。并指定节点的颜色
        /// </summary>
        /// <param name="data"></param>
        /// <param name="colour"></param>
        public BIbTreeNode(T data, BTreeNodeColor colour)
        {
            Data = data;
            Colour = colour;

            Left = new IBTreeNullNode<T>();
            Left.Parent = this;
            Right = new IBTreeNullNode<T>();
            Right.Parent = this;
            Parent = new IBTreeNullNode<T>();
        }

        public virtual bool IsNull => false;

        public override string ToString() => IsNull ? "Nil" : string.Format("{0} {1}", Data, BTreeNodeColor.Black == Colour ? "Black" : "Red");
    }
}
