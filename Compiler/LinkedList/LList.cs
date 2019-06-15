using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.LinkedList
{
    class LList<T>
    {
        private LNode<T> first;
        private LNode<T> last;

        public int size = 0;

        public void Add(T element)
        {
            LNode<T> l = last;
            LNode<T> newNode = new LNode<T>(l, element, null);
            last = newNode;
            if (l == null)
            {
                first = newNode;
            } else
            {
                l.next = newNode;
            }
            size++;
        }

        public T Get(int position)
        {
            var els = GetAllElements();
            return els[els.Length - 1 - position];
        }

        public void Remove(int position)
        {
            var els = GetAllElements();
            if(els.Length > 0 && els.Length - 1 - position < els.Length)
            {
                size = 0;
            }
            first = null;
            last = null;
            for (int i = els.Length - 1; i >= 0; i--)
            {
                if (i != els.Length - 1 - position)
                {
                    Add(els[i]);
                } 
            }
        }

        public List<T> GetElements()
        {
            List<T> lst = new List<T>();
            LNode<T> entries = last;
            while(entries != null)
            {
                lst.Add(entries.element);
                entries = entries.prev;
            }
            return lst;
        }

        private T[] GetAllElements()
        {
            return GetElements().ToArray();
        }
    }
}
