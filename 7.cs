using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnifiedApp
{
    // === 1. Класс Student для LINQ-анализа ===
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double AverageScore { get; set; }
        public override string ToString() =>
            $"{Name}, возраст: {Age}, средний балл: {AverageScore:F1}";
    }

    // === Главный класс с меню ===
    class Program
    {
        // Для менеджера задач
        private static List<string> tasks = new List<string>();
        private const string TaskFileName = "tasks.txt";

        static void Main()
        {
            LoadTasksFromFile(); // Загружаем задачи один раз при старте

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Универсальный калькулятор (с делегатами)");
                Console.WriteLine("2. Менеджер задач (сохранение в файл)");
                Console.WriteLine("3. Анализ студентов с LINQ");
                Console.WriteLine("4. Выбор календаря из 8 культур");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт (0–4): ");

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
                        RunUniversalCalculator();
                        break;
                    case 2:
                        RunTaskManager();
                        break;
                    case 3:
                        RunStudentAnalysis();
                        break;
                    case 4:
                        RunCalendarSelector();
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

        // === 1. Универсальный калькулятор с делегатами ===
        static void RunUniversalCalculator()
        {
            Console.Clear();
            Console.WriteLine("--- Универсальный калькулятор ---");
            Console.Write("Введите первое число: ");
            if (!double.TryParse(Console.ReadLine(), out var a))
            {
                Console.WriteLine("Ошибка: неверный формат числа.");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите второе число: ");
            if (!double.TryParse(Console.ReadLine(), out var b))
            {
                Console.WriteLine("Ошибка: неверный формат числа.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n--- Результаты вычислений ---");
            Console.WriteLine($"Сложение: {PerformOperation(a, b, (x, y) => x + y)}");
            Console.WriteLine($"Вычитание: {PerformOperation(a, b, (x, y) => x - y)}");
            Console.WriteLine($"Умножение: {PerformOperation(a, b, (x, y) => x * y)}");

            if (b != 0)
                Console.WriteLine($"Деление: {PerformOperation(a, b, (x, y) => x / y)}");
            else
                Console.WriteLine("Деление невозможно: деление на ноль!");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static double PerformOperation(double x, double y, Func<double, double, double> op) => op(x, y);

        // === 2. Менеджер задач ===
        static void RunTaskManager()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ShowTaskMenu();
                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ShowTasks();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowTaskMenu()
        {
            Console.WriteLine("--- Менеджер задач ---");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Посмотреть задачи");
            Console.WriteLine("3. Вернуться в главное меню");
            Console.Write("Выберите действие: ");
        }

        static void LoadTasksFromFile()
        {
            try
            {
                if (File.Exists(TaskFileName))
                    tasks = File.ReadAllLines(TaskFileName).ToList();
                else
                    tasks = new List<string>();
            }
            catch { tasks = new List<string>(); }
        }

        static void SaveTasksToFile()
        {
            try
            {
                File.WriteAllLines(TaskFileName, tasks);
            }
            catch { }
        }

        static void AddTask()
        {
            Console.Write("Введите новую задачу: ");
            string task = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(task))
            {
                tasks.Add(task);
                SaveTasksToFile();
                Console.WriteLine("Задача добавлена!");
            }
            else
            {
                Console.WriteLine("Задача не может быть пустой!");
            }
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ShowTasks()
        {
            Console.WriteLine("--- Текущие задачи ---");
            if (tasks.Count == 0)
                Console.WriteLine("Список пуст.");
            else
                for (int i = 0; i < tasks.Count; i++)
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        // === 3. Анализ студентов с LINQ ===
        static void RunStudentAnalysis()
        {
            Console.Clear();
            var students = new List<Student>
            {
                new Student { Name = "Петров Иван", Age = 20, AverageScore = 85.5 },
                new Student { Name = "Сидорова Анна", Age = 23, AverageScore = 78.1 },
                new Student { Name = "Кузнецов Олег", Age = 19, AverageScore = 74.9 },
                new Student { Name = "Васильева Мария", Age = 26, AverageScore = 82.3 },
                new Student { Name = "Смирнов Алексей", Age = 22, AverageScore = 95.2 }
            };

            Console.WriteLine("--- Студенты-хорошисты (75–90 баллов) ---");
            var goodStudents = students.Where(s => s.AverageScore >= 75 && s.AverageScore <= 90).ToList();
            foreach (var s in goodStudents)
                Console.WriteLine($"{s.Name} — {s.AverageScore:F1}");

            Console.WriteLine("\n--- Полные имена студентов ---");
            var names = students.Select(s => s.Name).ToList();
            foreach (var name in names)
                Console.WriteLine(name);

            Console.WriteLine("\n--- Студенты по возрасту (по возрастанию) ---");
            var byAge = students.OrderBy(s => s.Age).ToList();
            foreach (var s in byAge)
                Console.WriteLine(s.ToString());

            Console.WriteLine("\n--- Лучшие молодые студенты (<25 лет) ---");
            var topYoung = students
                .Where(s => s.Age < 25)
                .OrderByDescending(s => s.AverageScore)
                .ToList();
            foreach (var s in topYoung)
                Console.WriteLine($"{s.Name} — {s.AverageScore:F1}");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // === 4. Выбор календаря ===
        static void RunCalendarSelector()
        {
            Console.Clear();
            string[] modernRussian = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            string[] slavic = { "Сечень", "Лютый", "Березозол", "Цветень", "Травень", "Червень", "Липец", "Серпень", "Вересень", "Листопад", "Грудень", "Студень" };
            string[] tolkien = { "Narwain", "Nínui", "Gwaeron", "Gwirith", "Lothron", "Nórui", "Cerveth", "Urui", "Ivanneth", "Narbeleth", "Hithui", "Girithron" };
            string[] japanese = { "Муцуки", "Кусацуки", "Яёи", "Удзуки", "Суцуки", "Минозуки", "Фумизуки", "Хазуки", "Нагасуцуки", "Каназуки", "Симоцуки", "Симатуки" };
            string[] egyptian = { "Тибот", "Паофи", "Хатор", "Киахк", "Тиби", "Махтер", "Фаменот", "Фармути", "Пашонс", "Пайни", "Эпифи", "Мешир" };
            string[] chinese = { "Тайчжэн", "Эрьюэ", "Саньюэ", "Сичжэнь", "Уэ", "Лиюэ", "Цицзюэ", "Байлу", "Шучжунь", "Цзяою", "Дунши", "Лаочжи" };
            string[] dnd = { "Hammer", "Alturiak", "Chairwind", "Tarvess", "Mirtul", "Klondlant", "Elembur", "Flamefurl", "Eltrugar", "Marpennot", "Uktar", "Nightal" };
            string[] creative = { "Элона", "Келона", "Селона", "Тэлона", "Нэлона", "Хелона", "Мэлона", "Йэлона", "Релона", "Велон", "Шелон", "Телонис" };

            string[]? selected = null;
            bool chosen = false;

            while (!chosen)
            {
                Console.WriteLine("Выберите календарь:");
                Console.WriteLine("1. Современный русский");
                Console.WriteLine("2. Старославянский");
                Console.WriteLine("3. Средиземье (Толкин)");
                Console.WriteLine("4. Японский традиционный");
                Console.WriteLine("5. Древнеегипетский");
                Console.WriteLine("6. Китайский лунный");
                Console.WriteLine("7. Forgotten Realms (D&D)");
                Console.WriteLine("8. Творческая альтернатива");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1": selected = modernRussian; chosen = true; break;
                    case "2": selected = slavic; chosen = true; break;
                    case "3": selected = tolkien; chosen = true; break;
                    case "4": selected = japanese; chosen = true; break;
                    case "5": selected = egyptian; chosen = true; break;
                    case "6": selected = chinese; chosen = true; break;
                    case "7": selected = dnd; chosen = true; break;
                    case "8": selected = creative; chosen = true; break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.\n");
                        continue;
                }
            }

            while (true)
            {
                Console.Write($"\nВведите номер месяца (1–12): ");
                if (int.TryParse(Console.ReadLine(), out int num) && num >= 1 && num <= 12)
                {
                    Console.WriteLine($"Месяц: {selected[num - 1]}");
                    break;
                }
                Console.WriteLine("Ошибка: введите число от 1 до 12.");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}