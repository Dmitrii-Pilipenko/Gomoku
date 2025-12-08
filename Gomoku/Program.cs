using System;
using System.Reflection.PortableExecutable;
using System.Text;


class LeaderBoard
{
    private Dictionary<string, int> _scores = new Dictionary<string, int>();
    private const string FileName = "leaderboard.txt";

    public void Load()
    {
        if (!File.Exists(FileName)) // если файла нет то мы выходим
        {
            return;
        }
        foreach(var line in File.ReadAllLines(FileName)) // чтение файла построчно
        {
            var parts = line.Split(";");
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
            {
                _scores[parts[0]] = score;
            }
        }
    }

    public void Save()
    {
        var lines = _scores.Select(kvp => $"{kvp.Key};{kvp.Value}");
        File.WriteAllLines(FileName, lines);
    }

    public void AddWin(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return;
        }
        if (!_scores.ContainsKey(playerName))
        {
            _scores[playerName] = 0;
        }

        _scores[playerName]++;
    }

    public void Print()
    {
        Console.WriteLine("======Таблица лидеров=====");
        if (_scores.Count == 0)
        {
            Console.WriteLine("Победителей нет");
            return;
        }

        foreach(var kvp in _scores.OrderByDescending(k => k.Value))
        {
            Console.WriteLine($"{kvp.Key} - {kvp.Value}");
        }
    }
}

class GameField // класс для игрового поля
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
        int intRow = 1;
        int intCol = 1;
        Console.Write('\t' + "  ");
        for (int i = 0; i < 15; i++)
        {
            if(intRow < 10)
            {
                Console.Write(intRow++ + "   ");
            }
            else
            {
                Console.Write(intRow++ + "  ");
            }
        }
        Console.WriteLine();
        Console.WriteLine($"\t{fieldLine}");
        for (int row = 0; row < 15; row++)
        {
            
            Console.Write(intCol++);
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
        if (row - 1 >= 0 && row - 1 < 15 && col - 1 >= 0 && col - 1 < 15)
        {
            field[row - 1, col - 1] = value;
        }
        else
        {
            Console.WriteLine("Некорректные данные!");
        }
    }

    public bool CheckValue(int row, int col) // Возвращает true если ячейка не пустая
    {
        if (field[row - 1, col - 1] != " ")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckNullCell() // Возвращает true если все поле заполнено
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

    public bool CheckWin(string value) // возвращает true если 5 одинаковых симовола в ряду
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

class Player // класс игрока
{
    private string _name;
    private int _queue;
    private string _figure;
    
    public Player(string name, int queue)
    {
        _name = name;
        _queue = queue;
    }

    public int GetQueue() // возвращает место в очереди
    {
        return _queue;
    }

    public void SetFigure(string figure) // устнавливает значение
    {
        _figure = figure;
    }

    public string GetFigure() // возвращает символ
    {
        return _figure;
    }

    public void Name(string name) // меняет имя игрока
    {
        _name = name;
    }

    public string GetName() // Возвращает имя игрока
    {
        return _name;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        LeaderBoard leaderboard = new LeaderBoard();
        leaderboard.Load();
        do // глобальный цикл для всей игры, чтобы при окончании игры выводилось меню
        {
            bool StartChecker = false;

            do // цикл для начала игры
            {
                Console.WriteLine("1: Начать игру");
                Console.WriteLine("2: Правила игры");
                Console.WriteLine("3: Таблица лидеров");
                Console.WriteLine("0: Выйти из игры");
                Console.Write("Введите число: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number)) // проверка числа на цилочисленное
                {
                    if (number >= 0 && number <= 3)
                    {
                        switch (number) // правила игры
                        {
                            case 1: StartChecker = true; break;
                            case 2:
                                Console.Clear(); Console.Write("\x1b[3J");
                                Console.WriteLine("Игра ведётся на квадратном поле («доске»), расчерченном вертикальными и горизонтальными линиями. Пересечения линий называются «пунктами». Наиболее распространённым является поле размером 15×15 линий.");
                                Console.WriteLine("Играют две стороны — «чёрные» и «белые». Каждая сторона использует фишки («камни») своего цвета.");
                                Console.WriteLine("Каждым ходом игрок выставляет камень своего цвета в один из свободных пунктов доски. Первый ход делают чёрные в центральный пункт доски. Далее ходы делаются по очереди.");
                                Console.WriteLine("Цель игры — первым построить камнями своего цвета непрерывный ряд из пяти камней в горизонтальном, вертикальном или диагональном направлении.");
                                Console.WriteLine("Если доска заполнена и ни один из игроков не построил ряд из пяти камней, может быть объявлена ничья.");
                                break;
                            case 3: Console.Clear(); Console.Write("\x1b[3J"); leaderboard.Print(); Console.WriteLine(); break;
                            case 0: Environment.Exit(0); break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вы ввели неверное число!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели не число!");
                    Console.ResetColor();
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
            int randomNumber = random.Next(1, 3); // рандом для рандомного первого хода

            GameField field = new GameField();
            field.InitializeField();

            field.DisplayField();

            while (true) // цикл для самой игры
            {
                if (field.CheckNullCell() == false)
                {
                    Console.WriteLine("Ничья!");
                    break;
                }
                if (player1.GetQueue() == randomNumber) // проверка ходит ли игрок первым
                {
                    player1.SetFigure("X");
                    player2.SetFigure("O");
                    bool checkExit = false;

                    bool checkMassiv = false;

                    do // цикл чтобы игрок точно написал все правильно
                    {
                        Console.WriteLine("0: Выйти в меню");
                        Console.Write($"{player1.GetName()} введи координаты (Первое число - строка, второе  - столбец): ");

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
                            if (row >= 1 && row <= 15 && col >= 1 && col <= 15)
                            {
                                if(field.CheckValue(row, col)  == false)
                                {
                                    checkMassiv = true;
                                    field.SetValue(row, col, player1.GetFigure());
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Эта клетка уже занята!");
                                    Console.ResetColor();
                                }
                                
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вы ввели неверный диапазон!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели неправильно!");
                            Console.ResetColor();
                        }
                    } while (checkMassiv != true);

                    if (checkExit == true)
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8
                        break;
                    }
                    if (field.CheckWin(player1.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        leaderboard.AddWin(player1.GetName());
                        leaderboard.Save();

                        Console.WriteLine($"Победил игрок: {player1.GetName()}!");
                        Console.WriteLine("0 : Вернуться в главное меню");
                        bool checkExit2 = false;
                        do // чтобы игрок точно нажал 0
                        {
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int exit))
                            {
                                if (exit == 0)
                                {
                                    checkExit2 = true;
                                    Console.Clear();
                                    Console.Write("\x1b[3J"); // баг NET8
                                }
                            }
                        } while (checkExit2 != true);
                        break;
                    }

                    Console.Clear();
                    Console.Write("\x1b[3J"); // баг NET8

                    field.DisplayField();

                    bool checkExit1 = false;

                    bool checkMassiv1 = false;

                    do // ход второго игрока
                    {
                        Console.WriteLine("0: Выйти в меню");
                        Console.Write($"{player2.GetName()} введи координаты (Первое число - строка, второе  - столбец): ");

                        string input = Console.ReadLine();
                        string[] inputMasssiv1 = input.Split(" ");
                        if (input == "0")
                        {
                            checkExit1 = true;
                            break;
                        }
                        if (inputMasssiv1.Length == 2)
                        {
                            int row = int.Parse(inputMasssiv1[0]);
                            int col = int.Parse(inputMasssiv1[1]);
                            if (row >= 1 && row <= 15 && col >= 1 && col <= 15)
                            {
                                if (field.CheckValue(row, col) == false)
                                {
                                    checkMassiv1 = true;
                                    field.SetValue(row, col, player2.GetFigure());
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Эта клетка уже занята!");
                                    Console.ResetColor();
                                }
                                
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вы ввели неверный диапазон!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели неправильно!");
                            Console.ResetColor();
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

                    

                    if (field.CheckWin(player2.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        leaderboard.AddWin(player2.GetName());
                        leaderboard.Save();

                        Console.WriteLine($"Победил игрок: {player2.GetName()}!");
                        Console.WriteLine("0 : Вернуться в главное меню");
                        bool checkExit2 = false;
                        do
                        {
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int exit))
                            {
                                if (exit == 0)
                                {
                                    checkExit2 = true;
                                    Console.Clear();
                                    Console.Write("\x1b[3J"); // баг NET8
                                }
                            }
                        } while (checkExit2 != true);
                        break;
                    }
                }
                else
                {
                    player2.SetFigure("X");
                    player1.SetFigure("O");
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
                            if (row >= 1 && row <= 15 && col >= 1 && col <= 15)
                            {
                                if (field.CheckValue(row, col) == false)
                                {
                                    checkMassiv = true;
                                    field.SetValue(row, col, player2.GetFigure());
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Эта клетка уже занята!");
                                    Console.ResetColor();
                                }
                                
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вы ввели неверный диапазон!");
                                Console.ResetColor();
                            }
                           
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели неправильно!");
                            Console.ResetColor();
                        }
                    } while (checkMassiv != true);

                    if (checkExit == true)
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8
                        break;
                    }

                    if (field.CheckWin(player2.GetFigure()))
                    {
                        Console.Clear();
                        Console.Write("\x1b[3J"); // баг NET8

                        field.DisplayField();

                        leaderboard.AddWin(player2.GetName());
                        leaderboard.Save();

                        Console.WriteLine($"Победил игрок: {player2.GetName()}!");
                        Console.WriteLine("0 : Вернуться в главное меню");
                        bool checkExit2 = false;
                        do
                        {
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int exit))
                            {
                                if (exit == 0)
                                {
                                    checkExit2 = true;
                                    Console.Clear();
                                    Console.Write("\x1b[3J"); // баг NET8
                                }
                            }
                        } while (checkExit2 != true);
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
                            if (row >= 1 && row <= 15 && col >= 1 && col <= 15)
                            {
                                if (field.CheckValue(row, col) == false)
                                {
                                    checkMassiv1 = true;
                                    field.SetValue(row, col, player1.GetFigure());
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Эта клетка уже занята!");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вы ввели неверный диапазон!");
                                Console.ResetColor();
                            }
                            
                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели неправильно!");
                            Console.ResetColor();
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

                        leaderboard.AddWin(player1.GetName());
                        leaderboard.Save();

                        Console.WriteLine($"Победил игрок: {player1.GetName()}!");
                        Console.WriteLine("0 : Вернуться в главное меню");

                        bool checkExit2 = false;
                        do
                        {
                            string input = Console.ReadLine();
                            if(int.TryParse(input, out int exit))
                            {
                                if(exit == 0)
                                {
                                    checkExit2 = true;
                                    Console.Clear();
                                    Console.Write("\x1b[3J"); // баг NET8
                                }
                            }
                        } while (checkExit2 != true);
                        break;
                    }

                    

                }
                
            }
            
        } while (true);
        

    }
}

