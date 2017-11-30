using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    class CommandAddItem : ICommand
    {
        public string commandName() => "addItem";

        public int argCount() => 2;

        public string usage() => commandName() + " <key> <value>";

        public string action(object[] args)
        {
            return (string)args.ElementAt(0) + " => " + (string)args.ElementAt(1);

            return usage();
        }
    }
}
