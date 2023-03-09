using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rotation_Editor
{
	public abstract class BaseWindow : Window
	{
		public Message Message { get; set; }

		public BaseWindow(Message message)
		{
			Message = message;

			this.DataContext = Message;
		}

		protected void ConfirmClick(object sender, RoutedEventArgs e)
		{

		}
		protected void CancelClick(object sender, RoutedEventArgs e)
		{

		}

		protected void IntegerNumericTextInput_Preview(object sender, TextCompositionEventArgs e)
		{
			//Create a check if the text is valid: would the new text result in a number-Integer
		}

		protected void DoubleNumericTextInput_Preview(object sender, TextCompositionEventArgs e)
		{
			//Create a check if the text is valid: would the new text result in a number-Double
		}
	}
}
