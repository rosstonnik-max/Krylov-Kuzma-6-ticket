using System;

namespace UnifiedStudentAndGeometryApp
{
    // === 1. Класс Student с валидацией возраста ===
    public class StudentWithValidation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value < 6 || value > 100)
                {
                    Console.WriteLine("Ошибка: возраст должен быть в диапазоне от 6 до 100!");
                    return;
                }
                _age = value;
            }
        }
        public StudentWithValidation(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public string FullName => $"{FirstName} {LastName}";
    }

    // === 2. Структура Book и класс Student с любимой книгой ===
    public struct Book
    {
        public string Title;
        public string Author;
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }

    public class StudentWithBook
    {
        private static int _studentCount = 0;
        public static int StudentCount => _studentCount;
        public string Name { get; set; }
        public Book FavoriteBook { get; set; }
        public StudentWithBook(string name, Book book)
        {
            Name = name;
            FavoriteBook = book;
            _studentCount++;
        }
    }

    // === 3. Класс Rectangle ===
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double GetArea() => Width * Height;
        public double GetPerimeter() => 2 * (Width + Height);
    }

    // === Главный класс с меню ===
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Управление профилями студентов (с валидацией возраста)");
                Console.WriteLine("2. Система учета студентов с любимыми книгами");
                Console.WriteLine("3. Работа с прямоугольниками (площадь и периметр)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт (0–3): ");

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RunStudentValidationDemo();
                        break;
                    case 2:
                        RunStudentBookDemo();
                        break;
                    case 3:
                        RunRectangleDemo();
                        break;
                    case 0:
                        Console.WriteLine("Выход. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // === 1. Демонстрация студентов с валидацией возраста ===
        static void RunStudentValidationDemo()
        {
            Console.Clear();
            Console.WriteLine("--- Управление профилями студентов ---");
            var student1 = new StudentWithValidation("Иван", "Петров", 19);
            Console.WriteLine($"\nСтудент 1:\nПривет, меня зовут {student1.FullName}, мне {student1.Age} лет.");

            var student2 = new StudentWithValidation("Анна", "Сидорова", 20);
            Console.WriteLine($"\nСтудент 2:\nПривет, меня зовут {student2.FullName}, мне {student2.Age} лет.");

            Console.WriteLine("\n--- Попытка установить некорректный возраст ---");
            student1.Age = -5;
            Console.WriteLine($"Текущий возраст студента 1: {student1.Age}");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // === 2. Демонстрация студентов с книгами (класс vs структура) ===
        static void RunStudentBookDemo()
        {
            Console.Clear();
            Console.WriteLine("--- Система учета студентов с любимыми книгами ---");
            Console.WriteLine($"Начальное количество студентов: {StudentWithBook.StudentCount}");

            var book1 = new Book("Хоббит", "Дж. Р. Р. Толкин");
            var student1 = new StudentWithBook("Иван", book1);
            Console.WriteLine($"Создан студент {student1.Name}. Количество: {StudentWithBook.StudentCount}");

            var book2 = new Book("Война и мир", "Л. Н. Толстой");
            var student2 = new StudentWithBook("Анна", book2);
            Console.WriteLine($"Создан студент {student2.Name}. Количество: {StudentWithBook.StudentCount}");

            Console.WriteLine("\n--- Эксперимент с копированием ---");
            Console.WriteLine($"Оригинал: {student1.Name}, книга: \"{student1.FavoriteBook.Title}\"");

            var copiedStudent = student1;
            var copiedBook = student1.FavoriteBook;
            copiedStudent.Name = "Петр";
            copiedBook.Title = "Властелин Колец";

            Console.WriteLine("\n...После изменения копий...");
            Console.WriteLine($"Имя оригинального студента: {student1.Name}");
            Console.WriteLine($"Книга оригинального студента: {student1.FavoriteBook.Title}");
            Console.WriteLine("\n→ Имя изменилось (класс — ссылка), книга — нет (структура — значение).");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // === 3. Прямоугольники ===
        static void RunRectangleDemo()
        {
            Console.Clear();
            Console.WriteLine("--- Работа с прямоугольниками ---");

            var rect1 = new Rectangle { Width = 10, Height = 5 };
            Console.WriteLine("Прямоугольник 1:");
            Console.WriteLine($" - Ширина: {rect1.Width}");
            Console.WriteLine($" - Высота: {rect1.Height}");
            Console.WriteLine($" - Площадь: {rect1.GetArea()}");
            Console.WriteLine($" - Периметр: {rect1.GetPerimeter()}");

            var rect2 = new Rectangle { Width = 7.5, Height = 3.2 };
            Console.WriteLine("\nПрямоугольник 2:");
            Console.WriteLine($" - Ширина: {rect2.Width}");
            Console.WriteLine($" - Высота: {rect2.Height}");
            Console.WriteLine($" - Площадь: {rect2.GetArea()}");
            Console.WriteLine($" - Периметр: {rect2.GetPerimeter()}");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}