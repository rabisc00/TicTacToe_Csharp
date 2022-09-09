using System.Linq;

// Variables declaration
string[,] gameBoard;
string? spotToTake, currentPlayer;
string[] spotsTaken;
int counter, spotNum;
bool isInputValid;

// Will keep restarting the game until user quits
while (true)
{
    // Default values for a new game
    counter = 0;
    spotsTaken = new string[9];
    gameBoard = new string[,]
    {
    {"1", "2", "3"},
    {"4", "5", "6"},
    {"7", "8", "9"}
    };

    ShowBoard(gameBoard);

    // Runs the game until either a draw or win
    do
    {
        // Switch between players
        if (counter % 2 == 0) { currentPlayer = "Player 1"; }
        else { currentPlayer = "Player 2"; }

        // Asks for spot to change
        Console.WriteLine("\n\nWhich spot do you want to take, {0}? ", currentPlayer);
        spotToTake = Console.ReadLine();
        isInputValid = int.TryParse(spotToTake, out spotNum);
        Console.Clear();

        // Asks the question until a valid input
        while(!isInputValid || spotNum > 9 || spotNum < 1 || spotsTaken.Contains(spotToTake))
        {
            ShowBoard(gameBoard);

            Console.WriteLine("\n\nNot a valid input. Which spot do you want to take, {0}? ", currentPlayer);
            spotToTake = Console.ReadLine();
            isInputValid = int.TryParse(spotToTake, out spotNum);

            Console.Clear();
        }

        // Changes the spot
        if (spotToTake is not null)
        {
            gameBoard = ChangeSpot(gameBoard, spotToTake, currentPlayer == "Player 1" ? "X" : "O");
        }

        ShowBoard(gameBoard);

        counter++;
    } while (!WinCheck(gameBoard) && counter < 9);

    // Shows the results
    if (WinCheck(gameBoard)) { Console.WriteLine("\n\n{0} won! Congratulations!", currentPlayer); }
    else { Console.WriteLine("\n\nIt's a draw. Play again to see who truly wins."); }

    // Restart the game if key is pressed
    Console.WriteLine("Press any key to restart the game ");
    Console.ReadKey(true);
    Console.Clear();
}

// Functions
static void ShowBoard(string[,] boardToShow)
{
    Console.WriteLine("");

    // Displays a formatted gameboard
    for (int i = 0; i < boardToShow.GetLength(0); i++)
    {
        for (int j = 0; j < boardToShow.GetLength(1); j++)
        {
            if (j == 2) { Console.Write(" {0} ", boardToShow[i, j]); }
            else { Console.Write(" {0} |", boardToShow[i, j]); }
        }
        if (i != 2) { Console.WriteLine("\n----------"); }
    }
}
static string[,] ChangeSpot(string[,] boardToChange, string spotToTake, string changeToSign)
{
    // Changes the board based on which spot was chosen
    switch (spotToTake)
    {
        case "1": boardToChange[0, 0] = changeToSign; break;
        case "2": boardToChange[0, 1] = changeToSign; break;
        case "3": boardToChange[0, 2] = changeToSign; break;
        case "4": boardToChange[1, 0] = changeToSign; break;
        case "5": boardToChange[1, 1] = changeToSign; break;
        case "6": boardToChange[1, 2] = changeToSign; break;
        case "7": boardToChange[2, 0] = changeToSign; break;
        case "8": boardToChange[2, 1] = changeToSign; break;
        case "9": boardToChange[2, 2] = changeToSign; break;
    }
    return boardToChange;
}
static bool WinCheck(string[,] gameBoard)
{
    // Check if game is done
    bool CheckIfDone(string[] arrayToCheck)
    {
        bool isGameDone = arrayToCheck.All(value => value.Equals("X")) || arrayToCheck.All(value => value.Equals("O"));
        return isGameDone;
    }

    // Declarating the variables
    string[] currentRow, currentColumn, currentDiagonal;
    currentRow = currentColumn = currentDiagonal = new string[3];

    // Check horizontal win
    for (int i = 0; i < gameBoard.GetLength(0); i++)
    {
        for (int j = 0; j < gameBoard.GetLength(1); j++) { currentRow[j] = gameBoard[i, j]; }
        if (CheckIfDone(currentRow)) { return true; }
    }
    // Check vertical win
    for (int i = 0; i < gameBoard.GetLength(1); i++)
    {
        // Fill currentColumn
        for (int j = 0; j < gameBoard.GetLength(0); j++) { currentColumn[j] = gameBoard[j, i]; }
        if (CheckIfDone(currentColumn)) { return true; }
    }

    // Check right diagonal win
    for (int i = 0; i < gameBoard.GetLength(0); i++)
    {
        currentDiagonal[i] = gameBoard[i, i];
    }
    if (CheckIfDone(currentDiagonal)) { return true; }

    // Check left diagonal win
    for (int i = 0, j = 2; i < gameBoard.GetLength(0); i++, j--)
    {
        currentDiagonal[i] = gameBoard[i, j];
    }
    if (CheckIfDone(currentDiagonal)) { return true; }

    // Continues the game if none of them were right
    return false;
}