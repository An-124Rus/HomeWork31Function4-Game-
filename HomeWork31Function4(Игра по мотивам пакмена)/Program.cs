internal class Program
{
    private static void Main(string[] args)
    {
        int playerPositionX;
        int playerPositionY;
        int playerMoveX = 0;
        int playerMoveY = 0;
        bool isPlaying = true;

        Console.CursorVisible = false;

        char[,] map = ReadMap("map1", out playerPositionX, out playerPositionY);

        DrawMap(map);

        while (isPlaying)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                ChangePlayerPosition(key, ref playerMoveX, ref playerMoveY);

                if (map[playerPositionX + playerMoveX, playerPositionY + playerMoveY] != '#')
                    Move('@', ref playerPositionX, ref playerPositionY, playerMoveX, playerMoveY);
            }
        }
    }

    static void ChangePlayerPosition(ConsoleKeyInfo key, ref int moveX, ref int moveY)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                moveX = -1;
                moveY = 0;
                break;
            case ConsoleKey.DownArrow:
                moveX = 1;
                moveY = 0;
                break;
            case ConsoleKey.LeftArrow:
                moveX = 0;
                moveY = -1;
                break;
            case ConsoleKey.RightArrow:
                moveX = 0;
                moveY = 1;
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

    static char[,] ReadMap(string mapName, out int playerPositionX, out int playerPositionY)
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

                if (map[i, j] == '@')
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