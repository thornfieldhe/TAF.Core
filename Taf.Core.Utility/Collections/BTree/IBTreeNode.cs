namespace Taf.Core.Utility
{
    public interface IBTreeNode<T>
    {
        BTreeNodeColor Colour { get; set; }
        bool IsNull { get; }
        IBTreeNode<T> Parent { get; set; }
        IBTreeNode<T> Left { get; set; }
        IBTreeNode<T> Right { get; set; }
        T Data { get; set; }
    }
}
