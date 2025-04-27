namespace Car
{
    class Program
    {
        static void Main()
        {
            var list = new DoublyLinkedList<Car>();
            DoublyLinkedList<Car> clone = null;

            while (true)
            {
                Console.WriteLine("\n1. Добавить элементы с номерами 1,3,5... (рандомные)");
                Console.WriteLine("2. Вывести список");
                Console.WriteLine("3. Удалить элементы от заданного имени до конца");
                Console.WriteLine("4. Глубокое клонирование списка");
                Console.WriteLine("5. Удалить список из памяти");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Сколько нечётных элементов добавить?: ");
                        if (int.TryParse(Console.ReadLine(), out int count))
                            list.AddOddGenerated(count);
                        break;
                    case "2":
                        list.Print();
                        break;
                    case "3":
                        Console.Write("Введите имя бренда для удаления с него и до конца: ");
                        string name = Console.ReadLine();
                        list.DeleteFromName(name);
                        break;
                    
                    case "4":
                        // Клонируем список.
                        DoublyLinkedList<Car> cloned = (DoublyLinkedList<Car>)list.Clone();
                        Console.WriteLine("Список клонирован!");

                        // Изменяем CarId для каждого элемента в клонированном списке, чтобы убедиться в клонировании.
                        DoublyLinkedList<Car>.Node currentClone = cloned.head;
                        while (currentClone != null)
                        {
                            // Для каждого клонированного объекта меняем ID.
                            if (currentClone.Data != null)
                            {
                                currentClone.Data.CarId = new IdNumber(5);  // Генерация нового ID.
                            }
                            currentClone = currentClone.Next;
                        }
    
                        // Выводим клонированный список с изменёнными CarId.
                        Console.WriteLine("Клонированный список с изменёнными CarId:");
                        cloned.Print();
    
                        // Выводим оригинальный список снова, чтобы показать, что изменения в clone не повлияли на оригинал.
                        Console.WriteLine("\nОригинальный список после изменения CarId в клонированном списке:");
                        list.Print();
    
                        break;

                    case "5":
                        list.Clear();
                        Console.WriteLine("Список очищен из памяти.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод!");
                        break;
                }
            }
        }
    }
}