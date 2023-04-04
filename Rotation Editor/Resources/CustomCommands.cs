using System.Windows.Input;

namespace RotationEditor.Resources
{
	internal static class CustomCommands
	{
		public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(CustomCommands),
			new InputGestureCollection()
				{
					new KeyGesture(Key.F4, ModifierKeys.Alt),
				}
			);

		public static readonly RoutedUICommand Testing = new RoutedUICommand("Testing", "Testing", typeof(CustomCommands));

		public static readonly RoutedUICommand SaveAs = new RoutedUICommand("Save as", "SaveAs", typeof(CustomCommands));

	}
}