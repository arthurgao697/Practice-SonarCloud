// Class used to display the ingame grid.

using System;

namespace Project_00
{
    public class GUI_Game
    {
        // Fields

        // Constructor

        //  Methods
        public void GUI_GAME_PrintGameGrid(int INPUT_numColumns, int INPUT_numRows, List<string> INPUT_wordList)
        {
            for (int i = 0 ; i < INPUT_numRows ; i++)
            {
                GUI_GAME_PrintHorizontal(INPUT_numColumns);
                GUI_GAME_PrintVertial(INPUT_numColumns, INPUT_wordList[i]);
            }
            GUI_GAME_PrintHorizontal(INPUT_numColumns);
        }


        void GUI_GAME_PrintHorizontal(int INPUT_numColumns)
        {
            Console.WriteLine("+" + string.Concat(Enumerable.Repeat("-------+", INPUT_numColumns)));
        }

        void GUI_GAME_PrintVertial(int INPUT_numColumns, string INPUT_currentWord)
        {
            Console.WriteLine("|" + string.Concat(Enumerable.Repeat("       |", INPUT_numColumns)));
            Console.Write("|");
            if (INPUT_currentWord == "")
            {
                for (int i = 0 ; i < INPUT_numColumns ; i++)
                {
                    Console.Write("       |");
                }
            }
            else
            {
                for (int i = 0 ; i < INPUT_currentWord.Length ; i++)
                {
                    if (INPUT_currentWord[i] != '^' && INPUT_currentWord[i] != '*' && INPUT_currentWord[i] != '#')
                    {
                        Console.Write("   ");
                        if (INPUT_currentWord[i+1] == '^')
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(INPUT_currentWord[i]);
                            Console.ResetColor();
                        }
                        else if(INPUT_currentWord[i+1] == '*')
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(INPUT_currentWord[i]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(INPUT_currentWord[i]);
                        }
                        Console.Write("   |");
                    }
                }
            }
            Console.Write("\n");
            Console.WriteLine("|" + string.Concat(Enumerable.Repeat("       |", INPUT_numColumns)));
        }
    }
}