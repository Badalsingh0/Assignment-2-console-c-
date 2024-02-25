// See https://aka.ms/new-console-template for more information
using System;

public class Position
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;
    }

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U':
                Position = new Position(Position.X, Position.Y - 1);
                break;
            case 'D':
                Position = new Position(Position.X, Position.Y + 1);
                break;
            case 'L':
                Position = new Position(Position.X - 1, Position.Y);
                break;
            case 'R':
                Position = new Position(Position.X + 1, Position.Y);
                break;
            default:
                Console.WriteLine("Invalid direction.");
                break;
        }
    }
}

public class Cell
{
    public string Occupant { get; set; }

    public Cell()
    {
        Occupant = "-";
    }
}

public class Board
{
    public const int Size = 6;
    public Cell[,] Grid { get; }

    public Board()
    {
        Grid = new Cell[Size, Size];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        // Initialize grid with empty cells
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Grid[i, j] = new Cell();
            }
        }

        // Place players
        Grid[0, 0].Occupant = "P1";
        Grid[Size - 1, Size - 1].Occupant = "P2";

        // Place gems and obstacles (random placement not implemented)
        Grid[1, 1].Occupant = "G";
        Grid[4, 4].Occupant = "G";
        Grid[2, 2].Occupant = "O";
        Grid[3, 3].Occupant = "O";
        Grid[1, 2].Occupant = "G";
        Grid[4, 3].Occupant = "G";
        Grid[2, 5].Occupant = "O";
        Grid[4, 5].Occupant = "O";
    }

    public void Display()
    {
        Console.WriteLine("Current Board:");
        Console.WriteLine("");
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write($"{Grid[i, j].Occupant} ");
            }
            Console.WriteLine();
        }
    }

    public bool IsValidMove(Player player, char direction)
    {
        int newX = player.Position.X;
        int newY = player.Position.Y;

        switch (direction)
        {
            case 'U':
                newY--;
                break;
            case 'D':
                newY++;
                break;
            case 'L':
                newX--;
                break;
            case 'R':
                newX++;
                break;
            default:
                Console.WriteLine("Invalid direction.");
                return false;
        }

        // Check if the new position is within the bounds of the board
        if (newX < 0 || newX >= Size || newY < 0 || newY >= Size)
        {
            Console.WriteLine("Out of bounds.");
            Console.WriteLine("");
            return false;
        }

        // Check if the new position contains an obstacle
        if (Grid[newY, newX].Occupant == "O")
        {
            Console.WriteLine("Obstacle in the way.");
            Console.WriteLine("");
            return false;
        }

        return true;
    }

    public void CollectGem(Player player)
    {
        int x = player.Position.X;
        int y = player.Position.Y;

        if (Grid[y, x].Occupant == "G")
        {
            player.GemCount++;
            Grid[y, x].Occupant = "-"; // Remove the gem from the board
        }
    }
}
public class Game
{
    private const int TotalTurns = 30;
    public readonly Board _board;
    private readonly Player _Player1_;
    private readonly Player _Player2_;
    private Player _currentTurn;

    public Game()
    {
        _board = new Board();
        _Player1_ = new Player("P1", new Position(0, 0));
        _Player2_ = new Player("P2", new Position(5, 5));
        _currentTurn = _Player1_;
    }

    public void Start()
    {
        while (!IsGameOver())

        {
            Console.WriteLine("----------------------------");
            _board.Display();
            Console.WriteLine($"\nIt's {_currentTurn.Name}'s turn.\n");
            Console.WriteLine("Enter direction (U/D/L/R):");
            char direction = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (_board.IsValidMove(_currentTurn, direction))
            {


                // Update the board with "-" at the previous positions
                _board.Grid[_currentTurn.Position.Y, _currentTurn.Position.X].Occupant = "-";

                // Move the player to the new position
                _currentTurn.Move(direction);

                // Update the board with the new position
                _board.Grid[_currentTurn.Position.Y, _currentTurn.Position.X].Occupant = _currentTurn.Name;

                // Collect gem if present at the new position
                _board.CollectGem(_currentTurn);

                // Switch to the next turn
                SwitchTurn();
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
                Console.WriteLine("");
            }
        }
        AnnounceWinner();
    }

    private void SwitchTurn()
    {
        // Switch the turn
        _currentTurn = _currentTurn == _Player1_ ? _Player2_ : _Player1_;

        // Update player positions based on their names
        _Player1_.Position = GetPlayerPosition(_Player1_);
        _Player2_.Position = GetPlayerPosition(_Player2_);
    }

    private Position GetPlayerPosition(Player player)
    {
        for (int i = 0; i < Board.Size; i++)
        {
            for (int j = 0; j < Board.Size; j++)
            {
                if (_board.Grid[i, j].Occupant == player.Name)
                {
                    return new Position(j, i);
                }
            }
        }

        // Return the default position if not found (this should not happen in a correct game state)
        return player.Position;
    }



    private bool IsGameOver()
    {
        return _Player1_.GemCount + _Player2_.GemCount >= TotalTurns;
    }

    private void AnnounceWinner()
    {
        Console.WriteLine("");
        Console.WriteLine("Game over!");
        Console.WriteLine("");
        Console.WriteLine($"Player 1 collected {_Player1_.GemCount} gems.");
        Console.WriteLine($"Player 2 collected {_Player2_.GemCount} gems.");
        Console.WriteLine("");
        if (_Player1_.GemCount > _Player2_.GemCount)
        {
            Console.WriteLine("Player 1 wins!");
        }
        else if (_Player1_.GemCount < _Player2_.GemCount)
        {
            Console.WriteLine("Player 2 wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}

class Badal
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}
