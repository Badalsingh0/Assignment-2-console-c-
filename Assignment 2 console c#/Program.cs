// See https://aka.ms/new-console-template for more information
using System;

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

class Player
{
    public string Name { get; set; }
    public Position Position { get; set; }
    public int GemsCount { get; set; } = 0;

    public Player(string name, int x, int y) => (Name, Position, GemsCount) = (name, new Position(x, y), 0);

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U':
                Position.Y--;
                break;
            case 'D':
                Position.Y++;
                break;
            case 'L':
                Position.X--;
                break;
            case 'R':
                Position.X++;
                break;
        }
    }
}

class Game
{
    private char[,] Occupant = new char[6, 6];
    private Player player1 = new Player("P1", 0, 0);
    private Player player2 = new Player("P2", 5, 5);
    private int turn = 0;
    private Random rand = new Random();

    public Game()
    {
        PlaceItems();
    }
    private void PlaceItems()
    {
        for (int y = 0; y < 6; y++)
            for (int x = 0; x < 6; x++)
                Occupant[x, y] = '-';

        PlaceRandomItems('G', 6);
        PlaceRandomItems('O', 7);
    }

    private void PlaceRandomItems(char item, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int x, y;
            do
            {
                x = rand.Next(6);
                y = rand.Next(6);
            } while (Occupant[x, y] != '-' || (x == player1.Position.X && y == player1.Position.Y) || (x == player2.Position.X && y == player2.Position.Y));
            Occupant[x, y] = item;
        }
    }

    public void Start()
    {
        while (turn < 30)
        {

            Console.WriteLine($"---------------------------------------");
            Console.WriteLine();
            Display();
            Console.WriteLine();
            Player currentPlayer = turn % 2 == 0 ? player1 : player2;
            Console.WriteLine($"{currentPlayer.Name}'s turn. Enter your move (U/D/L/R): ");
            var direction = Char.ToUpper(Console.ReadKey(true).KeyChar);
            Console.WriteLine();

            Position CurrentPosition = new Position(currentPlayer.Position.X, currentPlayer.Position.Y); // Save original position
            currentPlayer.Move(direction);

            if (!IsValidMove(currentPlayer))
            {
                Console.WriteLine("You've hit an obstacle! Try a different direction.");
                currentPlayer.Position = CurrentPosition; // Reset to original position if invalid move
                continue;
            }

            CollectGemsCount(currentPlayer);
            turn++;
            Console.WriteLine($"---------------------------------------");
        }
        AnnounceWinner();
    }

    private bool IsValidMove(Player player) => player.Position.X >= 0 && player.Position.X < 6 && player.Position.Y >= 0 && player.Position.Y < 6 && Occupant[player.Position.X, player.Position.Y] != 'O';

    private void CollectGemsCount(Player player)
    {
        if (Occupant[player.Position.X, player.Position.Y] == 'G')
        {
            player.GemsCount++;
            Occupant[player.Position.X, player.Position.Y] = '-';
        }
    }

    private void Display()
    {
        Console.WriteLine($"Turn: {turn + 1}");
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                if (x == player1.Position.X && y == player1.Position.Y)
                    Console.Write("P1 ");
                else if (x == player2.Position.X && y == player2.Position.Y)
                    Console.Write("P2 ");
                else Console.Write($"{Occupant[x, y]} ");
            }
            Console.WriteLine();
        }
    }

    private void AnnounceWinner()
    {
        Console.WriteLine($"Game Over. \n{player1.Name} GemsCount: {player1.GemsCount} \n{player2.Name} GemsCount: {player2.GemsCount}");
        Console.WriteLine();

        string winner = player1.GemsCount > player2.GemsCount ? player1.Name : player2.GemsCount > player1.GemsCount ? player2.Name : "It's a tie!";

        Console.WriteLine($"{winner} wins!");
    }
}

class Program
{
    static void Main()
    {
        new Game().Start();
    }
}
