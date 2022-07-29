// Class used to control the logic of game (Wordle) for classic (5x6 grid)

using System;
using System.IO;
using System.Text;

namespace Project_00
{
    class LOGIC_Game
    {
        // Fields
        private int GAME_PROP_numWordLength;
        private int GAME_PROP_numGuesses;
        private string GAME_DATA_correctWord  = "";
        private List<string> GAME_DATA_wordList = new List<string>{};

        // Constructors
        public LOGIC_Game(int INPUT_numWordLength, int INPUT_numGuesses)
        {
            this.GAME_PROP_numWordLength = INPUT_numWordLength;
            this.GAME_PROP_numGuesses = INPUT_numGuesses;
            
            DATA_Game GEN_randWord = new DATA_Game();
            this.GAME_DATA_correctWord = GEN_randWord.DATA_GAME_randWord();

            for (int i = 0 ; i < INPUT_numGuesses ; i++)
            {
                GAME_DATA_wordList.Add("");
            }
        }

            // Constructor for testing, has argument to input correct word
        // public LOGIC_Game(int INPUT_numWordLength, int INPUT_numGuesses, string INPUT_randWord)
        // {
        //     this.GAME_PROP_numWordLength = INPUT_numWordLength;
        //     this.GAME_PROP_numGuesses = INPUT_numGuesses;
        //     this.GAME_DATA_correctWord = INPUT_randWord;

        //     for (int i = 0 ; i < INPUT_numGuesses ; i++)
        //     {
        //         GAME_DATA_wordList.Add("");
        //     }
        // }
    
        // Methods

        public void LOGIC_GAME_MAIN()
        {
            int COUNTER_numGuesses = 0;
            bool FLAG_wonGame = false;
            
            DateTime WORK_TimerPrior = DateTime.Now;
            GUI_Game GAME_Display = new GUI_Game();
            for (int i = 0 ; i < GAME_PROP_numGuesses ; i++)
            {
                Console.Clear();
                GAME_Display.GUI_GAME_PrintGameGrid(GAME_PROP_numWordLength, GAME_PROP_numGuesses, GAME_DATA_wordList);
                
                string INPUT_userWord = LOGIC_GAME_getValidGuess();
                if(INPUT_userWord == "!@#$%^&*()_+")
                {
                    break;
                }

                COUNTER_numGuesses++;
                string WORK_userWord = LOGIC_GAME_charCheck(INPUT_userWord);
                GAME_DATA_wordList[i] = WORK_userWord;   

                if(INPUT_userWord == GAME_DATA_correctWord)
                {
                    FLAG_wonGame = true;
                    break;
                }        

                //Debug
                // Console.WriteLine(WORK_userWord);
                // Console.ReadKey();
            }
            DateTime WORK_TimerPost = DateTime.Now;
            TimeSpan WORK_DifferenceTime = WORK_TimerPost - WORK_TimerPrior;
            int WORK_usedTime = Convert.ToInt32(WORK_DifferenceTime.TotalSeconds);

            Console.Clear();

            GAME_Display.GUI_GAME_PrintGameGrid(GAME_PROP_numWordLength, GAME_PROP_numGuesses, GAME_DATA_wordList);


            Console.WriteLine();
            Console.WriteLine();
            if (FLAG_wonGame == true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   !! CONGRATULATIONS !!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("   Nice attempt, please try again.");
            }            

            Console.WriteLine("\nGame Stats: ");
            Console.WriteLine("\tCorrect Word: " + GAME_DATA_correctWord);
            Console.WriteLine("\tNumber of Guesses: " + COUNTER_numGuesses);    
            Console.WriteLine("\tTime Used: " + WORK_usedTime + " sec");

            Console.WriteLine();
            if (FLAG_wonGame == true)
            {
                LOGIC_GAME_uploadLeaderboards(COUNTER_numGuesses, WORK_usedTime);
            }

            Console.WriteLine();
            Console.WriteLine("Press anything to continue");
            Console.ReadKey();
        }

        private string LOGIC_GAME_getValidGuess()
        {
            string OUTPUT_Guess = "";
            bool FLAG_hasValidGuess = false;

            DATA_Game DB_currentWord = new DATA_Game();
            
            while (FLAG_hasValidGuess == false)
            {
                string? INPUT_userWord = "";
                Console.WriteLine(@"Please enter a word or type ""quit"" to exit: ");
                INPUT_userWord = Console.ReadLine();
                INPUT_userWord = INPUT_userWord.ToLower();

                if (INPUT_userWord == "debug_showcorrectword")
                {
                    Console.WriteLine(GAME_DATA_correctWord);
                }
                else if (INPUT_userWord == "quit")
                {
                    OUTPUT_Guess = "!@#$%^&*()_+";
                    FLAG_hasValidGuess = true;
                }
                else if (INPUT_userWord.Length < this.GAME_PROP_numWordLength)
                {
                    Console.WriteLine(" ~ Sorry, inputed word is too short, please try again.");
                }
                else if (INPUT_userWord.Length > this.GAME_PROP_numWordLength)
                {
                    Console.WriteLine(" ~ Sorry, inputed word is too long, please try again.");  
                }
                else if (DB_currentWord.DATA_GAME_checkValidChoice(INPUT_userWord) == false)
                {
                    Console.WriteLine(" ~ Sorry, inputed word does not exist, please try again.");
                }
                else
                {
                    OUTPUT_Guess = INPUT_userWord;
                    FLAG_hasValidGuess = true;
                }
            }

            return OUTPUT_Guess;
        }

        private string LOGIC_GAME_charCheck(string INPUT_word)
        {
            StringBuilder WORK_userWord = new StringBuilder(INPUT_word);

            int WORK_counterCorrctWord = 0;
            for(int j = 0 ; j < WORK_userWord.Length ; j++)
            {
                // Debug
                // Console.WriteLine(WORK_userWord[j] + ", " +GAME_DATA_correctWord[WORK_counterCorrctWord]);

                if (WORK_userWord[j] == GAME_DATA_correctWord[WORK_counterCorrctWord])
                {
                    j++;
                    WORK_userWord.Insert(j,"^");
                }
                else if(GAME_DATA_correctWord.Contains(INPUT_word[WORK_counterCorrctWord]))
                {
                    j++;
                    WORK_userWord.Insert(j, "*");
                }
                WORK_counterCorrctWord++;
            }

            WORK_userWord.Append('#');
            string OUTPUT_word = WORK_userWord.ToString();

            Console.WriteLine(OUTPUT_word);
            return OUTPUT_word;
        }

        private void LOGIC_GAME_uploadLeaderboards(int INPUT_usedTurns, int INPUT_usedTime)
        {
            Console.WriteLine("Do you want to submit your game to the leaderboards [y/n]");
            ConsoleKeyInfo INPUT_userChoice;

            bool FLAG_validChoice = false;
            while (FLAG_validChoice == false)
            {
                INPUT_userChoice = Console.ReadKey();
                switch (INPUT_userChoice.KeyChar)
                {
                    case 'y':
                        FLAG_validChoice = true;
                        
                        Console.WriteLine();
                        string INPUT_playerName = LOGIC_GAME_getUserName();

                        DATA_Game DB_uploader = new DATA_Game();
                        DB_uploader.DATA_GAME_uploadDatabase(INPUT_playerName, INPUT_usedTurns, INPUT_usedTime);

                        Console.WriteLine("Record uploaded.");

                        break;

                    case 'n':
                        FLAG_validChoice = true;
                        break;
                }
                Console.Write(" ");
            }
        }

        private string LOGIC_GAME_getUserName()
        {
            string INPUT_playerName;
            while (true)
            {
                Console.WriteLine("Please enter your name: ");
                INPUT_playerName = Console.ReadLine();  
                
                if(INPUT_playerName.Length > 20)
                {
                    Console.WriteLine("   ~ Sorry, inputed name is too long, please try again.");
                }
                else if (INPUT_playerName == "") {}
                else
                {
                    break;
                }
            } 
            return INPUT_playerName;
        }
    }
}