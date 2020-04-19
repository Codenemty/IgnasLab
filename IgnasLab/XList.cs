using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public sealed class XList<T> : IEnumerable<T> where T : IEquatable<T>, IComparable<T>
    {
        private XNode<T> head { get; set; }
        private XNode<T> tail { get; set; }

        private XNode<T> iterator { get; set; }
        private int count { get; set; }
        public XList()
        {
            this.head = null;
            this.tail = null;

        }
        public XList(T data)
            {
                XNode<T> node = new XNode<T>(data);
                this.head = node;
                this.tail = node;
            }
        public XList(XNode<T> node)
            {
                this.head = node;
                this.tail = node;
            }
        public void Add(T data)
        {
            if (Contains(data)) return;
            XNode<T> node = new XNode<T>(data);
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
        public void Add(XNode<T> node)
        {
            Add(node.Data);
        }
        public bool Contains(T data)
        {
            for (XNode<T> d = head; d != null; d = d.Link)
            {
                if (d.Data.Equals(data)) return true;
            }
            return false;
        }
        public int Count() => this.count;
        public void Dispose()
        {
            while (head != null)
            {
                iterator = head;
                head = head.Link;
                iterator = null;
            }
            head = null;
            tail = null;
            this.count = 0;
        }
        public int IndexOf(T data)
        {
            int index = 0;
            iterator = head;
            while(iterator != null)
            {
                if (iterator.Data.Equals(data)) return index;
                iterator = iterator.Link;
                index++;
            }
            return -1;
        }
        private XNode<T> FindNode(T data)
        {
            for (XNode<T> d = head; d != null; d = d.Link)
            {
                if (d.Data.Equals(data)) return d;
            }
            return null;
        }
        private XNode<T> FindPriorNode(XNode<T> current)
        {
            if (current == this.head) return null;

            XNode<T> result;
            for (result = head; result.Link != current; result = result.Link) ;

            return result;


        }
        public void Remove(T data)
        {
            if (!Contains(data)) return;
            if (count == 1)
            {
                Dispose();
                count--;
                iterator = head;
                return;
            }
            var node = FindNode(data);
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
            iterator = head;
        }
        public void Sort()
        {
            for (XNode<T> a = head; a?.Link != null; a = a.Link)
            {
                for (XNode<T> b = a.Link; b != null; b = b.Link)
                {
                    if (a.Data.CompareTo(b.Data) == -1)
                    {
                        Swap(a, b);
                    }
                }
            }
        }
        private void Swap(XNode<T> a, XNode<T> b)
        {
            T tempData = a.Data;
            a.Data = b.Data;
            b.Data = tempData;
        }

        public IEnumerator<T> GetEnumerator()
        {
            iterator = head;
            while (iterator != null)
            {
                yield return iterator.Data;
                iterator = iterator.Link;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    }
}