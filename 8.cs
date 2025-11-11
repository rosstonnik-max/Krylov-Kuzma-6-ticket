using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncFileLoaderApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Асинхронный загрузчик файлов (ваша программа)");
                Console.WriteLine("2. Последовательная (синхронная) загрузка — для сравнения");
                Console.WriteLine("3. Загрузка с выбором файлов");
                Console.WriteLine("4. Информация о программе");
                Console.WriteLine("5. Тест производительности: async vs sync");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт (0–5): ");

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
                        await RunAsyncFileLoader();
                        break;
                    case 2:
                        await RunSyncFileLoader();
                        break;
                    case 3:
                        await RunCustomFileLoader();
                        break;
                    case 4:
                        ShowInfo();
                        break;
                    case 5:
                        await RunPerformanceTest();
                        break;
                    case 0:
                        Console.WriteLine("Выход. Спасибо за использование!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // === 1. Ваша оригинальная программа ===
        static async Task RunAsyncFileLoader()
        {
            Console.Clear();
            Console.WriteLine("--- Асинхронный загрузчик файлов ---");
            string[] files = { "photo.jpg", "document.docx", "archive.zip" };
            Console.WriteLine($"Запускаю загрузку {files.Length} файлов...");
            var stopwatch = Stopwatch.StartNew();

            var downloadTasks = new List<Task>();
            foreach (var file in files)
            {
                downloadTasks.Add(DownloadFileAsync(file));
            }

            Console.WriteLine("Все загрузки запущены, ожидаем завершения...\n");
            await Task.WhenAll(downloadTasks);
            stopwatch.Stop();

            Console.WriteLine("--- Результаты ---");
            foreach (var file in files)
            {
                Console.WriteLine($"✅ Файл '{file}' успешно загружен");
            }
            Console.WriteLine($"\n⏱ Общее время: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // === 2. Синхронная загрузка (для сравнения) ===
        static async Task RunSyncFileLoader()
        {
            Console.Clear();
            Console.WriteLine("--- Синхронная загрузка файлов ---");
            string[] files = { "photo.jpg", "document.docx", "archive.zip" };
            Console.WriteLine($"Запускаю последовательную загрузку {files.Length} файлов...");
            var stopwatch = Stopwatch.StartNew();

            foreach (var file in files)
            {
                await DownloadFileAsync(file); // но выполняется по очереди
            }

            stopwatch.Stop();
            Console.WriteLine($"\n⏱ Общее время: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("(Обратите внимание: дольше, чем в асинхронном режиме!)");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        // === 3. Загрузка с выбором файлов ===
        static async Task RunCustomFileLoader()
        {
            Console.Clear();
            Console.WriteLine("--- Загрузка с выбором файлов ---");
            Console.Write("Введите имена файлов через запятую (например: report.pdf, image.png): ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ничего не введено.");
                Console.ReadKey();
                return;
            }

            var files = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (files.Length == 0)
            {
                Console.WriteLine("Нет файлов для загрузки.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nЗапускаю загрузку {files.Length} файлов...\n");
            var tasks = new List<Task>();
            foreach (var file in files)
            {
                tasks.Add(DownloadFileAsync(file));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("\n✅ Все файлы загружены!");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        // === 4. Информация ===
        static void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine("ИНФОРМАЦИЯ О ПРОГРАММЕ");
            Console.WriteLine("=======================");
            Console.WriteLine("Эта программа демонстрирует:");
            Console.WriteLine("- Асинхронную загрузку файлов с помощью async/await");
            Console.WriteLine("- Разницу между параллельной и последовательной обработкой");
            Console.WriteLine("- Использование Task.WhenAll для одновременного выполнения задач");
            Console.WriteLine("- Имитацию реальной задержки (2–6 секунд на файл)");
            Console.WriteLine("\nИсходный код основан на ваших файлах: 1212.txt, 77.txt");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        // === 5. Тест производительности ===
        static async Task RunPerformanceTest()
        {
            Console.Clear();
            Console.WriteLine("--- Тест производительности: Async vs Sync ---");
            string[] files = { "file1.dat", "file2.dat", "file3.dat" };

            // Async
            var sw = Stopwatch.StartNew();
            await Task.WhenAll(files.Select(f => DownloadFileSilentAsync(f)));
            sw.Stop();
            long asyncTime = sw.ElapsedMilliseconds;

            // Sync
            sw.Restart();
            foreach (var f in files) await DownloadFileSilentAsync(f);
            sw.Stop();
            long syncTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"Результаты для {files.Length} файлов:");
            Console.WriteLine($"⏱ Асинхронно: {asyncTime} мс");
            Console.WriteLine($"⏱ Последовательно: {syncTime} мс");
            Console.WriteLine($"\n🚀 Ускорение: {(double)syncTime / asyncTime:F1}x");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        // Вспомогательные методы
        static async Task DownloadFileAsync(string fileName)
        {
            Console.WriteLine($"📥 Начинаю загрузку '{fileName}'...");
            await Task.Delay(new Random().Next(2000, 6000));
            Console.WriteLine($"✅ '{fileName}' загружен!");
        }

        static async Task DownloadFileSilentAsync(string fileName)
        {
            await Task.Delay(new Random().Next(2000, 6000));
        }
    }
}