using System;
using System.Collections.Generic;
using System.IO;
using CSVInventoryStorage.Gui.Controls;
using CSVInventoryStorage.Inventory;
using CSVInventoryStorage.Serialization;
using CSVInventoryStorage.Utils;

namespace CSVInventoryStorage.Gui.Forms
{
    public class Index : IForm
    {
        readonly Action<string> Import = new Action<string>(path =>
        {
            try
            {
                Storage.GetInstance().SetItems(CsvSerializer.DeserializeList<Item>(File.ReadAllText(path)));
            }
            catch (Exception ex)
            {
                Renderer.SetError(ex.Message);
            }
        });


        readonly Action<string> Export = new Action<string>(path =>
        {
            try
            {
                File.WriteAllText(path, CsvSerializer.Serialize(Storage.GetInstance().GetItems()));
                Renderer.SetSuccess("Exported to " + path);
            }
            catch (Exception ex)
            {
                Renderer.SetError(ex.Message);
            }
        });

        public string Name() => "Inventory Storage";

        public List<IControl> Controls() => new List<IControl>
        {
            new Button(ConsoleKey.D1, "List items", () => Renderer.Render(new ItemList())),
            new Button(ConsoleKey.D2, "Import from CSV", () => Renderer.Render(new TextInput("CSV Import", "Import path: ", Import, AppDomain.CurrentDomain.BaseDirectory + "storage.csv"))),
			new Button(ConsoleKey.D3, "Export to CSV", () => Renderer.Render(new TextInput("CSV Export", "Export path: ", Export, AppDomain.CurrentDomain.BaseDirectory + "storage.csv"))),
            new Button(ConsoleKey.D4, "Console mode", () => {
                Console.Clear();
                return true;
            }),
            new Button(ConsoleKey.Backspace, "Exit", () => Renderer.Render(new YesNo("Confirm exit", "Are you sure you want to exit?", yes => {
                if (yes) Environment.Exit(0);
                return true;
            })))
        };

        public bool Proxy() => false;

        public bool KeyDown(ConsoleKeyInfo key) => false;
    }
}
