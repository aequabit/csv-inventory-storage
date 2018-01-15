using System;
using System.Collections.Generic;
using CSVInventoryStorage.UI;

namespace CSVInventoryStorage.UI.Forms
{
    public class Index : IForm
    {
        public string FormName() => "Index";
        public Dictionary<ConsoleKey, Option> Options() => new Dictionary<ConsoleKey, Option>() {
            {
                ConsoleKey.D1,
                new Option() {
                    Name = "List items",
                    Action = () => Renderer.Render(new ItemList())
                }
            }
        };
    }
}
