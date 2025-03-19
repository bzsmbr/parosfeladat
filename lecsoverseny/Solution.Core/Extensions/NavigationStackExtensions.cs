namespace Solution.Core.Extensions;

public static class NavigationStackExtensions
{
  public static void ClearNavigationStack(this Shell curentShell)
  {
    var stack = curentShell.Navigation.NavigationStack.ToArray();
    for (int i = stack.Length - 1; i > 0; i--)
    {
      curentShell.Navigation.RemovePage(stack[i]);
    }
  }
}
