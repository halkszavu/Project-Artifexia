using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
	}
}
