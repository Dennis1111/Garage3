using System;
using System.Collections.Generic;

namespace Garage.Menu
{
    public delegate void LeaveMenuHandler(Menu current,Menu targetMenu);

    public abstract class Menu : Tree<Menu>
    {
        private string Title { get; }
        //To walk through the dictionary in an ordered way
        private List<MenuRow> MenuRows;
        private int Width;
        private Dictionary<char, MenuRow> Dictionary;

        public LeaveMenuHandler leaveMenuHandler;
        //We will use only one one delegate for the whole Tree
        //and store it in the root element
        public LeaveMenuHandler LeaveMenu
        {
            get { return ((Menu)GetRoot()).leaveMenuHandler; }
            set { ((Menu)GetRoot()).leaveMenuHandler = value; }
        }

        public Menu(string title, int width)
        {
            this.Title = title;
            this.Width = width;
            this.MenuRows = new List<MenuRow>();
            this.Dictionary = new Dictionary<char, MenuRow>();
        }     

        //Move one step up in the tree
        protected void OnLeaveMenu() {
            
            OnLeaveMenu(this, (Menu)Parent);
        }

        protected void OnLeaveMenu(Menu current,Menu targetMenu)
        {
            LeaveMenu(current,targetMenu);
        }
       
        public void Start()
        {
            ShowMenu();
            UserActions();
          }

        public void AddMenuRow(MenuRow row)
        {
            MenuRows.Add(row);
            if (row.Menu != null)
                AddChild(row.Menu);
            Dictionary[GetRowKey(MenuRows.Count)] = row;
        }

        protected void ShowMenu()
        {
            Console.WindowWidth = Width;
            Console.Clear();
            Console.WriteLine(Title);
            int row = 1;
            foreach (var menuRow in MenuRows)
            {
                char key = GetRowKey(row);
                Console.WriteLine(LineFormat(menuRow.Message, row));
                row++;
            }
        }

        protected void UserActions()
        {
            bool leaveMenu = false;
            do
            {
                char key = Console.ReadKey(intercept:true).KeyChar;
                if (leaveMenu = Dictionary.ContainsKey(key))
                {
                    MenuRow menuRow = Dictionary[key];
                    //A menuRow either contains an action or a new Menu
                    if (menuRow.Action != null)
                    {
                        menuRow.Action();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey(intercept: true);
                    }
                    else
                    {
                        OnLeaveMenu(this, menuRow.Menu);
                    }
                }
                else
                    Console.WriteLine($"Key '{key}' is not an option");
            } while (!leaveMenu);
        }

        private string LineFormat(string message, int row)
        {
            return GetRowKey(row) + ": " + message;
        }

        private static char GetRowKey(int row)
        {
            if (row < 10)
                return (char)('0' + row);
            else
                return ((char)('A' + row - 10));
        }
    }
}