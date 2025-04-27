namespace Car
{
    public class DoublyLinkedList<T> : ICloneable where T : Car, ICloneable
    {
        public class Node
        {
            public T Data;
            public Node Prev, Next;

            public Node(T data)
            {
                Data = data;
            }
        }

        public Node head;
        public Node tail;
        public int Count { get; private set; }

        // Добавление элемента
        public void Add(T data)
        {
            Node newNode = new Node(data);
            if (head == null)
                head = tail = newNode;
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            Count++;
        }
        
        // печать
        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            Node current = head;
            int index = 1;
            while (current != null)
            {
                Console.Write($"{index}: ");
                if (current.Data is Car car)
                    car.Show();
                else
                    Console.WriteLine("null");
                current = current.Next;
                index++;
            }
        }

        // добавление нечетных рандомом
        public void AddOddGenerated(int count)
        {
            Type[] derivedTypes = { typeof(LightCar), typeof(BigCar), typeof(DeliveryCar) };
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                if ((Count + 1) % 2 == 1)  // Считаем позиции от текущего размера
                {
                    Type type = derivedTypes[rand.Next(derivedTypes.Length)];
                    Car car = (Car)Activator.CreateInstance(type);
                    car.RandomInit();
                    Add((T)car);
                }
                else
                {
                    Add(null);  // Добавляем null на чётные позиции
                }
            }
        }
        
        //Удаление с определенного бренда 
        public void DeleteFromName(string name)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data is Car car && car.Brand == name)
                {
                    // Начинаем удаление с текущего и до конца
                    Node toDelete = current;

                    // Если удаляем с начала
                    if (toDelete.Prev != null)
                    {
                        toDelete.Prev.Next = null;
                        tail = toDelete.Prev;
                    }
                    else
                    {
                        head = tail = null;
                    }

                    // Удаляем все узлы от toDelete до конца
                    while (toDelete != null)
                    {
                        Node nextNode = toDelete.Next;
                        toDelete.Prev = toDelete.Next = null;
                        toDelete = nextNode;
                        Count--;
                    }

                    break;
                }

                current = current.Next;
            }
        }


        public object Clone()
        {
            DoublyLinkedList<T> cloneList = new DoublyLinkedList<T>();
            Node current = head;

            while (current != null)
            {
                if (current.Data != null)
                {
                    cloneList.Add((T)current.Data.Clone());
                }
                else
                {
                    // Если элемент равен null, просто добавляем null
                    cloneList.Add(null);
                }

                current = current.Next;
            }

            return cloneList;
        }

        public void Clear()
        {
            Node current = head;
            while (current != null)
            {
                Node next = current.Next;
                current.Prev = current.Next = null;
                current = next;
            }
            head = tail = null;
            Count = 0;
        }
    }
}