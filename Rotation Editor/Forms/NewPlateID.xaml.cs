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
	/// <summary>
	/// Interaction logic for NewPlateID.xaml
	/// </summary>
	public partial class NewPlateID : BaseWindow
	{
		public NewPlateID(NewPlateIDMessage message) : base(message)
		{
			InitializeComponent();
		}
	}
}
