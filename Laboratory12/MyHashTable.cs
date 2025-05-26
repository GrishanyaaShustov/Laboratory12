using System;

namespace Car
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
        public override int GetHashCode()
        {
            if (Data is Car car && car.CarId != null)
                return car.CarId.GetHashCode();
            return 0;
        }
    }
    public class MyHashTable<T>
    {

        public Point<T>[] table;
        private int size, count;
        private const int defaultLength = 10;

        public MyHashTable()
        {
            size = defaultLength;
            table = new Point<T>[size];
            count = 0;
        }

        public int Count => count;
        
        private int GetIndex(Point<T> point)
            => Math.Abs(point?.GetHashCode() ?? 0) % size;
        
        private int GetIndex(object key)
            => Math.Abs(key?.GetHashCode() ?? 0) % size;

        public void Add(T item)
        {
            var node = new Point<T>(item);
            int index = GetIndex(node);

            if (table[index] == null)
                table[index] = node;
            else
            {
                var current = table[index];
                while (current.Next != null) current = current.Next;
                current.Next = node;
            }

            count++;
        }

        public bool Remove(object key)
        {
            int index = GetIndex(key);

            var current = table[index];
            Point<T> prev = null;

            while (current != null)
            {
                if (EqualsByKey(current.Data, key))
                {
                    if (prev == null)
                        table[index] = current.Next;
                    else
                        prev.Next = current.Next;

                    count--;
                    return true;
                }

                prev = current;
                current = current.Next;
            }

            return false;
        }

        public T Find(object key)
        {
            int index = GetIndex(key);

            var current = table[index];
            while (current != null)
            {
                if (EqualsByKey(current.Data, key))
                    return current.Data;

                current = current.Next;
            }

            return default;
        }

        public void Clear()
        {
            table = new Point<T>[size];
            count = 0;
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
