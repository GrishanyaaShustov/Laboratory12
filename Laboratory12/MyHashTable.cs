using System;

namespace Car
{
    public class MyHashTable<T>
    {
        public class Point<T>
        {
            public T Data { get; set; }
            public Point<T> Next { get; set; }

            public Point(T data)
            {
                Data = data;
                Next = null;
            }

            public override string ToString() => Data?.ToString();
        }

        private Point<T>[] table;
        private int size, count;
        private const int defaultLength = 10;

        public MyHashTable()
        {
            size = defaultLength;
            table = new Point<T>[size];
            count = 0;
        }

        public int Count => count;
        
        private int GetIndex(T item)
            => Math.Abs(item?.GetHashCode() ?? 0) % size;

        public void Add(T item)
        {
            int index = GetIndex(item);
            var node = new Point<T>(item);

            if (table[index] == null)
            {
                table[index] = node;
            }
            else
            {
                var current = table[index];
                while (current.Next != null)
                    current = current.Next;
                current.Next = node;
            }

            count++;
        }

        public bool Remove(object key)
        {
            for (int i = 0; i < size; i++)
            {
                var current = table[i];
                Point<T> prev = null;

                while (current != null)
                {
                    if (EqualsByKey(current.Data, key))
                    {
                        if (prev == null) table[i] = current.Next;
                        else prev.Next = current.Next;
                        count--;
                        return true;
                    }

                    prev = current;
                    current = current.Next;
                }
            }
            return false;
        }

        public T Find(object key)
        {
            for (int i = 0; i < size; i++)
            {
                var current = table[i];
                while (current != null)
                {
                    if (EqualsByKey(current.Data, key))
                        return current.Data;

                    current = current.Next;
                }
            }
            return default;
        }

        public void Clear()
        {
            table = new Point<T>[size];
            count = 0;
        }

        public void PrintHS()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"[{i}]: ");
                var current = table[i];
                while (current != null)
                {
                    Console.Write($"{current} -> ");
                    current = current.Next;
                }
                Console.WriteLine("null");
            }
        }

        private bool EqualsByKey(T data, object key)
        {
            if (data is Car car)
            {
                var val = car.CarId;

                if (key is int num && val is IdNumber id2)
                    return num == id2.Number;

                return val?.Equals(key) ?? false;
            }

            return false;
        }
    }
}
