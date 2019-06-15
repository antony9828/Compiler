namespace Compiler.LinkedList
{
    public class LNode<T>
    {
        public T element;
        public LNode<T> next;
        public LNode<T> prev;

        public LNode(LNode<T> prev, T element, LNode<T> next)
        {
            this.element = element;
            this.prev = prev;
            this.next = next;
        }
    }
}
