namespace Car
{
    public class MyHashTable<T>
    {
        public class Point<T>
        {
            public T Data { get; set; }
            public Point<T> Next { get; set; }
            public Point(T data) { Data = data; Next = null; }
            public override string ToString() => Data.ToString();
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

        // Извлекаем ключ из объекта по свойству CarId
        private object GetKey(T item)
        {
            return ((Car)(object)item).CarId;
        }

        // Хешируем именно ключ (CarId)
        private int GetIndex(object key)
            => Math.Abs(key.GetHashCode()) % size;

        public void Add(T item)
        {
            var key = GetKey(item);
            int index = GetIndex(key);
            var node = new Point<T>(item);

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
                    // выкидываем узел из цепочки
                    if (prev == null) table[index] = current.Next;
                    else prev.Next = current.Next;
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

        public bool Contains(T item)
        {
            // Прямое сравнение по Equals
            foreach (var head in table)
            {
                var current = head;
                while (current != null)
                {
                    if (current.Data.Equals(item))
                        return true;
                    current = current.Next;
                }
            }
            return false;
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

        // Сравниваем ключи по CarId
        private bool EqualsByKey(T data, object key)
        {
            var val = ((Car)(object)data).CarId;
            
            if (key is int num && val is IdNumber id2)
                return num == id2.Number;

            return val?.Equals(key) ?? false;
        }
    }
}
