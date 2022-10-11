﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taf.Core.Utility
{
    public class BTreeBuildNode<T>
    {
        public T Data { get; set; }
        public BTreeNodeColor Colour { get; set; }

        public override string ToString() => string.Format("{0} {1}", Data, BTreeNodeColor.Black == Colour ? "Black" : "Red");
    }
}
