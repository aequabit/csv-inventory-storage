using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.UI;

namespace CSVInventoryStorage.UI.Forms
{
    public class ItemList : IForm
    {
        private static Dictionary<int, ConsoleKey> IntToDigit = new Dictionary<int, ConsoleKey>()
        {
            { 1, ConsoleKey.D1 },
            { 2, ConsoleKey.D2 },
            { 3, ConsoleKey.D3 },
            { 4, ConsoleKey.D4 },
            { 5, ConsoleKey.D5 },
            { 6, ConsoleKey.D6 },
            { 7, ConsoleKey.D7 },
            { 8, ConsoleKey.D8 },
            { 9, ConsoleKey.D9 },
            { 0, ConsoleKey.D0 }
        };

        public string FormName() => "Item List";
        public Dictionary<ConsoleKey, Option> Options()
        {
            var final = new Dictionary<ConsoleKey, Option>();

            // TODO: fix out of range error
            var items = Inventory.Storage.GetInstance().GetItems();
            for (var i = 0; i < items.Count(); i++)
                final.Add(IntToDigit[i], new Option()
                {
                    Name = items[i].InventoryId,
                    Action = () =>
                    {
                        return Renderer.Render(new ItemView()
                        {
                            Item = items[i]
                        });
                    }
                });

            return final;
        }
    }
}

/*
{
                ConsoleKey.Backspace,
                new Option() {
                    Name = "Go back",
                    Action = () => true
                }
            }*/
