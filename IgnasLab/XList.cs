using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public class XList
    {
        private XNode head;
        private XNode tail;

        private XNode iterator;
        private int count { get; set; }
        public XList()
        {
            this.head = null;
            this.tail = null;
        }
        public XList(Player data)
        {
            XNode node = new XNode(data);
            this.head = node;
            this.tail = node;
        }
        public XList(XNode node)
        {
            this.head = node;
            this.tail = node;
        }
        public void Add(Player data)
        {
            if (Contains(data)) return;
            XNode node = new XNode(data);
            //Empty
            if (this.head == null)
            {
                this.head = node;
                this.tail = node;
            }
            //Not empty
            else
            {
                tail.Link = node;
                tail = node;
            }
            this.count++;
        }
        public void Add(XNode node)
        {
            Add(node.Data);
        }
        public void Begin()
        {
            this.iterator = this.head;
        }
        public bool Contains(Player player)
        {
            for (XNode d = head; d != null; d = d.Link)
            {
                if (d.Data.Equals(player)) return true;
            }
            return false;
        }
        public int Count() => this.count;
        public void Dispose()
        {
            while (head != null)
            {
                XNode iterator = head;
                head = head.Link;
                iterator = null;
            }
            head = null;
            tail = null;
            this.count = 0;
        }
        public bool Exist() => iterator != null;
        public void Foreach(Action<Player> action)
        {
            for (Begin(); Exist(); Next())
            {
                action(Get());
            }
        }
        public Player Get()
        {
            return iterator.Data;
        }
        private XNode FindNode(Player player)
        {
            for (XNode d = head; d != null; d = d.Link)
            {
                if (d.Data.Equals(player)) return d;
            }
            return null;
        }
        private XNode FindPriorNode(XNode current)
        {
            if (current == this.head) return null;

            XNode result;
            for (result = head; result.Link != current; result = result.Link) ;

            return result;


        }
        public void Next()
        {
            iterator = iterator.Link;
        }
        public void Remove(Player player)
        {
            if (!Contains(player)) return;
            if (count == 1)
            {
                Dispose();
                count--;
                Begin();
                return;
            }
            var node = FindNode(player);
            var nodePrior = FindPriorNode(node);
            var nodeFollowing = node?.Link;

            node.Link = null;
            if (nodePrior != null)
            {
                nodePrior.Link = nodeFollowing;
            }
            else
            {
                head = nodeFollowing;
            }

            if (nodeFollowing == null)
            {
                tail = nodePrior;
            }
            count--;
            Begin();
        }
        public void Sort()
        {
            for (XNode a = head; a?.Link != null; a = a.Link)
            {
                for (XNode b = a.Link; b != null; b = b.Link)
                {
                    if (a.Data < b.Data)
                    {
                        Swap(a, b);
                    }
                }
            }
        }
        private void Swap(XNode a, XNode b)
        {
            Player tempData = a.Data;
            a.Data = b.Data;
            b.Data = tempData;
        }

    }
}