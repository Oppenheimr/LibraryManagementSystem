namespace LibraryManagementSystem.Data;

public class Selection
{
    public readonly string Description;
    public readonly Action? SelectScenario;

    public Selection(string description, Action? selectScenario)
    {
        Description = description;
        SelectScenario = selectScenario;
    }
}