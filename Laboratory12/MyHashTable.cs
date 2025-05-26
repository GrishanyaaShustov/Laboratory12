using System;

namespace Car
{
    // Узел связного списка для хранения элемента и ссылки на следующий
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

        // Возвращает хэш-код объекта Data
        public override int GetHashCode() => Data?.GetHashCode() ?? 0;
    }

    // Обобщённая хеш-таблица с методом цепочек
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
        public int DefaultLength => defaultLength;

        // Вычисляет индекс ячейки по хэш-коду
        private int GetIndex(Point<T> point)
            => Math.Abs(point?.GetHashCode() ?? 0) % size;

        // Добавляет элемент в таблицу
        public void Add(T item)
        {
            var node = new Point<T>(item);
            int index = GetIndex(node);

            if (table[index] == null)
                table[index] = node; // Если ячейка пуста — вставляем
            else
            {
                // Иначе добавляем в конец цепочки
                var current = table[index];
                while (current.Next != null) current = current.Next;
                current.Next = node;
            }

            count++;
        }

        // Удаление элемент по ключу
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
                        // Удаление головы или середины цепочки
                        if (prev == null)
                            table[i] = current.Next;
                        else
                            prev.Next = current.Next;

                        count--;
                        return true;
                    }

                    prev = current;
                    current = current.Next;
                }
            }

            return false;
        }

        // Поиск элемента по ключу
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

        // Полная очистка таблицы
        public void Clear()
        {
            table = new Point<T>[size];
            count = 0;
        }

        // Сравнение ключа с ключом объекта
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
