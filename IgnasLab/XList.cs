using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public sealed class XList
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
        public bool All(Func<Player, bool> query)
        {
            bool flag = true;
            Foreach((x) => { if (!query(x)) flag = false; });
            return flag;
        }
        public bool Any(Func<Player, bool> query)
        {
            bool flag = false;
            Foreach((x) => { if (query(x)) flag = true; });
            return flag;
        }
        public decimal Average(Func<Player, decimal> query)
        {
            try
            {
                return Sum(query) / Count();
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
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
        public void Insert(Player player, int index)
        {
            XNode prior = null;
            var node = new XNode(player);
            if (index == 0)//If insert as first
            {
                node.Link = head;
                head = node;
                this.count++;
                return;
            }
            if (index > count)//If insert as last
            {
                Add(node);
            }
            int i = 0;
            for (Begin(); Exist(); Next())
            {
                if (i < index - 1)
                {
                    prior = iterator;
                }
                i++;
            }

            node.Link = prior.Link;
            prior.Link = node;
            this.count++;

        }
        public Player Get()
        {
            return iterator.Data;
        }
        public Player Find(Func<Player, bool> query)
        {
            for (Begin(); Exist(); Next())
            {
                if (query(Get())) return Get();
            }
            return default(Player);
        }
        private XNode FindNode(Player player)
        {
            for (XNode d = head; d != null; d = d.Link)
            {
                if (d.Data.Equals(player)) return d;
            }
            return null;
        }
        private XNode FindNodeBefore(XNode current)
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
            if (count == 1) // If removing only element, dispose.
            {
                Dispose();
                count--;
                Begin();
                return;
            }
            var node = FindNode(player);
            var nodePrior = FindNodeBefore(node);
            var nodeFollowing = node?.Link;

            node.Link = null;
            if (nodePrior != null) // If removing first element, following becomes head.
            {
                nodePrior.Link = nodeFollowing;
            }
            else
            {
                head = nodeFollowing;
            }

            if (nodeFollowing == null)// If removing last element, prior becomes tail.
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
                    if (a.Data.CompareTo(b.Data) == -1)
                    {
                        Swap(a, b);
                    }
                }
            }
        }
        public decimal Sum(Func<Player, decimal> query)
        {
            decimal total = 0M;
            Foreach(x =>
            {
                total += query(x);
            });
            return total;
        }
        private void Swap(XNode a, XNode b)
        {
            Player tempData = a.Data;
            a.Data = b.Data;
            b.Data = tempData;
        }

    }
}