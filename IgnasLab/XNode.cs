using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public class XNode
    {
        public Player Data { get; set; }
        public XNode Link { get; set; }
        public XNode() { }
        public XNode(Player player)
        {
            this.Data = player;
        }
    }
}