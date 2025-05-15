using System;

namespace Car
{
    class Program
    {
        static void Main()
        {
            var list = new DoublyLinkedList<Car>();
            DoublyLinkedList<Car> clone = null;
            var hashTable = new MyHashTable<Car>();

            while (true)
            {
                Console.WriteLine("\n1. Операции с двусвязным списком");
                Console.WriteLine("2. Операции с хеш-таблицей");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        // Подменю для работы с двусвязным списком
                        while (true)
                        {
                            Console.WriteLine("\n--- Меню: Операции с двусвязным списком ---");
                            Console.WriteLine("1. Добавить элементы с номерами 1,3,5... (рандомные)");
                            Console.WriteLine("2. Вывести список");
                            Console.WriteLine("3. Удалить элементы от заданного имени до конца");
                            Console.WriteLine("4. Глубокое клонирование списка");
                            Console.WriteLine("5. Удалить список из памяти");
                            Console.WriteLine("0. Назад в главное меню");
                            Console.Write("Ваш выбор: ");

                            string listChoice = Console.ReadLine();
                            switch (listChoice)
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
                                    return;  // Выход в главное меню
                                default:
                                    Console.WriteLine("Некорректный ввод!");
                                    break;
                            }
                        }
                    case "2":
                        Random hashRand = new Random();
                        for (int i = 0; i < 20; i++)
                        {
                            Car newCar = null;
                            int randChoice = hashRand.Next(0, 3);
                            switch (randChoice)
                            {
                                case 0:
                                    newCar = new LightCar();
                                    newCar.RandomInit();
                                    hashTable.Add(newCar);
                                    break;
                                
                                case 1:
                                    newCar = new BigCar();
                                    newCar.RandomInit();
                                    hashTable.Add(newCar);
                                    break;

                                case 2:
                                    newCar = new DeliveryCar();
                                    newCar.RandomInit();
                                    hashTable.Add(newCar);
                                    break;
                            }
                        }
                        // Подменю для работы с хеш-таблицей
                        while (true)
                        {
                            
                            Console.WriteLine("\n--- Меню: Операции с хеш-таблицей ---");
                            Console.WriteLine("1. Добавить элемент в хеш-таблицу");
                            Console.WriteLine("2. Найти элемент по ключу");
                            Console.WriteLine("3. Вывести хеш-таблицу");
                            Console.WriteLine("4. Очистка");
                            Console.WriteLine("0. Назад в главное меню");
                            Console.Write("Ваш выбор: ");

                            string hashChoice = Console.ReadLine();
                            switch (hashChoice)
                            {
                                case "1":
                                    bool printIdMannualy = false;333
                                    Console.WriteLine("Выберите тип машины для добавления:");
                                    Console.WriteLine("1. Легковой автомобиль");
                                    Console.WriteLine("2. Грузовик");
                                    Console.WriteLine("3. Спортивный автомобиль");
                                    Console.Write("Ваш выбор: ");
                                    string carTypeChoice = Console.ReadLine();

                                    Car newCar = null;

                                    switch (carTypeChoice)
                                    {
                                        case "1":
                                            newCar = new LightCar();
                                            break;
                                        case "2":
                                            newCar = new BigCar();
                                            break;
                                        case "3":
                                            newCar = new DeliveryCar();
                                            break;
                                        default:
                                            Console.WriteLine("Неверный выбор типа автомобиля.");
                                            break;
                                    }
                                    
                                    newCar.RandomInit();
                                    if (printIdMannualy)
                                    {
                                        int carId;
                                        Console.WriteLine("Введите Id машины: ");
                                        Int32.TryParse(Console.ReadLine(), out carId);
                                        newCar.CarId = new IdNumber(carId);
                                    }
                                    hashTable.Add(newCar);
                                    Console.WriteLine($"Машина, {newCar.CarId.ToString()}, успешно добавлена в хеш-таблицу.\n");
                                    
                                    break;

                                case "2":
                                    Console.Write("Введите ключ для поиска (число CarId): ");
                                    if (int.TryParse(Console.ReadLine(), out int searchKey))
                                    {
                                        var foundCar = hashTable.Find(searchKey);
                                        if (foundCar != null)
                                        {
                                            Console.WriteLine("Найденный элемент:");
                                            Console.WriteLine(foundCar);

                                            Console.Write("Удалить этот элемент? (y/n): ");
                                            var confirm = Console.ReadLine();
                                            if (confirm?.ToLower() == "y")
                                            {
                                                if (hashTable.Remove(searchKey))
                                                    Console.WriteLine("Элемент удалён.");
                                                else
                                                    Console.WriteLine("Ошибка при удалении элемента.");
                                            }
                                        }
                                        else Console.WriteLine("Элемент с таким ключом не найден.");
                                    }
                                    else Console.WriteLine("Неверный формат ключа.");
                                    
                                    break;

                                case "3":
                                    Console.WriteLine("Вывод хеш-таблицы: \n");
                                    hashTable.PrintHS();
                                    break;
                                case "4":
                                    hashTable.Clear();
                                    Console.WriteLine("Хэш-таблица успешно очищена!");
                                    break;

                                case "0":
                                    return;  // Выход в главное меню
                                default:
                                    Console.WriteLine("Некорректный ввод!");
                                    break;
                            }
                        }

                    case "0":
                        return;  // Завершение работы программы
                    default:
                        Console.WriteLine("Некорректный ввод!");
                        break;
                }
            }
        }
    }
}
