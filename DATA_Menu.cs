// Class to communicate with database regarding game functions.

using System;
using System.Data.SqlClient;

namespace Project_00
{
    class DATA_Menu
    {
        // Fields
        private readonly string DB_PROP_connectionString;
        private static readonly string DB_COMMAND_printLeaderboardByTurns = @"SELECT TOP 10 * FROM Project00_Wordle.Leaderboards ORDER BY Used_Turns ASC, Used_Time;";
        private static readonly string DB_COMMAND_printLeaderboardByTime = @"SELECT TOP 10 * FROM Project00_Wordle.Leaderboards ORDER BY Used_Time ASC, Used_Turns;";
        // Constructor
        public DATA_Menu()
        {
            string PATH_connectionString = @"./../../connectionString.txt";
            DB_PROP_connectionString = File.ReadAllText(PATH_connectionString);

            // Console.WriteLine(DB_PROP_connectionString);
        }

        // Methods

        public void DATA_MENU_printLeaderboardData(char INPUT_choice)
        {
            using SqlConnection DB_connection = new SqlConnection(this.DB_PROP_connectionString);
            DB_connection.Open();
            
            string WORK_currentCommand = "";
            if (INPUT_choice == 'i')
            {
                WORK_currentCommand = DB_COMMAND_printLeaderboardByTurns;
            }
            else if (INPUT_choice == 'k')
            {
                WORK_currentCommand = DB_COMMAND_printLeaderboardByTime;
            }

            using SqlCommand DB_getLeaderboard = new SqlCommand(WORK_currentCommand, DB_connection);
            using SqlDataReader DB_reader = DB_getLeaderboard.ExecuteReader();
            

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("   Rank    Name                    Turns    Seconds");
            int WORK_counterLeaderboard = 0;
            while (DB_reader.Read())
            {
                WORK_counterLeaderboard++;
                Console.Write("    ");
                Console.Write(WORK_counterLeaderboard);
                Console.Write(".    ");
                Console.Write(DB_reader.GetString(1));
                for (int i = 20 ; i - DB_reader.GetString(1).Length > 0 ; i --)
                {
                    Console.Write(" ");
                }
                Console.Write("   |   ");
                Console.Write(DB_reader.GetInt32(2));
                Console.Write("   |   ");
                Console.Write(DB_reader.GetInt32(3));
                Console.Write("\n");
            }

            if (WORK_counterLeaderboard == 0)
            {
                Console.WriteLine("\tNo records are in the leaderboard right now.");
            }
        }
    }
}