/// Base interface for all commands
public interface ICommand
{
    // Execute the command action
    void Execute();
    void Undo();
}
