using System;
using System.Collections;
using System.Collections.Generic;

namespace MyList
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
            Next = null;
        }
        public T Value { get; }
        public Node<T> Next { get; set; }
    }

    public class MyList<T> : IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _count;

        public void Create(IEnumerable<T> array)
        {
            foreach (var element in array)
            {
                PushBack(element);
            }
        }

        public void PushBack(T value)
        {
            Node<T> node = new Node<T>(value);
            
            if (_head == null)
                _head = node;
            else
                _tail.Next = node;
            
            _tail = node;
            _count++;
        }
        
        public void PushFront(T value)
        {
            Node<T> node = new Node<T>(value);
            
            node.Next = _head;
            _head = node;
            if (_count == 0)
                _tail = _head;

            _count++;
        }
        
        public bool Delete(T value)
        {
            Node<T> current = _head;
            Node<T> previous = null;
            
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    if (previous == null)
                    {
                        _head = _head.Next;
                        if (_head == null)
                            _tail = null;
                    }
                    else
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            _tail = previous;
                    }
                    _count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }
        
        public void Reverse()
        {
            Node<T> current = _head;
            Node<T> previous = null;
            Node<T> next;

            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            _tail = _head;
            _head = previous;
        }
        
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }       

        public IEnumerator<T> GetEnumerator()
        {
            var cur = _head;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable).GetEnumerator();
        }
    }

    

    public static class Program
    {
        public static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>();

            Console.Write("Create: ");
            
            int[] arr = {2, 3, 4};
            list.Create(arr);

            foreach (var element in list)
                Console.Write(element + " ");
            Console.WriteLine();

            Console.Write("PushBack: ");

            list.PushBack(5);
            list.PushBack(6);

            foreach (var element in list)
                Console.Write(element + " ");
            Console.WriteLine();

            Console.Write("PushFront: ");

            list.PushFront(1);
            list.PushFront(0);

            foreach (var element in list)
                Console.Write(element + " ");
            Console.WriteLine();

            Console.Write("Delete 4: ");

            list.Delete(4);

            foreach (var element in list)
                Console.Write(element + " ");
            Console.WriteLine();
            
            Console.Write("Reverse: ");

            list.Reverse();

            foreach (var element in list)
                Console.Write(element + " ");
            Console.WriteLine();

            list.Clear();
        }
    }
}