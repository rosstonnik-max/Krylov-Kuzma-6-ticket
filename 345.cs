using System;

1.Создай репозиторий на GitHub
Название: Билет - XX - Фамилия Имя(например: Билет - 05 - Иванов Алексей)
Сделай репозиторий Public
Не добавляй README, .gitignore, license
2. Настрой локальную папку
git init
git remote add origin https://github.com/твой-логин/Название-репозитория.git
3.Сохраняй решения по мере готовности
git add .
git commit -m "Описание изменений"
git push -u origin main
4. Сдай работу
Скопируй ссылку на репозиторий из адресной строки
Вставь в Google Форму
Структура файлов:
README.md - ответы на теорию
.cs файлы - код практических заданий
Делай коммиты после решения каждого задания!

**Теоретическая часть**  
1. **Garbage Collector (GC)** — механизм автоматического управления памятью в .NET. Основная задача: освобождать память, занятую объектами, на которые больше нет активных ссылок, предотвращая утечки памяти.  
2. **Преобразование double в int**:  
   ```csharp
   double d = 10.7;
int i = (int)d; // i = 10 (дробная часть отбрасывается)
   ```  
3. * *Проверка диапазона * *:  
   ```csharp
   if (x >= 10 && x <= 20) { /* ... */ }
   ```  
4. * *Досрочное прерывание цикла**: Оператор `break`.  
5. **Область видимости (scope)** — участок кода, где переменная доступна (например, метод, блок `{}`). Переменные недоступны за пределами своей области.  
6. **Массив vs List<T>**: Массив имеет **фиксированный размер** после создания. `List<T>` динамически изменяет размер при добавлении/удалении элементов.  
7. **Интерфейс vs абстрактный класс**:  
   -**Интерфейс * * — контракт(только сигнатуры методов, свойства, события; реализация отсутствует). Может быть реализован множественно.  
   - **Абстрактный класс** — может содержать реализацию методов, поля, конструкторы. Поддерживает единственное наследование.  
8. **NuGet** — менеджер пакетов для подключения сторонних библиотек и инструментов в проекты .NET.  
9. **Асинхронный метод**:  
   -Объявляется с модификатором `async`.  
   - Использует `await` для ожидания завершения задачи без блокировки потока.  
   Пример: `public async Task<string> MyMethodAsync() { ... }`.  
10. * *Методы File * *:  
    -Чтение: `ReadAllText`, `ReadAllLines`, `ReadAllBytes`.  
    - Запись: `WriteAllText`, `WriteAllLines`, `AppendAllText`.  

---

**Практическая часть**  
11. **Асинхронное чтение файла**:  
```csharp
public async Task<string> ReadFileAsync(string path)
{
    return await File.ReadAllTextAsync(path);
}
```

12. * *Полиморфизм с интерфейсом**:  
```csharp
public interface IShape
{
    double CalculateArea();
}

public class Circle : IShape
{
    public double Radius { get; set; }
    public double CalculateArea() => Math.PI * Radius * Radius;
}

public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double CalculateArea() => Width * Height;
}

// Демонстрация полиморфизма:
var shapes = new List<IShape>
{
    new Circle { Radius = 5 },
    new Rectangle { Width = 4, Height = 6 }
};

foreach (var shape in shapes)
{
    Console.WriteLine($"Area: {shape.CalculateArea()}");
}
```  
**Вывод * *:  
```
Area: 78.53981633974483
Area: 24
```  

Ключевые моменты:  
-Интерфейс `IShape` определяет контракт для всех фигур.  
- Классы `Circle` и `Rectangle` реализуют метод `CalculateArea()` по-своему.  
- Список `List<IShape>` хранит разные фигуры, демонстрируя полиморфизм при вызове методов.
