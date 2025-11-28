class GameField
{
    private string[,] field = new string[15, 15];
    private string fieldLine = new string('—', 61);

    public void InitializeField()
    {
        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 15; col++)
            {
                field[row, col] = "O";
            }
        }
    }

    public void DisplayField()
    {
        Console.WriteLine(fieldLine);
        for (int row = 0; row < 15; row++)
        {
            Console.Write("| ");
            for (int col = 0; col < 15; col++)
            {
                Console.Write($"{field[row, col]} | ");
            }
            Console.WriteLine();
            Console.WriteLine(fieldLine);
        }
    }

    public void SetValue(int row, int col, string value)
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
            Console.Write("\x1b[3J");
            field.SetValue(x, y, value);
            field.DisplayField();
        }

    }
}

