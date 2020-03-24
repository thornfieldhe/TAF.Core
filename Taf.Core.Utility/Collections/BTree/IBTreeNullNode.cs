﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taf.Core.Utility
{
    public class IBTreeNullNode<T> : IBTreeNode<T>
    {
        private IBTreeNode<T> _Parent;

        public IBTreeNullNode()
        {
            
        }

        public bool IsNull
        {
            get
            {
                return true;
            }
        }

        public BTreeNodeColor Color
        {
            get { return BTreeNodeColor.Black; }
        }

        public BTreeNodeColor Colour
        {
            get
            {
                return BTreeNodeColor.Black;
            }
            set
            {
                
            }
        }

        public IBTreeNode<T> Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        public IBTreeNode<T> Left
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public IBTreeNode<T> Right
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public T Data
        {
            get
            {
                return default(T);
            }
            set
            {
                
            }
        }
    }
}
