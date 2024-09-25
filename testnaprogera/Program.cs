using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Ввод размеров матрицы
        Console.Write("Введите количество строк (n): ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Введите количество столбцов (m): ");
        int m = int.Parse(Console.ReadLine());

        // Проверка на корректность введённых данных
        if (n <= 1 || m <= 1)
        {
            Console.WriteLine("Размеры матрицы должны быть больше 1.");
            return;
        }

        // Инициализация матрицы для препятствий
        bool[,] obstacles = new bool[n, m];

        // Ввод препятствий
        Console.WriteLine("Введите количество препятствий: ");
        int obstacleCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < obstacleCount; i++)
        {
            Console.WriteLine($"Введите координаты препятствия {i + 1} (строка и столбец):");
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());

            if (row >= 0 && row < n && col >= 0 && col < m)
                obstacles[row, col] = true;
            else
                Console.WriteLine("Некорректные координаты препятствия.");
        }

        // Вычисление количества путей
        long[,] dp = new long[n, m];
        dp[0, 0] = obstacles[0, 0] ? 0 : 1;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (obstacles[i, j])
                {
                    dp[i, j] = 0;  // Если препятствие, то путей нет
                }
                else
                {
                    if (i > 0) dp[i, j] += dp[i - 1, j];  // Путь сверху
                    if (j > 0) dp[i, j] += dp[i, j - 1];  // Путь слева
                }
            }
        }

        Console.WriteLine($"Количество возможных путей: {dp[n - 1, m - 1]}");

        // Вывод всех путей с цветом
        Console.WriteLine("Все возможные пути (синим — путь, красным — препятствия):");
        List<string> allPaths = new List<string>();
        FindAllPaths(0, 0, n, m, "", obstacles, allPaths);

        foreach (var path in allPaths)
        {
            Console.WriteLine(path);
        }

        // Отрисовка матрицы с цветами
        DrawMatrixWithColors(n, m, obstacles, dp);
    }

    // Метод для рекурсивного поиска всех путей
    static void FindAllPaths(int i, int j, int n, int m, string path, bool[,] obstacles, List<string> allPaths)
    {
        if (i == n - 1 && j == m - 1)
        {
            allPaths.Add(path);
            return;
        }

        if (i < n - 1 && !obstacles[i + 1, j])  // Путь вниз
        {
            FindAllPaths(i + 1, j, n, m, path + "Вниз ", obstacles, allPaths);
        }

        if (j < m - 1 && !obstacles[i, j + 1])  // Путь вправо
        {
            FindAllPaths(i, j + 1, n, m, path + "Вправо ", obstacles, allPaths);
        }
    }

    // Метод для отрисовки матрицы с цветами
    static void DrawMatrixWithColors(int n, int m, bool[,] obstacles, long[,] dp)
    {
        Console.WriteLine("Отрисовка матрицы:");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (obstacles[i, j])
                {
                    // Отрисовка препятствий красным цветом
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" X ");
                }
                else if (dp[i, j] > 0)
                {
                    // Отрисовка пути синим цветом
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" O ");
                }
                else
                {
                    // Отрисовка пустых клеток
                    Console.ResetColor();
                    Console.Write(" . ");
                }
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}

