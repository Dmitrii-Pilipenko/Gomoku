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
                field[row, col] = "O";
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

    //public bool CheckWin()
    //{
    //    bool flag = false;
    //    for (int row = 0; row < 10; row++)
    //    {
    //        for (int col = 0; col < 15; col++)
    //        {
    //            if (field[row, col] == field[row + 1, col] && field[row + 1, col] == field[row + 2, col] && field[row + 3, col] == field[row + 4, col] && field[row + 4, col] == field[row + 5, col])
    //            {
    //                flag = true;
    //                break;
    //            }
    //        }
    //        if (flag)
    //        {
    //            break;
    //        }
    //    }

    //    for (int row = 0; row < 15; row++)
    //    {
    //        for (int col = 0; col < 10; col++)
    //        {
    //            if (field[row, col] == field[row, col + 1] && field[row, col + 1] == field[row, col + 2] && field[row, col + 2] == field[row, col + 3] && field[row, col + 3] == field[row, col + 4] && field[row, col + 4] == field[row, col + 5])
    //            {
    //                flag = false;
    //                break;
    //            }
    //        }
    //        if (flag)
    //        {
    //            break;
    //        }
    //    }



    //    return flag;
    //}
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
            int y = int.Parse(inputMasssiv[0]);
            int x = int.Parse(inputMasssiv[1]);
            string value = inputMasssiv[2];
            Console.Clear();
            Console.Write("\x1b[3J"); // баг NET8
            field.SetValue(x, y, value);
            field.DisplayField();
        }

    }
}

