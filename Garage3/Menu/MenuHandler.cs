using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage.Menu
{
    public class MenuHandler
    {
        private Menu Menu { get; set; }
        private bool LeaveMainMenu = false;

        public MenuHandler(Menu mainMenu)
        {
            Menu = mainMenu;
            Menu.LeaveMenu = new LeaveMenuHandler(OnChangeMenu);
        }

        public void ActivateMenus()
        {
            do
            {
                Menu.Start();

            } while (!LeaveMainMenu);
        }

        private void OnChangeMenu(Menu sender, Menu targetMenu)
        {
            //Console.WriteLine("OnLeave MenuHandler");
            if (targetMenu == null)
            {
                LeaveMainMenu = true;
                Console.WriteLine("Leaving the menu");
            }
            else
            {
                //Console.WriteLine("OnLeave new menu" + targetMenu);
                Menu = targetMenu;
            }
        }
    }
}
