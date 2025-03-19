namespace Microsoft.Maui.Controls;

public static class NavigationStackExtensions
{
    public static void ClearNavigationStack(this Shell currentShell)
    {
        var stack = currentShell.Navigation.NavigationStack.ToArray();
        for (int i = stack.Length - 1; i > 0; i--)
        {
            currentShell.Navigation.RemovePage(stack[i]);
        }
    }
}
