﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Rotation_Editor
{
	public abstract class BaseWindow : Window, INotifyPropertyChanged
	{
		public MainWindow ParentWnd => (Application.Current.MainWindow as MainWindow);

		public BaseWindow()
		{
			DataContext = this;
		}

		protected void ConfirmClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			this.Close();
		}
		protected void CancelClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			this.Close();
		}

		protected void IntegerNumericTextInput_Preview(object sender, TextCompositionEventArgs e)
		{
			//Create a check if the text is valid: would the new text result in a number-Integer
		}

		protected void DoubleNumericTextInput_Preview(object sender, TextCompositionEventArgs e)
		{
			//Create a check if the text is valid: would the new text result in a number-Double
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
	}
}
