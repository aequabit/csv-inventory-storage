using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    class CommandAddItem : ICommand
    {
        public string CommandName() => "addItem";

        public int ArgCount() => 2;

        public string Usage() => CommandName() + " <key> <value>";

        public string Action(object[] args)
        {
            return (string)args.ElementAt(0) + " => " + (string)args.ElementAt(1);

            return Usage();
        }
    }
}
