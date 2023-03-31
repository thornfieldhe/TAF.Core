namespace Taf.Core.Utility
{
    public class IBTreeNullNode<T> : IBTreeNode<T>
    {
        private IBTreeNode<T> _Parent;

        public bool IsNull => true;

        public BTreeNodeColor Color => BTreeNodeColor.Black;

        public BTreeNodeColor Colour
        {
            get => BTreeNodeColor.Black;
            set
            {
                
            }
        }

        public IBTreeNode<T> Parent
        {
            get => _Parent;
            set => _Parent = value;
        }

        public IBTreeNode<T> Left
        {
            get => null;
            set
            {
                
            }
        }

        public IBTreeNode<T> Right
        {
            get => null;
            set
            {
                
            }
        }

        public T Data
        {
            get => default(T);
            set
            {
                
            }
        }
    }
}
