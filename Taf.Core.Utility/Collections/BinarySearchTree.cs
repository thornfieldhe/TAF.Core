// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinarySearchTree.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   二叉树实实现
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Taf.Core.Utility{
    using System;

    public class Node{
        public int Data{ get; set; }

        public Node Left{ get; set; }

        public Node Right{ get; set; }

        public void DisplayNode(){
            Console.WriteLine(Data);
        }
    }

    public class BinarySearchTree{
        public Node Root{ get; set; }

        public BinarySearchTree() => Root = null;

        public void Insert(int i){
            var newNode = new Node{Data = i};
            if(Root == null){
                Root = newNode;
            } else{
                var current = Root;
                while(true){
                    var parent = current;
                    if(i < current.Data){
                        current = current.Left;
                        if(current == null){
                            parent.Left = newNode;
                            break;
                        }
                    } else{
                        current = current.Right;
                        if(current == null){
                            parent.Right = newNode;
                            break;
                        }
                    }
                }
            }
        }

        public void InOrder(Node theRoot){
            if(theRoot != null){
                InOrder(theRoot.Left);
                theRoot.DisplayNode();
                InOrder(theRoot.Right);
            }
        }

        public void PreOrder(Node theRoot){
            if(theRoot != null){
                theRoot.DisplayNode();
                InOrder(theRoot.Left);
                InOrder(theRoot.Right);
            }
        }

        public void PostOrder(Node theRoot){
            if(theRoot != null){
                InOrder(theRoot.Left);
                InOrder(theRoot.Right);
                theRoot.DisplayNode();
            }
        }

        public int FindMin(){
            var current = Root;
            while(current.Left != null){
                current = current.Left;
            }

            return current.Data;
        }

        public int FindMax(){
            var current = Root;
            while(current.Right != null){
                current = current.Right;
            }

            return current.Data;
        }

        public Node Find(int key){
            var current = Root;
            while(current.Data != key){
                if(key < current.Data){
                    current = current.Left;
                } else{
                    current = current.Right;
                }

                if(current == null){
                    return null;
                }
            }

            return current;
        }

        public bool Delete(int key){
            var current     = Root;
            var parent      = Root;
            var isLeftChild = true;
            while(current.Data != key){
                parent = current;
                if(key < current.Data){
                    isLeftChild = true;
                    current     = current.Left;
                } else{
                    isLeftChild = false;
                    current     = current.Right;
                }

                if(current == null){
                    return false;
                }
            }

            if(current.Left == null
            && current.Right == null){
                if(current == Root){
                    Root = null;
                } else if(isLeftChild){
                    parent.Left = null;
                } else{
                    parent.Right = null;
                }

                return true;
            }

            if(current.Right == null){
                if(current == Root){
                    Root = current.Left;
                } else if(isLeftChild){
                    parent.Left = current.Left;
                } else{
                    parent.Right = current.Right;
                }
            } else if(current.Left == null){
                if(current == Root){
                    Root = current.Right;
                } else if(isLeftChild){
                    parent.Left = current.Left;
                } else{
                    parent.Right = current.Right;
                }
            } else{
                var successor = GetSuccessor(current);
                if(current == Root){
                    Root = successor;
                } else if(isLeftChild){
                    parent.Left = successor;
                } else{
                    parent.Right = successor;
                }

                successor.Left = current.Left;
            }

            return true;
        }

        public Node GetSuccessor(Node delNode){
            var successorParent = delNode;
            var successor       = delNode;
            var current         = delNode.Right;
            while(current != null){
                successorParent = current;
                successor       = current;
                current         = current.Left;
            }

            if(successor != delNode.Right){
                successorParent.Left = successor.Right;
                successor.Right      = delNode.Right;
            }

            return successor;
        }
    }
}
