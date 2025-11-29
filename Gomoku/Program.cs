using System;
using System.Reflection.PortableExecutable;

class GameField
{
    private string[,] field = new string[15, 15];
    private string fieldLine = new string('—', 61);

    public void InitializeField() // Создание игрового поля
    {
        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 15; col++)
            {
                field[row, col] = ".";
            }
        }
    }

    public void DisplayField() // вывод игрового поля в консоль
    {
        Console.WriteLine($"\t{fieldLine}");
        for (int row = 0; row < 15; row++)
        {
            Console.Write('\t');
            Console.Write("| ");
            for (int col = 0; col < 15; col++)
            {
                Console.Write($"{field[row, col]} | ");
            }
            Console.WriteLine();
            Console.WriteLine($"\t{fieldLine}");
        }
    }

    public void SetValue(int row, int col, string value) // Изменяем определенную ячейку в поле по координатам
    {
        if (row >= 0 && row < 15 && col >= 0 && col < 15)
        {
            field[row, col] = value;
        }
        else
        {
            Console.WriteLine("Некорректные данные!");
        }
    }

    public bool CheckWin(string value)
    {
        if (value == ".")
        {
            return false;
        }
        for (int row = 0; row < 15; row++) // Горизонтальная проверка
        {
            for (int col = 0; col <= 10; col++)
            {
                bool flag = true;
                for (int k = 0; k < 5; k++)
                {
                    if (field[row, col + k] != value)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return true;
                }
            }
        }

        for (int row = 0; row <= 10; row++) // Вертикальная проверка
        {
            for (int col = 0; col < 15; col++)
            {
                bool flag = true;
                for (int k = 0; k < 5; k++)
                {
                    if (field[row + k, col] != value)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return true;
                }
            }
        }


        for (int row = 0; row <= 10; row++) // Диагональная (вниз-вправо) проверка
        {
            for (int col = 0; col <= 10; col++)
            {
                bool flag = true;
                for (int k = 0; k < 5; k++)
                {
                    if (field[row+k, col+k] != value)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return true;
                }
            }
        }

        for (int row = 4; row < 15; row++) // Диагональная (вверх-вправо) проверка
        {
            for (int col = 0; col <= 10; col++)
            {
                bool flag = true;
                for (int k = 0; k < 5; k++)
                {
                    if (field[row-k, col+k] != value)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        GameField field = new GameField();
        field.InitializeField();

        field.DisplayField();

        while (true)
        {
            Console.Write("Type cordinates: ");
            string input = Console.ReadLine();
            string[] inputMasssiv = input.Split(" ");
            int row = int.Parse(inputMasssiv[0]);
            int col = int.Parse(inputMasssiv[1]);
            string value = inputMasssiv[2];
            field.SetValue(row, col, value);
            Console.Clear();
            Console.Write("\x1b[3J");
            field.DisplayField();
            if (field.CheckWin(value))
            {
                Console.Clear();
                Console.Write("\x1b[3J"); // баг NET8
                field.DisplayField();
                Console.WriteLine($"Win player: {value}!");
                break;
            }
            
        }

    }
}

