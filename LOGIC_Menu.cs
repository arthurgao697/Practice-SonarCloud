using System;

namespace Project_00

{
    class LOGIC_Menu
    {
        // Fields
        private static GUI_Menu WORK_gameMenuNavigator = new GUI_Menu();
        // Constructor

        // Methods
        public void LOGIC_MENU_MAIN()
        {
            ConsoleKeyInfo INPUT_userMenu;
            bool FLAG_runMenu = true;

            while (FLAG_runMenu)
            {
                Console.Clear();
                WORK_gameMenuNavigator.GUI_MENU_printMainMenu();
                
                INPUT_userMenu = Console.ReadKey();
                switch (INPUT_userMenu.KeyChar)
                {
                    case 'g':
                        LOGIC_Game WORK_gameSession = new LOGIC_Game(5,6);
                        WORK_gameSession.LOGIC_GAME_MAIN();
                        break;
                    case 'c':
                        WORK_gameMenuNavigator.GUI_MENU_printCredits();
                        break;
                    case 'l':
                        WORK_gameMenuNavigator.GUI_MENU_printLeaderboards();
                        break;
                    case 'q':
                        FLAG_runMenu = false;
                        break;
                }
            }
        }
    }
}