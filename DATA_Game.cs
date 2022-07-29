// Class to communicate with database regarding game functions.

using System;
using System.Data.SqlClient;

namespace Project_00
{
    class DATA_Game
    {
        // Fields
        private readonly string DB_PROP_connectionString;
        
        // Constructor
        public DATA_Game()
        {
            string PATH_connectionString = @"./../../connectionString.txt";
            DB_PROP_connectionString = File.ReadAllText(PATH_connectionString);

            // Console.WriteLine(DB_PROP_connectionString);
        }

        // Methods

        public bool DATA_GAME_checkValidChoice(string INPUT_word)
        {
            using SqlConnection DB_connection = new SqlConnection(this.DB_PROP_connectionString);
            DB_connection.Open();
            
            string DB_COMMANDTEXT_validInput = @"SELECT TOP 1 (ID) FROM Project00_Wordle.WordDB_Classic WHERE Word = @INPUT_WORD;";

            using SqlCommand DB_CheckInput = new SqlCommand(DB_COMMANDTEXT_validInput, DB_connection);
            DB_CheckInput.Parameters.AddWithValue("@INPUT_WORD", INPUT_word);

            if(DB_CheckInput.ExecuteScalar() == null)
            {
                DB_connection.Close();
                return false;
            }
            else
            {
                DB_connection.Close();
                return true;
            }
        }

        public string DATA_GAME_randWord()
        {
            string OUTPUT_word = "";
            using SqlConnection DB_connection = new SqlConnection(this.DB_PROP_connectionString);
            DB_connection.Open();

            string DB_COMMANDTEXT_randWord = @"SELECT TOP 1 (Word) FROM Project00_Wordle.WordDB_Classic ORDER BY NEWID();";
            
            using SqlCommand DB_generateRandWord = new SqlCommand(DB_COMMANDTEXT_randWord, DB_connection);
            using SqlDataReader DB_reader = DB_generateRandWord.ExecuteReader();

            if (DB_reader.Read())
            {
                OUTPUT_word = DB_reader.GetString(0);
            }

            DB_connection.Close();
            return OUTPUT_word;
        }

        public void DATA_GAME_uploadDatabase(string INPUT_name, int INPUT_usedTurns, int INPUT_usedTime)
        {
            using SqlConnection DB_connection = new SqlConnection(this.DB_PROP_connectionString);
            DB_connection.Open();

            string DB_COMMANDTEXT_uploadLeaderboards = @"INSERT INTO Project00_Wordle.Leaderboards (Username, Used_Turns, Used_Time) VALUES (@Name, @Turns, @Time);";

            using SqlCommand DB_uploadLeaderboards = new SqlCommand(DB_COMMANDTEXT_uploadLeaderboards, DB_connection);
            DB_uploadLeaderboards.Parameters.AddWithValue("@Name", INPUT_name);
            DB_uploadLeaderboards.Parameters.AddWithValue("@Turns", INPUT_usedTurns);
            DB_uploadLeaderboards.Parameters.AddWithValue("@Time", INPUT_usedTime);

            DB_uploadLeaderboards.ExecuteNonQuery();
            DB_connection.Close();
        }
    }
}