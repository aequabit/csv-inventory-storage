using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    class CommandShow : ICommand
    {
        public string CommandName() => "showItem";

        public int ArgCount() => 1;

        public string Usage() => "Usage: " + CommandName() + " <command>";

        public string Action(object[] args)
        {
            // TODO: show all // show 1 // show 5 // ...
            return "list show placeholder";
        }
    }
}
