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

        // Извлекаем ‑ключ из объекта
        private object GetKey(T item)
        {
            var prop = item.GetType().GetProperty("CarId") ??
                       item.GetType().GetProperty("Id");
            if (prop != null)
            {
                var val = prop.GetValue(item)!;
                // Если это IdNumber, отдаём сам объект
                if (val is IdNumber) return val;
            }
            // В остальных случаях — сам объект
            return item!;
        }

        // Хешируем именно ключ
        private int GetIndex(object key)
            => Math.Abs(key.GetHashCode()) % size;

        public void Add(T item)
        {
            var key   = GetKey(item);
            int idx   = GetIndex(key);
            var node  = new Point<T>(item);

            if (table[idx] == null)
                table[idx] = node;
            else
            {
                var cur = table[idx];
                while (cur.Next != null) cur = cur.Next;
                cur.Next = node;
            }

            count++;
        }

        public bool Remove(object key)
        {
            int idx = GetIndex(key);
            var cur = table[idx];
            Point<T> prev = null;

            while (cur != null)
            {
                if (EqualsByKey(cur.Data, key))
                {
                    // выкидываем узел из цепочки
                    if (prev == null) table[idx] = cur.Next;
                    else              prev.Next = cur.Next;
                    count--;
                    return true;
                }
                prev = cur;
                cur  = cur.Next;
            }
            return false;
        }

        public T Find(object key)
        {
            int idx = GetIndex(key);
            var cur = table[idx];
            while (cur != null)
            {
                if (EqualsByKey(cur.Data, key))
                    return cur.Data;
                cur = cur.Next;
            }
            return default;
        }

        public bool Contains(T item)
        {
            // просто прямое сравнение по Equals
            foreach (var head in table)
            {
                var cur = head;
                while (cur != null)
                {
                    if (cur.Data.Equals(item))
                        return true;
                    cur = cur.Next;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException();
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException();

            int i = arrayIndex;
            foreach (var head in table)
            {
                var cur = head;
                while (cur != null)
                {
                    if (i >= array.Length)
                        throw new ArgumentException("Массив мал");
                    array[i++] = cur.Data;
                    cur = cur.Next;
                }
            }
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
                var cur = table[i];
                while (cur != null)
                {
                    Console.Write($"{cur} -> ");
                    cur = cur.Next;
                }
                Console.WriteLine("null");
            }
        }

        private bool EqualsByKey(T data, object key)
        {
            // Берём CarId или Id
            var prop = data.GetType().GetProperty("CarId") ??
                       data.GetType().GetProperty("Id");
            if (prop != null)
            {
                var val = prop.GetValue(data);
                // Сравниваем IdNumber==IdNumber
                if (key is IdNumber idObj && val is IdNumber idVal)
                    return idObj.Number == idVal.Number;
                // Сравниваем int ключ с val.Number
                if (key is int num && val is IdNumber id2)
                    return num == id2.Number;
                return val?.Equals(key) ?? false;
            }
            // fallback — напрямую
            return data.Equals(key);
        }
    }
}
