using System;
using System.Threading.Tasks;

namespace UnifiedProgram
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Улучшенный калькулятор");
                Console.WriteLine("2. Рецепт: Простой яичный бутерброд");
                Console.WriteLine("3. Анализатор постов (на основе книги Толкина)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт меню (0–3): ");

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RunCalculator();
                        break;
                    case 2:
                        ShowRecipe();
                        break;
                    case 3:
                        await RunPostAnalyzer();
                        break;
                    case 0:
                        Console.WriteLine("Выход из программы. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу для возврата в меню...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // === 1. Улучшенный калькулятор ===
        static void RunCalculator()
        {
            Console.Clear();
            Console.WriteLine("--- Улучшенный калькулятор ---");
            Console.Write("Введите первое число: ");
            double firstNumber;
            while (!double.TryParse(Console.ReadLine(), out firstNumber))
            {
                Console.WriteLine("Ошибка: пожалуйста, введите корректное число.");
                Console.Write("Введите первое число: ");
            }

            Console.Write("Введите второе число: ");
            double secondNumber;
            while (!double.TryParse(Console.ReadLine(), out secondNumber))
            {
                Console.WriteLine("Ошибка: пожалуйста, введите корректное число.");
                Console.Write("Введите второе число: ");
            }

            Console.Write("Введите символ операции (+, -, *, /): ");
            char operation = Console.ReadKey().KeyChar;
            Console.WriteLine();

            double result;
            switch (operation)
            {
                case '+':
                    result = firstNumber + secondNumber;
                    Console.WriteLine($"Результат: {firstNumber} + {secondNumber} = {result}");
                    break;
                case '-':
                    result = firstNumber - secondNumber;
                    Console.WriteLine($"Результат: {firstNumber} - {secondNumber} = {result}");
                    break;
                case '*':
                    result = firstNumber * secondNumber;
                    Console.WriteLine($"Результат: {firstNumber} * {secondNumber} = {result}");
                    break;
                case '/':
                    if (secondNumber == 0)
                    {
                        Console.WriteLine("Ошибка: Деление на ноль невозможно.");
                    }
                    else
                    {
                        result = firstNumber / secondNumber;
                        Console.WriteLine($"Результат: {firstNumber} / {secondNumber} = {result}");
                    }
                    break;
                default:
                    Console.WriteLine("Ошибка: Неверный символ операции. Пожалуйста, используйте +, -, *, /.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // === 2. Рецепт бутерброда ===
        static void ShowRecipe()
        {
            Console.Clear();
            Console.WriteLine("Простой Яичный Бутерброд");
            Console.WriteLine();
            Console.WriteLine("Ингредиенты:");
            Console.WriteLine("- 2 яйца");
            Console.WriteLine("- 2 ломтика хлеба");
            Console.WriteLine("- 1 столовая ложка майонеза");
            Console.WriteLine("- Соль и перец по вкусу");
            Console.WriteLine("- Щепотка зелени (по желанию)");
            Console.WriteLine();
            Console.WriteLine("Приготовление:");
            Console.WriteLine("1. Отварите яйца вкрутую (около 10 минут после закипания).");
            Console.WriteLine("2. Остудите яйца, очистите от скорлупы и разомните вилкой в миске.");
            Console.WriteLine("3. Добавьте майонез, соль, перец и зелень (если используете) к яйцам. Тщательно перемешайте.");
            Console.WriteLine("4. Поджарьте ломтики хлеба в тостере или на сухой сковороде до золотистой корочки.");
            Console.WriteLine("5. Равномерно распределите яичную смесь на одном ломтике хлеба.");
            Console.WriteLine("6. Накройте вторым ломтиком хлеба.");
            Console.WriteLine("7. Бутерброд готов! Приятного аппетита!");
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // === 3. Анализатор постов ===
        static async Task RunPostAnalyzer()
        {
            Console.Clear();
            Console.WriteLine("Загрузка списка постов...");
            const string BookUrl = "https://lib.ru/TOLKIEN/hran_1.txt";
            string fullText;

            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "CSharp-App/1.0");
                fullText = await httpClient.GetStringAsync(BookUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Данные успешно загружены!");
            Console.WriteLine("--- Анализ постов ---");

            var lines = fullText.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
            var paragraphs = new List<string?>();
            var currentParagraph = new List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentParagraph.Count > 0)
                    {
                        paragraphs.Add(string.Join(" ", currentParagraph).Trim());
                        currentParagraph.Clear();
                    }
                }
                else
                {
                    if (line.StartsWith("http") ||
                        line.Contains("Lib.ru") ||
                        line.Contains("Библиотека") ||
                        line.Contains("Оглавление") ||
                        Regex.IsMatch(line, @"^\s*\d+\s*$"))
                        continue;
                    currentParagraph.Add(line.Trim());
                }
            }

            if (currentParagraph.Count > 0)
            {
                paragraphs.Add(string.Join(" ", currentParagraph).Trim());
            }

            int maxPosts = Math.Min(paragraphs.Count, 50);
            for (int i = 0; i < maxPosts; i++)
            {
                string? bodyText = string.IsNullOrWhiteSpace(paragraphs[i]) ? null : paragraphs[i];
                string displayTitle = "<пусто>";
                string displayBody = bodyText ?? "<текст отсутствует>";
                Console.WriteLine($"Пост №{i + 1} (Автор ID: 1)");
                Console.WriteLine($"Заголовок: {displayTitle}");
                Console.WriteLine($"Текст: {displayBody}");
                Console.WriteLine();
            }

            Console.WriteLine($"Всего обработано постов (абзацев): {maxPosts}");
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}