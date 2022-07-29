//Class loads menus from directory to print to console.

using System;

namespace Project_00
{
    class GUI_Menu
    {
        // Fields
        
        // Constructors

        // Methods
        public void GUI_MENU_printMainMenu()
        {
            string PATH_mainMenu = @"./MENUS/MENU_Main.txt";

            using StreamReader WORK_fsStream = new StreamReader(PATH_mainMenu);
            string WORK_fsLine;
            while((WORK_fsLine = WORK_fsStream.ReadLine()) != null)
            {
                Console.WriteLine(WORK_fsLine);
            }
        }

        public void GUI_MENU_printCredits()
        {
            Console.Clear();
            string PATH_mainMenu = @"./MENUS/MENU_Credits.txt";

            using StreamReader WORK_fsStream = new StreamReader(PATH_mainMenu);
            string WORK_fsLine;
            while((WORK_fsLine = WORK_fsStream.ReadLine()) != null)
            {
                Console.WriteLine(WORK_fsLine);
            }

            Console.WriteLine();
            Console.WriteLine("Press anything to return to main menu");
            Console.ReadKey();
        }

        public void GUI_MENU_printLeaderboards()
        {
            ConsoleKeyInfo INPUT_userMenu;
            bool FLAG_runMenu = true;
            
            DATA_Menu DB_leaderboardAccess = new DATA_Menu();

            while (FLAG_runMenu)
            {
                Console.Clear();
                GUI_MENU_printLeaderboardsHeader();
                INPUT_userMenu = Console.ReadKey();
                switch (INPUT_userMenu.KeyChar)
                {
                    case 'i':
                        DB_leaderboardAccess.DATA_MENU_printLeaderboardData('i');

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Press anything to continue");
                        Console.ReadKey();

                        break;
                    case 'k':
                        DB_leaderboardAccess.DATA_MENU_printLeaderboardData('k');

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Press anything to continue");
                        Console.ReadKey();

                        break;
                    case 'q':
                        FLAG_runMenu = false;
                        break;
                }
            }
        }

        private void GUI_MENU_printLeaderboardsHeader()
        {
            string PATH_mainMenu = @"./MENUS/MENU_Leaderboard.txt";

            using StreamReader WORK_fsStream = new StreamReader(PATH_mainMenu);
            string WORK_fsLine;
            while((WORK_fsLine = WORK_fsStream.ReadLine()) != null)
            {
                Console.WriteLine(WORK_fsLine);
            }
        }
    }
}