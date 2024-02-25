Position Class: This class represents a position on the game board. It has two properties, X and Y, which represent the coordinates of the position.

Player Class: This class represents a player in the game. It has three properties: Name (the player's name), Position (the player's current position on the board), and GemCount (the number of gems collected by the player). The Move method updates the player's position based on the input direction (U, D, L, or R).

Cell Class: This class represents a cell on the game board. It has one property, Occupant, which represents what occupies the cell (P1, P2, G, O, or - for empty).

Board Class: This class represents the game board. It has one property, Grid, which is a 2D array of Cell objects. The InitializeBoard method sets up the board with players, gems, and obstacles. The Display method prints the current state of the board to the console. The IsValidMove method checks if a move is valid (within bounds and not blocked by an obstacle). The CollectGem method checks if the player's new position contains a gem and updates the player's GemCount.

Game Class: This class represents the game itself. It has five properties: Board (the game board), Player1 and Player2 (the two players), CurrentTurn (the player whose turn it is), and TotalTurns (the number of turns that have passed). The Start method begins the game, displaying the board and alternating player turns. The SwitchTurn method switches between Player1 and Player2. The IsGameOver method checks if the game has reached its end condition (30 turns). The AnnounceWinner method determines and announces the winner based on the players' GemCount.

Program Class: This class contains the Main method, which creates a new Game object and starts the game.

In the Main method, the game is started by creating a new Game object and calling its Start method. The game continues until IsGameOver returns true, at which point the AnnounceWinner method is called to determine and announce the winner.
