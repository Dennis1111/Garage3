using System;
using System.Collections.Generic;

namespace Garage.Menu
{
    public class MenuRow
    {       
        public string Message { get; }
        public Action Action { get; }
        public Menu Menu{ get; }

        public MenuRow(String message,Action action)
        {
            Message = message;
            Action = action;
        }

        public MenuRow(String message, Menu menu)
        {
            Message = message;
            Menu = menu;
        }

        public override string ToString()
        {            
            return Message+ " "+Action.ToString();
        }
    }
}
