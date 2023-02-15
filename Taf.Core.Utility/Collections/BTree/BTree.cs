﻿using System;
using System.Collections.Generic;

 namespace Taf.Core.Utility
{
    /// <summary>
    /// 红黑树
    /// </summary>
    /// <typeparam name="T">树中节点的值的类型</typeparam>
    /// <remarks>
    /// 红黑树是每个节点都带有颜色属性的二叉查找树，颜色为红色或黑色。在二叉查找树强制一般要求以外，对于任何有效的红黑树增加了如下的额外要求：
    /// 1、节点是红色或黑色
    /// 2、根是黑色。
    /// 3、所有叶子都是黑色（叶子是NIL节点）。
    /// 4、每个红色节点必须有两个黑色的子节点。（从每个叶子到根的所有路径上不能有两个连续的红色节点。）
    /// 5、从任一节点到其每个叶子的所有简单路径都包含相同数目的黑色节点。
    /// 
    /// 更详细的信息请参考：https://zh.wikipedia.org/wiki/%E7%BA%A2%E9%BB%91%E6%A0%91
    /// 或者 https://en.wikipedia.org/wiki/Red%E2%80%93black_tree
    /// 
    /// 可以在 https://cs.usfca.edu/~galles/visualization/RedBlack.html 获得构建红黑树的可视化的过程
    /// </remarks>
    public  class BTree<T> where T : IComparable
    {
        private IBTreeNode<T> _Root;

        public BTree() : this(new IBTreeNullNode<T>())
        {
            
        }

        public BTree(IBTreeNode<T> rootNode) => _Root = rootNode;

        /// <summary>
        /// 获取根节点
        /// </summary>
        public IBTreeNode<T> Root
        {
            get => _Root;
            private set => _Root = value;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public  IBTreeNode<T> CreateNode(T data) =>
            new BIbTreeNode<T>()
            {
                Data = data
            };

        /// <summary>
        /// 获取祖父节点
        /// </summary>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public IBTreeNode<T> GetGrandParent(IBTreeNode<T> targetNode)
        {
            if(!targetNode.Parent.IsNull && !targetNode.Parent.Parent.IsNull)
            {
                return targetNode.Parent.Parent;
            }

            return new IBTreeNullNode<T>();
        }

        /// <summary>
        /// 获取叔父节点
        /// </summary>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public IBTreeNode<T> GetUncle(IBTreeNode<T> targetNode)
        {
            var grandparent = GetGrandParent(targetNode);

            if(!grandparent.IsNull && !targetNode.Parent.IsNull)
            {
                if(targetNode.Parent == grandparent.Left)
                {
                    return grandparent.Right;
                }
                else
                {
                    return grandparent.Left;
                }
            }

            return new IBTreeNullNode<T>();
        }

        /// <summary>
        /// 获取兄弟节点
        /// </summary>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public IBTreeNode<T> GetSibling(IBTreeNode<T> targetNode)
        {
            if(null != targetNode)
            {
                if(!targetNode.Parent.IsNull && targetNode == targetNode.Parent.Left)
                {
                    return targetNode.Parent.Right;
                }
                else
                {
                    return targetNode.Parent.Left;
                }
            }

            return new IBTreeNullNode<T>();
        }

        /// <summary>
        /// 获取值最小的节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IBTreeNode<T> GetSmallest(IBTreeNode<T> node)
        {
            if (node.Left.IsNull)
                return node;

            return GetSmallest(node.Left);
        }

        /// <summary>
        /// 获取节点的最大子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IBTreeNode<T> GetLargestNode(IBTreeNode<T> node)
        {
            if (node.Right.IsNull)
                return node;

            return GetLargestNode(node.Right);
        }

        /// <summary>
        /// 查找新节点的父节点
        /// </summary>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        private void LocateNewNodePosition(IBTreeNode<T> newNode)
        {
            IBTreeNode<T> parent = new IBTreeNullNode<T>();
            var comparedNode = _Root; //从根节点开始查找

            while(!comparedNode.IsNull)
            {
                parent = comparedNode;
                if(-1 == newNode.Data.CompareTo(comparedNode.Data))
                {
                    //小于比较的节点值，往左查找
                    comparedNode = comparedNode.Left;
                }
                else
                {
                    //大于或者等于比较的节点值，往右查找
                    comparedNode = comparedNode.Right;
                }
            }

            newNode.Parent = parent;

            if (-1 == newNode.Data.CompareTo(parent.Data))
            {
                //小于父节点
                parent.Left = newNode;
            }
            else
            {
                parent.Right = newNode;
            }
        }

        #region 旋转

        private void LeftRotate(IBTreeNode<T> node)
        {
            var right = node.Right;

            //x的右节点不能为空
            if (!right.IsNull)
            {
                //将y的左节点变成x的右节点
                node.Right = right.Left;

                if (!right.Left.IsNull)
                {
                    right.Left.Parent = node;
                }

                if (!right.IsNull)
                {
                    right.Parent = node.Parent;
                }

                if (node.Parent.IsNull)
                {
                    _Root = right;
                }
                else if (node == node.Parent.Left)
                {
                    node.Parent.Left = right;
                }
                else
                {
                    node.Parent.Right = right;
                }

                right.Left = node;

                if (!node.IsNull)
                {
                    node.Parent = right;
                }
            }
        }

        /// <summary>
        /// 右旋
        /// </summary>
        /// <param name="node"></param>
        private void RightRotate(IBTreeNode<T> node)
        {
            var left = node.Left;

            if (!left.IsNull)
            {
                //将x的右节点设为y的左节点
                node.Left = left.Right;
                //将x的右节点的父节点设为y
                if (!left.IsNull && !left.Right.IsNull)
                {
                    left.Right.Parent = node;
                }

                //将x的父节点设置为y的父节点
                left.Parent = node.Parent;

                //如果y没有父节点（即根节点），那么x则为根节点
                if (node.Parent.IsNull)
                {
                    _Root = left;
                }
                else if (!node.Parent.IsNull && node == node.Parent.Right)
                {
                    //如果y是其父节点的右节点，那么设置y的父节点的右节点为x
                    node.Parent.Right = left;
                }
                else
                {
                    //如果y是其父节点的左节点，那么设置y的父节点的左节点为x
                    node.Parent.Left = left;
                }

                //x的右节点为y
                left.Right = node;

                //y的父节点为x
                node.Parent = left;
            }
        }

        #endregion

        public bool Find(T key, out IBTreeNode<T> item)
        {
            var isFound = false;
            var temp = _Root;
            item = null;

            while (!isFound)
            {
                if (null == temp)
                    break;

                if (-1 == key.CompareTo(temp.Data))
                {
                    temp = temp.Left;
                }

                if (1 == key.CompareTo(temp.Data))
                {
                    temp = temp.Right;
                }

                if (0 == key.CompareTo(temp.Data))
                {
                    isFound = true;
                    item = temp;
                }
            }

            return isFound;
        }


        #region 插入

        /// <summary>
        /// 情形1
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// 新节点N位于树的根上，没有父节点。在这种情形下，我们把它重绘为黑色以满足性质2。
        /// 因为它在每个路径上对黑节点数目增加一，性质5符合。
        /// </remarks>
        private void InsertCase1(IBTreeNode<T> targetNode)
        {
            if(targetNode.Parent.IsNull)
            {
                targetNode.Colour = BTreeNodeColor.Black;
            }
            else
            {
                InsertCase2(targetNode);
            }
        }

        /// <summary>
        /// 情形2
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// 新节点的父节点P是黑色，所以性质4没有失效（新节点是红色的）。
        /// 在这种情形下，树仍是有效的。
        /// 性质5也未受到威胁，尽管新节点N有两个黑色叶子子节点；
        /// 但由于新节点N是红色，通过它的每个子节点的路径就都有同通过它所取代的黑色的叶子的路径同样数目的黑色节点，所以依然满足这个性质。
        /// </remarks>
        private void InsertCase2(IBTreeNode<T> targetNode)
        {
            if(BTreeNodeColor.Black == targetNode.Parent.Colour)
            {
                return;
            }
            else
            {
                InsertCase3(targetNode);
            }
        }

        /// <summary>
        /// 情形3
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// 如果父节点P和叔父节点U二者都是红色，（此时新插入节点N做为P的左子节点或右子节点都属于情形3，这里右图仅显示N做为P左子的情形）
        /// 则我们可以将它们两个重绘为黑色并重绘祖父节点G为红色（用来保持性质4）。
        /// 现在我们的新节点N有了一个黑色的父节点P。
        /// 因为通过父节点P或叔父节点U的任何路径都必定通过祖父节点G，在这些路径上的黑节点数目没有改变。
        /// 但是，红色的祖父节点G可能是根节点，这就违反了性质2，也有可能祖父节点G的父节点是红色的，这就违反了性质4。为了解决这个问题，我们在祖父节点G上递归地进行情形1的整个过程。
        /// </remarks>
        private void InsertCase3(IBTreeNode<T> targetNode)
        {
            var uncle = GetUncle(targetNode);
            var grandParent = GetGrandParent(targetNode);

            if(!uncle.IsNull && BTreeNodeColor.Red == uncle.Colour)
            {
                targetNode.Parent.Colour = BTreeNodeColor.Black;
                uncle.Colour = BTreeNodeColor.Black;
                grandParent.Colour = BTreeNodeColor.Red;
                InsertCase1(grandParent);
            }
            else
            {
                InsertCase4(targetNode);
            }
        }

        /// <summary>
        /// 情形4
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// 父节点P是红色而叔父节点U是黑色或缺少，并且新节点N是其父节点P的右子节点而父节点P又是其父节点的左子节点。
        /// 在这种情形下，我们进行一次左旋转调换新节点和其父节点的角色;
        /// 接着，我们按情形5处理以前的父节点P以解决仍然失效的性质4。
        /// 注意这个改变会导致某些路径通过它们以前不通过的新节点N或不通过节点P，但由于这两个节点都是红色的，所以性质5仍有效。
        /// </remarks>
        private void InsertCase4(IBTreeNode<T> targetNode)
        {
            var grandParent = GetGrandParent(targetNode);
            
            if(targetNode == targetNode.Parent.Right &&
                targetNode.Parent == grandParent.Left)
            {
                LeftRotate(targetNode.Parent);
            }
            else if(targetNode == targetNode.Parent.Left &&
                targetNode.Parent == grandParent.Right)
            {
                RightRotate(targetNode.Parent);
                targetNode = targetNode.Right;
            }
            InsertCase5(targetNode);
        }

        /// <summary>
        /// 情形5
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// 父节点P是红色而叔父节点U是黑色或缺少，新节点N是其父节点的左子节点，而父节点P又是其父节点G的左子节点。
        /// 在这种情形下，我们进行针对祖父节点G的一次右旋转；
        /// 在旋转产生的树中，以前的父节点P现在是新节点N和以前的祖父节点G的父节点。
        /// 我们知道以前的祖父节点G是黑色，否则父节点P就不可能是红色（如果P和G都是红色就违反了性质4，所以G必须是黑色）。
        /// 我们切换以前的父节点P和祖父节点G的颜色，结果的树满足性质4。
        /// 性质5也仍然保持满足，因为通过这三个节点中任何一个的所有路径以前都通过祖父节点G，现在它们都通过以前的父节点P。
        /// 在各自的情形下，这都是三个节点中唯一的黑色节点。
        /// </remarks>
        private void InsertCase5(IBTreeNode<T> targetNode)
        {
            targetNode.Parent.Colour = BTreeNodeColor.Black;
            var grandParent = GetGrandParent(targetNode);

            grandParent.Colour = BTreeNodeColor.Red;

            if(targetNode == targetNode.Parent.Left && targetNode.Parent == grandParent.Left)
            {
                RightRotate(grandParent);
            }
            else
            {
                LeftRotate(grandParent);
            }
        }

        public IBTreeNode<T> Insert(T item)
        {
            var newNode = CreateNode(item);
            //如果没有根节点，新节点设置成根节点
            if (_Root.IsNull)
            {
                _Root = newNode;
                _Root.Colour = BTreeNodeColor.Black;
                return _Root;
            }

            LocateNewNodePosition(newNode);

            InsertCase1(newNode);

            return newNode;
        }

        #endregion

        #region 遍历

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="op"></param>
        public void DLR(Action<IBTreeNode<T>> op)
        {
            DLR(_Root, op);
        }

        private void DLR(IBTreeNode<T> node, Action<IBTreeNode<T>> op)
        {
            if (null != node)
            {
                op(node);
                DLR(node.Left, op);
                DLR(node.Right, op);
            }
        }

        /// <summary>
        /// 中序遍历(升序)
        /// </summary>
        /// <param name="op"></param>
        public void LDR(Order order, Action<IBTreeNode<T>> op)
        {
            LDR(order, _Root, op);
        }

        private void LDR(Order order, IBTreeNode<T> node, Action<IBTreeNode<T>> op)
        {
            if (!node.IsNull)
            {
                if (Order.ASC == order)
                {
                    LDR(order, node.Left, op);
                    op(node);
                    LDR(order, node.Right, op);
                }
                else
                {
                    LDR(order, node.Right, op);
                    op(node);
                    LDR(order, node.Left, op);
                }
            }
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <param name="op"></param>
        public void LRD(Action<IBTreeNode<T>> op)
        {
            LRD(_Root, op);
        }

        private void LRD(IBTreeNode<T> node, Action<IBTreeNode<T>> op)
        {
            if (null != node)
            {
                LRD(node.Right, op);
                op(node);
                LRD(node.Left, op);
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 情形1
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// targetNode是新的根。在这种情形下，我们就做完了。我们从所有路径去除了一个黑色节点，而新根是黑色的，所以性质都保持着。
        /// </remarks>
        private void DeleteCase1(IBTreeNode<T> targetNode)
        {
            if (null != targetNode.Parent)
                DeleteCase2(targetNode);
            else
                targetNode.Colour = BTreeNodeColor.Black;
        }

        /// <summary>
        /// 情形2
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// Sibling是红色。在这种情形下在targetNode的父亲上做左旋转，把红色兄弟转换成targetNode的祖父，接着对调targetNode的父亲和祖父的颜色。
        /// 完成这两个操作后，尽管所有路径上黑色节点的数目没有改变，但现在N有了一个黑色的兄弟和一个红色的父亲（它的新兄弟是黑色因为它是红色S的一个儿子），所以可以接下去按情形4、情形5或情形6来处理。
        /// </remarks>
        private void DeleteCase2(IBTreeNode<T> targetNode)
        {
            var sibling = GetSibling(targetNode);

            if(BTreeNodeColor.Red == sibling.Colour)
            {
                targetNode.Parent.Colour = BTreeNodeColor.Red;
                sibling.Colour = BTreeNodeColor.Black;
                if (targetNode == targetNode.Parent.Left)
                    LeftRotate(targetNode.Parent);
                else
                    RightRotate(targetNode.Parent);
            }

            DeleteCase3(targetNode);
        }

        /// <summary>
        /// 情形3
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// targetNode的父亲、Sibling和Sibling的儿子都是黑色的。在这种情形下，简单的重绘Sibling为红色。
        /// 结果是通过S的所有路径，它们就是以前不通过N的那些路径，都少了一个黑色节点。
        /// 因为删除targetNode的初始的父亲使通过targetNode的所有路径少了一个黑色节点，这使事情都平衡了起来。
        /// 但是，通过Parent的所有路径现在比不通过Parent的路径少了一个黑色节点，所以仍然违反性质5。
        /// 要修正这个问题，要从情形1开始，在Parent上做重新平衡处理。
        /// </remarks>
        private void DeleteCase3(IBTreeNode<T> targetNode)
        {
            var sibling = GetSibling(targetNode);

            if (BTreeNodeColor.Black == targetNode.Parent.Colour &&
                BTreeNodeColor.Black == sibling.Colour &&
                BTreeNodeColor.Black == sibling.Left.Colour &&
                BTreeNodeColor.Black == sibling.Right.Colour)
            {
                sibling.Colour = BTreeNodeColor.Red;
                DeleteCase1(targetNode.Parent);
            }
            else
            {
                DeleteCase4(targetNode);
            }
        }

        /// <summary>
        /// 情形4
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// Sibling和Sibling的儿子都是黑色，但是targetNode的父亲是红色。
        /// 在这种情形下，我们简单的交换N的兄弟和父亲的颜色。
        /// 这不影响不通过targetNode的路径的黑色节点的数目，但是它在通过targetNode的路径上对黑色节点数目增加了一，添补了在这些路径上删除的黑色节点。
        /// </remarks>
        private void DeleteCase4(IBTreeNode<T> targetNode)
        {
            var sibling = GetSibling(targetNode);

            if(BTreeNodeColor.Red == targetNode.Parent.Colour &&
                BTreeNodeColor.Black == sibling.Colour &&
                BTreeNodeColor.Black == sibling.Left.Colour &&
                BTreeNodeColor.Black == sibling.Right.Colour)
            {
                sibling.Colour = BTreeNodeColor.Red;
                targetNode.Parent.Colour = BTreeNodeColor.Black;
            }
            else
            {
                DeleteCase5(targetNode);
            }
        }

        /// <summary>
        /// 情形5
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// Sibling是黑色，Sibling的左儿子是红色，Sibling的右儿子是黑色，而targetNode是它父亲的左儿子。
        /// 在这种情形下我们在Sibling上做右旋转，这样Sibling的左儿子成为Sibling的父亲和targeNode的新兄弟。
        /// 接着交换Sibling和它的新父亲的颜色。
        /// 所有路径仍有同样数目的黑色节点，但是现在targetNode有了一个黑色兄弟，他的右儿子是红色的，所以我们进入了情形6。
        /// targetNode和它的父亲都不受这个变换的影响。
        /// </remarks>
        private void DeleteCase5(IBTreeNode<T> targetNode)
        {
            var sibling = GetSibling(targetNode);

            if(BTreeNodeColor.Black == sibling.Colour)
            {
                if(targetNode == targetNode.Parent.Left &&
                    BTreeNodeColor.Black == sibling.Right.Colour &&
                    BTreeNodeColor.Red == sibling.Left.Colour)
                {
                    sibling.Colour = BTreeNodeColor.Red;
                    sibling.Left.Colour = BTreeNodeColor.Black;
                    RightRotate(sibling);
                }
                else if(targetNode == targetNode.Right &&
                    BTreeNodeColor.Black == sibling.Left.Colour &&
                    BTreeNodeColor.Red == sibling.Right.Colour)
                {
                    sibling.Colour = BTreeNodeColor.Red;
                    sibling.Right.Colour = BTreeNodeColor.Black;
                    LeftRotate(sibling);
                }
            }

            DeleteCase6(targetNode);
        }

        /// <summary>
        /// 情形6
        /// </summary>
        /// <param name="targetNode"></param>
        /// <remarks>
        /// Sibling是黑色，Sibling的右儿子是红色，而targetNode是它父亲的左儿子。
        /// 在这种情形下我们在N的父亲上做左旋转，这样Sibling成为targetNode的父亲（P）和Sibling的右儿子的父亲。
        /// 接着交换targetNode的父亲和Sibling的颜色，并使Sibling的右儿子为黑色。
        /// 子树在它的根上的仍是同样的颜色，所以性质3没有被违反。
        /// 但是，targetNode现在增加了一个黑色祖先：要么targetNode的父亲变成黑色，要么它是黑色而Sibling被增加为一个黑色祖父。所以，通过targetNode的路径都增加了一个黑色节点。
        /// 此时，如果一个路径不通过targetNode，则有两种可能性：
        /// 它通过targetNode的新兄弟。那么它以前和现在都必定通过Sibling和targetNode的父亲，而它们只是交换了颜色。所以路径保持了同样数目的黑色节点。
        /// 它通过targetNode的新叔父，Sibling的右儿子。那么它以前通过Sibling、Sibling的父亲和Sibling的右儿子，但是现在只通过Sibling，它被假定为它以前的父亲的颜色，和Sibling的右儿子，它被从红色改变为黑色。合成效果是这个路径通过了同样数目的黑色节点。
        /// 在任何情况下，在这些路径上的黑色节点数目都没有改变。所以我们恢复了性质4。在示意图中的白色节点可以是红色或黑色，但是在变换前后都必须指定相同的颜色。
        /// </remarks>
        private void DeleteCase6(IBTreeNode<T> targetNode)
        {
            var sibling = GetSibling(targetNode);

            sibling.Colour = targetNode.Parent.Colour;
            targetNode.Parent.Colour = BTreeNodeColor.Black;

            if(targetNode == targetNode.Parent.Left)
            {
                sibling.Right.Colour = BTreeNodeColor.Black;
                LeftRotate(targetNode.Parent);
            }
            else
            {
                sibling.Left.Colour = BTreeNodeColor.Black;
                RightRotate(targetNode.Parent);
            }
        }

        private bool DeleteChild(IBTreeNode<T> node, T data)
        {
            var retValue = false;

            var compareResult = node.Data.CompareTo(data);

            switch(compareResult)
            {
                case 1:
                    if(!node.Left.IsNull)
                    {
                        return DeleteChild(node.Left, data);
                    }
                    break;
                case -1:
                    if (!node.Right.IsNull)
                    {
                        return DeleteChild(node.Right, data);
                    }
                    break;
                case 0:
                    if(node.Left.IsNull)
                    {
                        DeleteOneChild(node);
                        return true;
                    }
                    
                    var largestNode = GetLargestNode(node.Left);
                    SwapNodeValue(node, largestNode);
                    DeleteOneChild(largestNode);
                    return true;
            }

            return retValue;
        }

        private void SwapNodeValue(IBTreeNode<T> node, IBTreeNode<T> smallestNode)
        {
            var midValue = default(T);

            midValue = node.Data;
            node.Data = smallestNode.Data;
            smallestNode.Data = midValue;

            midValue = default(T);
        }

        private void DeleteOneChild(IBTreeNode<T> node)
        {
            var childNode = node.Left.IsNull ? node.Right : node.Left;

            if (node.Parent.IsNull && node.Left.IsNull && node.Right.IsNull)
            {
                node = null;
                _Root = node;
                return;
            }

            if (node.Parent.IsNull)
            {
                node = null;
                childNode.Parent = new IBTreeNullNode<T>();
                _Root = childNode;
                _Root.Colour = BTreeNodeColor.Black;
                return;
            }

            if (node == node.Parent.Left)
            {
                node.Parent.Left = childNode;
            }
            else
            {
                node.Parent.Right = childNode;
            }

            childNode.Parent = node.Parent;

            if (BTreeNodeColor.Black == node.Colour)
            {
                if (BTreeNodeColor.Red == childNode.Colour)
                {
                    childNode.Colour = BTreeNodeColor.Black;
                }
                else
                {
                    DeleteCase1(childNode);
                }
            }


            node = null;
        }

        public void Delete(T key)
        {
            DeleteChild(_Root, key);
        }

        #endregion

        /// <summary>
        /// 根据给定的前序序列与中序序列还原红黑树
        /// </summary>
        /// <param name="bTree"></param>
        /// <param name="dlrList">前序序列</param>
        /// <param name="ldrList">中序序列</param>
        /// <returns></returns>
        /// <remarks>
        /// 代码参考：http://www.cnblogs.com/cnblogsnearby/p/4508203.html
        /// </remarks>
        public static void BuildTree(BTree<T> bTree, List<BTreeBuildNode<T>> dlrList, List<BTreeBuildNode<T>> ldrList)
        {
            if (null == dlrList || null == ldrList)
                throw new ArgumentNullException("必须给定前序序列和中序序列");

            if (0 == dlrList.Count || 0 == ldrList.Count)
                throw new ArgumentException("前序序列和中序序列不能为空");

            if (dlrList.Count != ldrList.Count)
                throw new ArgumentException(string.Format("前序序列的数量{0}与中序序列的数量{1}不一致", dlrList.Count, ldrList.Count));

            var buildStack = new Stack<IBTreeNode<T>>();

            var dlrIndex = 0;  //指向先序遍历向量
            var ldrIndex = 0;  //指向中序遍历向量

            var flag = 0;  //flag = 0指向左子树，当flag = 1指向右子树

            var root = bTree.CreateNode(dlrList[dlrIndex].Data);
            root.Colour = dlrList[dlrIndex].Colour;

            buildStack.Push(root);

            bTree.Root = root;
            var tempNode = root;

            dlrIndex++;

            while (dlrIndex < dlrList.Count)
            {
                if(0 != buildStack.Count && 0 == buildStack.Peek().Data.CompareTo(ldrList[ldrIndex].Data))
                {
                    tempNode = buildStack.Pop(); //更换temp
                    flag = 1;
                    ldrIndex++;
                }
                else
                {
                    if(0 == flag)
                    {
                        tempNode.Left = bTree.CreateNode(dlrList[dlrIndex].Data);
                        tempNode.Left.Colour = dlrList[dlrIndex].Colour;
                        tempNode.Left.Parent = tempNode;
                        tempNode = tempNode.Left;
                        buildStack.Push(tempNode);
                        dlrIndex++;
                    }
                    else
                    {
                        flag = 0; //右节点不连续添加
                        tempNode.Right = bTree.CreateNode(dlrList[dlrIndex].Data);
                        tempNode.Right.Colour = dlrList[dlrIndex].Colour;
                        tempNode.Right.Parent = tempNode;
                        tempNode = tempNode.Right;
                        buildStack.Push(tempNode);
                        dlrIndex++;
                    }
                }
            }
        }
    }
}
