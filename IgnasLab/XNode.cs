using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public sealed class XNode<T>
    {
        public T Data { get; set; }
        public XNode<T> Link { get; set; }
        public XNode() { }
        public XNode(T data)
        {
            this.Data = data;
        }
    }
}