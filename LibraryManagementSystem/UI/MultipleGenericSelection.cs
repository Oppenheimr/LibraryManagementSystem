using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.UI;

public static class MultipleGenericSelection
{
    public static int MultipleChoose(params Selection[] selections)
    {
        UserInterface.RequestMessageToUser("For the menu you want to select, press the number to the left of" +
                                           " the menu name and press the \"Enter\" key.");
        string selectionDisplay = "";
        for (int i = 0; i < selections.Length; i++)
            selectionDisplay += $"{i + 1} -> {selections[i].Description}.\n";

        Console.WriteLine(selectionDisplay);
        var selected = UserInterface.ReadTheInteger(1, selections.Length) - 1;
        selections[selected].SelectScenario?.Invoke();
        return selected;
    }
}