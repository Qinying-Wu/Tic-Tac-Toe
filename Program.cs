using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    class Program
    {
        //constant integers to represent the number of rows and columns
        const int COLUMN = 3;
        const int ROW = 3;
      
        static void Main(string[] args)
        {             
            bool isGame=false; //a boolean value to indicate the game status
            bool winGame = false;//a boolean value to indicate whether one player has won or not
            bool full_board=false; //a boolean value to indicate if all the position on the board is filled with the player's symbols
            bool player_1_turn = true; //boolean values to store the turn status of player one
            string[,] board = new string[ROW, COLUMN] { { "*", "*", "*" }, { "*", "*", "*" }, { "*", "*", "*" } }; //the 2-D array as the tic tac toe board
            Console.WriteLine("Welcome to the Tic-Tac-Toe Game"); //user prompt message
            while (true)
            {
                //user prompt to choose the game options
                Console.ForegroundColor=ConsoleColor.Gray;
                Console.WriteLine("Press 1-Start New Game, 2-Exit Game");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    isGame = true;//start the game if the user presses 1
                }
                else if (input== "2")
                {
                    Console.ForegroundColor=ConsoleColor.DarkMagenta;
                    Console.WriteLine("You have exited the game");
                    isGame = false; //exit the game if the user presses 2
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input, please try again");
                }
                while (isGame == true)
                {
                    string symbol_1 = "", symbol_2 = "";
                    do
                    {
                        symbol_1 = custom_symbol("one");
                        symbol_2 = custom_symbol("two");
                        if (symbol_1 == symbol_2)
                        {
                            Console.ForegroundColor=ConsoleColor.DarkRed;
                            Console.WriteLine("The symbols representing each player must be different");
                        }//to make sure each player chooses a different symbol
                    } while (symbol_1 == symbol_2);
                    //store each player's custom symbol

                    //display the tic tac toe grid
                    display_board(board);
                    //to run the game continuously until one player wins the game or until the game is tied
                    while (winGame == false && full_board == false)
                    {
                        //check if there is a match of same row, column, or diagonal configuration of symbols
                        if (player_1_turn == true)
                        {
                            set_symbol("one", symbol_1, board);
                            player_1_turn = false;//player 1's turn is over
                        }
                        else if (player_1_turn == false)
                        {
                            set_symbol("two", symbol_2, board);
                            player_1_turn = true;//player 2's turn is over
                        }
                        //check if there is a match of same row, column, or diagonal configuration of symbols
                        winGame = check_win(board);
                        //if no match has found but the board is full, the result is tied
                        if (!winGame && full(board) == true)
                        {
                            Console.WriteLine("Player One and Player Two Tied");
                            full_board = true;
                        }
                    }
                    //display the winning message
                    if (winGame == true)
                    {
                        if (player_1_turn == true)
                        {
                            Console.WriteLine("Player Two Wins, congratulations!");
                            player_1_turn = false;
                        }
                        else
                        {
                            Console.WriteLine("Player One Wins, congratulations!");
                            player_1_turn = true;
                        }
                        //set the wingame status to false in case the user wants to start another game
                        winGame = false;
                    }
                    //clear the entries in the array
                    clear_board(board);
                    //the board is not set back to default value
                    full_board = false;
                    isGame = false;
                }
            }
        }
        /// <summary>
        /// a string funtion allowing each player to choose their own symbol to represent their moves in the game
        /// </summary>
        /// <param name="player_num">the player's index (i.e. player 1, player 2)</param>
        /// <returns>the symbol</returns>
        static string custom_symbol(string player_num)
        {
            string symbol;
            do
            {
                //use different colours to indicate different player
                if (player_num == "one")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else if (player_num == "two")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("Choose the symbol of player "+player_num+": ");
                symbol = Console.ReadLine();
                if (symbol.Length != 1)//length of the symbol must be one character long
                {
                    Console.ForegroundColor=ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Symbol, please limit it to one character only");
                }
            } while (symbol.Length != 1&&symbol!="*");//"*" is the default display of the tic tac toe grid, player cannot choose this symbol
            return symbol;
        }
        /// <summary>
        /// to obtain the input row number to insert the symbol
        /// </summary>
        /// <param name="player_num">the player's index number</param>
        /// <returns>the row number to insert the symbol</returns>
        static int row_number(string player_num)
        {
            int row_num;
            do
            {
                if (player_num == "one")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else if (player_num == "two")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                //user prompt to enter the row position on the board to insert the symbol
                Console.WriteLine("Player "+player_num+": Enter the row number to insert your symbol (between 1 to 3):");
                int.TryParse(Console.ReadLine(),out row_num);
                if (row_num < 1 || row_num > 3)
                {
                    //display the warning message if the user enters an invalid row number value
                    Console.ForegroundColor=ConsoleColor.DarkRed;
                    Console.WriteLine("Please enter a number between 1 and 3");
                }
            } while (row_num < 1 || row_num > 3);
            return row_num;
        }
        /// <summary>
        /// obtain the user's input column number to insert the symbol
        /// </summary>
        /// <param name="player_num">the player's index number</param>
        /// <returns>the column position</returns>
        static int column_number(string player_num)
        {
            int col_num;
            do
            {
                //different colour to indicate different players
                if (player_num == "one")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else if (player_num == "two")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                //user prompt to enter the column position to insert the symbol
                Console.WriteLine("Player "+player_num+": Enter the column number to insert your symbol (between 1 to 3):");
                int.TryParse(Console.ReadLine(),out col_num);
                if (col_num < 1 || col_num > 3)
                {
                    //display the warning message if the user enters an invalid value
                    Console.ForegroundColor=ConsoleColor.DarkRed;
                    Console.WriteLine("Please enter a number between 1 and 3");
                }
            } while (col_num < 1 || col_num > 3);
            return col_num;
        }
        /// <summary>
        /// display the tic tac toe game board
        /// </summary>
        /// <param name="game">the 2d array representing the game board configuration</param>
        static void display_board(string[,] game)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    Console.Write(game[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// a procedure to plot the symbols on the board
        /// </summary>
        /// <param name="player_num">the player's index</param>
        /// <param name="symbol">the symbol representing the player</param>
        /// <param name="game">the game board</param>
        /// <param name="player_turn">the status to indicate which player is having a turn</param>
        static void set_symbol(string player_num,string symbol, string[,] game)
        {
            bool isChanged = false; //a boolean value to indicate if the symbol on any position on the board is changed by the player
            int row_num, col_num;
            do
            {
                row_num = row_number(player_num);
                col_num = column_number(player_num);
                if (game[row_num - 1, col_num - 1] != "*")
                {
                    //indicate the position is not available if it is not the default symbol
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("position already taken, please pick a new position");
                }
                else
                {
                    //the position is available to insert the symbol, set the position now to display the new symbol
                    game[row_num - 1, col_num - 1] = symbol;
                    isChanged = true;
                    display_board(game);
                }
            } while (game[row_num - 1, col_num - 1] != symbol||isChanged==false);
        }
       /// <summary>
       /// to determine if a match in a ow, column or in the diagonal is identified
       /// </summary>
       /// <param name="game">the tic tac toe game configuration</param>
       /// <returns>true if a match is identified, false if not</returns>
        static bool check_win(string[,] game)
        {
            //first case -> all symbols match in a row
            for (int i = 0; i < ROW; i++)
            {
                if ((game[i, 0] == game[i, 1] && game[i, 1] == game[i, 2] && game[i, 0] != "*") || (game[0, i] == game[1, i] && game[1, i] == game[2, i] && game[0, i] != "*"))
                {
                    return true; //a row or column of the same symbols is identified and it is not of the default symbols
                }
            }
            //third case -> all symbols match diagnally
            if ((game[0, 0] == game[1, 1] && game[1, 1] == game[2, 2])&&game[1,1]!="*")
            {
                return true; //a diagonal from top left to bottom right of the same symbols is identified and it is not of the default symbols
            }
            else if ((game[0, 2] == game[1, 1] && game[1, 1] == game[2, 0])&&game[1,1]!="*")
            {
                return true;//a diagnal from the top right to bottom left of the same symbols is identified and it is not of the default symbols
            }
            else
            {
                return false; //if all other conditions did not fulfill, return false
            }
        }
        /// <summary>
        /// check if all the position has been filled with a symbol from either player 1 or player 2
        /// </summary>
        /// <param name="game">the tic tac toe game configuration</param>
        /// <returns>true if no default symbol "*" is found, otherwise return false</returns>
        static bool full(string[,] game)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    if (game[i, j] == "*")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// to clear the board to default values of "*"
        /// </summary>
        /// <param name="game">the tic tac toe board configuration</param>
        static void clear_board(string[,] game)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    game[i, j] = "*";
                }
            }
        }
    }
}
