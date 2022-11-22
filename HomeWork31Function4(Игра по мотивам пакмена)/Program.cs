internal class Program
{ 
    private static void Main(string[] args)
    {
        const ConsoleKey exitCommand = ConsoleKey.Escape;

        int playerPositionX;
        int playerPositionY;
        int playerMoveX = 0;
        int playerMoveY = 0;
        bool isPlaying = true;
        char wallSymbol = '#';
        char playerSymbol = '@';

        Console.CursorVisible = false;

        char[,] map = ReadMap("map1", playerSymbol, out playerPositionX, out playerPositionY);

        DrawMap(map);

        while (isPlaying)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (Console.KeyAvailable || key.Key != exitCommand)
            {
                GetPlayerDirection(key, ref playerMoveX, ref playerMoveY);

                if (map[playerPositionX + playerMoveX, playerPositionY + playerMoveY] != wallSymbol)
                    Move(playerSymbol, ref playerPositionX, ref playerPositionY, playerMoveX, playerMoveY);
            }
            else
            {
                isPlaying = false;
            }
        }
    }

    static void GetPlayerDirection(ConsoleKeyInfo key, ref int moveX, ref int moveY)
    {
        const ConsoleKey MoveUpCommand = ConsoleKey.UpArrow;
        const ConsoleKey MoveDownCommand = ConsoleKey.DownArrow;
        const ConsoleKey MoveLeftCommand = ConsoleKey.LeftArrow;
        const ConsoleKey MoveRightCommand = ConsoleKey.RightArrow;

        switch (key.Key)
        {
            case MoveUpCommand:
                moveX = -1;
                moveY = 0;
                break;
            case MoveDownCommand:
                moveX = 1;
                moveY = 0;
                break;
            case MoveLeftCommand:
                moveX = 0;
                moveY = -1;
                break;
            case MoveRightCommand:
                moveX = 0;
                moveY = 1;
                break;
            default:
                moveX = 0;
                moveY = 0;
                break;
        }
    }

    static void Move(char symbol, ref int positionX, ref int positionY, int moveX, int moveY)
    {
        Console.SetCursorPosition(positionY, positionX);
        Console.WriteLine(' ');

        positionX += moveX;
        positionY += moveY;

        Console.SetCursorPosition(positionY, positionX);
        Console.WriteLine(symbol);
    }

    static char[,] ReadMap(string mapName, char symbol, out int playerPositionX, out int playerPositionY)
    {
        playerPositionX = 0;
        playerPositionY = 0;

        string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
        char[,] map = new char[newFile.Length, newFile[0].Length];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = newFile[i][j];

                if (map[i, j] == symbol)
                {
                    playerPositionX = i;
                    playerPositionY = j;
                }
            }
        }

        return map;
    }

    static void DrawMap(char[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
                Console.Write(map[i, j]);

            Console.WriteLine();
        }
    }
}