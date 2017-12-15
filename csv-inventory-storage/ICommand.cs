namespace CSVInventoryStorage
{
    public interface ICommand
    {
        string CommandName();
        int ArgCount();
        string Usage();
        string Action(object[] args);
    }
}
