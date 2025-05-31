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

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("\n1. Операции с двусвязным списком");
                Console.WriteLine("2. Операции с хеш-таблицей");
                Console.WriteLine("3. Операции с бинарным деревом");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        bool backToMainFromList = false;
                        while (!backToMainFromList)
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
                                    DoublyLinkedList<Car> cloned = (DoublyLinkedList<Car>)list.Clone();
                                    Console.WriteLine("Список клонирован!");

                                    DoublyLinkedList<Car>.Node currentClone = cloned.head;
                                    while (currentClone != null)
                                    {
                                        if (currentClone.Data != null)
                                        {
                                            currentClone.Data.CarId = new IdNumber(5);
                                        }
                                        currentClone = currentClone.Next;
                                    }

                                    Console.WriteLine("Клонированный список с изменёнными CarId:");
                                    cloned.Print();

                                    Console.WriteLine("\nОригинальный список после изменения CarId в клонированном списке:");
                                    list.Print();
                                    break;
                                case "5":
                                    list.Clear();
                                    Console.WriteLine("Список очищен из памяти.");
                                    break;
                                case "0":
                                    backToMainFromList = true;
                                    break;
                                default:
                                    Console.WriteLine("Некорректный ввод!");
                                    break;
                            }
                        }
                        break;

                    case "2":
                        Random hashRand = new Random();
                        for (int i = 0; i < 20; i++)
                        {
                            Car newCar = null;
                            switch (hashRand.Next(3))
                            {
                                case 0: newCar = new LightCar(); break;
                                case 1: newCar = new BigCar(); break;
                                case 2: newCar = new DeliveryCar(); break;
                            }
                            newCar.RandomInit();
                            hashTable.Add(newCar);
                        }

                        bool backToMainFromHash = false;
                        while (!backToMainFromHash)
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
                                    Console.WriteLine("Выберите тип машины для добавления:");
                                    Console.WriteLine("1. Легковой автомобиль");
                                    Console.WriteLine("2. Грузовик");
                                    Console.WriteLine("3. Спортивный автомобиль");
                                    Console.Write("Ваш выбор: ");
                                    string carTypeChoice = Console.ReadLine();

                                    Car newCar = carTypeChoice switch
                                    {
                                        "1" => new LightCar(),
                                        "2" => new BigCar(),
                                        "3" => new DeliveryCar(),
                                        _ => null
                                    };

                                    if (newCar != null)
                                    {
                                        newCar.RandomInit();
                                        hashTable.Add(newCar);
                                        Console.WriteLine($"Машина {newCar.CarId} добавлена в хеш-таблицу.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Неверный выбор типа автомобиля.");
                                    }
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
                                            if (Console.ReadLine()?.ToLower() == "y")
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
                                    Console.WriteLine("Вывод хеш-таблицы:\n");
                                    for (int i = 0; i < hashTable.DefaultLength; i++)
                                    {
                                        Console.Write($"[{i}]: ");
                                        var current = hashTable.table[i];
                                        while (current != null)
                                        {
                                            Console.Write($"{current} -> ");
                                            current = current.Next;
                                        }
                                        Console.WriteLine("null");
                                    }
                                    break;

                                case "4":
                                    hashTable.Clear();
                                    Console.WriteLine("Хеш-таблица очищена.");
                                    break;

                                case "0":
                                    backToMainFromHash = true;
                                    break;

                                default:
                                    Console.WriteLine("Некорректный ввод!");
                                    break;
                            }
                        }
                        break;
                    
                    case "3":
                        Tree<Car> balancedTree = null;
                        Tree<Car> searchTree = null;

                        // Генерация объектов
                        List<Car> cars = new();
                        Random treeRand = new Random();
                        for (int i = 0; i < 15; i++)
                        {
                            Car car = treeRand.Next(3) switch
                            {
                                0 => new LightCar(),
                                1 => new BigCar(),
                                2 => new DeliveryCar(),
                                _ => new LightCar()
                            };
                            car.RandomInit();
                            cars.Add(car);
                        }
                        cars.Sort();

                        foreach (var car in cars)
                        {
                            Console.Write(car.Year + " ");
                        }

                        balancedTree = Tree<Car>.BuildBalancedTree(cars);

                        bool backToMainFromTree = false;
                        while (!backToMainFromTree)
                        {
                            Console.WriteLine("\n--- Меню: Операции с деревьями ---");
                            Console.WriteLine("1. Показать идеально сбалансированное дерево");
                            Console.WriteLine("2. Найти максимальный элемент (по году)");
                            Console.WriteLine("3. Преобразовать в дерево поиска");
                            Console.WriteLine("4. Показать дерево поиска");
                            Console.WriteLine("5. Удалить элемент по ключу (год)");
                            Console.WriteLine("6. Очистить дерево");
                            Console.WriteLine("0. Назад в главное меню");
                            Console.Write("Ваш выбор: ");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                    if (balancedTree != null)
                                        balancedTree.ShowTree(Car => $"{Car.Brand} (Id: {Car.CarId.Number}, Year: {Car.Year})");
                                    else
                                        Console.WriteLine("Дерево пусто.");
                                    break;

                                case "2":
                                    if (balancedTree != null)
                                    {
                                        var max = balancedTree.FindMaxElement(Car => Car.Year); // Передаём делегат по возрасту
                                        Console.WriteLine($"Максимальный элемент: {max}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Дерево пусто.");
                                    }
                                    break;

                                case "3":
                                    if (balancedTree != null)
                                    {
                                        searchTree = Tree<Car>.BuildBalancedSearchTree(balancedTree);
                                        Console.WriteLine("Преобразование в дерево поиска без повторов выполнено.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Идеально сбалансированное дерево отсутствует.");
                                    }
                                    break;

                                case "4":
                                    if (searchTree != null)
                                        searchTree.ShowTree(Car => $"{Car.Brand} (Id: {Car.CarId.Number}, Year: {Car.Year})");
                                    else
                                        Console.WriteLine("Дерево поиска пусто.");
                                    break;

                                case "5":
                                    Console.Write("Введите год машины для удаления: ");
                                    if (int.TryParse(Console.ReadLine(), out int year))
                                    {
                                        if (balancedTree != null)
                                        {
                                            balancedTree = Tree<Car>.RemoveByKey(balancedTree, year, car => car.Year);
                                            Console.WriteLine("Удаление выполнено.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Дерево поиска пусто.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Неверный ввод.");
                                    }
                                    break;

                                case "6":
                                    balancedTree = null;
                                    searchTree = null;
                                    Console.WriteLine("Деревья очищены.");
                                    break;

                                case "0":
                                    backToMainFromTree = true;
                                    break;

                                default:
                                    Console.WriteLine("Некорректный ввод!");
                                    break;
                            }
                        }
                        break;
                    
                    case "0":
                        exitProgram = true;
                        break;

                    default:
                        Console.WriteLine("Некорректный ввод!");
                        break;
                }
            }

            Console.WriteLine("Завершение работы программы.");
        }
    }
}
