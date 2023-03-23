﻿using System.Windows.Input;

namespace Rotation_Editor
{
	internal static class CustomCommands
	{
		public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(CustomCommands),
			new InputGestureCollection()
				{
					new KeyGesture(Key.F4, ModifierKeys.Alt),
				}
			);

		public static readonly RoutedUICommand SaveAs = new RoutedUICommand("Save as", "SaveAs", typeof(CustomCommands),
			new InputGestureCollection()
				{
					new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift),
				}
			);

		public static readonly RoutedUICommand Testing = new RoutedUICommand("Testing", "Testing", typeof(CustomCommands));
		public static readonly RoutedCommand Comment = new RoutedUICommand("Comment", "Comment", typeof(CustomCommands));
		public static readonly RoutedCommand Add = new RoutedUICommand("Add", "Add", typeof(CustomCommands));
		public static readonly RoutedCommand Move = new RoutedUICommand("Move", "Move", typeof(CustomCommands));
	}
}