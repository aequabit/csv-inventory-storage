﻿using System;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
    class AddItem : ICommand
    {
        public string CommandName() => "addItem";

        public int ArgCount() => 4;

        public string Usage() => CommandName() + " <description> <group> <inventoryId> <serialNumber>";

        public string Action(object[] args)
        {
            Storage.GetInstance().AddItem(new Item
            {
                Description = (string)args[0],
                InventoryGroup = (string)args[1],
                InventoryId = (string)args[2],
                SerialNumber = (string)args[3],
                AddedAt = DateTime.Now,
                AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name
            });

            return "Added item!";
        }
    }
}