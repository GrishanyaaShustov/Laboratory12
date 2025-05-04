namespace Tests;
using Car;
using NUnit.Framework;

[TestFixture]
public class MyHashTableTests
{
    private MyHashTable<Car> hashTable;

    [SetUp]
    public void Setup()
    {
        hashTable = new MyHashTable<Car>();
    }

    [Test]
    public void Add_ShouldIncreaseCount_WhenElementAdded()
    {
        var car = new LightCar();
        car.RandomInit();
        hashTable.Add(car);
        Assert.AreEqual(1, hashTable.Count);
    }

    [Test]
    public void Add_ShouldHandleCollisions_WhenKeyHashesAreEqual()
    {
        // Пример для тестирования коллизий
        var car1 = new LightCar { Brand = "Toyota" };
        car1.RandomInit();
        var car2 = new LightCar { Brand = "Honda" };
        car2.RandomInit();

        hashTable.Add(car1);
        hashTable.Add(car2);

        // Проверяем, что оба объекта добавлены, несмотря на возможную коллизию
        Assert.AreEqual(2, hashTable.Count);
    }

    [Test]
    public void Remove_ShouldDecreaseCount_WhenElementRemoved()
    {
        var car = new LightCar();
        car.RandomInit();  // Инициализация случайными данными
        car.CarId = new IdNumber(1);  // Устанавливаем явно CarId
        hashTable.Add(car);
    
        var carId = car.CarId.Number;
        Assert.IsTrue(hashTable.Remove(carId));  // Проверяем, что элемент удален
        Assert.AreEqual(0, hashTable.Count);  // Проверяем, что количество элементов уменьшилось
    }

    [Test]
    public void Remove_ShouldReturnFalse_WhenElementNotFound()
    {
        var car = new LightCar();
        car.RandomInit();
        var carId = car.CarId.Number;

        hashTable.Add(car);

        // Удаляем несуществующий элемент
        Assert.IsFalse(hashTable.Remove(carId + 1)); 
    }

    [Test]
    public void Find_ShouldReturnElement_WhenFound()
    {
        var car = new LightCar();
        car.RandomInit();
        hashTable.Add(car);
        var carId = car.CarId.Number;

        var foundCar = hashTable.Find(carId);
        Assert.IsNotNull(foundCar);
        Assert.AreEqual(carId, foundCar.CarId.Number);
    }

    [Test]
    public void Find_ShouldReturnNull_WhenElementNotFound()
    {
        var car = new LightCar();
        car.RandomInit();
        var carId = car.CarId.Number;

        hashTable.Add(car);
        var notFoundCar = hashTable.Find(carId + 1); // Ищем другой ID

        Assert.IsNull(notFoundCar);
    }

    [Test]
    public void Clear_ShouldEmptyTable()
    {
        hashTable.Add(new LightCar { Brand = "Toyota", Year = 2020, Color = "Black", Price = 10000, Clearance = 150, CarId = new IdNumber(1) });
        hashTable.Clear();

        Assert.AreEqual(0, hashTable.Count);
    }

    [Test]
    public void Contains_ShouldReturnTrue_WhenElementExists()
    {
        var car = new LightCar();
        car.RandomInit();
        hashTable.Add(car);

        Assert.IsTrue(hashTable.Contains(car));
    }

    [Test]
    public void Contains_ShouldReturnFalse_WhenElementDoesNotExist()
    {
        var car = new LightCar();
        car.RandomInit();

        Assert.IsFalse(hashTable.Contains(car));
    }

    [Test]
    public void CopyTo_ShouldCopyElementsToArray()
    {
        var car1 = new LightCar();
        car1.RandomInit();
        var car2 = new BigCar();
        car2.RandomInit();

        // Добавляем машины в хеш-таблицу
        hashTable.Add(car1);
        hashTable.Add(car2);

        // Массив для копирования элементов
        var array = new Car[2];
        hashTable.CopyTo(array, 0);

        // Проверяем, что массив был правильно скопирован
        Assert.AreEqual(2, array.Length);
        Assert.AreEqual(car1, array[0]);  // Проверяем, что car1 на первой позиции
        Assert.AreEqual(car2, array[1]);  // Проверяем, что car2 на второй позиции
    }
    
    
    [Test]
    public void Remove_ShouldReturnTrue_WhenElementExists()
    {
        var car1 = new LightCar { Brand = "Toyota", CarId = new IdNumber(1) };
        car1.RandomInit();
        hashTable.Add(car1);

        bool result = hashTable.Remove(1); // Удаление по CarId

        Assert.IsTrue(result); // Элемент должен быть удалён
        Assert.AreEqual(0, hashTable.Count); // Количество элементов должно быть 0
    }
}
