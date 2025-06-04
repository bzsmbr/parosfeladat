namespace Solution.DesktopApp.Behaviors;

public class EntryIntegerBehavior : Behavior<Entry>
{
	protected override void OnAttachedTo(Entry entry)
	{
		base.OnAttachedTo(entry);

		entry.TextChanged += OnTextChanged;
	}

	protected override void OnDetachingFrom(Entry entry)
	{
		base.OnDetachingFrom(entry);

		entry.TextChanged -= OnTextChanged;
	}

	private void OnTextChanged(object? sender, TextChangedEventArgs e)
	{
		Entry entry = sender as Entry;

		if (string.IsNullOrEmpty(e.NewTextValue))
		{
			entry!.Text = null;
			return;
		}

		bool isValid = int.TryParse(e.NewTextValue, out int result);
		entry!.Text = isValid ? result.ToString() : e.OldTextValue;
	}
}
