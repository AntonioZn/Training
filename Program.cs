using System.Collections.Generic;
using System.Threading;

namespace DontBeAngry
{
    using System;

    public class Program
    {
        private const int Height = 44;
        private const int Width = 88;
        
        private const int SquareWidth = 9;
        private const int SquareHeight = SquareWidth - 4;

        private const char player1 = '@';
        private const char player2 = '&';
        private const char player3 = '%';
        private const char player4 = '#';

        private static int leftOffset;
        private static int topOffset;
        private static Random random = new Random();
        

        private static List<char>[,] field = new List<char>[11, 11];
        private static char[] players = new char[4] { player1, player2, player3, player4 };
        

        static void Main()
        {
            string text = "The best DO NOT GET ANGRY in Shumen";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Format("{0," + ((Width / 2) + (text.Length / 2)) + "}", text));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(String.Format("{0," + ((Width / 2) + (text.Length / 2)) + "}", text));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format("{0," + ((Width / 2) + (text.Length / 2)) + "}", text));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(String.Format("{0," + ((Width / 2) + (text.Length / 2)) + "}", text));
            Console.ResetColor();


            Console.WindowHeight = Height;
            Console.WindowWidth = Width;
            Console.BufferHeight = Height;
            Console.BufferWidth = Width;
            Console.CursorVisible = false;

            leftOffset = (Console.WindowWidth - ((SquareWidth - 1) * 11 + 1)) / 2;
            topOffset = (Console.WindowHeight - ((SquareHeight - 1) * 11 + 1)) / 2;


            AddToMatrix(6,0, player2);

            PrintNamesTop(0, 2, "Antonio");
            PrintNamesTop(5, 5, "GAME OVER, PLAYER THREE WINS");
            PrintNamesTop(9, 2, "SV");
            PrintNamesBottom(0, 9, "YOANNA");
            PrintNamesBottom(9, 9, "Zdravko");
            var positionFirstPlayer = GetNextPosition(0, 4, player1);
            var positionSecondPlayer = GetNextPosition(6, 0, player2);
            var positionThirdPlayer = GetNextPosition(10, 6, player3);
            var positionFourthPlayer = GetNextPosition(4, 9, player4);

            PrintPlayerSigns(0, 0, player1);
            PrintPlayerSigns(9, 0, player2);
            PrintPlayerSigns(0, 9, player4);
            PrintPlayerSigns(9, 9, player3);

            PrintSquare(0, 0, ConsoleColor.Green);



            PrintPlayerSign(0, 4, player1);
            PrintPlayerSign(10, 6, player3);
            PrintPlayerSign(6, 0, player2);
            PrintPlayerSign(4, 11, player4);




            while (true)
            {
                Thread.Sleep(2000);
                PrintNamesTop(5, 5, "GAME OVER, PLAYER THREE WINS");




                int dices = random.Next(1, 7);
                
                for (int i = 0; i < dices; i++)
                {

                    PrintPlayerSign(positionSecondPlayer.x, positionSecondPlayer.y, player2);
                    positionSecondPlayer = GetNextPosition(positionSecondPlayer.x, positionSecondPlayer.y, player2);
                   
                }
                for (int i = 0; i < dices; i++)
                {
                    PrintPlayerSign(positionFirstPlayer.x, positionFirstPlayer.y, player1);
                    positionFirstPlayer = GetNextPosition(positionFirstPlayer.x, positionFirstPlayer.y, player1);

                }

                

            }
        }

        public static (int x, int y) GetNextPosition(int x, int y, char symbol,int counterThirdplayer = 0)
        {
            

         
            if (symbol == player1 && x >= 0 && x < 5 && y == 5)
                return (++x, y);

            if (symbol == player1 && x == 1 && y == 5)
                return (y, x++);

            if (symbol == player1 && x == 5 && y == 5)             
                return (x = 0, y = 4);                        
            
            if (symbol == player2 && y >= 0 && y < 5 && x == 5)
                return (x, ++y);

            if (symbol == player2 && x == 5 && y == 5)          
                return (x = 6, y = 0);           

            if (symbol == player4 && y <= 10 && y > 5 && x == 5)
                return (x, --y);
            if (symbol == player4 && x == 5 && y == 5)
                return (x = 4, y = 9);

            if (symbol == player3 && y == 5 && x > 5 && x <= 10)
                return (--x, y);
            if (symbol == player3 && x == 5 && y == 5)
                
                counterThirdplayer++;
                return (x = 10, y = 6);
           

            if (y == 4 && x < 4)
                return (++x, y);

            if (x == 4 && y > 0 && y != 6)
                return (x, --y);

            if (y == 0 && x > 0 && x != 6)
                return (++x, y);

            if (x == 6 && y < 4)
                return (x, ++y);

            if (y == 4 && x < 10)
                return (++x, y);

            if (x == 10 && y < 6)
                return (x, ++y);

            if (x == 10 && y > 6)
                return (--x, y);

            if (x == 6 && y < 10)
                return (x, ++y);

            if (y == 10 && x > 4)
                return (--x, y);

            if (x == 4 && y > 6)
                return (--x, y);

            if (y == 6 && x > 0)
                return (--x, y);

            if (x == 0 && y > 4)
                return (x, --y);

            throw new Exception();
        }

        private static void RemoveFromField(int x, int y, char symbol)
        {
            field[x, y].Remove(symbol);
        }

        private static void AddToMatrix(int x, int y, char symbol)
        {
            if (field[x, y] == null)
                field[x, y] = new List<char>();

            field[x, y].Add(symbol);
        }

        public static void PrintNamesTop(int x, int y, string name)
        {
            var pos = GetCoordinate(x, y);

            Console.SetCursorPosition(pos.left, pos.top + 1);
            Console.Write(name);
        }

        public static void PrintNamesBottom(int x, int y, string name)
        {
            var pos = GetCoordinate(x, y);
            Console.SetCursorPosition(pos.left, pos.top - 1);
            Console.Write(name);
        }

        public static void PrintBoard()
        {
            for (int i = 0; i < 11; i++)
            {
                PrintSquare(i, 4, i == 0 ? ConsoleColor.Green : ConsoleColor.Black);
                PrintSquare(i, 5);
                PrintSquare(i, 6, i == 10 ? ConsoleColor.Yellow : ConsoleColor.Black);

                PrintSquare(4, i, i == 10 ? ConsoleColor.Blue : ConsoleColor.Black);
                PrintSquare(5, i);
                PrintSquare(6, i, i == 0 ? ConsoleColor.Red : ConsoleColor.Black);
            }

            for (int i = 1; i < 10; i++)
            {
                if (i < 5)
                {
                    PrintSquare(i, 5, ConsoleColor.Green);
                    PrintSquare(5, i, ConsoleColor.Red);
                }
                else if (i > 5)
                {
                    PrintSquare(i, 5, ConsoleColor.Yellow);
                    PrintSquare(5, i, ConsoleColor.Blue);
                }
            }

            PrintPlayerField(0, 0, ConsoleColor.Black, ConsoleColor.Green);
            PrintPlayerField(9, 0, ConsoleColor.Black, ConsoleColor.Red);
            PrintPlayerField(0, 9, ConsoleColor.Black, ConsoleColor.Blue);
            PrintPlayerField(9, 9, ConsoleColor.Black, ConsoleColor.Yellow);
        }

        public static void PrintSquare(int x, int y, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White)
        {
            void PrintBackground()
            {
                Console.BackgroundColor = background;
                Console.ForegroundColor = foreground;
                Console.Write($"{new string(' ', SquareWidth - 2)}");

                Console.ResetColor();
            }

            var tuple = GetCoordinate(x, y);
            Console.SetCursorPosition(tuple.left+1, tuple.top+1);
            Console.Write(new string('-', SquareWidth));

            Console.SetCursorPosition(tuple.left+1, tuple.top + 1);
            Console.Write("|");
            PrintBackground();
            Console.Write("|");

            Console.SetCursorPosition(tuple.left+1, tuple.top + 2);
            Console.Write("|");
            PrintBackground();
            Console.Write("|");

            Console.SetCursorPosition(tuple.left+1, tuple.top + 3);
            Console.Write("|");
            PrintBackground();
            Console.Write("|");

            Console.SetCursorPosition(tuple.left+1, tuple.top + 3);
            Console.Write(new string('-', SquareWidth));
        }

        private static void PrintPlayerField(int x, int y, ConsoleColor foreground, ConsoleColor background)
        {
            PrintSquare(x, y, background, foreground);
            PrintSquare(x + 1, y, background, foreground);
            PrintSquare(x, y + 1, background, foreground);
            PrintSquare(x + 1, y + 1, background, foreground);
        }

        private static void PrintPlayerSigns(int x, int y, char symbol)
        {
            PrintPlayerSign(x, y, symbol);
            PrintPlayerSign(x + 1, y, symbol);
            PrintPlayerSign(x, y + 1, symbol);
            PrintPlayerSign(x + 1, y + 1, symbol);
        }

        public static void PrintPlayerSign(int x, int y, char symbol)
        {
            var tuple = GetCoordinate(x, y);
            Console.SetCursorPosition(tuple.left + SquareWidth / 2, tuple.top + SquareHeight / 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            try
            {
                Console.Write(symbol);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private static (int top, int left) GetCoordinate(int x, int y)
        {
            var top = y * (SquareHeight - 1) + topOffset;
            var left = x * (SquareWidth - 1) + leftOffset;

            return (top, left);
        }
        private static void GameOver(string name)
        {
            Console.Write);
        }
    }
}