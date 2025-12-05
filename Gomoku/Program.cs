using System;
using System.Reflection.PortableExecutable;
using System.Text;

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
                field[row, col] = " ";
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
            if (field[row, col] == " ")
            {
                field[row, col] = value;
            }
            else
            {
                Console.WriteLine("No");
            }
        }
        else
        {
            Console.WriteLine("Некорректные данные!");
        }
    }

    public bool CheckNullCell()
    {
        bool result = false;
        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 15; col++)
            {
                if (field[row, col] != null)
                {
                    result = true;
                }
            }
        }
        return result;
    }

    public bool CheckWin(string value)
    {
        if (value == " ")
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

class Player
{
    private string _name;
    private int _queue;
    private string _figure;
    private int _score = 0;
    
    public Player(string name, int queue)
    {
        Console.OutputEncoding = Encoding.UTF8;
        _name = name;
        _queue = queue;
    }

    public int GetQueue()
    {
        return _queue;
    }

    public void SetFigure(string figure)
    {
        _figure = figure;
    }

    public string GetFigure()
    {
        return _figure;
    }
    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int score)
    {
        _score = score;
    }

    public void Name(string name)
    {
        Console.OutputEncoding = Encoding.UTF8;
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        do
        {
            bool StartChecker = false;

            do
            {
                Console.WriteLine("1: Начать игру");
                Console.WriteLine("2: Правила игры");
                Console.WriteLine("0: Выйти из игры");
                Console.Write("Введите число: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number))
                {
                    if (number >= 0 && number <= 2)
                    {
                        switch (number)
                        {
                            case 1: StartChecker = true; break;
                            case 2:
                                Console.WriteLine("Игра ведётся на квадратном поле («доске»), расчерченном вертикальными и горизонтальными линиями. Пересечения линий называются «пунктами». Наиболее распространённым является поле размером 15×15 линий.");
                                Console.WriteLine("Играют две стороны — «чёрные» и «белые». Каждая сторона использует фишки («камни») своего цвета.");
                                Console.WriteLine("Каждым ходом игрок выставляет камень своего цвета в один из свободных пунктов доски. Первый ход делают чёрные в центральный пункт доски. Далее ходы делаются по очереди.");
                                Console.WriteLine("Цель игры — первым построить камнями своего цвета непрерывный ряд из пяти камней в горизонтальном, вертикальном или диагональном направлении.");
                                Console.WriteLine("Если доска заполнена и ни один из игроков не построил ряд из пяти камней, может быть объявлена ничья.");
                                break;
                            case 0: Environment.Exit(0); break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверное число!");
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели неверное число!");
                }
            } while (StartChecker != true);

            Console.Clear();
            Console.Write("\x1b[3J"); // баг NET8


            Console.Write("Введите имя первого игрока: ");
            string playerName1 = Console.ReadLine();
            Player player1 = new Player(playerName1, 1);

            Console.Clear();
            Console.Write("\x1b[3J"); // баг NET8

            Console.Write("Введите имя второго игрока: ");
            string playerName2 = Console.ReadLine();
            Player player2 = new Player(playerName2, 2);

            Console.Clear();
            Console.Write("\x1b[3J"); // баг NET8

            Random random = new Random();
            int randomNumber = random.Next(1, 3);

            GameField field = new GameField();
            field.InitializeField();

            field.DisplayField();

            while (true)
            {
                if (field.CheckNullCell() == false)
                {
                    Console.WriteLine("Ничья!");
                    break;
                }
                if (player1.GetQueue() == randomNumber)
                {
                    player1.SetFigure("X");
                    player2.SetFigure("O");
                    bool checkExit = false;

                    bool checkMassiv = false;

                    do
                    {
                        Console.WriteLine("0: Выйти в меню");
                        Console.Write($"{player1.GetName()} введи координаты (Первое число - строка, второе - столбец): ");

                        string input = Console.ReadLine();
                        string[] inputMasssiv = input.Split(" ");
                        if (input == "0")
                        {
                            checkExit = true;
                            break;
                        }
                        if (inputMasssiv.Length == 2)
                        {
                            int row = int.Parse(inputMasssiv[0]);
                            int col = int.Parse(inputMasssiv[1]);
                            checkMassiv = true;
                            field.SetValue(row, col, player1.GetFigure());
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели неправильно!");
                        }
                    } while (checkMassiv != true);

                    if (checkExit == true)
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8
                        break;
                    }

                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    field.DisplayField();

                    bool checkExit1 = false;

                    bool checkMassiv1 = false;

                    do
                    {
                        Console.WriteLine("0: Выйти в меню");
                        Console.Write($"{player2.GetName()} введи координаты (Первое число - строка, второе - столбец): ");

                        string input = Console.ReadLine();
                        string[] inputMasssiv = input.Split(" ");
                        if (input == "0")
                        {
                            checkExit1 = true;
                            break;
                        }
                        if (inputMasssiv.Length == 2)
                        {
                            int row = int.Parse(inputMasssiv[0]);
                            int col = int.Parse(inputMasssiv[1]);
                            checkMassiv1 = true;
                            field.SetValue(row, col, player2.GetFigure());
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели неправильно!");
                        }
                    } while (checkMassiv1 != true);

                    if (checkExit1 == true)
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8
                        break;
                    }

                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    field.DisplayField();

                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    if (field.CheckWin(player1.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        Console.WriteLine($"Победил игрок: {player1.GetName()}!");
                        break;
                    }

                    if (field.CheckWin(player2.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        Console.WriteLine($"Победил игрок: {player2.GetName()}!");
                        break;
                    }
                }
                else
                {
                    player2.SetFigure("X");
                    player1.SetFigure("O");
                    Console.WriteLine("0: Выйти в меню");
                    bool checkExit = false;

                    bool checkMassiv = false;

                    do
                    {
                        Console.WriteLine("0: Выйти в меню");
                        Console.Write($"{player2.GetName()} введи координаты (Первое число - строка, второе - столбец): ");

                        string input = Console.ReadLine();
                        string[] inputMasssiv = input.Split(" ");
                        if (input == "0")
                        {
                            checkExit = true;
                            break;
                        }
                        if (inputMasssiv.Length == 2)
                        {
                            int row = int.Parse(inputMasssiv[0]);
                            int col = int.Parse(inputMasssiv[1]);
                            checkMassiv = true;
                            field.SetValue(row, col, player2.GetFigure());
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели неправильно!");
                        }
                    } while (checkMassiv != true);

                    if (checkExit == true)
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8
                        break;
                    }

                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    field.DisplayField();

                    bool checkExit1 = false;

                    bool checkMassiv1 = false;

                    do
                    {
                        Console.WriteLine("0: Выйти в меню");
                        Console.Write($"{player1.GetName()} введи координаты (Первое число - строка, второе - столбец): ");

                        string input = Console.ReadLine();
                        string[] inputMasssiv = input.Split(" ");
                        if (input == "0")
                        {
                            checkExit1 = true;
                            break;
                        }
                        if (inputMasssiv.Length == 2)
                        {
                            int row = int.Parse(inputMasssiv[0]);
                            int col = int.Parse(inputMasssiv[1]);
                            checkMassiv1 = true;
                            field.SetValue(row, col, player1.GetFigure());
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели неправильно!");
                        }
                    } while (checkMassiv1 != true);

                    if (checkExit1 == true)
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8
                        break;
                    }

                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    field.DisplayField();

                    if (field.CheckWin(player1.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        Console.WriteLine($"Победил игрок: {player1.GetName()}!");
                        break;
                    }

                    if (field.CheckWin(player2.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        Console.WriteLine($"Победил игрок: {player1.GetName()}!");
                        break;
                    }


                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    field.DisplayField();


                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8
                    if (field.CheckWin(player2.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        Console.WriteLine($"Победил игрок: {player2.GetName()}!");
                        break;
                    }


                    if (field.CheckWin(player2.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        Console.WriteLine($"Победил игрок: {player2.GetName()}!");
                        break;
                    }
                }
                
            }
            
        } while (true);
        

    }
}

