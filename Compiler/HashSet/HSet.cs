using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.LinkedList;

namespace Compiler.HashSet
{
    class HSet<T>
    {
        private LList<T>[] lists;

        public HSet(int bucketsCount = 50)
        {
            lists = Enumerable.Range(0, bucketsCount)
                .Select(_ => new LList<T>()).ToArray();
        }
        public void Add(T element)
        {
            var targetList = lists[element.GetHashCode() % lists.Length];
            if (!targetList.GetElements().Contains(element))
                targetList.Add(element);
        }

        public void Remove(T element)
        {
            var targetList = lists[element.GetHashCode() % lists.Length];
            for (int i = 0; i < targetList.size; i++)
            {
                if (targetList.Get(i).Equals(element))
                {
                    targetList.Remove(i);
                    return;
                }
            }
        }


        public IEnumerable<T> GetElements()
        {
            foreach (var list in lists)
            {
                foreach (var item in list.GetElements())
                {
                    yield return item;
                }
            }
        }

        public bool Contains(T element)
        {
            var targetList = lists[element.GetHashCode() % lists.Length];
            for (int i = 0; i < targetList.size; i++)
            {
                if (targetList.Get(i).Equals(element))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
